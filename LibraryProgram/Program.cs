using cat.itb.gestioHR.DTO;
using cat.itb.gestioHR.Persistance.Mapping.DepartmentMap;
using cat.itb.gestioHR.Persistance.Mapping.EmployeeMap;
using cat.itb.M6UF3EA2.helpers;

namespace TestDepDAO;

public class Driver
{
    public static void Main()
    {
        const string ExitOption = "0",ExitName = "Exit Program";
        FileDepartmentImpl crudDepFile = new FileDepartmentImpl("../../../departments.json");
        SQLDepartmentImpl crudDeptSQL = new SQLDepartmentImpl();
        MongoDepartmentImpl crudDeptMongo = new MongoDepartmentImpl();
        Menu menu = new Menu(new Dictionary<string, string>()
        {
            {ExitOption, ExitName },
            {"2", "DepDAO: Check Insert all"},
            {"3", "DepDAO: Check Select all"},
        },"Pick an option: ");
        string option;
        do
        {
            Console.Write(menu.ToString(". "));
            option = Console.ReadLine();
            try
            {
                switch (option)
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
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            
        } while (option!=ExitOption);
        

        

        
    }
}