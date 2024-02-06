using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class EmployeeDataConverter : CustomCreationConverter<IEmployee>
{
    public Type EmployeeDataType { get; private set; }

    public EmployeeDataConverter(Type employeeDataType)
    {
        EmployeeDataType = employeeDataType;
    }

    public override IEmployee Create(Type objecType)
    {
        return (IEmployee)Activator.CreateInstance(EmployeeDataType);
    }
}