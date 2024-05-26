namespace Contracts;

public class Created : MessageBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}