using CourseWork.DataBase;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;
using System;

namespace CourseWork.DataConverter;

public class StorageItemDataConverter:CustomCreationConverter<IStorageItem>
{
    public Type StorageItemDataType { get; private set; }

    public StorageItemDataConverter(Type storageItemDataType)
    {
        StorageItemDataType = storageItemDataType;
    }

    public override IStorageItem Create(Type objecType)
    {
        return (IStorageItem)Activator.CreateInstance(StorageItemDataType);
    }
}