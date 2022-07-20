using MySql.Data.MySqlClient;

namespace ProFind_WebService.Lib.DataSource;

public interface IDataSourceLink
{
    public MySqlConnection getConnection();
}