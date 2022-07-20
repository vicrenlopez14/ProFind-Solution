using MySql.Data.MySqlClient;

namespace ProFind_WebService.Lib.DataSource;

public class MySqlDataSourceLink : IDataSourceLink
{
    public MySqlConnection getConnection() => new("server=localhost;database=ProFind;uid=root;Convert Zero Datetime=True;");
}