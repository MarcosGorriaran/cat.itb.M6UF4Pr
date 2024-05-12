
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
        [BsonIgnoreIfNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Manager { get; set; }
        public DateOnly StartDate { get; set; }
        public float Salary { get; set; }
        [BsonIgnoreIfNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? Comision { get; set; }
        public Department Department {  get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Surname)}={Surname}, {nameof(Job)}={Job}, {nameof(Manager)}={Manager.ToString()}, {nameof(StartDate)}={StartDate.ToString()}, {nameof(Salary)}={Salary.ToString()}, {nameof(Comision)}={Comision.ToString()}, {nameof(Department)}={Department}}}";
        }
    }
}
