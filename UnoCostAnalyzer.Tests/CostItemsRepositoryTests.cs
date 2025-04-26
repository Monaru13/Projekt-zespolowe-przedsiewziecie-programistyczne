namespace UnoCostAnalyzer.Tests;

[TestFixture]
public class CostItemsRepositoryTests
{
    private CostItemsRepository _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new CostItemsRepository();
    }

    [Test]
    public void Items_ShouldReturnAllItems_WhenNoFilterApplied()
    {
        _repository.Items.Should().HaveCount(4);
    }

    [Test]
    public void Tags_ShouldReturnAllDistinctTagsWithAllTagPrepended()
    {
        var tags = _repository.Tags.ToList();

        tags.Should().StartWith("wszystko");
        tags.Should().Contain(new[] { "zakupy", "biedronka", "lidl", "rachunki", "dom" });
    }

    [Test]
    public void Total_ShouldReturnSumOfAllCosts()
    {
        var expectedTotal = _repository.Items.Sum(x => x.Cost ?? 0);
        _repository.Total.Should().BeApproximately(expectedTotal, 0.01m);
    }

    [Test]
    public void AddItem_ShouldAddNewItem()
    {
        var newItem = new CostItem(Guid.NewGuid(), "Nowy zakup", 50m, new[] { "zakupy" }, DateTime.Now);

        var updatedRepo = _repository.AddItem(newItem);

        updatedRepo.Items.Should().ContainSingle(item => item.Id == newItem.Id);
        updatedRepo.Items.Should().HaveCount(5);
        _repository.Items.Should().HaveCount(4); // oryginalny repo niezmieniony (immutable)
    }

    [Test]
    public void EditItem_ShouldUpdateExistingItem()
    {
        var itemToEdit = _repository.Items.First();
        var editedItem = itemToEdit with { Description = "Edytowany opis", Cost = 999m };

        var updatedRepo = _repository.EditItem(editedItem);

        updatedRepo.Items.Should().ContainSingle(item => item.Id == editedItem.Id && item.Description == "Edytowany opis" && item.Cost == 999m);
        _repository.Items.Should().ContainSingle(item => item.Id == itemToEdit.Id && item.Description == itemToEdit.Description); // oryginalny repo niezmieniony
    }

    [Test]
    public void RemoveItem_ShouldDeleteItem()
    {
        var itemIdToRemove = _repository.Items.First().Id;

        var updatedRepo = _repository.RemoveItem(itemIdToRemove);

        updatedRepo.Items.Should().NotContain(item => item.Id == itemIdToRemove);
        updatedRepo.Items.Should().HaveCount(3);
    }

    [Test]
    public void ApplyFilter_ShouldReturnOnlyItemsWithGivenTag()
    {
        var updatedRepo = _repository.ApplyFilter("biedronka");

        updatedRepo.Items.Should().OnlyContain(item => item.Tags.Contains("biedronka"));
    }

    [Test]
    public void ApplyFilter_ShouldReturnNoItems_WhenTagDoesNotExist()
    {
        var updatedRepo = _repository.ApplyFilter("nieistniejący_tag");

        updatedRepo.Items.Should().BeEmpty();
    }

    [Test]
    public void ApplyFilter_ShouldReturnAllItems_WhenAllTagSelected()
    {
        var filteredRepo = _repository.ApplyFilter("biedronka"); // najpierw filtr
        var resetRepo = filteredRepo.ApplyFilter("wszystko"); // potem "wszystko"

        resetRepo.Items.Should().HaveCount(4);
    }
}
