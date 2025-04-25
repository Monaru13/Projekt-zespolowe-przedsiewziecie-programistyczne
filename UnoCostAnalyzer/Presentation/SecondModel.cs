namespace UnoCostAnalyzer.Presentation;

public partial record SecondModel
{
    private readonly INavigator _navigator;
    public CostItem EditableItem { get; set; }
    public string Tags { get; set; }
    public string? Title { get; }

    public IState<ValidationErrors> Errors => State.Value(this, () => new ValidationErrors());

    public SecondModel(
        INavigator navigator,
        IStringLocalizer localizer,
        CostItem item)
    {
        _navigator = navigator;
        Title = $"{localizer["EditPageTitle"]}";

        EditableItem = item ?? new();
        Tags = string.Join(" ", EditableItem.Tags);
    }

    public async Task OkCommand()
    {
        if (!await IsCostValid(EditableItem.Cost))
        {
            return;
        }

        await _navigator.NavigateBackWithResultAsync(this, data: EditableItem with
        {
            Tags = Tags.Split(' '),
            CreatedAt = DateTime.Now
        });
    }

    public async Task CancelCommand()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private async Task<bool> IsCostValid(decimal? cost)
    {
        if (cost is < 0)
        {
            await Errors.UpdateAsync(e => new ValidationErrors { CostErrorMessage = "Watrość nie może być ujemna" });

            return false;
        }

        return true;
    }

    public class ValidationErrors
    {
        public string? CostErrorMessage { get; set; }
    }
}
