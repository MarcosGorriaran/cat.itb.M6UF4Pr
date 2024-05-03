using cat.itb.gestioHR.DTO;

namespace cat.itb.gestioHR.Persistance.DAO
{
    public interface DepartmentDAO
    {

        void DeleteAll();
        void InsertAll(List<Department> deps);
        List<Department> SelectAll();
        Department Select(int depId);
        bool Insert(Department dep);

        bool Delete(int depId);

        bool Update(Department dep);

    }
}