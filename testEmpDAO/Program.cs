﻿using cat.itb.M6UF3EA2.helpers;
using cat.itb.gestioHR.Persistance.Mapping.EmployeeMap;
using cat.itb.gestioHR.DTO;

namespace TestEmpDAO;

public class Driver
{
    public static void Main()
    {
        const string ExitOption = "0", ExitName = "Exit Program";
        FileEmployeeImpl crudEmpFile = new FileEmployeeImpl("../../../employee.json");
        using SQLEmployeeImpl crudEmpSQL = new SQLEmployeeImpl();
        MongoEmployeeImpl crudEmpMongo = new MongoEmployeeImpl();
        Menu menu = new Menu(new Dictionary<string, string>()
        {
            {ExitOption, ExitName },
            {"5", "EmpDAO: SQL to File"},
            {"6", "EmpDAO: File to Mongo"},
            {"7", "EmpDAO: Read and update operations"}
        }, "Pick an option: ");
        string option;
        do
        {
            Console.Write(menu.ToString(". "));
            option = Console.ReadLine();
            try
            {
                switch (option)
                {
                    case "5":
                        List<Employee> empDBData = crudEmpSQL.GetAll().ToList();
                        crudEmpFile.Add(empDBData);
                        break;
                    case "6":
                        List<Employee> empFileData = crudEmpFile.GetAll().ToList();
                        crudEmpMongo.Add(empFileData);
                        break;
                    case "7":
                        Employee emp = crudEmpMongo.GetById<int>(7654);
                        Console.WriteLine(emp);
                        emp.Salary = 2000;

                        crudEmpMongo.Update(emp);
                        crudEmpFile.Update(emp);
                        crudEmpSQL.Update(emp);
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }

        } while (option != ExitOption);
    }
}