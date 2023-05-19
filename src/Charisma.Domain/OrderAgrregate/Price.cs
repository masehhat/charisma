using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Domain;

public record Price
{
    public Price(decimal value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("price value should be greater than -1");

        Value = value;
    }
    public decimal Value { get; }


    public static Price Zero => new(0);

    public static Price operator +(Price p1, Price p2)
    {
        if (p1 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 is null)
            throw new ArgumentNullException(nameof(p1));

        Price price = new(p1.Value + p2.Value);
        return price;
    }

    public static Price operator -(Price p1, Price p2)
    {
        if (p1 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2.Value > p1.Value)
            throw new ArgumentOutOfRangeException("price value should be greater than -1");

        Price price = new(p1.Value - p2.Value);
        return price;
    }
}
