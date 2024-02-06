using System.Collections.Generic;
using CourseWork.MainLogic;
using CourseWork.ViewModel;

namespace CourseWork.DataBase;

public class WorkProfileBinder:IWorkProfileBinder
{
    public List<IWorkProfile> Create(IEmployeeDataBase dateBase, List<EmployeeAccess> employeeAccesses,IEmployee employee)
    {
        List<IWorkProfile> workProfiles = new List<IWorkProfile>();
        IWorkProfile profile;
        foreach (var i in employeeAccesses)
        {
            switch (i)
            {
                case EmployeeAccess.Master:
                    profile = new Master("Master",new MasterAccessProfile(),employee);
                    workProfiles.Add(profile);
                    break;
                case EmployeeAccess.Manager:
                    profile = new Manager("Manager", new ManagerAccessProfile());
                    workProfiles.Add(profile);
                    break;
                case EmployeeAccess.Sysadmin:
                    profile = new SysAdmin("Sysadmin",(ISysAdminAccessProfile)dateBase,employee);
                    workProfiles.Add(profile);
                    break;
                case EmployeeAccess.Storekeeper:
                    profile = new Storekeeper("Storekeeper", new StorageDataBase());
                    workProfiles.Add(profile);
                    break;
            }
        }
        return workProfiles;
    }

}