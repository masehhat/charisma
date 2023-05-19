using Charisma.Domain;

namespace Charisma.Application.ProductApplication.Models;

public record ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public Price Price { get; init; }
    public bool IsBreakable { get; init; }
}