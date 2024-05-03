using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace APBD6;
[Route("api/[controller]")]
[ApiController]
public class WarehouseController: ControllerBase
{
    public NpgsqlConnection _connection;

    public WarehouseController(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public ActionResult<Product> AddProduct(Product product)
    {
        _connection.Open();
        
        //------------------Product exist
        
        var IdProduct = product.IdProduct;
        var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM product " +
                                    "WHERE IdProduct=@IdProduct", _connection);
        cmd.Parameters.AddWithValue("IdProduct", IdProduct);
        int countProduct = Convert.ToInt32(cmd.ExecuteScalar());
        
        if (!(countProduct > 0))
        {
            return BadRequest("Product does not exist.");
        }
        
        //------------------Warehouse exist
        
        var IdWarehouse = product.IdWarehouse;
        cmd = new NpgsqlCommand("SELECT COUNT(*) FROM warehouse " +
                                    "WHERE IdWarehouse = @IdWarehouse", _connection);
        cmd.Parameters.AddWithValue("IdProduct", IdProduct);
        int countWarehouse = Convert.ToInt32(cmd.ExecuteScalar());
        
        if (!(countWarehouse > 0))
        {
            return BadRequest("Warehouse does not exist.");
        }
        
        //------------------Product amount
        
        if (!(product.Amount > 0))
            
        {
            return BadRequest("Product amount must be > 0");
        }
        
        //------------------Product order
        
        var amount = product.Amount;
        cmd = new NpgsqlCommand("SELECT COUNT(*) FROM \"Order\" " +
                                "WHERE IdProduct = @IdProduct AND Amount = @Amount", _connection);
        
        cmd.Parameters.AddWithValue("IdProduct", IdProduct);
        cmd.Parameters.AddWithValue("Amount", amount);
        int countOrder = Convert.ToInt32(cmd.ExecuteScalar());
        
        if (!(countOrder > 0))
        {
            return BadRequest("Purchase order does not exist for the specified product and amount.");
        }
        
        //------------------Product_warehouse check
        
        //skip
        
        //------------------Update FullfilledAt
        
        cmd = new NpgsqlCommand(
            "UPDATE \"Order\" SET FulfilledAt = @FulfilledAt WHERE IdProduct = @IdProduct AND Amount = @Amount",
            _connection);
        cmd.Parameters.AddWithValue("FulfilledAt", DateTime.Now);
        cmd.Parameters.AddWithValue("IdProduct", IdProduct);
        cmd.Parameters.AddWithValue("Amount", amount);
        cmd.ExecuteNonQuery();
        
        //------------------Update FullfilledAt

        cmd = new NpgsqlCommand(
            "INSERT INTO Product_Warehouse (IdProduct, IdWarehouse, Price, CreatedAt) VALUES (@IdProduct, @IdWarehouse, @Price, @CreatedAt) RETURNING idproductwarehouse",
            _connection);
        
        cmd.Parameters.AddWithValue("IdProduct", product.IdProduct);
        cmd.Parameters.AddWithValue("IdWarehouse", product.IdWarehouse);


        var cmd2 = new NpgsqlCommand("SELECT Price FROM Product WHERE IdProduct = @IdProduct", _connection);
            
        cmd2.Parameters.AddWithValue("IdProduct",IdProduct);
        
        decimal price =  Convert.ToDecimal(cmd2.ExecuteScalar())* product.Amount;
        
        cmd.Parameters.AddWithValue("Price", price);

        cmd.Parameters.AddWithValue("CreatedAt", DateTime.Now);
        
        int idProductWarehouse = Convert.ToInt32(cmd.ExecuteScalar());
        
        return Ok(idProductWarehouse);
    }
    
}