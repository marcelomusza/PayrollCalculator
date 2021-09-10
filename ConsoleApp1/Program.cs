using System;
using System.Collections.Generic;

namespace PayrollCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT */
            try
            {
                var employee = LoadEmployee();
                var deductions = new EmployeeDeductions(employee);
                deductions.Calculate();
                deductions.DisplayPayroll();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Found during the execution of the application. Details: {ex.Message}");
            }
        }
        
        public static Employee LoadEmployee()
        {

            var emp = new Employee();

            Console.WriteLine("Insert Employee Location:");
            Console.WriteLine("Ireland [1], Italy [2], Germany [3]");
            switch (Console.ReadLine().ToString())
            {
                case "1":
                    emp.Location = "Ireland";
                    break;
                case "2":
                    emp.Location = "Italy";
                    break;
                case "3":
                    emp.Location = "Germany";
                    break;
                default:
                    throw new Exception("Invalid Selection");
            }
            

            Console.WriteLine("Insert Employee Hourly Rate: ");
            emp.HourlyRate = double.Parse(Console.ReadLine().ToString());

            Console.WriteLine("Insert Employee's Worked Hours: ");
            emp.HoursWorked = double.Parse(Console.ReadLine().ToString());

            return emp;
        }

        public class EmployeeDeductions
        {

            public EmployeeDeductions(Employee emp)
            {
                Employee = emp;
            }

            public double GrossAmount { get; set; }
            public double IncomeTax { get; set; }
            public double UniversalSocialCharge { get; set; }
            public double Pension { get; set; }
            public double NetAmount { get; set; }

            public Employee Employee { get; set; }

            public void Calculate()
            {
                //Gross
                GrossAmount = Employee.HourlyRate * Employee.HoursWorked;

                switch (Employee.Location)
                {
                    case "Ireland":
                        //Income Tax
                        IncomeTax = (GrossAmount <= 600) ? GrossAmount * 0.25 : GrossAmount * 0.40;

                        //Universal Social Charge
                        UniversalSocialCharge = (GrossAmount <= 500) ? GrossAmount * 0.07 : GrossAmount * 0.08;

                        //Compulsory Pension Contribution
                        Pension = GrossAmount * 0.04;
                        break;

                    case "Italy":
                        //Income Tax
                        IncomeTax = GrossAmount * 0.25;

                        //Universal Social Charge
                        UniversalSocialCharge = GrossAmount * 0.0919;

                        //Compulsory Pension Contribution
                        Pension = 0;
                        break;

                    case "Germany":
                        //Income Tax
                        IncomeTax = (GrossAmount <= 400) ? GrossAmount * 0.25 : GrossAmount * 0.32;

                        //Universal Social Charge
                        UniversalSocialCharge = 0;

                        //Compulsory Pension Contribution
                        Pension = GrossAmount * 0.02;                        
                        break;
                    default:
                        throw new Exception("Invalid Country to perform calculations");

                        
                }

                NetAmount = GrossAmount - IncomeTax - UniversalSocialCharge - Pension;

            }

            public void DisplayPayroll()
            {
                Console.WriteLine("***************");
                Console.WriteLine("*** Payroll ***");
                Console.WriteLine("***************");
                Console.WriteLine($"*** Employee Location: {Employee.Location}");
                Console.WriteLine($"*** Gross Amount: {GrossAmount}");
                Console.WriteLine($"*** Less Deductions ***");
                Console.WriteLine($"*** Income Tax: {IncomeTax}");
                Console.WriteLine($"*** Universal Social Charge: {UniversalSocialCharge}");
                Console.WriteLine($"*** Pension: {Pension}");
                Console.WriteLine("***************");
                Console.WriteLine($"*** Net Amount: {NetAmount}");
            }

        }

        public class Employee
        {
            public string Location { get; set; }
            public double HoursWorked { get; set; }
            public double HourlyRate { get; set; }
        }
    }


}
