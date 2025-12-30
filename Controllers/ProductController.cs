//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MyShop.Models;
//using System.Linq;

//namespace MyShop.Controllers
//{
//	public class ProductController : Controller
//	{
//		private readonly DbMyShopContext _context;

//        public ProductController(DbMyShopContext context)
//        {
//            _context = context;
//        }
//        [Route("san-pham")]
//		[Route("san-pham/page/{page:int}")]
//		[Route("san-pham/{parentSlug}")]
//		[Route("san-pham/{parentSlug}/page/{page:int}")]
//		[Route("san-pham/{parentSlug}/{slug}")]
//		[Route("san-pham/{parentSlug}/{slug}/page/{page:int}")]
//		public IActionResult Index(long? minPrice, long? maxPrice, string? orderby, string? parentSlug, string? slug, int page = 1, string search = "")
//		{
//			int pageSize = 12;
//			int totalItems = 0;
//			List<Product> productList = new List<Product>();
//			List<Product> products = new List<Product>();
//			List<int> childIds = new List<int>();
//			// Xác định slug danh mục
//			string? catSlug = slug ?? parentSlug;
//			string? categoryParentName = "";
//			string? categoryName = "";
//			if(string.IsNullOrEmpty(parentSlug))
//			{
//				var category = _context.Categories.FirstOrDefault(x => x.Slug == parentSlug);
//				categoryParentName = category?.Name;
//			}
//			if (string.IsNullOrEmpty(slug))
//			{
//				var category = _context.Categories.FirstOrDefault(x => x.Slug == slug);
//				categoryName = category?.Name;
//			}

//			IQueryable<Product> query = _context.Products.AsQueryable();

//			// Nếu có slug danh mục
//			if (!string.IsNullOrEmpty(catSlug))
//			{
//				var category = _context.Categories.FirstOrDefault(x => x.Slug == catSlug);
//				if (category != null)
//				{
//					// Nếu là danh mục CHA
//					if (category.ParentId == null)
//					{
//						// Lấy tất cả danh mục con của nó
//						childIds = _context.Categories
//							.Where(x => x.ParentId == category.Id)
//							.Select(x => x.Id)
//							.ToList();

//						// Bao gồm luôn parent category nếu sản phẩm thuộc cha
//						childIds.Add(category.Id);

//						foreach (var p in _context.Products.ToList())
//						{
//							if (childIds.Contains(p.CategoryId))
//							{
//								totalItems++;
//							}
//						}
//					}
//					else
//					{
//						// Nếu là danh mục CON → lọc bình thường
//						query = query.Where(x => x.CategoryId == category.Id);
//					}
//				}
//			}

//			// Lấy danh sách sản phẩm
//			if (!string.IsNullOrEmpty(catSlug) && catSlug.Equals(parentSlug))
//			{
//				foreach (var p in _context.Products.OrderByDescending(x => x.Id).ToList())
//				{
//					if (childIds.Contains(p.CategoryId))
//					{
//						productList.Add(p);
//					}
//				}
//				products = productList
//					.ToList();
//			}
//			else
//			{
//				products = query
//				.OrderByDescending(x => x.Id)
//				.ToList();
//			}

//			//lọc theo orderby
//			if (!string.IsNullOrEmpty(orderby))
//			{
//				if (orderby.Equals("price"))
//				{
//					products = products
//						.OrderBy(x => x.SalePrice>0?x.SalePrice:x.Price)
//						.ToList();
//				}

//                if (orderby.Equals("price-desc"))
//                {
//                    products = products
//                        .OrderByDescending(x => x.SalePrice > 0 ? x.SalePrice : x.Price)
//                        .ToList();
//                }
//            }

//			//lọc theo giá
//			if (minPrice.HasValue)
//			{
//				products = products
//					.Where(x => (x.SalePrice > 0 ? x.SalePrice : x.Price) >= minPrice.Value)
//					.ToList();
//			}

//			if (maxPrice.HasValue)
//			{
//				products = products
//					.Where(x => (x.SalePrice > 0 ? x.SalePrice : x.Price) <= maxPrice.Value)
//					.ToList();
//			}

//			//lọc theo tên
//			if (!string.IsNullOrEmpty(search))
//			{
//				products = products
//					.Where(x =>
//						x.Name.Trim().ToLower().Contains(search.Trim().ToLower())
//						|| x.Slug.Trim().ToLower().Contains(search.Trim().ToLower())
//						)
//					.ToList();
//			}

//			// Tổng số sản phẩm sau khi lọc
//			totalItems = totalItems > 0 ? totalItems : products.Count();
//			int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
//			decimal raw = _context.Products
//									.Select(x => x.SalePrice > 0 ? x.SalePrice : x.Price)
//									.Max() ?? 0m;

//			// Làm tròn lên theo bậc 10.000 rồi convert sang long
//			long topPrice = (long)(
//				Math.Ceiling(raw / 10000m) * 10000m
//			);

//			products = products
//				.Where(p => p.Status == "active")
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//				.ToList();

//            // Lấy danh mục cha + con
//            var categoryParent = _context.Categories
//				.Where(x => x.ParentId == null)
//				.ToList();
//			var categories = _context.Categories
//				.Include(x => x.Products)
//				.Where(x => x.ParentId != null)
//				.Where(x => x.Products.Any())
//				.ToList();

//			//Lấy page danh mục
//            var pagesL2 = _context.Pages
//                .OrderBy(x => x.Ord)
//                .Where(x => x.Level != null && x.Level.Length == 10 && x.Active == 1)
//                .ToList();

//            var pagesL3 = _context.Pages
//                .OrderBy(x => x.Ord)
//                .Where(x => x.Level != null && x.Level.Length == 15 && x.Active == 1)
//                .ToList();

//            var pageSanPham = _context.Pages
//				.Where(x => x.Position == 1 && x.Active == 1)
//				.FirstOrDefault(x => x.Name.Trim().ToLower() == "sản phẩm");

//            ViewBag.PagesL2 = pagesL2;
//            ViewBag.PagesL3 = pagesL3;
//            ViewBag.PageTinTucLevel = pageSanPham?.Level;

//            ViewBag.Page = page;
//			ViewBag.TotalPages = totalPages;
//			ViewBag.CategoryParent = categoryParent;
//			ViewBag.Category = categories;
//			ViewBag.ParentSlug = parentSlug;
//			ViewBag.Slug = slug;
//			ViewBag.Orderby = orderby;
//			ViewBag.MinPrice = minPrice;
//			ViewBag.MaxPrice = maxPrice;
//			ViewBag.TopPrice = topPrice;
//			ViewBag.CategoryParentName = categoryParentName;
//			ViewBag.CategoryName = categoryName;
//			ViewBag.Search = search;
//			return View(products);
//		}


//		[Route("chi-tiet/{slug}")]
//		public IActionResult Details(string slug) 
//		{
//			var categories = _context.Categories
//				.Include(c => c.Parent)
//				.ToList();
//			var product = _context.Products
//				.Include(x => x.Category)
//				.FirstOrDefault(x => x.Slug == slug);
//			var category = _context.Categories.FirstOrDefault(x => x.Id == product.CategoryId);
//			var relatedProducts = _context.Products.Where(x => x.CategoryId == category.Id).ToList();
//			var productSpecs = _context.ProductSpecs
//				.Where(x => x.ProductId == product.Id)
//				.ToList();
//			var productImages = _context.ProductImages
//				.Where(x => x.ProductId == product.Id)
//				.ToList();
//			ViewBag.Categories = categories;
//			ViewBag.ProductSpecs = productSpecs;
//			ViewBag.ProductImages = productImages;
//			ViewBag.RelatedProducts = relatedProducts;
//			return View("chi-tiet-san-pham", product);
//		}
//	}
//}
