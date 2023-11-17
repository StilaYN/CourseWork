using System;
using Microsoft.VisualBasic.CompilerServices;

namespace CourseWork;

public class Component
{
    public Component(string technicType, string componentType, string name, string article, int price)
    {
        _technicType = technicType;
        _componentType = componentType;
        _name = name;
        _article = article;
        _price = price;
    }

    public string TechnicType => _technicType;

    public string ComponentType => _componentType;

    public string Name => _name;

    public string Article => _article;

    private bool IsEqual(Component? right)
    {
        return right != null && _technicType==right._technicType && _componentType==right._componentType && _name==right._name &&
               _article==right._article && _price==right._price;
    }

    public static bool operator ==(Component? left, Component? right)
    {
        return left != null && left.IsEqual(right);
    }
    public static bool operator !=(Component? left, Component? right)
    {
        return left != null && !left.IsEqual(right);
    }
    public int Price
    {
        get => _price;

        set
        {
            if (value > 0) _price = value;
            else throw new ArgumentException("NOT Correct price");
        }
    }

    private readonly string _technicType;
    private readonly string _componentType;
    private readonly string _name;
    private readonly string _article;
    private int _price;
}