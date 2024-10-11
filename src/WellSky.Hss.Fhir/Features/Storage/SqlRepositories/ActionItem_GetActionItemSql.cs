namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public partial class ActionItemRepository
    {
        // For testing purposes only
        private const string GetActionItemSql = @"
            SELECT	  
                [AI].[ACTION_ITEM_UUID] AS Id
                , [AI].[CREATE_DATETIME] AS CreateDatetime
                , [AI].[CREATE_USER] AS CreateUser
                , [AI].[LUPDATE_DATETIME] AS LupdateDatetime
                , [AI].[LUPDATE_USER] AS LupdateUser
            FROM [ACTION_ITEM] AS [AI] WITH (NOLOCK)
            WHERE [AI].[ACTION_ITEM_UUID] = @id
        ";
    }
}
