using ApiTemplate.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiTemplate.Repositories
{
    public interface IVotoRepository
    {
        Task InsertVotoAsync(Voto voto);
    }
    public class VotoRepository : IVotoRepository
    {
        private readonly string _connectionString;

        public VotoRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DATABASE_CONNECTION_STRING"] ?? "";
        }

        public async Task InsertVotoAsync(Voto voto)
        {
            using SqlConnection connection = new(_connectionString);
            SqlCommand command = new("spInsertVoto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@Cedula", voto.Cedula);
            command.Parameters.AddWithValue("@Nombre", voto.Nombre);
            command.Parameters.AddWithValue("@Apellido", voto.Apellido);
            command.Parameters.AddWithValue("@CandidatoVotado", voto.CandidatoVotado);

            // EntityBase properties
            command.Parameters.AddWithValue("@Deleted", voto.Deleted ? 1 : 0);
            command.Parameters.AddWithValue("@DeletedDate", (object)voto.DeletedDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedDate", voto.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", (object)voto.UpdatedDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedBy", (object)voto.CreatedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@DeletedBy", (object)voto.DeletedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedBy", (object)voto.UpdatedBy ?? DBNull.Value);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }

}
