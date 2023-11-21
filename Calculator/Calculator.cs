using System;
using System.Data;

namespace Calculator
{
    public class Calculator
    {
        public static int OperationSelection(string expression)
        {
            var a = expression.Split();
            if (a.Length > 3)
            {
                throw new ArgumentException("Expression too Long");}
            var firstArgument = int.Parse(a[0]);
            var secondArgument = int.Parse(a[2]);
            var operation = char.Parse(a[1]);
            switch (operation)
            {
                case '+':
                    return Sum(firstArgument, secondArgument);
                    break;
                case '-':
                    return Sub(firstArgument, secondArgument);
                    break;
                case '*':
                    return Mult(firstArgument, secondArgument);
                    break;
                default:
                    return Div(firstArgument, secondArgument);
            }
        } 
        private static int Sum(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        private static int Sub(int firstNumber, int secondNumber)
        {
            return firstNumber- secondNumber;
        }

        private static int Mult(int firstNumber, int secondNumber)
        {
            return (firstNumber * secondNumber);
        }

        private static int Div(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException();
            }
            return firstNumber / secondNumber;
        }
    }
}