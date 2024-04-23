using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RandomDog;

public partial class MainWindowViewModel : ObservableObject
{
    private DogService service;

    [ObservableProperty]
    private ImageSource image;

    public MainWindowViewModel(DogService service)
    {
        this.service = service;
    }

    [RelayCommand]
    private async void LoadImage()
    {
        Image = await service.LoadDog();
    }
}
