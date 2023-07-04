using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using coreADOConnectedArchitectureProject.Models;
using Microsoft.Extensions.Configuration;
using coreADOConnectedArchitectureDAOProject.DAO;

namespace coreADOConnectedArchitectureProject.Controllers
{
    public class ProductController : Controller
    {
        public ProductDataAccessLayer productDAO;

        public ProductController(IConfiguration configuration)
        {
            productDAO = new ProductDataAccessLayer(configuration);
        }
        public IActionResult Index()
        {
            return View(productDAO.GetProducts());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                productDAO.AddProduct(product);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }


            
        }
        public IActionResult Details(int id)
        {
            Product product = productDAO.GetProduct(id);
            if (product != null)
            {
                return View(product);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            return View(productDAO.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Edit(Product product, int id)
        {
            try
            {
                productDAO.EditProduct(id, product);
                return RedirectToAction("Index");
            }
            catch(Exception ex) 
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            return View(productDAO.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Delete(Product product, int id)
        {
            productDAO.DeleteProduct(id, product);
            return RedirectToAction("Index");
        }
    }
}