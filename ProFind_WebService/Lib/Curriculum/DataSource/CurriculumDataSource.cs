using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.Curriculum.Model;
using ProFind_WebService.Lib.DataSource;

namespace ProFind_WebService.Lib.Curriculum.DataSource;

public class CurriculumDataSource
{
    readonly MySqlConnection connection;

    public CurriculumDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFCurriculum> Get(string id)
    {
        const string query =
            "SELECT Id_CU, Studies_CU, Experiences_CU, Years_CU FROM Curriculum WHERE id_CU = ':Id' LIMIT 1;";
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new {Id = id});

        var result = await connection.QueryAsync<PFCurriculum>(query, dynamicParameters);
        var firstRow = result.First();

        return firstRow;
    }

    public async Task<bool> Create(PFCurriculum curriculum)
    {
        const string query = "INSERT INTO Curriculum VALUES(':Id',':Studies',':Experiences',:Years);";
        var dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = curriculum.Id,
            Studies = curriculum.Studies,
            Experiences = curriculum.Experiences,
            Years = curriculum.Years
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFCurriculum curriculum)
    {
        const string query = "UPDATE Curriculum SET Studies_CU = ':Studies', Experiences_CU = ':Experiences', "
                             + "Years_CU = :Years WHERE Id_CU = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = curriculum.Id,
            Studies = curriculum.Studies,
            Experiences = curriculum.Experiences,
            Years = curriculum.Years
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(PFCurriculum curriculum)
    {
        const string query = "DELETE FROM Curriculum WHERE Id_CU = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = curriculum.Id
        });


        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }
}