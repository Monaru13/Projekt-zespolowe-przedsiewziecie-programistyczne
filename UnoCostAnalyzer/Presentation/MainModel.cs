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

    public IState<string> Name => State<string>.Value(this, () => string.Empty);
    public IState<CostItemsRepository> CostItems => State.Value(this, () => new CostItemsRepository());

    public ValueTask AddItemTest() => CostItems.UpdateAsync(c => c?.AddItem(new(Guid.NewGuid(), "test", 100, DateTime.Now)));

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
    }

    public async ValueTask GoToAddItem()
    {
        var result = await _navigator.GetDataAsync<SecondModel, CostItem>(this);

        if (result is not null)
        {
            await CostItems.UpdateAsync(c => c?.AddItem(result));
        }
    }
}
