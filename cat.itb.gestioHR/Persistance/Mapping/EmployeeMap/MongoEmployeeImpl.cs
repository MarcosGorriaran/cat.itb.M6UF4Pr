

using cat.itb.gestioHR.connections;
using cat.itb.gestioHR.DTO;
using cat.itb.gestioHR.Persistance.DAO;
using MongoDB.Driver;

namespace cat.itb.gestioHR.Persistance.Mapping.EmployeeMap
{
    public class MongoEmployeeImpl : IDAO<Employee>
    {
        const string DB = "itb";
        const string Collection = "employees";
        public void Add(Employee entity)
        {
            IMongoDatabase database = MongoConnection.GetDatabase(DB);
            IMongoCollection<Employee> collection = database.GetCollection<Employee>(Collection);

            collection.InsertOne(entity);
        }

        public void Add(IEnumerable<Employee> entities)
        {
            IMongoDatabase database = MongoConnection.GetDatabase(DB);
            IMongoCollection<Employee> collection = database.GetCollection<Employee>(Collection);

            collection.InsertMany(entities);
        }

        public IEnumerable<Employee> GetAll()
        {
            IMongoDatabase database = MongoConnection.GetDatabase(DB);
            IMongoCollection<Employee> collection = database.GetCollection<Employee>(Collection);

            return collection.AsQueryable();
        }

        public Employee GetById<TId>(TId id)
        {
            IMongoDatabase database = MongoConnection.GetDatabase(DB);
            IMongoCollection<Employee> collection = database.GetCollection<Employee>(Collection);

            return collection.AsQueryable().Where(element=>element.Id==Convert.ToInt32(id)).First();
        }

        public void Remove(Employee entity)
        {
            IMongoDatabase database = MongoConnection.GetDatabase(DB);
            IMongoCollection<Employee> collection = database.GetCollection<Employee>(Collection);

            collection.DeleteOne(element => element.Id == entity.Id);
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
            IMongoDatabase database = MongoConnection.GetDatabase(DB);
            IMongoCollection<Employee> collection = database.GetCollection<Employee>(Collection);

            Remove(entity);
            Add(entity);
        }

        public void Update(IEnumerable<Employee> entities)
        {
            foreach( Employee entity in entities)
            {
                Update(entity);
            }
        }
    }
}
