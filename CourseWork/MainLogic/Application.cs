using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseWork.MainLogic;

public class Application:IApplication
{
    private int _id;
    private string _technicName;
    private string _problem;
    private IEmployeePersonalData _employee;
    private IClient _client;
    private IDate _date;
    private ApplicationStatus _status;
    public Application()
    {

    }

    public Application(int id, string technicName, string problem, IEmployeePersonalData employee, IClient client, IDate date, ApplicationStatus status)
    {
        Id = id;
        TechnicName = technicName;
        Problem = problem;
        Employee = employee;
        Client = client;
        Date = date;
        Status = status;
        Operations = new List<IOperation>();
    }

    public string Problem
    {
        get => _problem;
        set => _problem = value ?? throw new ArgumentException("Problem will not be empty");
    }

    public int Id
    {
        get => _id;
        set
        {
            if (value < 0) throw new ArgumentException("Id less then zero");
            else _id = value;
        }
    }

    public string TechnicName
    {
        get => _technicName;
        set => _technicName = value ?? throw new ArgumentException("Technic will not be empty");
    }

    public IEmployeePersonalData Employee
    {
        get => _employee;
        set => _employee = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IClient Client
    {
        get => _client;
        set => _client = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IDate Date
    {
        get => _date;
        set => _date = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ApplicationStatus Status
    {
        get => _status;
        set => _status = value;
    }

    public List<IOperation> Operations { get; set; }

    public void AddOperation(IOperation operation)
    {
        Operations.Add(operation);
    }

    public void RemoveOperation(IOperation operation)
    {
        foreach (var i in Operations)
        {
            if (i.Equals(operation))
            {
                Operations.Remove(i);
                break;
            }
        }
    }

    public int TotalPrice
    {
        get
        {
            int sum = 0;
            foreach (var i in Operations)
            {
                sum += i.TotalPrice;
            }

            return sum;
        }
    }

    public bool Equals(IApplication right)
    {
        return Id == right.Id && TechnicName==right.TechnicName && Problem == right.Problem && Employee.Equals(right.Employee)
               &&Client.Equals(right.Client)&&Status==right.Status&&CompareCollection(Operations,right.Operations);
    }

    private bool CompareCollection(List<IOperation> left, List<IOperation> right)
    {
        if(left.Count!=right.Count) return false;
        else
        {
            for (int i = 0; i < left.Count; i++)
            {
                if (right[i] != left[i]) return false;
            }
        }
        return true;
    }

    public string StringStatus
    {
        get
        {
            switch (Status)
            {
                case ApplicationStatus.Accepted:
                    return "Accepted";
                case ApplicationStatus.AtWork:
                    return "AtWork";
                case ApplicationStatus.Completed:
                    return "Completed";
                case ApplicationStatus.Collected:
                    return "Collected";
                default: return "Rejection";
            }
        }
    }

}