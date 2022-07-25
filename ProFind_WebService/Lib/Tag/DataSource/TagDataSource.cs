using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;

namespace ProFind_WebService.Lib.Tag.DataSource;

public class TagDataSource
{
    readonly MySqlConnection _connection;

    public TagDataSource()
    {
        _connection = new MySqlDataSourceLink().getConnection();
    }

    // public async Task<PFProfessional> Get(string id)
    // {
    // }
    //
    // public async Task<bool> Create(PFProfessional professional)
    // {
    // }
    //
    // public async Task<bool> Update(PFProfessional professional)
    // {
    // }
    //
    // public async Task<bool> Delete(PFProfessional professional)
    // {
    // }
}