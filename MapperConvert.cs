using PocketMapper.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace PocketMapper
{
    public static class MapperConvert
    {
        private static void SetMapper(Type type)
        {
            MapperEngine.Instance.SetMapper(type);
        }

        public static List<T> ConvertMap<T>(this IDataReader reader) where T : new()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(Mapper<T>).IsAssignableFrom(p));

            foreach (var item in types)
            {
                SetMapper(item);
            }

            List<string> headers = new List<string>();
            var listMap = MapperEngine.Instance.GetTable(typeof(T).Name);
            var list = Activator.CreateInstance<List<T>>();

            if (((DbDataReader)reader).HasRows)
            {
                while (reader.Read())
                {
                    var instance = Activator.CreateInstance<T>();

                    if (UpdateMapper<T>(headers, reader, listMap))
                    {
                        listMap = MapperEngine.Instance.GetTable(typeof(T).Name);
                    }

                    foreach (var item in headers)
                    {
                        var collunm = listMap?.Properties?.Where(x => x.TableColumn.ToLower() == item.ToLower()).FirstOrDefault();

                        if (collunm != null)
                        {
                            var value = (reader[item] == null) ? null : Convert.ChangeType(reader[item], collunm.Type);
                            instance.GetType().GetProperty(collunm.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.SetValue(instance, value);
                        }
                        //else
                        //{
                        //    throw new Exception($"Mapeamento de colunas está errado: Tabela: {typeof(T).Name}  Coluna: {item}");
                        //}
                    }

                    list.Add(instance);
                }
            }

            return list;
        }

        private static bool UpdateMapper<T>(List<string> headers, IDataReader reader, Table listMap) where T : new()
        {
            bool ret = true;

            if (headers.Count == 0)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    headers.Add(reader.GetName(i));
                }

                if (listMap == null)
                {
                    AutoMapper<T>.MapperTable(headers);
                }
                else
                {
                    if (headers.Count > listMap.Properties.Count)
                    {
                        List<string> aux = new List<string>();

                        foreach (var item in listMap.Properties)
                        {
                            aux.Add(item.TableColumn);
                        }

                        var exp = headers.Except(aux).ToList();
                        AutoMapper<T>.MapperTable(exp);
                    }
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }
    }
}
