using CourseWork.MainLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using CourseWork.DataBase;

namespace CourseWork.ViewModel;

public class ShowOperationVM:INotifyPropertyChanged
{
    private ShowOpeationWindow _window;

    public delegate List<IOperationItem> GetListFunction();
    public delegate List<IOperationItem>? FindFunction(string name);
    private GetListFunction _listFunction;
    private FindFunction _findFunction;
    private ObservableCollection<IOperationItem> _operation;
    private Visibility _visible;
    private string _errorText;
    private string? _search;
    public ShowOperationVM(ShowOpeationWindow window, GetListFunction listFunction, FindFunction findFunction)
    {
        _window = window;
        _listFunction = listFunction;
        _findFunction = findFunction;
        Operation = new ObservableCollection<IOperationItem>(listFunction());
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

    public ObservableCollection<IOperationItem> Operation
    {
        get => _operation;
        set { _operation = value; OnPropertyChanged("Operation"); }
    }
    public ICommand Find => new Command(obj =>
    {
        try
        {
            Operation = new ObservableCollection<IOperationItem>(_findFunction(Search));
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
        Operation = new ObservableCollection<IOperationItem>(_listFunction());
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