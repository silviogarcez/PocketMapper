using System;

namespace PocketMapper.Model
{
    public class Property
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string TableColumn { get; set; }

        public void SetMap(string name)
        {
            var values = this.Name.Split(';');
            this.Name = values[0];
            TableColumn = name;

            MapperEngine.Instance.SetProperty(values[1], this);
        }
    }
}
