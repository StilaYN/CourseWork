using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CourseWork.DataBase;
using IComponent = CourseWork.MainLogic.IComponent;

namespace CourseWork.ViewModel;
 
public class AddOperationVM:INotifyPropertyChanged
{
    private AddOperationWindow _window;
    private IHaveAccessToStore _source;
    private AddFunction _addFunction;
    private Visibility _visibile;
    private string _errorText;

    public delegate void AddFunction(string name, int day, int month, int year, IComponent? component, int price);

    public AddOperationVM(AddOperationWindow window, IHaveAccessToStore storageItems, AddFunction addFunction)
    {
        _window = window;
        _source = storageItems;
        _addFunction = addFunction;
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

    private IComponent? _usedComponent;
    public string Name { get; set; }
    public int Price { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public IComponent? UsedComponent
    {
        get => _usedComponent;
        set { _usedComponent = value; OnPropertyChanged("UsedComponent"); }
    }
    public ICommand Create => new Command(obj =>
    {
        try
        {
            _addFunction(Name, Day, Month, Year, UsedComponent, Price);
            _window.Close();
        }
        catch (ArgumentException e)
        {
            Visible = Visibility.Visible;
            ErrorText = e.Message;
        }
    });
    public ICommand AddComponent => new Command(obj =>
    {
        var comp = new SelectComponentWindow();
        comp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new SelectComponentVM(comp, _source);
        comp.DataContext = info;
        comp.ShowDialog();
        if (info.CanCreate)
        {
            UsedComponent = info.SelectedItem.Component;
        }
    });
    public ICommand DeleteComponent => new Command(obj =>
    {
        UsedComponent = null;
    },obj=>UsedComponent!=null);

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}