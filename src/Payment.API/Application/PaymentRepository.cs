#region using namespace
using Core.ViewModel;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Payment.API.Interfaces;
using System.Transactions;
using CoreResource = Core.Properties;

#endregion

namespace Payment.API.Application
{
    public class PaymentRepository : IPaymentRepository
    {
        #region Constructor

        SQLServerDbContext _context;
        private readonly ISendMailService _sendMailService;

        public PaymentRepository(SQLServerDbContext context, ISendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }
        #endregion

        #region Create
        public async Task<bool> Create(PaymentViewModel payment)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    #region insert HistoryPayment
                    HistoryPayment entity = new HistoryPayment
                    {
                        IdUser = payment.IdUser,
                        IdProduct = payment.IdProduct,
                        DatePayment = DateTime.Now,
                        OrderNotes = payment.OrderNotes,
                        ShipingAddress = payment.ShipingAddress,
                        Telephone = payment.Telephone,
                        ZipCode = payment.ZIPCode,
                        Email = payment.Email,
                        FirstName = payment.FirstName,
                        LastName = payment.LastName,
                        OrderType = payment.OrderType,
                        TotalPrice = payment.TotalPrice,
                        Status = true
                    };

                    EntityEntry<HistoryPayment> paymentEntry = _context.HistoryPayments.Add(entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    #endregion

                    #region update Products
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.IdProduct == paymentEntry.Entity.IdProduct);
                    product.IsPayment = true;
                    product.IsSold = true;
                    string productName = product.ProductName;
                    Guid? productId = product.IdProduct;
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    string email = (from pro in _context.Products
                                    where pro.IdProduct == productId
                                    join u in _context.Users
                                    on pro.UserId equals u.IdUser
                                    select u.Email).FirstOrDefault();
                    #endregion

                    transaction.Complete();

                    #region send mail to bidder
                    string receiverBidder = payment.Email;
                    string subjectBidder = CoreResource.EmailSubjectResources.OrderStatus;
                    var messageBidder = new MailMessage(new string[] { receiverBidder }, subjectBidder, "", null);
                    await _sendMailService.SendEmailPaymentSuccessToBidderAsync(messageBidder, payment, productName).ConfigureAwait(false);
                    #endregion

                    #region send mail to auctioneer
                    string receiverAuctioneer = email;
                    string subjectAuctioneer = CoreResource.EmailSubjectResources.ProductStatus;
                    var messageAuctioneer = new MailMessage(new string[] { receiverAuctioneer }, subjectAuctioneer, "", null);
                    MailModel model = new MailModel
                    {
                        ProductName = product.ProductName,
                        CurrentPrice = payment.TotalPrice,
                        StartDate = (DateTime)entity.DatePayment
                    };
                    await _sendMailService.SendEmailPaymentSuccessToAuctioneerAsync(messageAuctioneer, model).ConfigureAwait(false);
                    #endregion

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion
    }
}

