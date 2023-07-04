using coreADODisconnectedArchitectureProject.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace coreADODisconnectedArchitectureProject.DAO
{
    public class InventoryDataAccessLayer : IInventoryDataAccessLayer
    {
        public IConfiguration Configuration {get;}
        public InventoryDataAccessLayer(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IEnumerable<Inventory> GetInventories()
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            List<Inventory> inventories = new List<Inventory>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Inventory";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach(DataRow dataRow in dataTable.Rows)
            {
                Inventory inventory = new Inventory();
                inventory.Id = Convert.ToInt32(dataRow[0]);
                inventory.Name = dataRow[1].ToString();
                inventory.Price = decimal.Parse(dataRow[2].ToString());
                inventory.Quantity = int.Parse(dataRow[3].ToString());
                inventories.Add(inventory);
            }
            return inventories;
        }

        public void AddInventory(Inventory inventory)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Inventory";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder command = new SqlCommandBuilder(dataAdapter);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            DataRow dataRow = dataTable.NewRow();
            dataRow[1] = inventory.Name;
            dataRow[2] = inventory.Price;
            dataRow[3] = inventory.Quantity;
            dataTable.Rows.Add(dataRow);
            dataAdapter.UpdateCommand = command.GetInsertCommand();
            dataAdapter.Update(dataTable);
        }

        public Inventory GetInventory(int id)
        {
            Inventory inventory = new Inventory();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Inventory";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if(Convert.ToInt32(dataRow[0]) == id)
                {
                    inventory.Id = Convert.ToInt32(dataRow[0]);
                    inventory.Name = dataRow[1].ToString();
                    inventory.Price = decimal.Parse(dataRow[2].ToString());
                    inventory.Quantity = int.Parse(dataRow[3].ToString());
                    inventory.AddedOn = Convert.ToDateTime(dataRow[4].ToString());
                }
            }
            return inventory;
        }

        public void DeleteInventory(int id, Inventory inventory)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string selectQuery = "Select * from Inventory";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);
            SqlCommandBuilder cmd = new SqlCommandBuilder(dataAdapter);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (Convert.ToInt32(dataRow[0]) == id)
                {
                    dataTable.Rows.Remove(dataRow);
                    dataAdapter.UpdateCommand = cmd.GetDeleteCommand();
                    dataAdapter.Update(dataTable);
                }
            }
        }

        public void EditInventory(int id, Inventory inventory)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string selectQuery = "Select * from [dbo].[Product]";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);
            SqlCommandBuilder cmd = new SqlCommandBuilder(dataAdapter);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (Convert.ToInt32(dataRow[0]) == id)
                {
                    dataRow[1] = inventory.Name;
                    dataRow[2] = inventory.Price;
                    dataRow[3] = inventory.Quantity;
                    dataAdapter.UpdateCommand = cmd.GetUpdateCommand();
                    dataAdapter.Update(dataTable);
                }
            }
        }

        
    }
}
