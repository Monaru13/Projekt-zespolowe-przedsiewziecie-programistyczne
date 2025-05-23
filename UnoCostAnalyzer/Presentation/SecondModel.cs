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
        if (!await IsCostValid(EditableItem.Cost) || !await IsDescriptionValid(EditableItem.Description))
        {
            return;
        }

        await _navigator.NavigateBackWithResultAsync(this, data: EditableItem with
        {
            Tags = Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            CreatedAt = DateTime.Now
        });
    }

    public async Task CancelCommand()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private async Task<bool> IsCostValid(decimal? cost)
    {
        if (cost is null)
        {
            await Errors.UpdateAsync(static e => new ValidationErrors { CostErrorMessage = "Wartość jest wymagana." });
            return false;
        }

        if (cost is < 0)
        {
            await Errors.UpdateAsync(e => new ValidationErrors { CostErrorMessage = "Wartość nie może być ujemna." });
            return false;
        }

        return true;
    }

    private async Task<bool> IsDescriptionValid(string? description)
    {
        if (description is null)
        {
            await Errors.UpdateAsync(static e => new ValidationErrors { DescriptionErrorMessage = "Wartość jest wymagana." });
            return false;
        }

        return true;
    }

    public class ValidationErrors
    {
        public string? CostErrorMessage { get; set; }
        public string? DescriptionErrorMessage { get; set; }
    }
}
