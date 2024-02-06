using System.Collections.Generic;
using System.IO;
using CourseWork.DataConverter;
using CourseWork.MainLogic;
using Newtonsoft.Json;

namespace CourseWork.WorkWithFile;

public class ApplicationFileManager:IApplicationFileManager
{
    public List<IApplication>? Load(string path)
    {
        var applicationList = new List<IApplication>();
        if (File.Exists(path))
        {
            var applicationConverter = new ApplicationDataConverter(typeof(Application));
            var dateItemConverter = new DateDataConverter(typeof(MainLogic.Date));
            var fullnameConverter = new FullnameDataConverter(typeof(Fullname));
            var employeePersonalDataConverter = new EmployeePersonalDataConverter(typeof(EmployeePersonalData));
            var clientDataConverter = new ClientDataConverter(typeof(Client));
            var operationDataConverter = new OperationDataConvert(typeof(Operation));
            var componentConverter = new ComponentDataConverter(typeof(Component));

            string json = File.ReadAllText(path);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(applicationConverter);
            settings.Converters.Add(dateItemConverter);
            settings.Converters.Add(fullnameConverter);
            settings.Converters.Add(employeePersonalDataConverter);
            settings.Converters.Add(clientDataConverter);
            settings.Converters.Add(operationDataConverter);
            settings.Converters.Add(componentConverter);
            applicationList = JsonConvert.DeserializeObject<List<IApplication>>(json, settings);
        }
        else
        {
            applicationList = new List<IApplication>();
            Save(applicationList,path);
        }

        return applicationList;
    }

    public void Save(List<IApplication> list, string path)
    {
        string json = JsonConvert.SerializeObject(list);
        File.WriteAllText(path, json);
    }
}