using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using ToDo.App.Models;
using ToDo.App.Services.Abstractions;
using ToDo.App.Validators;

namespace ToDo.App.Pages.EditToDoItemDialog;

public sealed class EditToDoItemDialogViewModel : ViewModelBase
{
    private const string InvalidLengthMessage = "Text must be at least 5 and maximum 255 characters long";
    private const string EmptyMessage = "Text must not be null or empty";
    
    private readonly IToDoService _toDoService;
    
    private readonly FluentValueValidator<string> _validator = new(x => x
        .NotNull()
        .NotEmpty()
        .WithMessage(EmptyMessage)
        .Length(5, 255)
        .WithMessage(InvalidLengthMessage));
    
    public FluentValueValidator<string> Validator => _validator;

    [Parameter]
    public ToDoItemModel Model { get; set; } = null!;
    
    [CascadingParameter]
    public MudDialogInstance DialogInstance { get; set; } = null!;

    public EditToDoItemDialogViewModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }
    
    public Task Cancel()
    {
        DialogInstance.Cancel();
        return Task.CompletedTask;
    }
    
    public async Task UpdateToDoItemText()
    {
        if (await IsRequestValid())
        {
            await _toDoService.UpdateToDoItemText(Model.Id, Model.Text);
            DialogInstance.Close();
        }
    }

    private async Task<bool> IsRequestValid()
        => (await _validator.ValidateAsync(Model.Text)).IsValid;
}