
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace cat.itb.gestioHR.DTO
{
    public class Employee
    {
        [BsonElement("_id")]
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public Employee Manager { get; set; }
        public DateOnly StartDate { get; set; }
        public float Salary { get; set; }
        [BsonIgnoreIfNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? Comision { get; set; }
        public Department Department {  get; set; }
        public List<Employee> Lackeys { get; set; }
    }
}
