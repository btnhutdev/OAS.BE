using AutoMapper;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Product.API.Interfaces;
using System.Transactions;
using System.Collections;
using Image = Domain.Entities.Image;
using Product.API.Utilities;
using Core.ViewModel;
using Core.Constant;
using Core.AutoMapperProfile;

namespace Product.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Constructor
        private readonly IS3BucketService _s3BucketService;
        private readonly IS3FileService _s3FileService;
        private readonly IHandleProductService _handleProductService;
        SQLServerDbContext _context;
        Mapper _mapperProduct;
        Mapper _mapperImg;

        public ProductRepository(SQLServerDbContext context, IS3BucketService s3BucketService, IS3FileService s3FileService, IHandleProductService handleProductService)
        {
            _context = context;
            _mapperProduct = ProductMapperConfig.InitAutomapper();
            _mapperImg = ImageMapperConfig.InitAutomapper();
            _s3BucketService = s3BucketService;
            _s3FileService = s3FileService;
            _handleProductService = handleProductService;
        }
        #endregion

        #region get detail product for update product page
        public async Task<ProductViewModel> GetDetail(Guid? id)
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        where p.IdProduct.Equals(id)
                        select new ProductViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName
                        };

            var product = query.FirstOrDefault();
            product.Images = new List<ImageViewModel>();
            List<Image> images = await _context.Images.Where(x => x.IdProduct.Equals(id)).ToListAsync().ConfigureAwait(false);
            IList<ImageViewModel> imageViewModels = _mapperImg.Map<IList<ImageViewModel>>(images);
            product.Images.AddRange(imageViewModels);
            return product;
        }
        #endregion

        #region get detail product for update product page - auctioneer
        public async Task<ProductViewModel> GetDetailProductForAuctioneer(Guid? id)
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        where p.IdProduct.Equals(id)
                        select new ProductViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName
                        };

            var product = query.FirstOrDefault();
            product.Images = new List<ImageViewModel>();

            List<Image> images = await _context.Images.Where(x => x.IdProduct.Equals(id)).ToListAsync().ConfigureAwait(false);
            IList<ImageViewModel> imageViewModels = _mapperImg.Map<IList<ImageViewModel>>(images);

            foreach (var item in imageViewModels)
            {
                if (string.IsNullOrEmpty(item.S3Uri))
                {
                    product.Images.Add(item);
                }
                else
                {
                    if (await _s3BucketService.CheckConnectS3BucketAsync())
                    {
                        var imgFile = await _s3FileService.DownloadFileAsync(BucketNameConst.BucketName, item.S3Uri);
                        item.Data = imgFile;
                        product.Images.Add(item);
                    }
                }
            }

            return product;
        }
        #endregion

        #region get detail product for bidder detail page
        public async Task<DetailProductBidderViewModel> GetDetailProductForBidder(Guid? id)
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IdProduct.Equals(id)
                        && p.IsApprove == true
                        && auc.IsStart == true
                        && auc.IsEnd == false
                        select new DetailProductBidderViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = auc.StartDate,
                            EndDate = auc.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName,
                            Duration = auc.Duration,
                            IdAuction = auc.IdAuction,
                            PriceCurrentMax = auc.PriceCurrentMax,
                            HasBid = auc.HasBid,
                            IsStart = auc.IsStart,
                            IsEnd = auc.IsEnd,
                            IsReject = p.IsReject,
                            UserId = p.UserId
                        };

            var product = query.FirstOrDefault();
            product.Images = new List<ImageViewModel>();
            List<Image> images = await _context.Images.Where(x => x.IdProduct.Equals(id)).ToListAsync().ConfigureAwait(false);
            IList<ImageViewModel> imageViewModels = _mapperImg.Map<IList<ImageViewModel>>(images);
            product.Images.AddRange(imageViewModels);
            return product;
        }
        #endregion

        #region get list All Product, All Status By Auctioneer Id for Auctioneer home page
        public async Task<IList<ProductViewModel>> GetListProductAllStatusByAuctioneerID(Guid? userId)
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        where p.UserId.Equals(userId)
                        select new ProductViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName
                        };

            return await query.ToListAsync().ConfigureAwait(false);
        }
        #endregion

        #region get detail history auction for admin
        public async Task<IList<HistoryAuctionViewModel>> GetDetaiHistoryAuctionByAdmin(Guid? productId)
        {
            var query = (from pro in _context.Products
                         join auc in _context.Auctions
                         on pro.IdProduct equals auc.IdProductNavigation.IdProduct
                         join de in _context.DetailAuctions
                         on auc.IdAuction equals de.IdAuction
                         join user in _context.Users
                         on de.IdBidder equals user.IdUser
                         where pro.IdProduct.Equals(productId) &&
                         pro.IsApprove == true
                         select new HistoryAuctionViewModel()
                         {
                             IdAuction = de.IdAuction,
                             IdDetailAuction = de.IdDetailAuction,
                             IdBidder = de.IdBidder,
                             BidderName = user.LastName + " " + user.FirstName,
                             CurrentPrice = de.CurrentPrice,
                             MaxBidPrice = de.MaxBidPrice,
                             AuctionType = de.AuctionType,
                             AuctionTypeName = de.AuctionType == 0 ? "Manual Auction" : "Auto Auction"
                         });

            return await query.ToListAsync().ConfigureAwait(false);
        }
        #endregion

        #region get list Product for admin view
        public async Task<IList<ProductViewModel>> GetListProductByAdmin(string type)
        {
            IQueryable<ProductViewModel>? query = null;
            switch (type)
            {
                case StatusNameConst.All:
                    query = from p in _context.Products
                            join c in _context.Categories
                            on p.CategoryId equals c.IdCategory
                            select new ProductViewModel()
                            {
                                IdProduct = p.IdProduct,
                                ProductName = p.ProductName,
                                InitPrice = p.InitPrice,
                                StepPrice = p.StepPrice,
                                Description = p.Description,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                IsApprove = p.IsApprove,
                                IsPayment = p.IsPayment,
                                IsSold = p.IsSold,
                                CategoryId = c.IdCategory,
                                CategoryName = c.CategoryName
                            }; break;

                case StatusNameConst.Processing:
                    query = from p in _context.Products
                            join c in _context.Categories
                            on p.CategoryId equals c.IdCategory
                            where p.IsApprove == false
                            && p.IsSold == false
                            && p.IsReject == false
                            select new ProductViewModel()
                            {
                                IdProduct = p.IdProduct,
                                ProductName = p.ProductName,
                                InitPrice = p.InitPrice,
                                StepPrice = p.StepPrice,
                                Description = p.Description,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                IsApprove = p.IsApprove,
                                IsPayment = p.IsPayment,
                                IsSold = p.IsSold,
                                CategoryId = c.IdCategory,
                                CategoryName = c.CategoryName
                            }; break;

                case StatusNameConst.Approved:
                    query = from p in _context.Products
                            join c in _context.Categories
                            on p.CategoryId equals c.IdCategory
                            where p.IsApprove == true
                            && p.IsSold == false
                            && p.IsReject == false
                            select new ProductViewModel()
                            {
                                IdProduct = p.IdProduct,
                                ProductName = p.ProductName,
                                InitPrice = p.InitPrice,
                                StepPrice = p.StepPrice,
                                Description = p.Description,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                IsApprove = p.IsApprove,
                                IsPayment = p.IsPayment,
                                IsSold = p.IsSold,
                                CategoryId = c.IdCategory,
                                CategoryName = c.CategoryName
                            }; break;

                case StatusNameConst.Rejected:
                    query = from p in _context.Products
                            join c in _context.Categories
                            on p.CategoryId equals c.IdCategory
                            where p.IsApprove == false
                            && p.IsSold == false
                            && p.IsReject == true
                            select new ProductViewModel()
                            {
                                IdProduct = p.IdProduct,
                                ProductName = p.ProductName,
                                InitPrice = p.InitPrice,
                                StepPrice = p.StepPrice,
                                Description = p.Description,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                IsApprove = p.IsApprove,
                                IsPayment = p.IsPayment,
                                IsSold = p.IsSold,
                                CategoryId = c.IdCategory,
                                CategoryName = c.CategoryName
                            }; break;

                case StatusNameConst.Sold:
                    query = from p in _context.Products
                            join c in _context.Categories
                            on p.CategoryId equals c.IdCategory
                            where p.IsSold == true &&
                             p.IsPayment == true &&
                             p.IsApprove == true
                            select new ProductViewModel()
                            {
                                IdProduct = p.IdProduct,
                                ProductName = p.ProductName,
                                InitPrice = p.InitPrice,
                                StepPrice = p.StepPrice,
                                Description = p.Description,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                IsApprove = p.IsApprove,
                                IsPayment = p.IsPayment,
                                IsSold = p.IsSold,
                                CategoryId = c.IdCategory,
                                CategoryName = c.CategoryName
                            }; break;
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }
        #endregion

        #region get list product for bidder home page
        public async Task<IList<ProductViewModel>> GetListProductAuctioning()
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IsApprove == true
                        && auc.IsStart == true
                        && auc.IsEnd == false
                        select new ProductViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName
                        };

            var products = query.ToList();

            foreach (var pro in products)
            {
                pro.Images = new List<ImageViewModel>();
                List<Image> images = await _context.Images.Where(x => x.IdProduct == pro.IdProduct).ToListAsync().ConfigureAwait(false);
                IList<ImageViewModel> imageViewModels = _mapperImg.Map<IList<ImageViewModel>>(images);
                pro.Images.AddRange(imageViewModels);
            }

            return products;
        }
        #endregion

        #region get list win product by bidder for my product home page
        public async Task<IList<DetailProductBidderViewModel>> GetListWinnerProductByBidder(Guid? bidderId)
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IsApprove == true
                        && auc.IsStart == false
                        && auc.IsEnd == true
                        && auc.IdWinner.Equals(bidderId)
                        select new DetailProductBidderViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName,
                            PriceCurrentMax = auc.PriceCurrentMax
                        };

            var products = query.ToList();

            foreach (var pro in products)
            {
                pro.Images = new List<ImageViewModel>();
                List<Image> images = await _context.Images.Where(x => x.IdProduct == pro.IdProduct).ToListAsync().ConfigureAwait(false);
                IList<ImageViewModel> imageViewModels = _mapperImg.Map<IList<ImageViewModel>>(images);
                pro.Images.AddRange(imageViewModels);
            }

            return products;
        }
        #endregion

        #region get detail win product for my product detail page
        public async Task<DetailProductBidderViewModel> GetDetailWinnerProductByBidder(Guid? bidderId, Guid? productId)
        {
            var query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        join detail in _context.DetailAuctions
                        on auc.IdAuction equals detail.IdAuction
                        where p.IdProduct.Equals(productId) &&
                         p.IsApprove == true &&
                         auc.IsStart == false &&
                         auc.IsEnd == true &&
                         auc.IdWinner.Equals(bidderId) &&
                         detail.IdBidder.Equals(auc.IdWinner)
                        select new DetailProductBidderViewModel()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            InitPrice = p.InitPrice,
                            StepPrice = p.StepPrice,
                            Description = p.Description,
                            StartDate = auc.StartDate,
                            EndDate = auc.EndDate,
                            IsApprove = p.IsApprove,
                            IsPayment = p.IsPayment,
                            IsSold = p.IsSold,
                            CategoryId = c.IdCategory,
                            CategoryName = c.CategoryName,
                            Duration = auc.Duration,
                            IdAuction = auc.IdAuction,
                            PriceCurrentMax = auc.PriceCurrentMax,
                            HasBid = auc.HasBid,
                            IsStart = auc.IsStart,
                            IsEnd = auc.IsEnd,
                            IsReject = p.IsReject,
                            UserId = p.UserId,
                            AuctionType = detail.AuctionType == 0 ? "Manual Auction" : "Auto Auction"
                        };

            var product = query.FirstOrDefault();
            product.Images = new List<ImageViewModel>();
            List<Image> images = await _context.Images.Where(x => x.IdProduct.Equals(productId)).ToListAsync().ConfigureAwait(false);
            IList<ImageViewModel> imageViewModels = _mapperImg.Map<IList<ImageViewModel>>(images);
            product.Images.AddRange(imageViewModels);
            return product;
        }
        #endregion

        #region Is Product Auctioning
        public async Task<bool> IsProductAuctioning(Guid? id)
        {
            var result = await _context.Auctions.Where(x => x.IdAuction == id && x.IsStart == true && x.IsEnd == false).FirstOrDefaultAsync();
            return result != null;
        }
        #endregion

        #region Create
        public async Task<bool> Create(ProductViewModel productViewModel)
        {
            // automapper
            Domain.Entities.Product product = _mapperProduct.Map<ProductViewModel, Domain.Entities.Product>(productViewModel);
            product.IdProduct = Guid.NewGuid();

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // update product
                    EntityEntry<Domain.Entities.Product> productEntry = _context.Products.Add(product);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    // loop new file images and convert to images
                    for (int i = 0; i < productViewModel.ImageFiles.Count; i++)
                    {
                        Image img = new Image();
                        img.IdImage = Guid.NewGuid();
                        img.ImageName = Guid.NewGuid().ToString();
                        img.Extension = Path.GetExtension(productViewModel.ImageFiles[i].FileName);
                        img.IdProduct = productEntry.Entity.IdProduct;

                        if (await _s3BucketService.CheckConnectS3BucketAsync())
                        {
                            img.S3Uri = await _s3FileService.UploadFileAsync(productViewModel.ImageFiles[i], BucketNameConst.BucketName, img.ImageName + img.Extension);
                            img.Data = null;
                        }
                        else
                        {
                            using (var stream = new MemoryStream())
                            {
                                productViewModel.ImageFiles[i].CopyTo(stream);
                                img.Data = stream.ToArray();
                                img.S3Uri = string.Empty;
                            }
                        }

                        _context.Images.Add(img);
                    }

                    // save and complete transaction
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Complete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion

        #region Approve
        public bool Approve(Guid id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.IdProduct == id);

                if (product != null)
                {
                    product.IsApprove = true;
                    product.IsPayment = false;
                    product.IsSold = false;
                    product.IsReject = false;

                    _handleProductService.StartAuctionTask(product);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Reject
        public bool Reject(Guid id, string message)
        {
            try
            {
                // update product
                var product = _context.Products.FirstOrDefault(x => x.IdProduct == id);
                product.IsApprove = false;
                product.IsPayment = false;
                product.IsSold = false;
                product.IsReject = true;
                _context.SaveChangesAsync();

                var mailModel = from pro in _context.Products
                                join cate in _context.Categories on pro.CategoryId equals cate.IdCategory
                                join user in _context.Users on pro.UserId equals user.IdUser
                                where pro.IdProduct == product.IdProduct
                                select new MailModel()
                                {
                                    ProductName = pro.ProductName,
                                    CategoryName = cate.CategoryName,
                                    Email = user.Email,
                                    StartDate = DateTime.Now,
                                    Init_Price = pro.InitPrice,
                                    Step_Price = pro.StepPrice
                                };

                if (mailModel != null)
                {
                    _handleProductService.SendMailRejectTask(mailModel.FirstOrDefault(), message);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Update
        public async Task<bool> Update(ProductViewModel productViewModel)
        {
            // automapper
            Domain.Entities.Product product = _mapperProduct.Map<ProductViewModel, Domain.Entities.Product>(productViewModel);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // update product
                    EntityEntry<Domain.Entities.Product> productEntry = _context.Products.Update(product);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    // get old images from database
                    var oldImages = _context.Images.Where(image => image.IdProduct == productViewModel.IdProduct).ToList();

                    // get old images from s3
                    foreach (var item in oldImages)
                    {
                        if (!string.IsNullOrEmpty(item.S3Uri))
                        {
                            if (await _s3BucketService.CheckConnectS3BucketAsync())
                            {
                                var imgFile = await _s3FileService.DownloadFileAsync(BucketNameConst.BucketName, item.S3Uri);
                                item.Data = imgFile;
                            }
                        }
                    }

                    // loop new file images and convert to images
                    List<Image> newImages = new List<Image>();
                    for (int i = 0; i < productViewModel.ImageFiles.Count; i++)
                    {
                        Image img = new Image();
                        img.IdProduct = productEntry.Entity.IdProduct;
                        img.ImageName = Guid.NewGuid().ToString();
                        img.Extension = "." + productViewModel.ImageFiles[i].ContentType.Split('/')[1];

                        using (var stream = new MemoryStream())
                        {
                            productViewModel.ImageFiles[i].CopyTo(stream);
                            img.Data = stream.ToArray();
                        }

                        newImages.Add(img);
                    }

                    List<Image> imageAdds = new List<Image>();
                    List<Image> imageDeletes = new List<Image>();

                    for (int i = 0; i < newImages.Count(); i++)
                    {
                        bool isDuplicate = false;

                        foreach (var oldImage in oldImages)
                        {
                            if (ByteArrayCompare(newImages[i].Data, oldImage.Data))
                            {
                                isDuplicate = true;
                                break;
                            }
                        }

                        if (isDuplicate == false) // chưa tồn tại, thêm mới
                        {
                            imageAdds.Add(newImages[i]);
                        }
                    }

                    // check image database exist in new image
                    foreach (var oldImage in oldImages)
                    {
                        bool isDeleted = true;

                        foreach (var newImage in newImages)
                        {
                            if (ByteArrayCompare(oldImage.Data, newImage.Data))
                            {
                                isDeleted = false;
                                break;
                            }
                        }

                        // remove old images
                        if (isDeleted)
                        {
                            _context.Images.RemoveRange(oldImage);
                        }
                    }

                    for (int i = 0; i < imageAdds.Count; i++)
                    {
                        if (await _s3BucketService.CheckConnectS3BucketAsync())
                        {
                            imageAdds[i].S3Uri = await _s3FileService.UploadFileAsync(productViewModel.ImageFiles[i], BucketNameConst.BucketName, imageAdds[i].ImageName + imageAdds[i].Extension);
                        }

                        imageAdds[i].Data = null;
                        _context.Images.Add(imageAdds[i]);
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    // set Data null for images
                    var tempImages = _context.Images.Where(image => image.IdProduct == productViewModel.IdProduct && image.Data != null).ToList();
                    foreach (var item in tempImages)
                    {
                        if (item.Data.Count() > 0)
                        {
                            item.Data = null;
                        }
                    }

                    // save and complete transaction
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Complete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion

        #region ByteArrayCompare
        private static bool ByteArrayCompare(byte[] firstArr, byte[] secondArr)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(firstArr, secondArr);
        }


        #endregion

        #region SendMailNotHighestPrice
        public async Task<bool> SendMailNotHighestPrice(string email, Guid productId, float currentPrice, float yourPrice)
        {
            try
            {
                var query = from p in _context.Products
                            join c in _context.Categories
                            on p.CategoryId equals c.IdCategory
                            where p.IdProduct.Equals(productId)
                            select new ProductViewModel()
                            {
                                IdProduct = p.IdProduct,
                                ProductName = p.ProductName,
                                StartDate = DateTime.Now,
                                CategoryId = c.IdCategory,
                                CategoryName = c.CategoryName
                            };

                var product = query.FirstOrDefault();
                if (product != null)
                {
                    await _handleProductService.SendMailNotHighestPrice(email, product.ProductName, product.CategoryName, currentPrice, yourPrice);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
