using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using ToDo.App.Services.Abstractions;
using ToDo.App.Validators;

namespace ToDo.App.Pages.CreateToDoItemDialog;

public sealed class CreateToDoItemDialogViewModel : ViewModelBase
{
    private const string InvalidLengthMessage = "Text must be at least 5 and maximum 255 characters long";
    private const string EmptyMessage = "Text must not be empty";

    private readonly IToDoService _toDoService;
    private readonly FluentValueValidator<string> _validator = new(x => x
        .NotNull()
        .NotEmpty()
        .WithMessage(EmptyMessage)
        .Length(5, 255)
        .WithMessage(InvalidLengthMessage));

    public FluentValueValidator<string> Validator => _validator;

    [CascadingParameter]
    public MudDialogInstance DialogInstance { get; set; } = null!;

    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set => Set(ref _text, value);
    }

    public CreateToDoItemDialogViewModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    public Task Cancel()
    {
        DialogInstance.Cancel();
        return Task.CompletedTask;
    }

    public async Task CreateToDoItem()
    {
        if (!await IsRequestValid())
        {
            return;
        }
        
        await _toDoService.CreateToDoItems(Text);
        DialogInstance.Close();
    }
    
    private async Task<bool> IsRequestValid()
        => (await _validator.ValidateAsync(Text)).IsValid;
}

