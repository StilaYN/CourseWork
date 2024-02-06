using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CourseWork.ViewModel;

public class EnterLoginVM:INotifyPropertyChanged
{
    private EnterLoginWindow _window;
    private string _fieldName;
    private string _field;

    public string Field
    {
        get => _field;
        set { _field = value;OnPropertyChanged("Field");  }
    }

    public bool CanCreate { get; private set; }

    public EnterLoginVM(EnterLoginWindow window, string fieldName)
    {
        _window = window;
        FieldName = fieldName;
        CanCreate = false;
    }

    public string FieldName
    {
        get => _fieldName;
        set { _fieldName = value;OnPropertyChanged("FieldName"); }
    }

    public ICommand Done => new Command(obj => { CanCreate = true; _window.Close(); }, 
        obj => !string.IsNullOrEmpty(Field) );
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}