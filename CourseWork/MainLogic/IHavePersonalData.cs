using System.Collections.Generic;

namespace CourseWork.MainLogic;

public interface IHavePersonalData
{
    IFullname Name { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }

}