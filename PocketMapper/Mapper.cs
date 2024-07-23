using PocketMapper.Model;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PocketMapper
{
    public abstract class Mapper<T> where T : new()
    {
        public Mapper()
        {
            MapperEngine.Instance.SetTable(typeof(T).Name);
        }

        public Property Mapping(Expression<Func<T, dynamic>> predicate)
        {
            var body = predicate.Body;
            dynamic expression;

            if (body is UnaryExpression)
            {
                expression = ((UnaryExpression)body).Operand;
            }
            else
            {
                expression = body;
            }

            var ret = ((MemberExpression)expression).Member;
            var type = ((PropertyInfo)ret).PropertyType;
            var name = ((PropertyInfo)ret).Name;

            return new Property { Type = type, Name = name + ";" + typeof(T).Name };
        }

        public void SetTable(string name)
        {
            MapperEngine.Instance.SetTable(name);
        }
    }
}
