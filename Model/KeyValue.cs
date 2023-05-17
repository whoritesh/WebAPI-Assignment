using System.ComponentModel.DataAnnotations;

namespace Assignment_1_KeyValuePairs.Model
{
    public class KeyValue
    {
        [Key]
        public int Id { get; set; } 
        public string? Key { get; set; }
        public string? Value { get; set; }
        
    }
}
