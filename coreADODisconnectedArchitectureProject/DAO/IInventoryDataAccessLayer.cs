using coreADODisconnectedArchitectureProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreADODisconnectedArchitectureProject.DAO
{
    interface IInventoryDataAccessLayer
    {
        public IEnumerable<Inventory> GetInventories();
        public void AddInventory(Inventory inventory);
        public Inventory GetInventory(int id);
        public void DeleteInventory(int id, Inventory inventory);
        public void EditInventory(int id, Inventory inventory);
    }
}
