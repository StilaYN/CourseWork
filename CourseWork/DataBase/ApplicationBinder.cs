using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using CourseWork.DataConverter;
using CourseWork.MainLogic;
using Newtonsoft.Json;

namespace CourseWork.DataBase
{

    public class ApplicationBinder : IApplicationBinder
    {
        private List<int> _usedId;
        private string _path;

        public ApplicationBinder()
        {
            _path = "UsedId.json";
            LoadStorageData(_path);
        }

        public IApplication Create(string technicName, string problem, IEmployeePersonalData employee, IClient client,
            IDate date)
        {
            IApplication application = new Application(GenerateId(), technicName, problem, employee, client, date,
                ApplicationStatus.Accepted);
            return application;
        }

        private int GenerateId()
        {
            int i = 1;
            while (_usedId.Contains(i))
            {
                i++;
            }

            _usedId.Add(i);
            SaveUsedId(_path);
            return i;
        }

        private void LoadStorageData(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _usedId = JsonConvert.DeserializeObject<List<int>>(json);
            }
            else
            {
                _usedId = new List<int>();
                SaveUsedId(path);
            }
        }

        private void SaveUsedId(string path)
        {
            string json = JsonConvert.SerializeObject(_usedId);
            File.WriteAllText(path, json);
        }

    }
}