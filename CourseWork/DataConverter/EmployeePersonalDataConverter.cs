using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;
using System;

namespace CourseWork.DataConverter;

public class EmployeePersonalDataConverter:CustomCreationConverter<IEmployeePersonalData>
{
    public Type EmployeePersonalDataType { get; private set; }

    public EmployeePersonalDataConverter(Type employeeDataType)
    {
        EmployeePersonalDataType = employeeDataType;
    }

    public override IEmployeePersonalData Create(Type objecType)
    {
        return (IEmployeePersonalData)Activator.CreateInstance(EmployeePersonalDataType);
    }
}