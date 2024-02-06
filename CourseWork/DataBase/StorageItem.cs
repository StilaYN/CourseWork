using System;
using System.Windows.Input;
using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public class StorageItem:IStorageItem
{
    private IComponent _component;
    private int _componentNumber;

    public IComponent Component
    {
        get => _component;
        set => _component = value;
    }

    public int ComponentNumber
    {
        get => _componentNumber;
        set => _componentNumber = value;
    }

    public StorageItem()
    {

    }

    public StorageItem(IComponent component, int componentNumber)
    {
        _component = component;
        _componentNumber = componentNumber;
    }
}