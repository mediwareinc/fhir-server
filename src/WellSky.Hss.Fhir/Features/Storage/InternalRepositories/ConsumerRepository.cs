namespace WellSky.Hss.Fhir.Features.Storage.InternalRepositories
{
    using Dapper;
    using DataModels;
    using Microsoft.Extensions.Logging;

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
