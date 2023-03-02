using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using ToDo.App.Components.Validators;
using ToDo.App.Infrastructure.Services.Abstractions;

namespace ToDo.App.Components.Pages.DeleteToDoItemDialog;

public sealed class DeleteToDoItemDialogViewModel : ViewModelBase
{
    private const string EmptyMessage = "Guid must not be Guid.Empty";
    
    private readonly IToDoService _toDoService;
    
    private readonly FluentValueValidator<Guid> _validator = new(x => x
        .NotNull()
        .NotEqual(Guid.Empty)
        .WithMessage(EmptyMessage));
    
    [CascadingParameter]
    public MudDialogInstance DialogInstance { get; set; } = null!;
    
    [Parameter]
    public Guid ToDoItemId { get; set; }

    public DeleteToDoItemDialogViewModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }
    
    public Task Cancel()
    {
        DialogInstance.Cancel();
        return Task.CompletedTask;
    }
    
    public async Task DeleteToDoItem()
    {
        if (!await IsRequestValid())
        {
            return;
        }
        
        await _toDoService.DeleteToDoItem(ToDoItemId);
        DialogInstance.Close();
    }
    
    private async Task<bool> IsRequestValid()
        => (await _validator.ValidateAsync(ToDoItemId)).IsValid;
}