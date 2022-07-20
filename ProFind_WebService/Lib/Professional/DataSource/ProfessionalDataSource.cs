using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;
using ProFind_WebService.Lib.Professional.Model;

namespace ProFind_WebService.Lib.Professional.DataSource;

public class ProfessionalDataSource
{
    readonly MySqlConnection connection;

    public ProfessionalDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFProfessional> Get(string id)
    {
        const string query = "SELECT Id_P, Name_P, Date_Birth_P, Email_P, Curriculum.Studies_CU, " +
            "Curriculum.Experiences_CU, Curriculum.Years_CU, Job.Name_J FROM Professional, Curriculum, Job "
            + "WHERE (Id_P = ':ID' AND Curriculum.Id_CU = Professional.Id_CU1 AND Job.Id_J = Professional.Id_J1);";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = id
        });

        var result = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = result.First() as IDictionary<string, object>;

        return PFProfessional.FromDictionary(firstRow);
    }

    public async Task<bool> Create(PFProfessional professional)
    {
        const string query = "INSERT INTO Professional VALUES(':Id',':Name',':DateBirth',':Email',':Password',"
            + "':Curriculum',':Job');";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = professional.Id,
            Name = professional.Name,
            DateBirth = professional.DateBirth,
            Email = professional.Email,
            Password = professional.Password,
            Curriculum = professional.Curriculum.Id,
            Job = professional.Job.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFProfessional professional)
    {
        const string query = "UPDATE Professional SET Name_P = ':Name', Date_Birth_P = ':DateBirth', "
            + "Email_P = ':Email', Password_P = ':Password', Id_CU1 = ':Curriculum', Id_J1 = ':Job' "
            + "WHERE Id_P = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = professional.Id,
            Name = professional.Name,
            DateBirth = professional.DateBirth,
            Email = professional.Email,
            Password = professional.Password,
            Curriculum = professional.Curriculum.Id,
            Job = professional.Job.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);

    }

    public async Task<bool> Delete(PFProfessional professional)
    {
        const string query = "DELETE FROM Professional WHER Id_P = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = professional.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters)) > 0;
    }

    private async Task<bool> CheckCredentials(string email, string password)
    {
        const string query = "SELECT * FROM Professional " 
            + "WHERE Email_P = ':Email' AND Password_P = ':Password';";

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