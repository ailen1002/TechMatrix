using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using AvoTech.ViewModels;

namespace AvoTech.Views;

public partial class RegisterView : Window
{
    public RegisterView(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        DataContext = registerViewModel;
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
    
    private void OnLoginPressed(object sender, PointerPressedEventArgs e)
    {
        this.Close();
    }
}