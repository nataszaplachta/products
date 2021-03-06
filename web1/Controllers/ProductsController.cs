using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ProductsController {
public List<Product> GetAllProducts(string searchedPhrase)
    {
        SqlConnection connection=new SqlConnection();
        connection.ConnectionString="Server=MO;Database=TSQL2012;Trusted_Connection=True;";
        connection.Open();
        SqlCommand command=new SqlCommand();
        
        command.CommandType=CommandType.Text;
        command.CommandText=$"SELECT * FROM GetAllProducts WHERE productname like '%{searchedPhrase}%'";
        command.Connection=connection;
        SqlDataReader reader = command.ExecuteReader();
        
        List<Product> productNames=new List<Product>();
        while (reader.Read())
        {
             Product temp=new Product();
             temp.ProductID=int.Parse(reader["productid"].ToString());
             temp.ProductName=reader["productName"].ToString();
             temp.SupplierID=int.Parse(reader["supplierid"].ToString());
             temp.CategoryID=int.Parse(reader["categoryid"].ToString());
             temp.UnitPrice=double.Parse(reader["unitprice"].ToString());
             temp.Discontinued=bool.Parse(reader["discontinued"].ToString());
             productNames.Add(temp);
        }
        return productNames; 
    }
}