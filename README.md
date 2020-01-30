# PocketMapper

MicroORM

Facil de usar

Caso as tabelas e as classes de modelo sejam iguais somente fazer conforme o codigo abaixo:

Baixar do nuget : PocketMapper 

```c#

using PocketMapper;

namespace Repository
{
  public class LojaRepository
  {
      public LojaRepository()
      {

      }

      public List<Loja> Lista()
      {
          SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=LojaDB;Integrated Security=SSPI;");
          SqlCommand command = new SqlCommand("select * from Loja", con);

          con.Open();

          var reader = command.ExecuteReader().ConvertMap<Loja>();

          con.Close();

          return reader;
      }
  }
}
```

A função "command.ExecuteReader()" deverá retornar um IDataReader e automaticamente vai aparecer o extension method "ConvertMap" no generics Utilizar a Classe de Retorno

pronto! o retorno da sua query irá polular automaticamente o objeto.

Caso necessite mapear a tabela pois é diferente da classe do modelo

```c#

using PocketMapper;

namespace Repository.Mapping
{
    public class LojaMap : Mapper<Loja>
    {
        public LojaMap()
        {         
            SetTable("tab_loja");
            Mapping(x => x.nome).SetMap("name");
        }
    }
}
```
