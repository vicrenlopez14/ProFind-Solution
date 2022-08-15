using Application.Models;
using Dapper;
using MySql.Data.MySqlClient;
using WebService.Lib.DataSource;
using WebService.Utils;

namespace WebService.Lib.Professional.DataSource;

public class ProfessionalDataSource
{
    readonly MySqlConnection _connection;

    public ProfessionalDataSource()
    {
        _connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFProfessional> Get(string id)
    {
        const string query = "SELECT * FROM Professional WHERE IdP = @Id;";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["Id"] = id
        });

        var result = await _connection.QueryAsync<PFProfessional>(query, dynamicParameters);
        return result.ToList()[0];
    }

    public async Task<IEnumerable<PFProfessional>> List()
    {
        const string query = "SELECT * FROM Professional;";

        var result = await _connection.QueryAsync<PFProfessional>(query);

        return result.ToList();
    }

    public async Task<bool> Create(PFProfessional professional)
    {
        const string query =
            "INSERT INTO Professional VALUES(@Id,@Name,@DateBirth,@Email,@Password, @Sex, @Curriculum, @Profession, @Department);";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["Id"] = professional.IdCU1,
            ["Name"] = professional.NameP,
            ["DateBirth"] = professional.DateBirthP,
            ["Email"] = professional.EmailP,
            ["Password"] = SHAPassword.ShaThisPassword(professional.PasswordP!),
            ["Sex"] = professional.SexP,
            ["Curriculum"] = professional.IdCU1,
            ["Profession"] = professional.IdPFS1,
            ["Department"] = professional.IdDP1
        });

        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(string id, PFProfessional professional)
    {
        const string query = "UPDATE Professional SET NameP = @Name, DateBirthP = @DateBirth, "
                             + "EmailP = @Email, PasswordP = @Password, IdCU1 = @Curriculum WHERE IdP = @Id;";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["Id"] = id,
            ["Name"] = professional.NameP,
            ["DateBirth"] = professional.DateBirthP,
            ["Email"] = professional.EmailP,
            ["Password"] = professional.PasswordP!,
            ["Curriculum"] = professional.IdCU1,
        });

        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(string id)
    {
        const string query = "DELETE FROM Professional WHERE IdP = @Id;";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["Id"] = id
        });

        return (await _connection.ExecuteAsync(query, dynamicParameters)) > 0;
    }

    public async Task<bool> Login(string email, string password)
    {
        const string query = "SELECT * FROM Professional "
                             + "WHERE EmailP = @Email AND PasswordP = @Password;";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["Email"] = email,
            ["Password"] = SHAPassword.ShaThisPassword(password)
        });

        var res = await _connection.QueryAsync(query, dynamicParameters);

        if (res == null) return false;
        return true;
    }
}