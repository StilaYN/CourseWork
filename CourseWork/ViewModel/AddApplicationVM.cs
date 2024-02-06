using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModel;

public class AddApplicationVM:INotifyPropertyChanged
{
    private AddApplication _window;
    private CreateFunction _create;
    private Visibility _visibile;
    private string _errorText;

    public AddApplicationVM(AddApplication window, CreateFunction create)
    {
        _window = window;
        _create = create;
        _visibile = Visibility.Hidden;
    }

    public delegate void CreateFunction(string technicName, string problem, string firstName,
        string middleName, string lastName, string phoneNumber, string email, string passportData, int day, int month,
        int year);

    
    public string TechnicName { get; set; }
    public string Problem { get; set; }
    public string ClientName { get; set; }
    public string ClientSurname { get; set; }
    public string? ClientMiddleName { get; set;}
    public string ClientEmail { get; set; }
    public string ClientPhone { get; set; }
    public string ClientPassport { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Visibility Visible
    {
        get => _visibile;
        set { _visibile = value;OnPropertyChanged("Visible"); }
    }
    public string ErrorText
    {
        get => _errorText;
        set { _errorText = value;OnPropertyChanged("ErrorText"); }
    }
    public ICommand Create => new Command(obj =>
    {
        try
        {
            _create(TechnicName, Problem, ClientName, ClientMiddleName, ClientSurname, ClientPhone, ClientEmail,
                ClientPassport, Day, Month, Year);
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