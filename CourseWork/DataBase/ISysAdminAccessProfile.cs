using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface ISysAdminAccessProfile:IAccessProfile
{
    void AddUser(string login,string password,string name,string middleName,string lastName,string phoneNumber,
        string email,string position,List<EmployeeAccess> accesses);
    void DeleteUser(IEmployee employee);
    void ChangeLogin(IEmployee employee,string newLogin);
    void ChangePassword(IEmployee employee,string newPassword);
    //void ChangeUserInfo(IEmployee employee,IFullname newName, string newPosition);
    void ChangeAccess(IEmployee employee,List<EmployeeAccess> newAccess);
    List<IEmployee> FindEmployeeBySurname(string surname);
    List<IEmployee> EmployeeList { get; }
}