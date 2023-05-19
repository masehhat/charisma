using Charisma.Application.ProductApplication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Application.ProductApplication.Queries.GetProducts;

public class GetProductsQuery : IRequest<ProductDto[]>
{
    public int[] ProductIds { get; set; } = Array.Empty<int>();
}
