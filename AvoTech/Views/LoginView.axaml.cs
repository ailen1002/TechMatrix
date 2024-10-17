using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvoTech.ViewModels;

namespace AvoTech.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
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
        var registerWindow = new RegisterView();
        
        registerWindow.ShowDialog(this);
    }
}