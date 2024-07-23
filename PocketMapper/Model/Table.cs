using System.Collections.Generic;

namespace PocketMapper.Model
{
    public class Table
    {
        public Table()
        {
            Properties = new List<Property> { };
        }
        public string TableName { get; set; }
        public List<Property> Properties { get; set; }
    }
}
