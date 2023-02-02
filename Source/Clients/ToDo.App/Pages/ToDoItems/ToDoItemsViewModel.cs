using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using ToDo.App.Models;
using ToDo.App.Pages.CreateToDoItemDialog;
using ToDo.App.Services.Abstractions;

namespace ToDo.App.Pages.ToDoItems;

public sealed class ToDoItemsViewModel : ViewModelBase
{
    private const string CREATE_DIALOG_TITLE = "Create ToDo";
    private const string EDIT_DIALOG_TITLE = "Edit ToDo";
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

    [Parameter]
    public IDialogService DialogService { get; set; } = null!;

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
        bool isCanceled = await ShowDialog(CREATE_DIALOG_TITLE, new() { ["IsCreate"] = true });
        if (!isCanceled)
        {
            ToDoItems = await _toDoService.GetToDoItems(1, 100);
        }
    }

    public async Task EditToDoItem()
    {
        bool isCanceled = await ShowDialog(CREATE_DIALOG_TITLE, new() { ["IsCreate"] = false });
        if (!isCanceled)
        {
            ToDoItems = await _toDoService.GetToDoItems(1, 100);
        }
    }

    private async Task<bool> ShowDialog(string title, DialogParameters parameters)
    {
        IDialogReference dialog = await DialogService.ShowAsync<CreateOrEditToDoItemDialogView>(title, parameters);
        DialogResult dialogResult = await dialog.Result;
        return dialogResult.Canceled;
    }
}