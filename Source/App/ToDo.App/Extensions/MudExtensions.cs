using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToDo.App.Extensions;

public static class MudExtensions
{
    public static async Task<DialogResult> ShowDialogAsync<T>(this IDialogService dialogService, string title, DialogParameters parameters) where T : ComponentBase
    {
        IDialogReference? dialogReference = await dialogService.ShowAsync<T>(title, parameters);
        return await dialogReference.Result;
    }
}