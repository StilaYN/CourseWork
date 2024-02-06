using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public class ClientBinder:IClientBinder
{
    public IClient Create(string firstName, string middleName, string lastName, string phoneNumber, string email, string passportData)
    {
        return new Client(firstName, middleName, lastName, phoneNumber, email, passportData);
    }
}