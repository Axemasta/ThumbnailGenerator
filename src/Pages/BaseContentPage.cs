using CommunityToolkit.Mvvm.ComponentModel;

namespace ThumbnailGenerator.Pages;

public abstract class BaseContentPage<TViewModel>(TViewModel viewModel) : BaseContentPage(viewModel) where TViewModel : ObservableObject
{
    public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

public abstract class BaseContentPage : ContentPage
{
    protected BaseContentPage(object? viewModel = null)
    {
        BindingContext = viewModel;

        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }
    }
}