using CourseWork.MainLogic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace CourseWork.ViewModel;

public class ChangeAccessVM:INotifyPropertyChanged
{
    public delegate void ChangeFunction(List<EmployeeAccess>  list);
    private ChangeAccessWindow _window;
    private ChangeFunction _function;
    private bool _isSysAdmin;
    private bool _isManager;
    private bool _isStorage;
    private bool _isMaster;

    public ChangeAccessVM(ChangeAccessWindow window, ChangeFunction function)
    {
        _window = window;
        _function = function;
    }
    public List<EmployeeAccess> Accesses => GetAccesses();
    public bool IsSysAdmin
    {
        get => _isSysAdmin;
        set { _isSysAdmin = value; OnPropertyChanged("IsSysAdmin"); }
    }

    public bool IsManager
    {
        get => _isManager;
        set { _isManager = value; OnPropertyChanged("IsManager"); }
    }

    public bool IsStorage
    {
        get => _isStorage;
        set { _isStorage = value; OnPropertyChanged("IsStorage"); }
    }

    public bool IsMaster
    {
        get => _isMaster;
        set { _isMaster = value; OnPropertyChanged("IsMaster"); }
    }
    public ICommand Create => new Command(
        obj =>
        {
            _function(GetAccesses());
            _window.Close();
        },
        obj => (IsManager || IsSysAdmin || IsMaster || IsStorage));
    private List<EmployeeAccess> GetAccesses()
    {
        var list = new List<EmployeeAccess>();
        if (IsSysAdmin) list.Add(EmployeeAccess.Sysadmin);
        if (IsManager) list.Add(EmployeeAccess.Manager);
        if (IsStorage) list.Add(EmployeeAccess.Storekeeper);
        if (IsMaster) list.Add(EmployeeAccess.Master);
        return list;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}