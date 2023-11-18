using System;

namespace CourseWork;

public class Operation
{
    public Operation(string name, Component? usedComponent, int price, int applicationId, int date)
    {
        _name = name;
        _usedComponent = usedComponent;
        _price = price;
        _applicationId = applicationId;
        _date = date;
    }

    public int ApplicationId => _applicationId;

    public int Date => _date;

    public Operation(string name, int price, int date)
    {
        _name = name;
        _price = price;
        _date = date;
    }  

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Component? UsedComponent
    {
        get => _usedComponent;
        set => _usedComponent = value;
    }

    public int Price
    {
        get => _price;
        set => _price = value;
    }
    public int TotalPrice
    {
        get
        {
            if (_usedComponent != null) return _price + _usedComponent.Price;
            else return _price;
        }
    }

    private string _name;
    private Component? _usedComponent;
    private int _price;
    private readonly int _applicationId;
    private readonly int _date;
}