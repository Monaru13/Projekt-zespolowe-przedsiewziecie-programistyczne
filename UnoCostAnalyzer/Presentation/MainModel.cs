using UnoCostAnalyzer.Services;

namespace UnoCostAnalyzer.Presentation;

public partial record MainModel
{
    private INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;

        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IState<CostItemsRepository> CostItems => State.Value(this, () => new CostItemsRepository());

    public async ValueTask GoToEditItem(CostItem item)
    {
        var result = await _navigator.GetDataAsync<SecondModel, CostItem>(this, data: item);

    public async ValueTask GoToAddItem()
    {
        var result = await _navigator.GetDataAsync<SecondModel, CostItem>(this);

        if (result is not null)
        {
            await CostItems.UpdateAsync(c => c?.AddItem(result));
        }
    }
}
