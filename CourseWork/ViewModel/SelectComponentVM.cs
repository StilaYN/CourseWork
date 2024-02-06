using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CourseWork.DataBase;

namespace CourseWork.ViewModel;

public class SelectComponentVM:INotifyPropertyChanged
{
    private SelectComponentWindow _window;
    private IHaveAccessToStore _source;
    private ObservableCollection<IStorageItem> _storage;
    private StorageItem? _selectedItem; 
    private Visibility _visibile;
    private string _errorText;
    private string? _search;
    public bool CanCreate { get; set; }
    public SelectComponentVM(SelectComponentWindow window,IHaveAccessToStore source)
    {
        _window = window;
        _source = source;
        Storage = new ObservableCollection<IStorageItem>(_source.Storage);
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
    public string? Search
    {
        get => _search;
        set { _search = value; OnPropertyChanged("Search"); }
    }

    public ObservableCollection<IStorageItem> Storage
    {
        get => _storage;
        set { _storage = value;OnPropertyChanged("Storage"); }
    }

    public StorageItem? SelectedItem
    {
        get => _selectedItem;
        set => _selectedItem = value;
    }

    public ICommand SelectComponent => new Command(obj =>
    {
        if (SelectedItem != null)
        {
            CanCreate = true;
            _window.Close();
        }
        else _window.Close();
    });
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
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}