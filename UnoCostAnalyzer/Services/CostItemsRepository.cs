namespace UnoCostAnalyzer.Services;

public record CostItemsRepository
{
    private const string ALL_TAG = "wszystko";

    private List<CostItem> _items =
    [
        new (Guid.NewGuid(), "Biedronka - Zakupy", 234.54m, ["zakupy", "biedronka"], DateTime.Now),
        new (Guid.NewGuid(), "Lidl - jabłka", 12.4m, ["zakupy", "lidl"],DateTime.Now),
        new (Guid.NewGuid(), "Rachunki za gaz", 123.21m, ["rachunki", "dom"], DateTime.Now),
        new (Guid.NewGuid(), "Prąd", 154.45m, ["rachunki"], DateTime.Now),
    ];

    private string _selectedTag = ALL_TAG;

    public IEnumerable<CostItem> Items => _items.Where(item => _selectedTag is null or ALL_TAG ? true : item.Tags.Contains(_selectedTag));
    public IEnumerable<string> Tags => _items.SelectMany(item => item.Tags).Distinct().Prepend(ALL_TAG);
    
    public CostItemsRepository AddItem(CostItem itemToAdd)
    {
        return this with { _items = [.. _items, itemToAdd] };
    }

    public CostItemsRepository EditItem(CostItem itemToEdit)
    {
        return this with { _items = [.. _items.Select(item => item.Id == itemToEdit.Id ? itemToEdit : item)] };
    }

    public CostItemsRepository RemoveItem(Guid itemId)
    {
        return this with { _items = [.. _items.Where(item => item.Id != itemId)] };
    }

    public CostItemsRepository ApplyFilter(string tag)
    {
        return this with { _selectedTag = tag };
    }
}
