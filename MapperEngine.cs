using PocketMapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMapper
{
    internal sealed class MapperEngine
    {
        private static List<Type> _mymappers { get; set; }
        private static DbMapper _dbmapper;
        private static readonly Lazy<MapperEngine> _lazy = new Lazy<MapperEngine>(() => new MapperEngine());
        public static MapperEngine Instance { get { return _lazy.Value; } }

        private MapperEngine()
        {
            _mymappers = new List<Type>();
            _dbmapper = new DbMapper();
        }

        public void SetMapper(Type type)
        {
            var mapper = GetMapper(type);

            if (mapper == null)
            {
                Activator.CreateInstance(type);
                _mymappers.Add(type);
            }
        }

        public Type GetMapper(Type type)
        {
            return _mymappers?.Where(x => x == type).FirstOrDefault();
        }

        public void SetTable(Table value)
        {
            _dbmapper.Tables.Add(value);
        }

        public void SetTable(string name)
        {
            var ret = MapperEngine.Instance.GetTable(name);

            if (ret == null || ret.TableName != name)
            {
                MapperEngine.Instance.SetTable(new Table { TableName = name });
            }
        }

        public Table GetTable(string tableName)
        {
            return _dbmapper.Tables?.Where(x => x.TableName == tableName).FirstOrDefault();
        }

        public void SetProperty(string tableName, Property value)
        {
            _dbmapper.Tables?.Where(x => x.TableName == tableName).FirstOrDefault()?.Properties.Add(value);
        }
    }
}
