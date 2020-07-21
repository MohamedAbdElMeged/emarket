using emarket.Models;
using emarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emarket.Controllers
{
    public class ProductsController : Controller
    {
        private emarketContext _con = new emarketContext();

      
        // GET: Products
        public ActionResult Index()
        {
         
            var model = new ProductListViewModel();
            model.Products = _con.Products.ToList();
            model.Categories = _con.Categories.ToList();
            return View(model);
        }

        // Get Request to open add product page
        [HttpGet]
        public ActionResult Create()
        {
            // create new instance of product view model
            var model = new ProductViewModel();
            // add categories to the instance
            model.categories = _con.Categories.ToList();
            //return page
            return View(model);
        }

        // POST Request to add product in DB
        [HttpPost]
        public ActionResult Create (ProductViewModel productViewModel , HttpPostedFileBase photo)
        {
            // create new intance from product model
            Product product = new Product();
            //mapping from the view model instance to the product instance
            product.Name = productViewModel.name;
            product.Price = productViewModel.price;
            product.Description = productViewModel.description;
            product.category_id = productViewModel.category_id;
            // get the category by its id
            var category = _con.Categories.FirstOrDefault(c=> c.Id == productViewModel.category_id);
            
            
            // Save the product image 
            String photoName = "";
            photoName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
            string photoPath = Server.MapPath("~/Images/" + photoName);
            photo.SaveAs(photoPath);
            product.Image = photoName;
            
            //add the new product in db
            _con.Products.Add(product);
            // update the number of products in category 
            category.Number_Of_Products += 1;
           // save the DB
            _con.SaveChanges();
            // return to the home page
            return RedirectToAction("Index");
        }


        // open the product details page by its id
        public ActionResult Details (int id)
        {
            // create new instance from product details virtual model
            var model = new ProductDetailsViewModel();
            // retrieve the product from database by the id
            var product = _con.Products.FirstOrDefault(p => p.Id == id);
            // mapping from the model to the view model
            model.category_id = product.category_id;
            model.category_name = _con.Categories.FirstOrDefault(c => c.Id == model.category_id).Name;
            model.product_id = id;
            model.product_name = product.Name;
            model.product_price = product.Price;
            model.product_description = product.Description;
            model.product_image = product.Image;

            //return the details page
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            var model = new ProductViewModel();
            var product = _con.Products.FirstOrDefault(p => p.Id == id);
            model.categories = _con.Categories.ToList();
            model.id = product.Id;
            model.image = product.Image;
            model.name = product.Name;
            model.price = product.Price;
            model.description = product.Description;
            model.category_id = (int) product.category_id;
            model.category_name = _con.Categories.FirstOrDefault(c => c.Id == model.category_id).Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel , HttpPostedFileBase photo)
        {
            Product product = _con.Products.FirstOrDefault(p=>p.Id == productViewModel.id);
            product.Name = productViewModel.name;
            product.Price = productViewModel.price;
            product.Description = productViewModel.description;
            product.category_id = productViewModel.category_id;
            if (photo != null)
            {
                String photoName = "";
                photoName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
                string photoPath = Server.MapPath("~/Images/" + photoName);
                photo.SaveAs(photoPath);
                product.Image = photoName;
            }
           
          
            _con.SaveChanges();

            return RedirectToAction("Index");

        }

        // delete specific product by its id
        [HttpGet]
        public ActionResult Delete(int id)
        {
            // retrieve product from db by its id
            var product = _con.Products.FirstOrDefault(p => p.Id == id);

            // retrieve category from db by product category id
            var category = _con.Categories.FirstOrDefault(c => c.Id == product.category_id);

            // update the number of products in the category
            category.Number_Of_Products -= 1;

            // remove the product from db 
            _con.Products.Remove(product);
            //save changes
            _con.SaveChanges();

            //return the home page
            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult Filter(int id)
        {
            var model = new ProductListViewModel();
            model.Products = _con.Products.Where(p => p.category_id == id).ToList();
            model.Categories = _con.Categories.ToList();
            return View("Index", model);
        }

        }
}