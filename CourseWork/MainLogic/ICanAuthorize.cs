using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseWork.MainLogic;

public interface ICanAuthorize:IEquatable<ICanAuthorize>
{
    public string Login { get; set; }
    public byte[] Salt { get; set; }
    public string Password { get; set; }
}