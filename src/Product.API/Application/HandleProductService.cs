using Core.Constant;
using Core.ViewModel;
using Domain.Entities;
using Hangfire;
using Infrastructure.Contexts;
using Newtonsoft.Json;
using Product.API.Interfaces;
using StackExchange.Redis;
using CoreResource = Core.Properties;
using ProductEntity = Domain.Entities.Product;

namespace Product.API.Application
{
    public class HandleProductService : IHandleProductService
    {

        #region Constructor
        SQLServerDbContext _context;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly ISendMailService _sendMailService;
        private readonly string ManualAuction = "Manual Auction";
        private readonly string AutomaticAuction = "Automatic Auction";

        public HandleProductService(SQLServerDbContext context, IConnectionMultiplexer connectionMultiplexer, ISendMailService sendMailService)
        {
            _context = context;
            _redisConnection = connectionMultiplexer;
            _sendMailService = sendMailService;
        }
        #endregion

        #region SendMailSuccessTask
        // Send mail to winner bidder
        public void SendMailSuccessTask(MailModel detail)
        {
            string receiver = detail.Email;
            string subject = CoreResource.EmailSubjectResources.AuctionResult;
            var message = new MailMessage(new string[] { receiver }, subject, "", null);
            _sendMailService.SendMailSuccessAsync(message, detail);
        }
        #endregion

        #region SendMailFailTask
        // Send mail to failed bidders in auction
        public void SendMailFailTask(List<MailModel> detail)
        {
            foreach (var item in detail)
            {
                string receiver = item.Email;
                string subject = CoreResource.EmailSubjectResources.AuctionResult;
                var message = new MailMessage(new string[] { receiver }, subject, "", null);
                _sendMailService.SendMailFailedAsync(message, item);
            }
        }
        #endregion

        #region SendMailApproveTask
        public void SendMailApproveTask(MailModel detail)
        {
            string receiver = detail.Email;
            string subject = CoreResource.EmailSubjectResources.ProductApproved;
            var message = new MailMessage(new string[] { receiver }, subject, "", null);
            _sendMailService.SendMailApproveTask(message, detail);
        }
        #endregion

        #region SendMailRejectTask
        public void SendMailRejectTask(MailModel detail, string reason)
        {
            string receiver = detail.Email;
            string subject = CoreResource.EmailSubjectResources.ProductRejected;
            var message = new MailMessage(new string[] { receiver }, subject, "", null);
            _sendMailService.SendMailRejectTask(message, detail, reason);
        }
        #endregion

        #region SendMailNotHighestPrice
        public async Task SendMailNotHighestPrice(string email, string productName, string categoryName, float currentPrice, float yourPrice)
        {
            string receiver = email;
            string subject = CoreResource.EmailSubjectResources.AuctionResult;
            var message = new MailMessage(new string[] { receiver }, subject, "", null);
            await _sendMailService.SendMailNotHighestPrice(message, productName, categoryName, currentPrice, yourPrice);
        }
        #endregion

        #region StartAuctionTask
        public void StartAuctionTask(ProductEntity product)
        {
            // add auction
            Auction auction = new Auction();
            auction.IdAuction = Guid.NewGuid();
            auction.IdProduct = product.IdProduct;
            auction.StartDate = DateTime.Now;
            auction.EndDate = null;
            auction.PriceCurrentMax = product.InitPrice;
            auction.Duration = CalculatorDuration(product.InitPrice);
            auction.IsStart = true;
            auction.IsEnd = false;
            auction.HasBid = false;

            var auctionInserted = _context.Auctions.Add(auction);
            _context.SaveChanges();

            // get info mailModel
            var mailModel = (from pro in _context.Products
                             join cate in _context.Categories on pro.CategoryId equals cate.IdCategory
                             join user in _context.Users on pro.UserId equals user.IdUser
                             where pro.IdProduct == auctionInserted.Entity.IdProduct
                             select new MailModel()
                             {
                                 ProductName = pro.ProductName,
                                 CategoryName = cate.CategoryName,
                                 Email = user.Email,
                                 StartDate = auctionInserted.Entity.StartDate,
                                 Init_Price = pro.InitPrice,
                                 Step_Price = pro.StepPrice
                             }).FirstOrDefault();

            if (mailModel != null)
            {
                BackgroundJob.Enqueue(() => SendMailApproveTask(mailModel));
            }

            // end auction
            TimeSpan durationTimeSpan = TimeSpan.FromMinutes(auction.Duration);
            Guid? IdAuction = auctionInserted.Entity.IdAuction;
            TimeSpan delay = durationTimeSpan.Add(TimeSpan.FromSeconds(10));
            BackgroundJob.Schedule(() => EndAuctionTask(IdAuction), delay);
        }
        #endregion

        #region EndAuctionTask
        public void EndAuctionTask(Guid? IdAuction)
        {
            var auction = _context.Auctions.FirstOrDefault(x => x.IdAuction == IdAuction);
            if (auction != null)
            {
                // update auction
                auction.EndDate = DateTime.Now;
                auction.IsEnd = true;
                auction.IsStart = false;
                _context.SaveChanges();

                #region Handle duplicate message if any
                var historyMessage = GetHistoryMessages(IdAuction);
                var duplicateMessages = historyMessage.GroupBy(m => new { m.AuctionId, m.BidderId, m.CurrentPrice, m.AuctionType })
                    .Where(g => g.Count() > 1)
                    .SelectMany(g => g.Skip(1))
                    .ToList();

                foreach (var message in duplicateMessages)
                {
                    historyMessage.Remove(message);
                }
                #endregion

                if (historyMessage.Count > 0)
                {
                    // update Price
                    var lastRecord = historyMessage?.LastOrDefault();
                    auction.PriceCurrentMax = lastRecord.CurrentPrice;
                    _context.SaveChanges();

                    var result = UpdateDetailAuction(historyMessage);

                    if (result)
                    {
                        #region get winner bidder
                        var winner = (from detail in _context.DetailAuctions
                                      join user in _context.Users on detail.IdBidder equals user.IdUser
                                      join auc in _context.Auctions on detail.IdAuction equals auc.IdAuction
                                      join product in _context.Products on auction.IdProduct equals product.IdProduct
                                      join category in _context.Categories on product.CategoryId equals category.IdCategory
                                      where detail.IdAuction == IdAuction &&
                                            detail.CurrentPrice == auc.PriceCurrentMax &&
                                            detail.IdBidder == user.IdUser
                                      select new MailModel()
                                      {
                                          IdBidder = user.IdUser,
                                          ProductName = product.ProductName,
                                          CategoryName = category.CategoryName,
                                          Email = user.Email,
                                          StartDate = auc.StartDate,
                                          EndDate = auc.EndDate,
                                          PriceCurrentMax = auc.PriceCurrentMax,
                                          CurrentPrice = detail.CurrentPrice,
                                          AuctionType = detail.AuctionType == 0 ? ManualAuction : AutomaticAuction
                                      }).FirstOrDefault();
                        #endregion

                        #region get closers bidder
                        var closers = (from detail in _context.DetailAuctions
                                       join user in _context.Users on detail.IdBidder equals user.IdUser
                                       join auc in _context.Auctions on detail.IdAuction equals auc.IdAuction
                                       join product in _context.Products on auc.IdProduct equals product.IdProduct
                                       join category in _context.Categories on product.CategoryId equals category.IdCategory
                                       where detail.IdAuction == IdAuction &&
                                             detail.CurrentPrice < auc.PriceCurrentMax &&
                                             detail.IdBidder == user.IdUser
                                       group detail by user.IdUser into userGroup
                                       select new MailModel()
                                       {
                                           IdBidder = userGroup.FirstOrDefault().IdBidderNavigation.IdUser,
                                           ProductName = userGroup.OrderByDescending(d => d.CurrentPrice).FirstOrDefault().IdAuctionNavigation.IdProductNavigation.ProductName,
                                           CategoryName = userGroup.OrderByDescending(d => d.CurrentPrice).FirstOrDefault().IdAuctionNavigation.IdProductNavigation.Category.CategoryName,
                                           Email = userGroup.FirstOrDefault().IdBidderNavigation.Email,
                                           StartDate = userGroup.OrderByDescending(d => d.CurrentPrice).FirstOrDefault().IdAuctionNavigation.StartDate,
                                           EndDate = userGroup.OrderByDescending(d => d.CurrentPrice).FirstOrDefault().IdAuctionNavigation.EndDate,
                                           PriceCurrentMax = userGroup.OrderByDescending(d => d.CurrentPrice).FirstOrDefault().IdAuctionNavigation.PriceCurrentMax,
                                           CurrentPrice = userGroup.Max(d => d.CurrentPrice),
                                           AuctionType = userGroup.OrderByDescending(d => d.CurrentPrice).FirstOrDefault().AuctionType == 0 ? ManualAuction : AutomaticAuction
                                       }).ToList();
                        #endregion

                        #region handle dupplicate user, if any

                        var itemsToRemove = new List<MailModel>();

                        foreach (var item in closers)
                        {
                            if (item.IdBidder == winner.IdBidder)
                            {
                                itemsToRemove.Add(item);
                            }
                        }

                        foreach (var itemToRemove in itemsToRemove)
                        {
                            closers.Remove(itemToRemove);
                        }

                        #endregion

                        // send mail winner
                        if (winner != null)
                        {
                            auction.IdWinner = winner.IdBidder;
                            _context.SaveChanges();
                            BackgroundJob.Enqueue(() => SendMailSuccessTask(winner));
                        }

                        // send mail closers
                        if (closers.Count() > 0)
                        {
                            BackgroundJob.Enqueue(() => SendMailFailTask(closers));
                        }

                        ClearHistoryMessages(IdAuction);
                    }
                }
            }
        }
        #endregion

        #region GetHistoryMessages
        public List<Message> GetHistoryMessages(Guid? auctionId)
        {
            var redisDb = _redisConnection.GetDatabase();
            var groupKey = $"{KeyRedisNameConst.GroupMessageHistory}:{auctionId}";
            var messages = redisDb.ListRange(groupKey, start: 0, stop: -1);

            // Đảo ngược thứ tự các phần tử
            messages = messages.Reverse().ToArray();
            return messages.Select(m => JsonConvert.DeserializeObject<Message>(m)).ToList();
        }
        #endregion

        #region ClearHistoryMessages
        public void ClearHistoryMessages(Guid? auctionId)
        {
            var redisDb = _redisConnection.GetDatabase();
            var groupKey = $"{KeyRedisNameConst.GroupMessageHistory}:{auctionId}";
            redisDb.KeyDelete(groupKey);
        }
        #endregion

        #region UpdateDetailAuction
        public bool UpdateDetailAuction(List<Message> messages)
        {
            try
            {
                foreach (var item in messages)
                {
                    DetailAuction detailAuction = new DetailAuction();
                    detailAuction.IdDetailAuction = Guid.NewGuid();
                    detailAuction.IdAuction = item.AuctionId;
                    detailAuction.IdBidder = item.BidderId;
                    detailAuction.CurrentPrice = item.MyPrice;
                    detailAuction.AuctionType = item.AuctionType;
                    detailAuction.MaxBidPrice = item.MyMaxPriceAuto;

                    _context.DetailAuctions.Add(detailAuction);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Calculator Duration
        private float CalculatorDuration(double price)
        {
            if (price < 5000000) // nhỏ hơn 5 triệu, thời gian đấu giá 3 phút
            {
                float duration = 3;
                return duration;
            }
            else if (price < 10000000) // từ 5 triệu đến nhỏ hơn 10 triệu, thời gian đấu giá 5 phút
            {
                float duration = 5;
                return duration;
            }
            else if (price < 50000000) //  từ 10 triệu đến nhỏ hơn 50 triệu, thời gian đấu giá 10 phút
            {
                float duration = 10;
                return duration;
            }
            else // lớn hơn 50 triệu, thời gian đấu giá 60 phút
            {
                float duration = 60;
                return duration;
            }
        }
        #endregion
    }
}
