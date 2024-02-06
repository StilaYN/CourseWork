using System;

namespace CourseWork.MainLogic;

public class Date:IDate
{
    private int _day;
    private int _month;
    private int _year;

    public Date()
    {

    }
    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    public int Day
    {
        get => _day;
        set
        {
            if (value > 0 && value <= 31) _day = value;
            else throw new ArgumentException("Day should be more then 0 and less then 32");
        }
    }

    public int Month
    {
        get => _month;
        set
        {
            if (value > 0 && value <= 12) _month = value;
            else throw new ArgumentException("Month should be more then 0 and less then 13");
        }
    }

    public int Year
    {
        get => _year;
        set
        {
            if (value >= 2023 && value <= 2024) _year = value;
            else throw new ArgumentException("Wrong year, today 2023");
        }
    }

    public bool Equals(IDate right)
    {
        return Day==right.Day&&Month==right.Month&&Year==right.Year;
    }

    public string GetStringDate
    {
        get { return $"{Day}/{Month}/{Year}"; }
    }
}