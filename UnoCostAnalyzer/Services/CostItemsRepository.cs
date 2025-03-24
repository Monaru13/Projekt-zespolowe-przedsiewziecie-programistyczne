using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoCostAnalyzer.Services;

public record CostItemsRepository
{
    private List<CostItem> _items =
    [
        new (Guid.NewGuid(), "Biedronka - Zakupy", 234.54m, DateTime.Now),
        new (Guid.NewGuid(), "Lidl - jabłka", 12.4m, DateTime.Now),
        new (Guid.NewGuid(), "Rachunki za gaz", 123.21m, DateTime.Now),
        new (Guid.NewGuid(), "Prąd", 154.45m, DateTime.Now),
    ];

    public IEnumerable<CostItem> Items => _items; // todo dodać filtracje

    public CostItemsRepository AddItem(CostItem item)
    {
        return this with { _items = [.. _items, item] };
    }
}
