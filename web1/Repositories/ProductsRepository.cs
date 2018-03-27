using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using web1.Models;

namespace web1.Repository

{
    public class ProductsRepository
    {

        public List<Product> GetAllProducts(string sortBy, string searchPhrase, int page)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server=MO;Database=TSQL2012;Trusted_Connection=True;";
            connection.Open();
            SqlCommand command = new SqlCommand();
            if (sortBy == null)
            {
                sortBy = "ProductName";
            }

            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM GetAllProducts WHERE productname like '%{searchPhrase}%' order by {sortBy} offset {page * 10} rows fetch next 10 rows only";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();


            List<Product> productNames = new List<Product>();
            while (reader.Read())
            {
                Product temp = new Product();
                temp.ProductID = int.Parse(reader["productid"].ToString());
                temp.ProductName = reader["productName"].ToString();
                temp.SupplierID = int.Parse(reader["supplierid"].ToString());
                temp.CategoryID = int.Parse(reader["categoryid"].ToString());
                temp.UnitPrice = double.Parse(reader["unitprice"].ToString());
                temp.Discontinued = bool.Parse(reader["discontinued"].ToString());
                productNames.Add(temp);
            }
            return productNames;

        }

}
}