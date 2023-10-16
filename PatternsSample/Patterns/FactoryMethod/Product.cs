namespace PatternsSample.Patterns.FactoryMethod;

public abstract class Product
{
    public abstract void PostConstruction();
}

public class SampleProduct : Product
{
    public override void PostConstruction()
    {
        Console.WriteLine("Product1 was created!!!");
    }
}

public class TestProduct : Product
{
    public override void PostConstruction()
    {
        Console.WriteLine("Product2 was created!!!");
    }
}

public static class ProductFactory
{
    public static T Create<T>() where T : Product, new()
    {
        var product = new T();
        product.PostConstruction();

        return product;
    }
}