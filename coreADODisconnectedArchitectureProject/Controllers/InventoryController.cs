using coreADODisconnectedArchitectureProject.DAO;
using coreADODisconnectedArchitectureProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreADODisconnectedArchitectureProject.Controllers
{
    public class InventoryController : Controller
    {
        InventoryDataAccessLayer inventoryDAO;
        public InventoryController(IConfiguration configuration)
        {
            inventoryDAO = new InventoryDataAccessLayer(configuration);
        }
        public IActionResult Index()
        {
            return View(inventoryDAO.GetInventories());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Inventory inventory)
        {
            inventoryDAO.AddInventory(inventory);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            return View(inventoryDAO.GetInventory(id));
        }
        public IActionResult Edit(int id)
        {
            return View(inventoryDAO.GetInventory(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, Inventory inventory)
        {
            inventoryDAO.EditInventory(id, inventory);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View(inventoryDAO.GetInventory(id));
        }
        [HttpPost]
        public IActionResult Delete(int id, Inventory inventory)
        {
            inventoryDAO.DeleteInventory(id, inventory);
            return RedirectToAction("Index");
        }
        
    }
}
