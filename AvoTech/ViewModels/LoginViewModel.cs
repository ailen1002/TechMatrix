using System.Reactive;
using AvoTech.Interfaces;
using ReactiveUI;

namespace AvoTech.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly IUserService _userService;
    public ReactiveCommand<Unit, Unit> OpenRegisterCommand { get; }
    
    public LoginViewModel(IUserService userService)
    {
        _userService = userService;
        OpenRegisterCommand = ReactiveCommand.Create(Login);
    }
    
    private static void Login()
    {
        // Todo 制作注册页面
    }
}