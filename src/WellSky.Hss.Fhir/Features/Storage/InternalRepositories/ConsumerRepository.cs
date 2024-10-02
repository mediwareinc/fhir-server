using Dapper;
using Microsoft.Extensions.Logging;
using WellSky.Hss.Fhir.Features.Storage.DataModels;
using WellSky.Hss.Fhir.Features.Storage.InternalRepositories;

namespace WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.InternalRepositories
{
    public partial class ConsumerRepository : BaseRepository, IConsumerRepository
    {
        public ConsumerRepository(ILogger<ConsumerRepository> logger, IDatabaseConnectionFactory databaseConnectionFactory) : base(logger, databaseConnectionFactory)
        {
        }

        public async Task AddAsync(string deploymentId, Consumer provider)
        {
            throw new NotImplementedException();
        }

        public async Task<Consumer> GetAsync(string deploymentId, Guid id)
        {
            using var connection = await GetConnectionAsync(deploymentId);

            // Get data from DB, call A&D->FHIR mapper and return wrapper.
            var consumer = (await connection.QueryAsync<Consumer, Client, Consumer>(GetConsumerSql, (consumer, client) =>
                 {
                     consumer.Client = client;
                     return consumer;
                 },
                 param: new { id }
                 ,
                 splitOn: "Id,Id"
             )).FirstOrDefault();

            return consumer;
        }
    }
}
