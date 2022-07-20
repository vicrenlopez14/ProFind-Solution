using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;

namespace ProFind_WebService.Lib.User.DataSource;

public class UserDataSource
{
    readonly MySqlConnection connection;

    public UserDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<int> SearchUser(string email)
    {
        const string queryClient = "SELECT COUNT(Email_C) FROM Client WHERE Email_C = ':Email'; ";
        const string queryProfessional = "SELECT COUNT(Email_P) FROM Professional WHERE Email_C = ':Email'; ";
        const string queryAdmin = "SELECT COUNT(Email_A) FROM Admin WHERE Email_C = ':Email';";

        DynamicParameters dynamicParametersClient = new DynamicParameters();
        DynamicParameters dynamicParametersProfessional = new DynamicParameters();
        DynamicParameters dynamicParametersAdmin = new DynamicParameters();

        dynamicParametersClient.AddDynamicParams(new
        {
            Email = email
        });

        dynamicParametersProfessional.AddDynamicParams(new
        {
            Email = email
        });

        dynamicParametersAdmin.AddDynamicParams(new
        {
            Email = email
        });

        var resultClient = await connection.ExecuteAsync(queryClient, dynamicParametersClient);
        var resultProfessional = await connection.ExecuteAsync(queryProfessional, dynamicParametersProfessional);
        var resultAdmin = await connection.ExecuteAsync(queryAdmin, dynamicParametersProfessional);

        return (resultAdmin > 0 ? 1 : resultProfessional > 0 ? 2 : resultClient > 0 ? 3 : -1);
    }

    public int GetUserType(string email)
    {
        return SearchUser(email).Result;
    }

}