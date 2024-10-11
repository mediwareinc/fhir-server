namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public partial class JournalRepository
    {
        // For testing purposes only
        private const string GetJournalSql = @"
            SELECT	  
                [J].[JOURNAL_UUID] AS Id
                , [J].[CREATE_DATETIME] AS CreateDatetime
                , [J].[CREATE_USER] AS CreateUser
                , [J].[LUPDATE_DATETIME] AS LupdateDatetime
                , [J].[LUPDATE_USER] AS LupdateUser
            FROM [JOURNAL] AS [J] WITH (NOLOCK)
            WHERE [J].[JOURNAL_UUID] = @id
        ";
    }
}
