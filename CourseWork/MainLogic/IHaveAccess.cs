using System.Collections.Generic;

namespace CourseWork.MainLogic;

public interface IHaveAccess
{
    List<EmployeeAccess> Access { get; set; } 
}