using AutoMapper;
using Core.AutoMapperProfile;
using Core.ViewModel;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Search.API.Interfaces;

namespace Search.API.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        #region Constructor
        SQLServerDbContext _context;
        Mapper _mapperImg;

        public SearchRepository(SQLServerDbContext context)
        {
            _context = context;
            _mapperImg = ImageMapperConfig.InitAutomapper();
        }
        #endregion

        #region Get List Category
        public async Task<IList<CategoryViewModel>> GetListCategory()
        {
            var query = from c in _context.Categories
                        select new CategoryViewModel()
                        {
                            IdCategory = c.IdCategory,
                            CategoryName = c.CategoryName,
                        };
            if (query != null)
            {
                return await query.ToListAsync().ConfigureAwait(false);
            }
            return new List<CategoryViewModel>();
        }
        #endregion

        #region SearchNameProduct
        public async Task<IList<string>> SearchNameProduct()
        {
            var results = _context.Products.Select(x => x.ProductName);
            if (results != null)
            {
                return results.ToList();
            }
            return new List<string>();
        }
        #endregion

        #region Get List Product Auctioning By Category
        public async Task<IList<ProductViewModel>> GetListProductAuctioningByCategory(Guid categoryId)
        {
            var category = await _context.Categories.Where(x => x.IdCategory.Equals(categoryId)).FirstOrDefaultAsync();
            IQueryable<ProductViewModel> query = null;
            if (category != null)
            {
                query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IsApprove == true
                        && auc.IsStart == true
                        && auc.IsEnd == false
                        && c.IdCategory == categoryId
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
            }
            else
            {
                query = from p in _context.Products
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
            }

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

        #region SearchProductByProductName
        public async Task<IList<ProductViewModel>> SearchProductByProductName(string productName)
        {
            IQueryable<ProductViewModel> query = null;

            if (productName == Core.Constant.SearchNameConst.All)
            {
                query = from p in _context.Products
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
            }
            else
            {
                query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IsApprove == true
                        && auc.IsStart == true
                        && auc.IsEnd == false
                        && p.ProductName.Contains(productName)
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
            }

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

        #region SearchByProductNameAndCategory
        public async Task<IList<ProductViewModel>> SearchByProductNameAndCategory(Guid categoryId, List<string> optionsList)
        {
            IQueryable<ProductViewModel> query = null;
            var category = await _context.Categories.Where(x => x.IdCategory.Equals(categoryId)).FirstOrDefaultAsync();

            if (category == null && optionsList.First() == Core.Constant.SearchNameConst.All)  // categoryId = All, productName = All (empty)
            {
                query = from p in _context.Products
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
                            CategoryName = c.CategoryName,
                            IsReplaceSearch = false
                        };
            }
            else if (category == null && optionsList.First() != Core.Constant.SearchNameConst.All) // categoryId = All, productName = specific name
            {
                query = from p in _context.Products
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
                            CategoryName = c.CategoryName,
                            IsReplaceSearch = false
                        };

                var queryResults = query.AsEnumerable();
                query = queryResults.Where(p => optionsList.Any(option => p.ProductName.Trim().ToLower().Contains(option.Trim().ToLower()))).AsQueryable();
            }
            else if (category != null && optionsList.First() == Core.Constant.SearchNameConst.All) // categoryId = specific category, productName = All
            {
                query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IsApprove == true
                        && auc.IsStart == true
                        && auc.IsEnd == false
                        && c.IdCategory == categoryId
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
                            CategoryName = c.CategoryName,
                            IsReplaceSearch = false
                        };
            }
            else if (category != null && optionsList.First() != Core.Constant.SearchNameConst.All) // categoryId = specific category, productName = specific name
            {
                query = from p in _context.Products
                        join c in _context.Categories
                        on p.CategoryId equals c.IdCategory
                        join auc in _context.Auctions
                        on p.IdProduct equals auc.IdProduct
                        where p.IsApprove == true
                        && auc.IsStart == true
                        && auc.IsEnd == false
                        && c.IdCategory == categoryId
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
                            CategoryName = c.CategoryName,
                            IsReplaceSearch = false
                        };

                var queryResults = query.AsEnumerable();
                query = queryResults.Where(p => optionsList.Any(option => p.ProductName.Trim().ToLower().Contains(option.Trim().ToLower()))).AsQueryable();
            }

            var temps = query.ToList();
            var products = temps.OrderBy(product => product.CategoryName)
                                  .ThenBy(product => product.ProductName)
                                  .ToList();

            #region Replace Search
            if (products.Count == 0)
            {
                if (category == null && optionsList.First() != Core.Constant.SearchNameConst.All) // categoryId = All, productName = specific name
                {
                    query = from p in _context.Products
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
                                CategoryName = c.CategoryName,
                                IsReplaceSearch = true
                            };
                }
                else if (category != null && optionsList.First() != Core.Constant.SearchNameConst.All) // categoryId = specific category, productName = specific name
                {
                    query = from p in _context.Products
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
                                CategoryName = c.CategoryName,
                                IsReplaceSearch = true
                            };

                    var queryResults = query.AsEnumerable();
                    query = queryResults.Where(p => optionsList.Any(option => p.ProductName.Trim().ToLower().Contains(option.Trim().ToLower()))).AsQueryable();
                }

                var temps2 = query.ToList();
                products = temps2.OrderBy(product => product.CategoryName)
                                   .ThenBy(product => product.ProductName)
                                   .ToList();
            }
            #endregion

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
    }
}
