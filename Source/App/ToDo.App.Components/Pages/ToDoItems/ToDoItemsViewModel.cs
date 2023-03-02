using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using ToDo.App.Components.Pages.CreateToDoItemDialog;
using ToDo.App.Components.Pages.DeleteToDoItemDialog;
using ToDo.App.Components.Pages.EditToDoItemDialog;
using ToDo.App.Core;
using ToDo.App.Infrastructure.Services.Abstractions;

namespace ToDo.App.Components.Pages.ToDoItems;

public sealed class ToDoItemsViewModel : ViewModelBase
{
    private const string CreateDialogTitle = "Create ToDo";
    private const string DeleteDialogTitle = "Delete ToDo";
    private const string EditDialogTitle = "Edit ToDo";
    private readonly IToDoService _toDoService;
    private IEnumerable<ToDoItemModel> _toDoItems = new List<ToDoItemModel>();

    public IEnumerable<ToDoItemModel> ToDoItems
    {
        get => _toDoItems;
        private set => Set(ref _toDoItems, value);
    }

    private string _searchString = string.Empty;

    public string SearchString
    {
        get => _searchString;
        set => Set(ref _searchString, value);
    }

    private ToDoItemModel _selectedItem;

    public ToDoItemModel SelectedItem
    {
        get => _selectedItem;
        set => Set(ref _selectedItem, value);
    }

    [Parameter] public IDialogService DialogService { get; set; } = null!;

    public ToDoItemsViewModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    public override async Task OnInitializedAsync()
    {
        ToDoItems = await _toDoService.GetToDoItems(1, 100);
        await base.OnInitializedAsync();
    }

    public async Task CreateToDoItem()
    {
        if (await DialogSuccess<CreateToDoItemDialogView>(CreateDialogTitle, new DialogParameters()))
        {
            ToDoItems = await _toDoService.GetToDoItems(1, 100);
        }
    }

    public async Task EditToDoItem()
    {
        if (await DialogSuccess<EditToDoItemDialogView>(EditDialogTitle, new DialogParameters {["Model"] = SelectedItem }))
        {
            ToDoItems = await _toDoService.GetToDoItems(1, 100);
        }
    }

    public async Task DeleteToDoItem()
    {
        if (await DialogSuccess<DeleteToDoItemDialogView>(DeleteDialogTitle, new DialogParameters
            {
                ["ToDoItemId"] = SelectedItem.Id
            }))
        {
            ToDoItems = await _toDoService.GetToDoItems(1, 100);
        }
    }

    private async Task<bool> DialogSuccess<T>(string title, DialogParameters parameters) where T : ComponentBase
        => !(await (await DialogService.ShowAsync<T>(title, parameters)).Result)
            .Canceled;
}