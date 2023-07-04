using coreADOConnectedArchitectureProject.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace coreADOConnectedArchitectureDAOProject.DAO
{
    public class ProductDataAccessLayer : IProductDataAccessLayer
    {
        public IConfiguration Configuration { get; }
        public ProductDataAccessLayer(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void AddProduct(Product product)
        {
            //string strConnection = "Data Source=DESKTOP-GF33IH9;Initial Catalog=SampleDB;Integrated Security=true;";
            string strConnection = Configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(strConnection);
            connection.Open();
            string query = "INSERT INTO Product VALUES(@name, @price, @quantity);";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", product.ProductName);
            command.Parameters.AddWithValue("@price", product.ProductPrice);
            command.Parameters.AddWithValue("@quantity", product.ProductQuantity);
            int result = command.ExecuteNonQuery();

        }

        public void DeleteProduct(int id, Product product)
        {
            // string strConnection = "Data Source=DESKTOP-GF33IH9;Initial Catalog=SampleDB;Integrated Security=true;";
            string strConnection = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                connection.Open();
                string query = "Delete from Product where ProductId = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int result = command.ExecuteNonQuery();

                }
            }
        }

        public void EditProduct(int id, Product product)
        {
            // string strConnection = "Data Source=DESKTOP-GF33IH9;Initial Catalog=SampleDB;Integrated Security=true;";
            string strConnection = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                connection.Open();
                string query = "Update Product Set ProductName=@name, ProductPrice=@price, ProductQuantity=@quantity Where ProductId=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nam", product.ProductName);
                    command.Parameters.AddWithValue("@price", product.ProductPrice);
                    command.Parameters.AddWithValue("@quantity", product.ProductQuantity);
                    command.Parameters.AddWithValue("@id", product.ProductId);
                    int result = command.ExecuteNonQuery();

                }
            }
        }

        public Product GetProduct(int id)
        {
            Product product = new Product();
            // string strConnection = "Data Source=DESKTOP-GF33IH9;Initial Catalog=SampleDB;Integrated Security=true;";
            string strConnection = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                connection.Open();
                string query = "select * from Product where ProductId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product.ProductId = int.Parse(reader["ProductId"].ToString());
                            product.ProductName = reader["ProductName"].ToString();
                            product.ProductPrice = decimal.Parse(reader["ProductPrice"].ToString());
                            product.ProductQuantity = int.Parse(reader["ProductQuantity"].ToString());
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            string strConnection = Configuration["ConnectionStrings:DefaultConnection"];
            List<Product> productList = new List<Product>();
            SqlConnection connection = new SqlConnection(strConnection);
            try
            {
                connection.Open();
                string query = "Select * from Product";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.ProductId = int.Parse(dataReader["ProductId"].ToString());
                    product.ProductName = dataReader["ProductName"].ToString();
                    product.ProductPrice = decimal.Parse(dataReader["ProductPrice"].ToString());
                    product.ProductQuantity = int.Parse(dataReader["ProductQuantity"].ToString());
                    productList.Add(product);
                }
                connection.Close();
            }
            catch (Exception ex) {   }
            return productList;
        }
    }
}
