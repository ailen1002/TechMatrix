using ReactiveUI;

namespace AvoTech.ViewModels;

public partial class MainViewModel : ViewModelBase
{ 
    private string _greeting = "Welcome to Avalonia!";

    public string Greeting
    {
        get => _greeting;
        set => this.RaiseAndSetIfChanged(ref _greeting, value);
    }
}