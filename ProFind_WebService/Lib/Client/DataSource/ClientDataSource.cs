using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.Client.Model;
using ProFind_WebService.Lib.DataSource;

namespace ProFind_WebService.Lib.Client.DataSource;

public class ClientDataSource
{
    readonly MySqlConnection connection;

    public ClientDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFClient> Get(string id)
    {
        const string query = "SELECT * FROM Client WHERE Id_C = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = id
        });

        var result = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = result.First() as IDictionary<string, object>;

        return PFClient.FromDictionary(firstRow);
    }

    public async Task<bool> Create(PFClient client)
    {
        const string query = "INSERT INTO Client VALUES (':Id', ':Name', ':Email', ':Password', ':CreditCard');";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            Password = client.Password,
            CreditCard = client.CreditCard
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFClient client)
    {
        const string query = "UPDATE Client SET Name_C = ':Name', Email_C = ':Email', Password_C = ':Password', "
            + "CreditCard_C = ':CreditCard' WHERE Id_C = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            Password = client.Password,
            CreditCard = client.CreditCard
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(PFClient client)
    {
        const string query = "DELETE FROM Client WHERE Id_C = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = client.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    private async Task<bool> CheckCredentials(string email, string password)
    {
        const string query = "SELECT * FROM Client WHERE Email_C = ':Email' AND Password_C = ':Password';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Email = email,
            Password = password
        });

        var res = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = res.First() as IDictionary<string,object>;

        if (firstRow == null) return false;
        else return true;
    }

    public bool login(string email, string password)
    {
        if (CheckCredentials(email, password).Result) return true;
        else return false;
    }

    private async Task <bool> CheckEmail(string email)
    {
        const string query = "SELECT * FROM Client WHERE Email_C = ':Email';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Email = email
        });

        var res = await connection.QueryAsync(query, dynamicParameters);
        var emailRow = res.First() as IDictionary<string,object>;

        if (emailRow == null) return true;
        else return false;
    }

    private bool CheckPassword(string password)
    {
        if (password.Length <= 5) return false;

        bool letter = false; bool number = false; bool upper = false;

        foreach (char character in password)
        {
            if (Char.IsLetter(character)) letter = true;
            if (Char.IsNumber(character)) number = true;
            if (Char.IsUpper(character)) upper = true;
        }
        return (letter && number && upper);
    }

    public bool register(string name, string email, string password, string creditcard)
    {
        if (CheckEmail(email).Result && CheckPassword(password))
        {
            string id = Nanoid.Nanoid.Generate();

            PFClient client = new PFClient(id,name,email,password,creditcard);

            if (Create(client).Result) return true;
            else return false;
        }
        else return false;
    }

}