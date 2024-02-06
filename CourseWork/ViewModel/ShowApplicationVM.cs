using CourseWork.DataBase;
using CourseWork.MainLogic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.Generic;

namespace CourseWork.ViewModel;

public class ShowApplicationVM:INotifyPropertyChanged
{
    private ShowApplicationWindow _window;

    public delegate List<IApplication> GetListFunction();
    public delegate List<IApplication>? FindFunction(string name);
    private GetListFunction _listFunction;
    private FindFunction _findFunction;
    private ObservableCollection<IApplication> _application;
    private Visibility _visible;
    private string _errorText;
    private string? _search;
    public ShowApplicationVM(ShowApplicationWindow window, GetListFunction listFunction, FindFunction findFunction)
    {
        _window = window;
        _listFunction = listFunction;
        _findFunction = findFunction;
        Application = new ObservableCollection<IApplication>(listFunction());
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

    public ObservableCollection<IApplication> Application
    {
        get => _application;
        set { _application = value; OnPropertyChanged("Application"); }
    }
    public IApplication? SelectedApplication { get; set; }
    public ICommand Find => new Command(obj =>
    {
        try
        {
            Application = new ObservableCollection<IApplication>(_findFunction(Search));
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
        Application = new ObservableCollection<IApplication>(_listFunction());
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