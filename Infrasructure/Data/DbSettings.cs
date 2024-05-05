using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DbSettings
    {
        public const string SectionName = "DbSettings";
        public const string ConnectionStr   = "Data Source=DESKTOP-OIB64S6\\SQLEXPRESS;Initial Catalog=MK_WebApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public const string MonsterDataBase = "Server=db3996.public.databaseasp.net; Database=db3996; User Id=db3996; Password=Dy4%z_R97n!T; Encrypt=False; MultipleActiveResultSets=True;";
        public const string ConnectionStrNewLabTop = "Data Source=DESKTOP-SPN63E3;Initial Catalog=MK_WebApi;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // this data base for mahmoud
        // public const string ConnectionStrNewLabTop = "data source = mahmoud\\SQLEXPRESS;initial catalog = AuthMigration; trusted_connection=true";
    }
}
 