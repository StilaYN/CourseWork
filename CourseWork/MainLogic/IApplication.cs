using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseWork.MainLogic;

public interface IApplication:IEquatable<IApplication>
{
    int Id { get; set; }
    string TechnicName { get; set; }
    string Problem { get; set; }
    IEmployeePersonalData Employee { get; set; }
    IClient Client { get; set; }
    IDate Date { get; set; }
    ApplicationStatus Status { get; set; }
    List<IOperation> Operations { get; set; }
    int TotalPrice { get; }
    public void AddOperation(IOperation operation);
    public void RemoveOperation(IOperation operation);
    public string StringStatus { get; }

}