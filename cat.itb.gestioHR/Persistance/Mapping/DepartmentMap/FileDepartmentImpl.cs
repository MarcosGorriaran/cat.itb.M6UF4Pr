using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Linq;
using cat.itb.gestioHR.Persistance.DAO;
using cat.itb.gestioHR.DTO;

namespace cat.itb.gestioHR.Persistance.Mapping.DepartmentMap
{
    public class FileDepartmentImpl : DepartmentDAO
    {
        private string FilePath { get; set; }

        public FileDepartmentImpl(string path)
        {
            FilePath = path;
        }
        public void DeleteAll()
        {

        }
        public void InsertAll(List<Department> departments)
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
            StreamWriter fileWriter = new StreamWriter(FilePath);
            try
            {
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
            List<Department> result;
            try
            {
                result = JsonSerializer.Deserialize<List<Department>>(fileReader.ReadToEnd());
            }
            finally
            {
                fileReader.Close();
            }


            return result;
        }
        public Department Select(int depId)
        {
            throw new NotImplementedException();
        }
        public bool Insert(Department dep)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int depId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Department dep)
        {
            throw new NotImplementedException();
        }
    }
}
