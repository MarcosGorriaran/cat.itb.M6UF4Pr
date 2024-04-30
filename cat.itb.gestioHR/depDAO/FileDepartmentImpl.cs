using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Linq;

namespace cat.itb.gestioHR.depDAO
{
    public class FileDepartmentImpl : DepartmentDAO
    {
        private string FilePath {  get; set; }

        public FileDepartmentImpl(string path)
        {
            this.FilePath = path;
        }
        public void DeleteAll()
        {

        }
        public void InsertAll(List<Department> departments)
        {
            StreamWriter fileWriter = new StreamWriter(FilePath, true);
            try
            {
                List<Department> concatDepartments;
                try
                {
                    concatDepartments = departments.Concat(SelectAll()).ToList();
                }
                catch (Exception)
                {
                    concatDepartments = departments;
                }
                fileWriter.WriteLine(JsonSerializer.Serialize(concatDepartments, new JsonSerializerOptions { WriteIndented = true }));
                fileWriter.Close();
            }
            finally
            {
                fileWriter.Close();
            }
            
        }
        public List<Department> SelectAll()
        {
            StreamReader fileReader = new StreamReader(FilePath);
            List<Department> result = JsonSerializer.Deserialize<List<Department>>(fileReader.ReadToEnd());
            fileReader.Close();
            return result;
        }
        public Department Select(int depId)
        {
            throw new NotImplementedException ();
        }
        public Boolean Insert(Department dep)
        {
            throw new NotImplementedException () ;
        }

        public Boolean Delete(int depId)
        {
            throw new NotImplementedException() ;
        }

        public Boolean Update(Department dep)
        {
            throw new NotImplementedException();
        }
    }
}
