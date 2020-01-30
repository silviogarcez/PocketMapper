using PocketMapper.Model;
using System.Collections.Generic;

namespace PocketMapper
{
    public static class AutoMapper<T>
    {
        public static void MapperTable(List<string> listTable)
        {
            MapperEngine.Instance.SetTable(typeof(T).Name);

            foreach (var bdproperty in listTable)
            {
                foreach (var classproperty in typeof(T).GetProperties())
                {
                    if (classproperty.Name.ToLower() == bdproperty.ToLower())
                    {
                        MapperEngine.Instance.SetProperty(typeof(T).Name, new Property { Type = classproperty.PropertyType, Name = classproperty.Name, TableColumn = bdproperty });
                        break;
                    }
                }
            }
        }
    }
}
