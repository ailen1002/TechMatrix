using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using AvoTech.Interfaces;
using AvoTech.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using ReactiveUI;

namespace AvoTech.ViewModels;

public class RegisterViewModel : ReactiveObject
{
    private readonly IUserService _userService; // 注入的 IUserService 实例
    private string _userName = string.Empty;
    private string _passWord = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _hasError;
    
    public string UserName
    {
        get => _userName;
        set => this.RaiseAndSetIfChanged(ref _userName, value);
    }

    public string PassWord
    {
        get => _passWord;
        set => this.RaiseAndSetIfChanged(ref _passWord, value);
    }
    
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
    }
    
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool HasError
    {
        get => _hasError;
        set => this.RaiseAndSetIfChanged(ref _hasError, value);
    }
    
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
    public RegisterViewModel(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        RegisterCommand = ReactiveCommand.CreateFromTask(RegisterAsync);
    }

    private async Task RegisterAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
        {
            OpenDialog(false,"请输入用户名密码");
            return;
        }

        if (PassWord != ConfirmPassword)
        {
            OpenDialog(false,"密码不一致");
            return;
        }

        HasError = false;
        ErrorMessage = string.Empty;

        // 创建新用户并添加到数据库
        var newUser = new User
        {
            UserName = UserName,
            PassWordHash = HashPassword(PassWord)
        };

        await _userService.AddUserAsync(newUser);
        OpenDialog(true,"用户注册成功");
    }

    private static void OpenDialog(bool state ,string message)
    {
        MessageBoxManager.GetMessageBoxCustom(new MessageBoxCustomParams
        {
            ButtonDefinitions = new List<ButtonDefinition>
            {
                new() { Name = "Yes", },
            },
            ContentTitle = "title",
            ContentMessage = message,
            Icon = state?MsBox.Avalonia.Enums.Icon.Success:MsBox.Avalonia.Enums.Icon.Warning,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false,
            Width = 250,
            Height = 150,
        }).ShowAsync();
    }
    private static string HashPassword(string password)
    {
        // 生成盐并哈希密码
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    private async Task UpdateErrorMessageAsync(string message)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            ErrorMessage = message;
            HasError = true;
        });
    }
}