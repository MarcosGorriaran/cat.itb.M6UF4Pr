using cat.itb.gestioHR.DTO;
using cat.itb.gestioHR.Persistance.DAO;
using SharpCompress.Common;
using System.Text.Json;

namespace cat.itb.gestioHR.Persistance.Mapping.EmployeeMap
{
    public class FileEmployeeImpl : EmployeeDAO
    {
        private string FilePath { get; set; }

        public FileEmployeeImpl(string path)
        {
            FilePath = path;
        }
        public void Add(Employee entity)
        {
            List<Employee> concatEmployee = GetAll().ToList();
            concatEmployee.Add(entity);
            
            using (StreamWriter fileWriter = new StreamWriter(FilePath))
            {
                fileWriter.WriteLine(JsonSerializer.Serialize(concatEmployee, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public void Add(IEnumerable<Employee> entities)
        {
            List<Employee> concatEmployee;
            try
            {
                concatEmployee = entities.Concat(GetAll()).ToList();
            }
            catch (Exception)
            {
                concatEmployee = entities.ToList();
            }
            using (StreamWriter fileWriter = new StreamWriter(FilePath))
            {
                fileWriter.WriteLine(JsonSerializer.Serialize(concatEmployee, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            StreamReader fileReader = new StreamReader(FilePath);
            List<Employee> result;
            try
            {
                result = JsonSerializer.Deserialize<List<Employee>>(fileReader.ReadToEnd());
            }
            finally
            {
                fileReader.Close();
            }


            return result;
        }

        public Employee GetById<TId>(TId id)
        {
            return GetAll().Where(element => element.Id==Convert.ToInt32(id)).First();
        }

        public void Remove(Employee entity)
        {
            List<Employee> employees = GetAll().ToList();
            employees = employees.Where(element => element.Id !=  entity.Id).ToList();
            using (StreamWriter fileWriter = new StreamWriter(FilePath))
            {
                fileWriter.WriteLine(JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public void Remove(IEnumerable<Employee> entities)
        {
            List<Employee> employees = GetAll().ToList();
            foreach(Employee entity in entities)
            {
                employees = employees.Where(element => element.Id != entity.Id).ToList();
            }
            using (StreamWriter fileWriter = new StreamWriter(FilePath))
            {
                fileWriter.WriteLine(JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public void Update(Employee entity)
        {
            List<Employee> employees = GetAll().ToList();
            employees = employees.Where(element => element.Id != entity.Id).ToList();
            employees.Add(entity);
            using (StreamWriter fileWriter = new StreamWriter(FilePath))
            {
                fileWriter.WriteLine(JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public void Update(IEnumerable<Employee> entities)
        {
            List<Employee> employees = GetAll().ToList();
            foreach (Employee entity in entities)
            {
                employees = employees.Where(element => element.Id != entity.Id).ToList();
                employees.Add(entity);
            }
            using (StreamWriter fileWriter = new StreamWriter(FilePath))
            {
                fileWriter.WriteLine(JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}
