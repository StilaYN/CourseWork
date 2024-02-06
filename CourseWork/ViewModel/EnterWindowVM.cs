using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CourseWork.MainLogic;
using CourseWork.DataBase;
namespace CourseWork.ViewModel;

public class EnterWindowVM:INotifyPropertyChanged
{
    private IEmployeeDataBase _date = new EmployeeDataBase();
    private IAccessControl _accessControl;
    private string _login;
    private string _password;
    private Visibility _visibile;
    private string _errorText;


    public EnterWindowVM()
    {
        _accessControl = new AccessControl(_date);
    }

    public string Login
    {
        get => _login;
        set => _login = value;
    }

    public string Password
    {
        get => _password;
        set => _password = value;
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

    public ICommand Authorize => new Command(obj =>
    {
        try
        {
            List<IWorkProfile> profiles = _accessControl.Authorize(Login, Password);
            Visible = Visibility.Hidden;
            if (profiles.Count > 1)
            {
                ChoseWorkProfileWindow window = new ChoseWorkProfileWindow();
                var prof = new ChoseWorkProfileVM(window, profiles);
                window.DataContext = prof;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.ShowDialog();
                if (prof.CanCreate)
                {
                    prof.SelectedWorkProfile.StartWork();
                }

            }
            else profiles[0].StartWork();
        }
        catch (ArgumentException e)
        {
            Visible = Visibility.Visible;
            ErrorText = e.Message;
        }

    });
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}