using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using System.Drawing;
using StoreFront.UI.MVC.Utilities;//Added for access to ImageUtility
using PagedList;//Added for Paged listing
using PagedList.Mvc;//Added for access to Server-Side Paged Listing
using StoreFront.UI.MVC.Models;//Added for access to CartItemViewModel

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();


        public ActionResult AddToCart(int qty, int productID)
        {
            #region Custom Add-To-Cart Functionality (Called from Details View)

            //Create an empty shell for the LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if the Session shopping cart exists. If so, we can use it to populate the local version
            if (Session["cart"] != null)
            {
                //Session shopping car exists. Put it's items in the local version, which is easier to work with
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //We need to UNBOX the Session object to its smaller, more specific type -- Explicit Casting
            }

            else
            {
                //If the Session cart doesn't exist yet, we need to instantiate it to get started
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }//After this if/else, we now have a local cart thats ready to add things to it

            //Find the product they referenced by its ID
            Product product = db.Products.Where(b => b.ProductID == productID).FirstOrDefault();
            if (product == null)
            {
                //If given a bad ID, return the user to some other page to try again.
                //Alternatively, we could throw some kind of error, which we will
                //discuss further in Module 6.
                return RedirectToAction("Index");
            }
            else
            {
                //If the book is valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //Put the item in the local cart. If they already have that product as a 
                //cart item, instead we will update the quantity. This is a big part
                //of why we have the dictionary.
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //Now update the SESSION version of the cart so we can maintain that info between requests
                Session["cart"] = shoppingCart; //No explicit casting needed here
            }

            //Send them to View their Cart Items
            return RedirectToAction("Index", "ShoppingCart");

        }

#endregion

        // GET: Products
        public ActionResult Index(string categoryFilter,int page = 1)
        {
            //Filtering
            var products = db.Products.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.ShoeSize).Include(p => p.Status).ToList();//All the products here
            //But if they give a categoriesfilter
            //We then go into the if statment and filter out the category names and allow us to pinpoints the Models in Categories.
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                //LINK Logic!!!
                var productFilter = db.Products.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.ShoeSize).Include(p => p.Status).Where(
                    p => p.Category.CategoryName.ToLower()==categoryFilter.ToLower()
                    ).ToList();
                //P is every product we are checking the category name to see if it matches our category filter. .toLower allows both lowercase or upercase on logic so they match and not have issues with casing.
                int pageSizeFilter = 5;

                return View(productFilter.ToPagedList(page, pageSizeFilter));

            }

            //Pagelist functionality

            int pageSize = 5;
            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            //ViewBag.StockStatusID = new SelectList(db.Statuses, "StockStatusID", "Statuses");
            return View(products.ToPagedList(page, pageSize));


        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ManufactureID = new SelectList(db.Manufacturers, "ManufactureID", "ManufactureName");
            ViewBag.ShoeSizeID = new SelectList(db.ShoeSizes, "ShoeSizeID", "ShoeSizeID");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Statuses");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ShoeSizeID,ManufactureID,CategoryID,Price,UnitsInStock,StatusID,Description,ProductImage")] Product product,
            HttpPostedFileBase ProductImageForCreate)
        {
            if (ModelState.IsValid)
            {
                #region FileUpload

                string file = "NoImage.png";

                if (ProductImageForCreate != null)
                {
                    file = ProductImageForCreate.FileName;

                    string extension = file.Substring(file.LastIndexOf('.'));

                    string[] goodextensions = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file ext is in our list of acceptable extensions
                    //This if statement is to check the image is under 4mb.
                    if (goodextensions.Contains(extension.ToLower()) && ProductImageForCreate.ContentLength <= 4194304)
                    {
                        //Create a new file name (GUID)
                        file = Guid.NewGuid() + extension;

                        //Resize the Image
                        string savePath = Server.MapPath("~/Content/assets/images/Products/");

                        Image convertedImage = Image.FromStream(ProductImageForCreate.InputStream);

                        int MaxImageSize = 500;
                        int MaxThumSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, MaxImageSize, MaxThumSize);
                    }
                    //Update the Photo URL
                    product.ProductImage = file;
                }
                #endregion

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ManufactureID = new SelectList(db.Manufacturers, "ManufactureID", "ManufactureName", product.ManufactureID);
            ViewBag.ShoeSizeID = new SelectList(db.ShoeSizes, "ShoeSizeID", "ShoeSizeID", product.ShoeSizeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Statuses", product.StatusID);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles=("Admin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ManufactureID = new SelectList(db.Manufacturers, "ManufactureID", "ManufactureName", product.ManufactureID);
            ViewBag.ShoeSizeID = new SelectList(db.ShoeSizes, "ShoeSizeID", "ShoeSizeID", product.ShoeSizeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Statuses", product.StatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ShoeSizeID,ManufactureID,CategoryID,Price,UnitsInStock,StatusID,Description,ProductImage")] Product product,
            HttpPostedFileBase ProductImageForEdit)
        {
            if (ModelState.IsValid)
            {
                #region FileUpload

                string file = "NoImage.png";

                if (ProductImageForEdit != null)
                {
                    file = ProductImageForEdit.FileName;

                    string extension = file.Substring(file.LastIndexOf('.'));

                    string[] goodextensions = { ".jpeg", ".jpg", ".png", ".gif" };

              
                    if (goodextensions.Contains(extension.ToLower()) && ProductImageForEdit.ContentLength <= 4194304)
                    {
                        
                        file = Guid.NewGuid() + extension;

                        
                        string savePath = Server.MapPath("~/Content/assets/images/Products/");

                        Image convertedImage = Image.FromStream(ProductImageForEdit.InputStream);

                        int MaxImageSize = 500;
                        int MaxThumSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, MaxImageSize, MaxThumSize);

                        if (product.ProductImage != null && product.ProductImage != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/assets/images/Products/");
                            ImageUtility.Delete(path, product.ProductImage);
                        }
                        //Update the Photo URL
                        product.ProductImage = file;
                    }

                }
                #endregion

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ManufactureID = new SelectList(db.Manufacturers, "ManufactureID", "ManufactureName", product.ManufactureID);
            ViewBag.ShoeSizeID = new SelectList(db.ShoeSizes, "ShoeSizeID", "ShoeSizeID", product.ShoeSizeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Statuses", product.StatusID);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            //Delete
            string path = Server.MapPath("~/Content/assets/images/Products/");
            ImageUtility.Delete(path, product.ProductImage);


            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
