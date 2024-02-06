using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModel;

public class AddComponentVM:INotifyPropertyChanged
{
    private AddComponentWindow _window;
    private Visibility _visibile;
    private string _errorText;

    public delegate void AddFunction(string technnicType, string componentType, string name, string article, int price,
        int number = 1);
    private AddFunction _function;

    public AddComponentVM(AddComponentWindow window, AddFunction function)
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

    public string TechnicType { get; set; }
    public string ComponentType { get; set; }
    public string Name { get; set; }
    public string Article { get; set; }
    public int Price { get; set; }
    public int NumberOfComponent { get; set; }

    public ICommand Add => new Command(obj =>
        {
            try
            {
                _function(TechnicType, ComponentType, Name, Article, Price, NumberOfComponent);
                _window.Close();
            }
            catch (ArgumentException o)
            {
                Visible = Visibility.Visible;
                ErrorText = o.Message;
            }
        });
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}