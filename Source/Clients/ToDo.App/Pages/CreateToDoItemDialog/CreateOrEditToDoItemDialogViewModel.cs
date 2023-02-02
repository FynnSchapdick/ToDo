using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using ToDo.App.Services.Abstractions;
using ToDo.App.Validators;

namespace ToDo.App.Pages.CreateToDoItemDialog;

public sealed class CreateOrEditToDoItemDialogViewModel : ViewModelBase
{
    private const string INVALID_LENGTH_MESSAGE = "Text must be at least 5 and maximum 255 characters long";
    private const string EMPTY_MESSAGE = "Text must not be empty";

    private readonly IToDoService _toDoService;
    private readonly FluentValueValidator<string> _validator = new(x => x
        .NotNull()
        .NotEmpty()
        .WithMessage(EMPTY_MESSAGE)
        .Length(5, 255)
        .WithMessage(INVALID_LENGTH_MESSAGE));

    public FluentValueValidator<string> Validator => _validator;

    [CascadingParameter]
    public MudDialogInstance DialogInstance { get; set; } = null!;

    [Parameter]
    public bool IsCreate { get; set; }

    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set => Set(ref _text, value);
    }

    public CreateOrEditToDoItemDialogViewModel(IToDoService toDoService)
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
        ValidationResult validationResult = await _validator.ValidateAsync(Text);
        if (validationResult.IsValid)
        {
            await _toDoService.CreateToDoItems(Text);
            DialogInstance.Close();
        }
    }
}

