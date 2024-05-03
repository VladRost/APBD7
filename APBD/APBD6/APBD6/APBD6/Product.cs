namespace APBD6;

public class Product
{
    public Product(int idProduct, int idWarehouse, int amount, string createdAt)
    {
        IdProduct = idProduct;
        IdWarehouse = idWarehouse;
        Amount = amount;
        CreatedAt = createdAt;
    }

    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    public string CreatedAt { get; set; }
}