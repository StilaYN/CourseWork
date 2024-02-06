using System;
using Microsoft.VisualBasic.CompilerServices;

namespace CourseWork.MainLogic;

public class Component: IComponent
{
    public Component()
    {

    }
    public Component(string technicType, string componentType, string name, string article, int price)
    {
        TechnicType = technicType;
        ComponentType = componentType;
        Name = name;
        Article = article;
        Price = price;
    }

    public string TechnicType
    {
        get => _technicType;
        set => _technicType = value ?? throw new ArgumentException("Technic type will not be empty");
    }

    public string ComponentType
    {
        get => _componentType;
        set => _componentType = value ?? throw new ArgumentException("Component type will not be empty");
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentException("Component name will not be empty");
    }

    public string Article
    {
        get => _article;
        set
        {
            if (!string.IsNullOrEmpty(value) && IsDigitalOnly(value)) _article = value;
            else if (string.IsNullOrEmpty(value)) throw new ArgumentException("Article will not be empty");
            else throw new ArgumentException("Article should contain only numbers");
        }
    }

    public bool Equals(IComponent? right)
    {
        return right != null && TechnicType == right.TechnicType && ComponentType == right.ComponentType && Name == right.Name &&
               Article == right.Article && Price == right.Price;
    }
    public int Price
    {
        get => _price;

        set
        {
            if (value > 0) _price = value;
            else throw new ArgumentException("Component price should be bigger then zero");
        }
    }

    public string ComponentToString()
    {
        return $"{Article} {TechnicType} {ComponentType} {Name}";
    }

    private bool IsDigitalOnly(string str)
    {
        foreach (char i in str)
        {
            if (i < '0' || i > '9') return false;
        }
        return true;
    }

    private string _technicType;
    private string _componentType;
    private string _name;
    private string _article;
    private int _price;
}