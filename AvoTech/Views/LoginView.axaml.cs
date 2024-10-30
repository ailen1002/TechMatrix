using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using AvoTech.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AvoTech.Views;

public partial class LoginView : Window
{
    private readonly IServiceProvider _serviceProvider;
    public LoginView(IServiceProvider serviceProvider, LoginViewModel loginViewModel)
    {
        _serviceProvider = serviceProvider;
        DataContext = loginViewModel;
        InitializeComponent();
    }
    
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    private void OnRegisterPressed(object sender, PointerPressedEventArgs e)
    {
        var registerView = _serviceProvider.GetRequiredService<RegisterView>();
        registerView.ShowDialog(this);
    }
}