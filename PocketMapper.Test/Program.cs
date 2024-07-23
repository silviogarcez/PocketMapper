using BlazorProject.Domain;
using PocketMapper.Test;
using System.Diagnostics;

var result1 = GetTimer<User>(() => PocketTest.GetList<User>("Users"));
var result2 = GetTimer<User>(() => PocketTest.GetList<User>("Users"));
var result3 = GetTimer<Permission>(() => PocketTest.GetList<Permission>("Permission"));
var result4 = GetTimer<Permission>(() => PocketTest.GetList<Permission>("Permission"));
string a = ""; 

Tuple<List<T>, string> GetTimer<T>(Func<List<T>> func)
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    stopwatch.Start();
    var ret = func.Invoke();
    return Tuple.Create(ret,stopwatch.Elapsed.ToString());
}