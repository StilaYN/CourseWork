using System.Collections.Generic;
using System.IO;
using CourseWork.DataBase;
using CourseWork.DataConverter;
using CourseWork.MainLogic;
using Newtonsoft.Json;

namespace CourseWork.WorkWithFile;

public class EmployeeFileManager:IEmployeeFileManager
{
    private List<IEmployee>? _employeeList;

    public List<IEmployee>? Load(string path)
    {
        List<IEmployee>? _employeeList = new List<IEmployee>();
        IEmployeeBinder _employeeBinder = new EmployeeBinder();
        if (File.Exists(path))
        {
            var EmployeeConverter = new EmployeeDataConverter(typeof(Employee));
            var FullnameConverter = new FullnameDataConverter(typeof(Fullname));
            string json = File.ReadAllText(path);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(EmployeeConverter);
            settings.Converters.Add(FullnameConverter);
            _employeeList = JsonConvert.DeserializeObject<List<IEmployee>>(json, settings);
        }
        else
        {
            _employeeList = new List<IEmployee>();
            List<EmployeeAccess> employeeAccessControls = new List<EmployeeAccess>();
            employeeAccessControls.Add(EmployeeAccess.Sysadmin);
            _employeeList.Add(_employeeBinder.Create("admin", "admin", "Egor", "Genrihovich", "Avdeev", "8800",
                "@gmail.com", "admin", employeeAccessControls));
            Save(_employeeList,path);
        }
        return _employeeList;
    }

    public void Save(List<IEmployee> o, string path)
    {
        string json = JsonConvert.SerializeObject(o);
        File.WriteAllText(path, json);
    }
}