using ApiTemplate.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiTemplate.Repositories
{
    public interface IEventRecordRepository
    {
        Task InsertEventRecordAsync(EventRecord record);
    }

    public class EventRecordRepository(IConfiguration configuration) : IEventRecordRepository
    {
        private readonly string _connectionString = configuration["DATABASE_CONNECTION_STRING"] ?? "";

        public async Task InsertEventRecordAsync(EventRecord record)
        {
            using SqlConnection connection = new(_connectionString);
            SqlCommand command = new("spInsertEventRecord", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", Guid.Empty);
            command.Parameters.AddWithValue("@DocumentType", record.DocumentType);
            command.Parameters.AddWithValue("@Document", record.Document);
            command.Parameters.AddWithValue("@FirstName", record.FirstName);
            command.Parameters.AddWithValue("@LastName", record.LastName);
            command.Parameters.AddWithValue("@RegistrationDate", record.RegistrationDate);
            command.Parameters.AddWithValue("@LicensePlate", record.LicensePlate);
            command.Parameters.AddWithValue("@Registration", record.Registration);
            command.Parameters.AddWithValue("@Address", record.Address);
            command.Parameters.AddWithValue("@Injured", record.Injured);
            command.Parameters.AddWithValue("@Dead", record.Dead);
            command.Parameters.AddWithValue("@EventDate", record.EventDate);
            command.Parameters.AddWithValue("@Photo", record.Photo);
            command.Parameters.AddWithValue("@Status", record.Status);
            command.Parameters.AddWithValue("@Note", record.Note);

            // EntityBase properties
            command.Parameters.AddWithValue("@Deleted", record.Deleted ? 1 : 0);
            command.Parameters.AddWithValue("@DeletedDate", (object)record.DeletedDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedDate", record.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", (object)record.UpdatedDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedBy", (object)record.CreatedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@DeletedBy", (object)record.DeletedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedBy", (object)record.UpdatedBy ?? DBNull.Value);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }
}
