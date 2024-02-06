using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class ApplicationDataConverter : CustomCreationConverter<IApplication>
{
    public Type ApplicationDataType { get; private set; }

    public ApplicationDataConverter(Type applicationDataType)
    {
        ApplicationDataType = applicationDataType;
    }

    public override IApplication Create(Type objecType)
    {
        return (IApplication)Activator.CreateInstance(ApplicationDataType);
    }
}