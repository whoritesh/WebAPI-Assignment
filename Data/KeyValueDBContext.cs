using Assignment_1_KeyValuePairs.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1_KeyValuePairs.DBContext
{
    public class KeyValueDBContext : DbContext
    {
        public KeyValueDBContext(DbContextOptions<KeyValueDBContext> options) : base(options) { }   
        public DbSet<KeyValue> KeyValuePairs { get; set; }
    }
}