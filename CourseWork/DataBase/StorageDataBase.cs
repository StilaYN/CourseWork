using CourseWork.WorkWithFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace CourseWork.DataBase;

public class StorageDataBase:IStorageDataBase,INotifyPropertyChanged
{
    private List<IStorageItem> _storageItemList;
    private IComponentBinder _componentBinder;
    private string _storagePath;
    private IStorageFileManager _storageFileManager;

    public StorageDataBase()
    {
        _componentBinder = new ComponentBinder();
        _storagePath = "Storage.json";
        _storageFileManager = new StorageFileManager();
        _storageItemList = _storageFileManager.Load(_storagePath);
    }
    public List<IStorageItem> Storage => _storageItemList;

    public void SetNewComponentPrice(MainLogic.IComponent component, int price)
    {
        bool inList = false;
        foreach (var i in _storageItemList)
        {
            if (i.Component.Equals(component))
            {
                i.Component.Price = price;
                break;
            }
        }

        _storageFileManager.Save(_storageItemList, _storagePath);
        OnPropertyChanged("StorageItemList");
    }

    public List<IStorageItem> FindItem(string item)
    {
        List<IStorageItem> foundItem = new List<IStorageItem>();
        foreach (var i in Storage)
        {
            if (i.Component.ComponentToString().Contains(item))
            {
                foundItem.Add(i);
            }
        }

        if (foundItem.Count > 0)
        {
            return foundItem;
        }
        throw new ArgumentException("Component was not found");
    }

    public void AddComponent(MainLogic.IComponent component, int number = 1)
    {
        if (number <= 0) throw new ArgumentException("Number of component cant be less then zero");
        if (_storageItemList.Count == 0)
        {
            _storageItemList.Add(new StorageItem(component, number));
        }
        else
        {
            bool inList = false;
            foreach (var i in _storageItemList)
            {
                if (i.Component.Equals(component))
                {
                    i.ComponentNumber += number;
                    inList = true;
                }
            }

            if (!inList)
            {
                _storageItemList.Add(new StorageItem(component, number));
            }
        }
        _storageFileManager.Save(_storageItemList,_storagePath);
        OnPropertyChanged("StorageItemList");
    }

    public void AddComponent(string technnicType, string componentType, string name, string article, int price,
        int number = 1)
    {
        AddComponent(_componentBinder.Create(technnicType, componentType, name, article, price), number);
    }

    public void RemoveComponent(MainLogic.IComponent component, int number = 1)
    {
        if (number <= 0) throw new ArgumentException("Number of component cant be less then zero");
        foreach (var i in _storageItemList)
        {
            if (i.Component.Equals(component))
            {
                int newNumber = i.ComponentNumber - number;
                if (newNumber <= 0)
                {
                    _storageItemList.Remove(i);
                    break;
                }
                else
                {
                    i.ComponentNumber = newNumber;
                    break;
                }
            }
        }

        _storageFileManager.Save(_storageItemList, _storagePath);
        OnPropertyChanged("StorageItemList");
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}