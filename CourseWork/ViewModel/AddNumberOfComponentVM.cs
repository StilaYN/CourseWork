using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModel;

public class AddNumberOfComponentVM:INotifyPropertyChanged
{
    private Visibility _visibile;
    private string _errorText;
    private AddNumberOfComponentWindow _window;
    private AddFunction _function;

    public delegate void AddFunction(int componentNumber);

    public AddNumberOfComponentVM(AddNumberOfComponentWindow window, AddFunction function)
    {
        _window = window;
        _function = function;
    }
    public Visibility Visible
    {
        get => _visibile;
        set { _visibile = value; OnPropertyChanged("Visible"); }
    }
    public string ErrorText
    {
        get => _errorText;
        set { _errorText = value; OnPropertyChanged("ErrorText"); }
    }
    public ICommand Enter => new Command(obj =>
    {
        try
        {
            _function(ComponentNumber);
            _window.Close();
        }
        catch (ArgumentException e)
        {
            Visible = Visibility.Visible;
            ErrorText = e.Message;
        }
    }, obj => { return ComponentNumber > 0;});
    public int ComponentNumber { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}