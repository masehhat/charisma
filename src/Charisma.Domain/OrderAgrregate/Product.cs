namespace Charisma.Domain;

public class Product
{
    private Product()
    {
    }

    //Id for seed data
    public Product(int id,string name, Price price, bool isBreakable)
    {
        Id = id;   
        Name = name;
        Price = price;
        IsBreakable = isBreakable;
    }

    public int Id { get; }

    private string _name;

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(Name));

            if (value.Length > 50)
                throw new ArgumentOutOfRangeException("name max allowed length is 50");

            _name = value;
        }
    }

    public Price Price { get; private set; }
    public bool IsBreakable { get; private set; }

    public void Modify(string name, Price price, bool isBreakable)
    {
        Name = name;
        Price = price;
        IsBreakable = isBreakable;
    }
}