using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ThumbnailGenerator.Pages;
using ThumbnailGenerator.Services;
using ThumbnailGenerator.ViewModels;

namespace ThumbnailGenerator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // builder.Services.AddTransient<IThumbnailService, SpriteThumbnailService>();
        builder.Services.AddTransient<IThumbnailService, ThumbnailService>();
        builder.Services.AddTransientWithShellRoute<ThumbnailPage, ThumbnailViewModel>(nameof(ThumbnailPage));

        return builder.Build();
    }
}