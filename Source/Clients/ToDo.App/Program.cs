using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using ToDo.App;
using ToDo.App.Pages.CreateToDoItemDialog;
using ToDo.App.Pages.DeleteToDoItemDialog;
using ToDo.App.Pages.EditToDoItemDialog;
using ToDo.App.Pages.ToDoItems;
using ToDo.App.Services;
using ToDo.App.Services.Abstractions;
using ToDo.Framework.ToDoClient.Abstractions;
using ToDo.Framework.ToDoClient.Validators;

const string apiBaseUrl = "http://localhost:5201";
const string app = "#app";
const string headOutlet = "head::after";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>(app);
builder.RootComponents.Add<HeadOutlet>(headOutlet);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddMvvm();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateToDoItemRequestValidator>();
builder.Services.AddRefitClient<IToDoClient>(_ => new RefitSettings())
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddTransient<ToDoItemsViewModel>();
builder.Services.AddTransient<CreateToDoItemDialogViewModel>();
builder.Services.AddTransient<DeleteToDoItemDialogViewModel>();
builder.Services.AddTransient<EditToDoItemDialogViewModel>();

await builder.Build().RunAsync();
