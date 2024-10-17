using System.Reactive;
using ReactiveUI;

namespace AvoTech.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> OpenRegisterCommand { get; }
    
    public LoginViewModel()
    {
        OpenRegisterCommand = ReactiveCommand.Create(Register);
    }
    
    private void Register()
    {
        // Todo 制作注册页面
    }
}