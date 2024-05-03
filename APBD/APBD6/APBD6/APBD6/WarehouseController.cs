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
        var IdProduct = product.IdProduct;
        var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM product " +
                                    "WHERE IdProduct=@IdProduct", _connection);
        cmd.Parameters.AddWithValue("IdProduct", IdProduct);
        int countProduct = Convert.ToInt32(cmd.ExecuteScalar());
        
        if (!(countProduct > 0))
        {
            return BadRequest("Product does not exist.");
        }
        
        var IdWarehouse = product.IdWarehouse;
        cmd = new NpgsqlCommand("SELECT COUNT(*) FROM warehouse " +
                                    "WHERE IdWarehouse = @IdWarehouse", _connection);
        cmd.Parameters.AddWithValue("IdProduct", IdProduct);
        int countWarehouse = Convert.ToInt32(cmd.ExecuteScalar());
        
        if (!(countWarehouse > 0))
        {
            return BadRequest("Warehouse does not exist.");
        }

        return Ok(product);
    }
}