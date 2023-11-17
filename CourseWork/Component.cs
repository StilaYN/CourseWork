using System;

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

    public int Price
    {
        get => _price;

        set
        {
            if (value > 0) _price = value;
            else throw new ArgumentException("NOT Correct price");
        }
    }

    private string _technicType;
    private string _componentType;
    private string _name;
    private string _article;
    private int _price;
}