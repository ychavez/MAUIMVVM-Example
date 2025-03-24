using MediatR;
using Microsoft.Extensions.Logging;
using MiToDoListApp.Applicationx.Behaviors;
using MiToDoListApp.Applicationx.Services.Abstractions;
using MiToDoListApp.Mobile.Services;
using MiToDoListApp.Mobile.ViewModels;

namespace MiToDoListApp.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<INavigationService, NavigationService>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(MauiApp).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            builder.Services
              .AddTransient<TaskListViewModel>();
            builder.Services
              .AddTransient<AddTodoPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
