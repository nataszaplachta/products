using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

public class SuppliersController : Controller
{
    public List<Suppliers> GetAllSuppliers()
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = "Server=MO;Database=TSQL2012;Trusted_Connection=True;";
        connection.Open();
        SqlCommand command = new SqlCommand();

        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM Production.Suppliers";
        command.Connection = connection;
        SqlDataReader reader = command.ExecuteReader();


        List<Suppliers> supplierNames = new List<Suppliers>();
        while (reader.Read())
        {
            Suppliers temp = new Suppliers();
            temp.supplierID = int.Parse(reader["supplierid"].ToString());
            temp.companyName = reader["companyname"].ToString();
            supplierNames.Add(temp);
        }
        return supplierNames;
    }
}