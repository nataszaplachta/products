using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using web1.Models;
using web1.Repository;


public class ProductsController : Controller
{
    [HttpGet]
    public ViewResult GetAllProducts(string searchPhrase, int page, string sortBy = "ProductName")
    {
        
        ProductsRepository ProductRepo = new ProductsRepository(); 
          ViewData["Product"]=ProductRepo.GetAllProducts(sortBy, searchPhrase, page);

    ViewData["currentPage"]=page;
    ViewData["sortBy"]=sortBy;
    
        return View();
    
    }
        [HttpGet]
    public ViewResult AddProductForm()
    {
        SqlConnection connection= new SqlConnection();
        connection.ConnectionString="Server=MO;Database=TSQL2012;Trusted_Connection=True;";
        connection.Open();
        SqlCommand command= new SqlCommand();
        command.CommandType=CommandType.Text;
        command.CommandText=$@"select*from Production.Products as pp inner join Production.Suppliers as ps on pp.supplierid= ps.supplierid inner join Production.Categories as pc on pp.categoryid = pc.categoryid";
        
        command.Connection=connection;

        SqlDataReader reader = command.ExecuteReader();
      
        List <SelectListItem> item=new List<SelectListItem>();
        List <SelectListItem> item2=new List<SelectListItem>();
         while(reader.Read())
        {
            Suppliers temp1=new Suppliers();
            temp1.companyName=reader["companyname"].ToString();
            temp1.supplierID=int.Parse(reader["supplierid"].ToString());
             Category temp2=new Category();
            temp2.CategoryID=int.Parse(reader["categoryid"].ToString());
            temp2.categoryName=reader["categoryname"].ToString();
           
            item.Add(new SelectListItem { Text =  temp1.companyName,Value=temp1.supplierID.ToString()});
            item2.Add(new SelectListItem { Text =  temp2.categoryName,Value=temp2.CategoryID.ToString()});
            
        
        }
        ViewData["supplier"]=item;
        ViewData["category"]=item2;
        return View();
    }

    [HttpPost]
    public void AddProduct([FromForm] Product p)
    {
        // todo add to database
    
    SqlConnection connection = new SqlConnection();
        connection.ConnectionString = "Server=MO;Database=TSQL2012;Trusted_Connection=True;";
        connection.Open();
        SqlCommand command = new SqlCommand();
         command.CommandType=CommandType.Text;
        command.CommandText=$"EXEC [dbo].[addProduct] {p.ProductName},1,2,{p.UnitPrice},{p.Discontinued}";
        command.Connection=connection;
        SqlDataReader reader = command.ExecuteReader();
    }
    
}