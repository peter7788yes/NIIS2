public interface IHelloService
{
    string Hello(string name);
}

public class HelloService : IHelloService
{
    public string Hello(string name)
    {
        return "Hello, " + name;
    }
}