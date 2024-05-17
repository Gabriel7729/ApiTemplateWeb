using ApiTemplate.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiTemplate.Repositories
{
    public interface ICandidatoRepository
    {
        Task InsertCandidatoAsync(Candidato candidato);
        Task<List<Candidato>> GetAllCandidatosAsync();
    }

    public class CandidatoRepository(IConfiguration configuration) : ICandidatoRepository
    {
        private readonly string _connectionString = configuration["DATABASE_CONNECTION_STRING"] ?? "";

        public async Task InsertCandidatoAsync(Candidato candidato)
        {
            using SqlConnection connection = new(_connectionString);
            SqlCommand command = new("spInsertCandidato", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@Nombre", candidato.Nombre);
            command.Parameters.AddWithValue("@Partido", candidato.Partido);
            command.Parameters.AddWithValue("@CantidadVotos", candidato.CantidadVotos);

            // EntityBase properties
            command.Parameters.AddWithValue("@Deleted", candidato.Deleted ? 1 : 0);
            command.Parameters.AddWithValue("@DeletedDate", (object)candidato.DeletedDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedDate", candidato.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", (object)candidato.UpdatedDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedBy", (object)candidato.CreatedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@DeletedBy", (object)candidato.DeletedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedBy", (object)candidato.UpdatedBy ?? DBNull.Value);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
        public async Task<List<Candidato>> GetAllCandidatosAsync()
        {
            List<Candidato> candidatos = new();

            using SqlConnection connection = new(_connectionString);
            SqlCommand command = new("spGetAllCandidatos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Candidato candidato = new()
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Partido = reader.GetString(reader.GetOrdinal("Partido")),
                    CantidadVotos = reader.GetInt32(reader.GetOrdinal("CantidadVotos")),
                    // EntityBase properties
                    Deleted = reader.GetBoolean(reader.GetOrdinal("Deleted")),
                    DeletedDate = reader.IsDBNull(reader.GetOrdinal("DeletedDate")) ? null : reader.GetDateTime(reader.GetOrdinal("DeletedDate")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : reader.GetString(reader.GetOrdinal("CreatedBy")),
                    DeletedBy = reader.IsDBNull(reader.GetOrdinal("DeletedBy")) ? null : reader.GetString(reader.GetOrdinal("DeletedBy")),
                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                };

                candidatos.Add(candidato);
            }

            return candidatos;
        }
    }
}
