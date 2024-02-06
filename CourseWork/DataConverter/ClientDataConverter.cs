using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class ClientDataConverter : CustomCreationConverter<IClient>
{
    public Type ClientDataType { get; private set; }
    public ClientDataConverter(Type clientDataType)
    {
        ClientDataType = clientDataType;
    }

    public override IClient Create(Type objectType)
    {
        return (IClient)Activator.CreateInstance(ClientDataType);
    }
}