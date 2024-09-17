using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using ThumbnailGenerator.ViewModels;

namespace ThumbnailGenerator.Pages;

public partial class ThumbnailPage : BaseContentPage<ThumbnailViewModel>
{
    public ThumbnailPage(ThumbnailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
        
        viewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(BindingContext.Video) && BindingContext.Video is not null)
        {
            var source = MediaSource.FromFile(BindingContext.Video.FullPath);

            MediaElement.Source = source;
            MediaElement.HeightRequest = 300;
        }
    }

    private void OnMediaOpened(object? sender, EventArgs e)
    {
        Debug.WriteLine("MediaElement - Media Opened!");
    }

    private void OnMediaFailed(object? sender, MediaFailedEventArgs e)
    {
        Debug.WriteLine($"MediaElement - OnMediaFailed: {e.ErrorMessage}");
    }
}