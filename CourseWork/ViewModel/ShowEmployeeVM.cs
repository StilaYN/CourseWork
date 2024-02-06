using CourseWork.MainLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;

namespace CourseWork.ViewModel;

public class ShowEmployeeVM:INotifyPropertyChanged
{
    private ShowEmployeeWindow _window;

    public delegate List<IEmployeePersonalData> GetListFunction();
    public delegate List<IEmployeePersonalData>? FindFunction(string name);
    private GetListFunction _listFunction;
    private FindFunction _findFunction;
    private ObservableCollection<IEmployeePersonalData> _employee;
    private Visibility _visible;
    private string _errorText;
    private string? _search;
    public ShowEmployeeVM(ShowEmployeeWindow window, GetListFunction listFunction, FindFunction findFunction)
    {
        _window = window;
        _listFunction = listFunction;
        _findFunction = findFunction;
        Employee = new ObservableCollection<IEmployeePersonalData>(listFunction());
    }


    public string? Search
    {
        get => _search;
        set { _search = value; OnPropertyChanged("Search"); }
    }

    public Visibility Visible
    {
        get => _visible;
        set { _visible = value; OnPropertyChanged("Visible"); }
    }
    public string ErrorText
    {
        get => _errorText;
        set { _errorText = value; OnPropertyChanged("ErrorText"); }
    }

    public ObservableCollection<IEmployeePersonalData> Employee
    {
        get => _employee;
        set { _employee = value; OnPropertyChanged("Employee"); }
    }
    public ICommand Find => new Command(obj =>
    {
        try
        {
            Employee = new ObservableCollection<IEmployeePersonalData>(_findFunction(Search));
            Visible = Visibility.Hidden;
        }
        catch (ArgumentException e)
        {
            Visible = Visibility.Visible;
            ErrorText = e.Message;
        }
    }, obj => !string.IsNullOrEmpty(Search));
    public ICommand Clear => new Command(obj =>
    {
        Employee = new ObservableCollection<IEmployeePersonalData>(_listFunction());
        Visible = Visibility.Hidden;
        Search = null;

    }, obj => !string.IsNullOrEmpty(Search));

    public ICommand Close => new Command(obj =>
    {
        _window.Close();
    });
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}