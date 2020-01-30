using System.Collections.Generic;

namespace PocketMapper.Model
{
    public class DbMapper
    {
        public DbMapper()
        {
            Tables = new List<Table> { };
        }
        public List<Table> Tables { get; set; }
    }
}
