using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

public class CategoryController:Controller
{
    public List<Category> GetAllCategories()
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = "Server=MO;Database=TSQL2012;Trusted_Connection=True;";
        connection.Open();
        SqlCommand command = new SqlCommand();

        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM Production.Categories";
        command.Connection = connection;
        SqlDataReader reader = command.ExecuteReader();


        List<Category> categoryNames = new List<Category>();
        while (reader.Read())
        {
            Category temp = new Category();
            temp.CategoryID = int.Parse(reader["categoryid"].ToString());
            temp.categoryName = reader["categoryname"].ToString();
            categoryNames.Add(temp);
        }
        return categoryNames;
    }
}