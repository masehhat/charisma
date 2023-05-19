using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charisma.Application.Common.Interfaces;
using Charisma.Application.ProductApplication.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Application.ProductApplication.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductDto[]>
{
    private const string _cacheKey = "All_Products";

    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly ICharismaDbContext _context;

    public GetProductsQueryHandler(IMemoryCache memoryCache, ICharismaDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<ProductDto[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        bool hasCachedData = _memoryCache.TryGetValue(_cacheKey, out ProductDto[] allProducts);

        if (!hasCachedData)
        {
            allProducts = await _context.Products
                .AsNoTracking()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            _memoryCache.Set(_cacheKey, allProducts, TimeSpan.FromMinutes(30));
        }

        return request.ProductIds.Any() ? allProducts.Where(x=> request.ProductIds.Contains(x.Id)).ToArray() : allProducts;
    }
}
