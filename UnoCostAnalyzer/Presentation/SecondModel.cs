namespace UnoCostAnalyzer.Presentation;

public partial record SecondModel
{
    private readonly INavigator _navigator;
    public CostItem EditableItem { get; set; }
    public string Tags { get; set; }

    public SecondModel(INavigator navigator, CostItem item)
    {
        _navigator = navigator;

        EditableItem = item ?? new();
        Tags = string.Join(" ", EditableItem.Tags);
    }

    public async Task OkCommand()
    {
        await _navigator.NavigateBackWithResultAsync(this, data: EditableItem with { Tags = Tags.Split(' ')});
    }

    public async Task CancelCommand()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
