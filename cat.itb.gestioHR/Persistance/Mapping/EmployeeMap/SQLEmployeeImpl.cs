using cat.itb.gestioHR.connections;
using cat.itb.gestioHR.DTO;
using cat.itb.gestioHR.Persistance.DAO;
using cat.itb.gestioHR.Persistance.Mapping.DepartmentMap;
using Npgsql;

namespace cat.itb.gestioHR.Persistance.Mapping.EmployeeMap
{
    public class SQLEmployeeImpl : EmployeeDAO, IDisposable
    {
        const string IdName = nameof(Employee.Id);
        const string SurnameName = nameof(Employee.Surname);
        const string ManagerName = nameof(Employee.Manager);
        const string DepartmentName = nameof(Employee.Department);
        const string JobName = nameof(Employee.Job);
        const string StartDateName = nameof(Employee.StartDate);
        const string SalaryName = nameof(Employee.Salary);
        const string ComisionName = nameof(Employee.Comision);
        NpgsqlConnection Conn;
        private SQLEmployeeImpl(NpgsqlConnection conn)
        {
            Conn = conn;
        }
        public SQLEmployeeImpl():this(new SQLConnection().GetConnection()) { }

        public void Add(Employee entity)
        {
            string sql = "INSERT INTO EMPLOYEE VALUES " +
                $"(@{IdName}, @{SurnameName}, @{JobName}, @{ManagerName}, @{StartDateName}, @{SalaryName}, @{ComisionName}, @{DepartmentName})";
            NpgsqlCommand cmd = new NpgsqlCommand(sql,Conn);
            cmd.Parameters.AddWithValue(IdName, entity.Id);
            cmd.Parameters.AddWithValue(SurnameName, entity.Surname);
            cmd.Parameters.AddWithValue(ManagerName, entity.Manager);
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
            string sql = "SELECT * FROM employee";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
            List<Employee> entities = new List<Employee>();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee();

                employee.Id = reader.GetInt32(0);
                employee.Surname = reader.GetString(1);
                employee.Job = reader.GetString(2);
                employee.Manager = reader.IsDBNull(3) ? null : reader.GetInt32(3);
                
                employee.StartDate = DateOnly.FromDateTime(reader.GetDateTime(4));
                employee.Salary = reader.GetFloat(5);
                employee.Comision = reader.IsDBNull(6) ? null : reader.GetFloat(6);
                employee.Department = new SQLDepartmentImpl().Select(reader.GetInt32(7));
                
                entities.Add(employee);
            }
            return entities;
        }

        public Employee GetById<TId>(TId id)
        {
            string sql = $"SELECT * FROM employee WHERE _id=@{IdName}";
            NpgsqlCommand cmd = new NpgsqlCommand( sql, Conn);
            Employee employee = new Employee();
            cmd.Parameters.AddWithValue(IdName, id);

            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            employee.Id = reader.GetInt32(0);
            employee.Surname = reader.GetString(1);
            employee.Job = reader.GetString(2);
            employee.Manager = reader.IsDBNull(3) ? null : reader.GetInt32(3);

            employee.StartDate = DateOnly.FromDateTime(reader.GetDateTime(4));
            employee.Salary = reader.GetFloat(5);
            employee.Comision = reader.IsDBNull(6) ? null : reader.GetFloat(6);
            employee.Department = new SQLDepartmentImpl().Select(reader.GetInt32(7));
            return employee;
        }

        public void Remove(Employee entity)
        {
            string sql = $"DELETE FROM employee WHERE _id=@{IdName}";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
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
            string sql = $"UPDATE employee SET surname=@{SurnameName},job=@{JobName},managerid=@{ManagerName}" +
                $",startdate=@{StartDateName},salary=@{SalaryName},commission=@{ComisionName}, depid=@{DepartmentName} " +
                $"WHERE _id=@{IdName}";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue(IdName, entity.Id);
            cmd.Parameters.AddWithValue(SurnameName, entity.Surname);
            cmd.Parameters.AddWithValue(ManagerName, entity.Manager);
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

        public void Dispose()
        {
            Conn.Close();
        }
    }
}
