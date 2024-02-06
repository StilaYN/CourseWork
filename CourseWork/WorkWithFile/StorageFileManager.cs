using System.Collections.Generic;
using System.IO;
using CourseWork.DataBase;
using CourseWork.DataConverter;
using CourseWork.MainLogic;
using Newtonsoft.Json;

namespace CourseWork.WorkWithFile;

public class StorageFileManager:IStorageFileManager
{

    public List<IStorageItem>? Load(string path)
    {
        List<IStorageItem> _storageItemList = new List<IStorageItem>();
        if (File.Exists(path))
        {
            var componentConverter = new ComponentDataConverter(typeof(Component));
            var storageItemConverter = new StorageItemDataConverter(typeof(StorageItem));
            string json = File.ReadAllText(path);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(componentConverter);
            settings.Converters.Add(storageItemConverter);
            _storageItemList = JsonConvert.DeserializeObject<List<IStorageItem>>(json, settings);
        }
        else
        {
            _storageItemList = new List<IStorageItem>();
            Save(_storageItemList,path);
        }
        return _storageItemList;
    }

    public void Save(List<IStorageItem> o, string path)
    {
        string json = JsonConvert.SerializeObject(o);
        File.WriteAllText(path, json);
    }
}