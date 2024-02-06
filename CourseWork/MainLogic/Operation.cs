using System;

namespace CourseWork.MainLogic;

public class Operation:IOperation
{
    public Operation()
    {

    }
    public Operation(string name, IComponent? usedComponent, int price, IDate date)
    {
        Name = name;
        Price = price;
        Date = date;
        UsedComponent = usedComponent;
    }


    public IDate Date
    {
        get { return _date; }
        set { _date = value; }
    }

    public Operation(string name, int price, IDate date)
    {
        Name = name;
        Price = price;
        Date = date;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentException("Operation name will not be empty");
    }

    public IComponent? UsedComponent
    {
        get => _usedComponent;
        set => _usedComponent = value;
    }

    public int Price
    {
        get => _price;
        set
        {
            if (value >= 0) _price = value;
            else throw new ArgumentException("Price can not be less then 0");
        }
    }

    public int TotalPrice
    {
        get
        {
            if (_usedComponent != null) return _price + _usedComponent.Price;
            else return _price;
        }
    }

    public bool Equals(IOperation right)
    {
        if (UsedComponent != null)
        {
            return Name == right.Name && UsedComponent.Equals(right.UsedComponent) && Price == right.Price &&
                   Date == right.Date;
        }
        else if (UsedComponent == null & right.UsedComponent != null)
        {
            return false;
        }
        return Name == right.Name && Price == right.Price &&
               Date.Equals(right.Date);
    }

    private string _name;
    private IComponent? _usedComponent;
    private int _price;
    private IDate _date;
}