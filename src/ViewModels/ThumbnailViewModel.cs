
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ThumbnailGenerator.Abstractions;

namespace ThumbnailGenerator.ViewModels;

public partial class ThumbnailViewModel(IThumbnailService thumbnailService) : ObservableObject
{
    private readonly IThumbnailService thumbnailService = thumbnailService;
    
    [ObservableProperty]
    private FileResult? video;

    [ObservableProperty]
    private ImageSource thumbnailImageSource;
    
    [RelayCommand]
    private async Task LoadVideo()
    {
        var fileResult = await MediaPicker.PickVideoAsync();

        if (fileResult is null)
        {
            await Shell.Current.DisplayAlert("Error", "Please select a video to load", "Ok");
            return;
        }

        Video = fileResult;
    }

    [RelayCommand]
    private async Task LoadThumbnail()
    {
        if (Video is null)
        {
            await Shell.Current.DisplayAlert("No Video Selected", "You need to select a video before you can generate a thumbnail", "Ok");
            return;
        }

        var generateResult = await thumbnailService.GenerateThumbnail(Video.FullPath);

        if (generateResult.IsOperationFailed)
        {
            await Shell.Current.DisplayAlert("Unable to generate thumbnail", generateResult.OperationFailed.Exception.ToString(), "Ok");
            return;
        }
        
        ThumbnailImageSource = ImageSource.FromStream(() => new MemoryStream(generateResult.ThumbnailGenerated.ThumbnailBytes));
    }
}