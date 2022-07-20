using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.Admin.Model;
using ProFind_WebService.Lib.DataSource;

namespace ProFind_WebService.Lib.Admin.DataSource;

public class AdminDataSource
{
    readonly MySqlConnection connection;

    public AdminDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFAdmin?> Get(string id)
    {

        const string query = "SELECT Id_A, Name_A, Email_A, Tel_A; `Rank`.Name_R FROM Admin, `Rank` "
            + "WHERE (Id_A = ':Id' AND Admin.Id_R1 = `Rank`.Id_R);";

        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            Id = id
        });

        var result = await connection.QueryAsync(query, dynamicParameters);
        var FirstRow = result.First() as IDictionary<string, object>;

        return PFAdmin.FromDictionary(FirstRow);
    }

    public async Task<bool> Create(PFAdmin admin)
    {
        const string query = "INSET INTO ADMIN VALUES(':Id',':Name',':Email',':Tel',':Password',':Rank');";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = admin.Id,
            Name = admin.Name,
            Email = admin.Email,
            Tel = admin.Tel,
            Password = admin.Password,
            Rank = admin.Rank.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFAdmin admin)
    {
        const string query = "UPDATE Admin SET Name_A = ':Name', Email_A = ':Email', Tel_A = 'Tel',"
            + " Password_A = 'Password', Id_R1 = ':Rank' WHERE Id_A = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = admin.Id,
            Name = admin.Name,
            Email = admin.Email,
            Tel = admin.Tel,
            Password = admin.Password,
            Rank = admin.Rank.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(PFAdmin admin)
    {
        const string query = "DELETE FROM Admin WHERE Id_A = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            Id = admin.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    private async Task<bool> CheckCredentials(string email, string password)
    {
        const string query = "SELECT * FROM Admin WHERE Email_A = ':Email' AND Password_A = ':Password';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Email = email,
            Password = password
        });

        var res = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = res.First() as IDictionary<string, object>;

        if (firstRow == null) return false;
        else return true;

    }

    public bool login(string email, string password)
    {
        if (CheckCredentials(email, password).Result) return true;
        else return false;
    }

}