using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IClientBinder
{
    IClient Create(string firstName, string middleName, string lastName, string phoneNumber, string email, string passportData);
}