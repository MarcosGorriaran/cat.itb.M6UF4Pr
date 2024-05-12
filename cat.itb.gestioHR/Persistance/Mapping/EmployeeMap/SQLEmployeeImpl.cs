using cat.itb.gestioHR.connections;
using cat.itb.gestioHR.DTO;
using cat.itb.gestioHR.Persistance.DAO;
using cat.itb.gestioHR.Persistance.Mapping.DepartmentMap;
using Npgsql;

namespace cat.itb.gestioHR.Persistance.Mapping.EmployeeMap
{
    public class SQLEmployeeImpl : EmployeeDAO
    {
        const string IdName = nameof(Employee.Id);
        const string SurnameName = nameof(Employee.Surname);
        const string ManagerName = nameof(Employee.Manager);
        const string DepartmentName = nameof(Employee.Department);
        const string LackeysName = nameof(Employee.Lackeys);
        const string JobName = nameof(Employee.Job);
        const string StartDateName = nameof(Employee.StartDate);
        const string SalaryName = nameof(Employee.Salary);
        const string ComisionName = nameof(Employee.Comision);
        public void Add(Employee entity)
        {
            using NpgsqlConnection conn = new SQLConnection().GetConnection();
            string sql = "INSERT INTO EMPLOYEE VALUES " +
                $"(@{IdName}, @{SurnameName}, @{JobName}, @{ManagerName}, @{StartDateName}, @{SalaryName}, @{ComisionName}, @{DepartmentName})";
            NpgsqlCommand cmd = new NpgsqlCommand(sql,conn);
            cmd.Parameters.AddWithValue(IdName, entity.Id);
            cmd.Parameters.AddWithValue(SurnameName, entity.Surname);
            cmd.Parameters.AddWithValue(ManagerName, entity.Manager.Id);
            cmd.Parameters.AddWithValue(StartDateName, entity.StartDate);
            cmd.Parameters.AddWithValue(SalaryName, entity.Salary);
            cmd.Parameters.AddWithValue(ComisionName, entity.Comision);
            cmd.Parameters.AddWithValue(DepartmentName, entity.Department._id);

            cmd.ExecuteNonQuery();
        }

        public void Add(IEnumerable<Employee> entities)
        {
            foreach(Employee entity in entities)
            {
                Add(entity);
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using NpgsqlConnection conn = new SQLConnection().GetConnection();
            string sql = "SELECT * FROM employee";
            NpgsqlCommand cmd = new NpgsqlCommand(sql,conn);
            List<Employee> entities = new List<Employee>();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                entities.Add(new Employee()
                {
                    Id = reader.GetInt32(0),
                    Surname = reader.GetString(1),
                    Job = reader.GetString(2),
                    Manager = GetById(reader.GetInt32(3)),
                    StartDate = DateOnly.FromDateTime(reader.GetDateTime(4)),
                    Salary = reader.GetFloat(5),
                    Comision = reader.IsDBNull(6) ? null : reader.GetFloat(6),
                    Department = new SQLDepartmentImpl().Select(reader.GetInt32(7)),
                    Lackeys = GetAll().Where(employee => employee.Manager.Id==reader.GetInt32(0)).ToList()
                }) ;
            }
            return entities;
        }

        public Employee GetById<TId>(TId id)
        {
            using NpgsqlConnection conn = new SQLConnection().GetConnection();
            string sql = $"SELECT * FROM employee WHERE _id=@{IdName}";
            NpgsqlCommand cmd = new NpgsqlCommand( sql,conn);
            Employee result;
            cmd.Parameters.AddWithValue(IdName, id);

            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result = new Employee()
            {
                Id = reader.GetInt32(0),
                Surname = reader.GetString(1),
                Job = reader.GetString(2),
                Manager = GetById(reader.GetInt32(3)),
                StartDate = DateOnly.FromDateTime(reader.GetDateTime(4)),
                Salary = reader.GetFloat(5),
                Comision = reader.IsDBNull(6) ? null : reader.GetFloat(6),
                Department = new SQLDepartmentImpl().Select(reader.GetInt32(7)),
                Lackeys = GetAll().Where(employee => employee.Manager.Id == reader.GetInt32(0)).ToList()
            };
            return result;
        }

        public void Remove(Employee entity)
        {
            using NpgsqlConnection conn = new SQLConnection().GetConnection();
            string sql = $"DELETE FROM employee WHERE _id=@{IdName}";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue(IdName, entity.Id);

            cmd.ExecuteNonQuery();
        }

        public void Remove(IEnumerable<Employee> entities)
        {
            foreach(Employee entity in entities)
            {
                Remove(entity);
            }
        }

        public void Update(Employee entity)
        {
            using NpgsqlConnection conn = new SQLConnection().GetConnection();
            string sql = $"UPDATE employee SET surname=@{SurnameName},job=@{JobName},managerid=@{ManagerName}" +
                $",startdate=@{StartDateName},salary=@{SalaryName},commission=@{ComisionName}, depid=@{DepartmentName} " +
                $"WHERE _id=@{IdName}";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue(IdName, entity.Id);
            cmd.Parameters.AddWithValue(SurnameName, entity.Surname);
            cmd.Parameters.AddWithValue(ManagerName, entity.Manager.Id);
            cmd.Parameters.AddWithValue(StartDateName, entity.StartDate);
            cmd.Parameters.AddWithValue(SalaryName, entity.Salary);
            cmd.Parameters.AddWithValue(ComisionName, entity.Comision);
            cmd.Parameters.AddWithValue(DepartmentName, entity.Department._id);

            cmd.ExecuteNonQuery();
        }

        public void Update(IEnumerable<Employee> entities)
        {
            foreach(Employee entity in entities)
            {
                Update(entity);
            }
        }
    }
}
