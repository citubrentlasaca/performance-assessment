using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployerAssessmentChoiceRepository : IEmployerAssessmentChoiceRepository
    {
        private readonly DapperContext _context;

        public EmployerAssessmentChoiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployerAssessmentChoice(EmployerAssessmentChoice choice)
        {
            var sql = "INSERT INTO [dbo].[EmployerAssessmentChoice] ([ChoiceValue], [EmployerAssessmentItemId]) " +
                      "VALUES (@ChoiceValue, @EmployerAssessmentItemId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, choice);
            }
        }

        public async Task<IEnumerable<EmployerAssessmentChoiceDto>> GetAllEmployerAssessmentChoices()
        {
            var sql = "SELECT * FROM [dbo].[EmployerAssessmentChoice];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployerAssessmentChoiceDto>(sql);
            }
        }

        public async Task<EmployerAssessmentChoiceDto> GetEmployerAssessmentChoiceById(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployerAssessmentChoice] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployerAssessmentChoiceDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateEmployerAssessmentChoice(EmployerAssessmentChoice choice)
        {
            var sql = "UPDATE [dbo].[EmployerAssessmentChoice] SET " +
                      "[ChoiceValue] = @ChoiceValue " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = choice.Id,
                        ChoiceValue = choice.ChoiceValue
                    }
                );
            }
        }

        public async Task<int> DeleteEmployerAssessmentChoice(int id)
        {
            var sql = "DELETE FROM [dbo].[EmployerAssessmentChoice] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}