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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductViewModel();
            model.categories = _con.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create (ProductViewModel productViewModel , HttpPostedFileBase photo)
        {

            Product product = new Product();
            product.Name = productViewModel.name;
            product.Price = productViewModel.price;
            product.Description = productViewModel.description;
            var category = _con.Categories.FirstOrDefault(c=> c.Id == productViewModel.category_id);
            product.category_id =  productViewModel.category_id;

            String photoName = "";
            photoName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
            string photoPath = Server.MapPath("~/Images/" + photoName);
            photo.SaveAs(photoPath);
            product.Image = photoName;
            _con.Products.Add(product);
            category.Number_Of_Products += 1;
           
            _con.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult Details (int id)
        {
            var model = new ProductDetailsViewModel();
            var product = _con.Products.FirstOrDefault(p => p.Id == id);
            model.category_id = (int)product.category_id;
            model.category_name = _con.Categories.FirstOrDefault(c => c.Id == model.category_id).Name;
            model.product_id = id;
            model.product_name = product.Name;
            model.product_price = product.Price;
            model.product_description = product.Description;
            model.product_image = product.Image;
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = _con.Products.FirstOrDefault(p => p.Id == id);
            _con.Products.Remove(product);
            _con.SaveChanges();
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