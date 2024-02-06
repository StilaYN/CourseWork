using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CourseWork.ViewModel;

public class ChoseWorkProfileVM:INotifyPropertyChanged
{
    private ChoseWorkProfileWindow _window;
    private ObservableCollection<IWorkProfile> _workProfiles;

    public ChoseWorkProfileVM(ChoseWorkProfileWindow window, List<IWorkProfile> workProfiles)
    {
        _window = window;
        WorkProfiles = new ObservableCollection<IWorkProfile>(workProfiles);
        CanCreate = false;
    }

    public ObservableCollection<IWorkProfile> WorkProfiles
    {
        get => _workProfiles;
        set { _workProfiles = value;OnPropertyChanged("IWorkProfile"); }
    }

    public bool CanCreate { get; private set; }
    public IWorkProfile? SelectedWorkProfile { get; set; }

    public ICommand Chose => new Command(obj =>
    {
        CanCreate = true;
        _window.Close();
    }, obj => SelectedWorkProfile != null);
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

}