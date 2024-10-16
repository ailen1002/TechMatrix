using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvoTech.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
    }
    private void Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}