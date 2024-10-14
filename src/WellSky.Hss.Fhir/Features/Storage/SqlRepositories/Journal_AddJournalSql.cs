namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public partial class JournalRepository
    {
        private const string AddJournalSql = """
                                             INSERT INTO [dbo].[JOURNAL]
                                                        ([JOURNAL_UUID]
                                                        ,[CAREPLAN_UUID]
                                                        ,[CONSUMER_UUID]
                                                        ,[CREATE_DATETIME]
                                                        ,[CREATE_USER]
                                                        ,[LUPDATE_DATETIME]
                                                        ,[LUPDATE_USER]
                                                        ,[IS_RESTRICTED_ENTRY]
                                                        ,[ENTRY_DATE]
                                                        ,[ENTRY_TIME]
                                                        ,[SUBJECT]
                                                        ,[NOTES]
                                                        ,[JOURNAL_TYPE_UUID])
                                                  VALUES
                                                        (@Id
                                                        ,@CareplanId
                                                        ,@ConsumerId
                                                        ,@CreateDatetime
                                                        ,@CreateUser
                                                        ,@LupdateDatetime
                                                        ,@LupdateUser
                                                        ,@IsRestrictedEntry
                                                        ,@EntryDate
                                                        ,@EntryTime
                                                        ,@Subject
                                                        ,@Notes
                                                        ,@JournalTypeId)
                                             """;
    }
}
