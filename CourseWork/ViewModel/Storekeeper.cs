using CourseWork.DataBase;
using CourseWork.MainLogic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Component = CourseWork.MainLogic.Component;

namespace CourseWork.ViewModel;

public class Storekeeper: IWorkProfile, INotifyPropertyChanged
{
    private string _name;
    private IStorekeeperAccessProfile _source;
    private ObservableCollection<IStorageItem> _storage;
    private Visibility _visibile;
    private string _errorText;
    private string? _search;
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public string? Search
    {
        get => _search;
        set { _search = value; OnPropertyChanged("Search"); }
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

    public Storekeeper(string name, IStorekeeperAccessProfile source)
    {
        _name = name;
        _source = source;
        _source.PropertyChanged += SourcePropertyChangedEventHandler;
    }

    private void AddNewComponentMethod(string technicType, string componentType, string name, string article,
        int price, int number = 1)
    {
        _source.AddComponent(technicType, componentType, name, article, price, number);
    }

    private void AddSelectedComponentMethod(int componentNumber)
    {
        _source.AddComponent(SelectedItem.Component, componentNumber);
    }

    private void RemoveComponentMethod(int componentNumber)
    {
        _source.RemoveComponent(SelectedItem.Component, componentNumber);
    }
    public ICommand Find => new Command(obj =>
    {
        try
        {
            Storage = new ObservableCollection<IStorageItem>(_source.FindItem(Search));
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
        Storage = new ObservableCollection<IStorageItem>(_source.Storage);
        Visible = Visibility.Hidden;
        Search = null;

    }, obj => !string.IsNullOrEmpty(Search));

    public ICommand AddNewComponent => new Command(obj =>
    {
        var enterWindow = new AddComponentWindow();
        enterWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new AddComponentVM(enterWindow,AddNewComponentMethod);
        enterWindow.DataContext = info;
        enterWindow.ShowDialog();
        Visible = Visibility.Hidden;
        Search = null;
    });

    public ICommand AddSelectedComponent => new Command(o =>
    {
        var enterWindow = new AddNumberOfComponentWindow();
        enterWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new AddNumberOfComponentVM(enterWindow,AddSelectedComponentMethod);
        enterWindow.DataContext = info;
        enterWindow.ShowDialog();
        Visible = Visibility.Hidden;
        Search = null;
    }, o => { return SelectedItem != null; });

    public ICommand RemoveComponent => new Command(obj =>
    {
        var enterWindow = new AddNumberOfComponentWindow();
        enterWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new AddNumberOfComponentVM(enterWindow,RemoveComponentMethod);
        enterWindow.DataContext = info;
        enterWindow.ShowDialog();
        Visible = Visibility.Hidden;
        Search = null;
    }, obj =>
    {
        return SelectedItem != null;
    });

    public ICommand SaveReport => new Command(obj =>
    {

    }, obj =>
    {
        return Storage.Count != 0;
    });
    public ObservableCollection<IStorageItem> Storage
    {
        get => _storage;
        set { _storage = value; OnPropertyChanged("Storage"); }
    }
    public IStorageItem? SelectedItem { get; set; }
    public void StartWork()
    {
        Storage = new ObservableCollection<IStorageItem>(_source.Storage);
        StorageWindow window = new StorageWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.DataContext = this;
        window.Show();
    }
    private void SourcePropertyChangedEventHandler(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Storage = new ObservableCollection<IStorageItem>(_source.Storage);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}