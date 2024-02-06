using System;

namespace CourseWork.MainLogic;

public class EmployeePersonalData:IEmployeePersonalData
{
    private IFullname _name;
    private string _phoneNumber;
    private string _email;
    private string _position;

    public EmployeePersonalData()
    {
    }

    public IFullname Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Position
    {
        get => _position;
        set => _position = value ?? throw new ArgumentNullException(nameof(value));
    }
    public bool Equals(IEmployeePersonalData right)
    {
        return Name.Equals(right.Name) && Position == right.Position;
    }
}