using cat.itb.gestioHR.connections;
using cat.itb.gestioHR.DTO;
using cat.itb.gestioHR.Persistance.Mapping.DepartmentMap;
using cat.itb.gestioHR.Persistance.Utils;
using cat.itb.M6UF3EA2.helpers;
using System.Reflection;

namespace LibraryProgram;

public class Driver
{
    public static void Main()
    {
        const string ExitOption = "0",ExitName = "Exit Program";
        FileDepartmentImpl crudDepFile = new FileDepartmentImpl("../../../departments.json");
        SQLDepartmentImpl crudDeptSQL = new SQLDepartmentImpl();
        MongoDepartmentImpl crudDeptMongo = new MongoDepartmentImpl();
        Console.WriteLine(((ColumnName)typeof(Employee).GetProperty("Id").GetCustomAttribute(typeof(ColumnName))).Name);
        Menu menu = new Menu(new Dictionary<string, string>()
        {
            {ExitOption, ExitName },
            {"2","Check Insert all"},
            {"3", "Check Select all"}
        },"Pick an option: ");
        string option;
        do
        {
            Console.Write(menu);
            option = Console.ReadLine();

            switch(option) 
            {
                case "2":
                    
                    List<Department> depDBData = crudDeptSQL.SelectAll();
                    

                    crudDepFile.InsertAll(depDBData);
                    break;
                case "3":
                    

                    List<Department> depFileData = crudDepFile.SelectAll();
                    crudDeptMongo.InsertAll(depFileData);
                    break;
            }
        } while (option!=ExitOption);
        

        

        
    }
}