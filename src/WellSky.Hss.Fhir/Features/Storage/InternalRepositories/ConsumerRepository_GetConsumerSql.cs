namespace WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.InternalRepositories
{
    public partial class ConsumerRepository
    {
        private const string GetConsumerSql = @"
            WITH 
            CONSUMER_DATES AS (
                SELECT CONSUMER_UUID, MAX(LUPDATE_DATETIME) AS LUPDATE_DATETIME
                FROM (
                    SELECT CONSUMER_UUID, LUPDATE_DATETIME 
                    FROM CONSUMER WITH (NOLOCK) WHERE CONSUMER_UUID = @id
                    UNION ALL
                    SELECT CONSUMER_UUID, LUPDATE_DATETIME 
                    FROM CLIENT WITH (NOLOCK) WHERE CONSUMER_UUID = @id
                    UNION ALL
                    SELECT CONSUMER_UUID, LUPDATE_DATETIME 
                    FROM PHONE WITH (NOLOCK) WHERE CONSUMER_UUID = @id
                    UNION ALL
                    SELECT CONSUMER_UUID, LUPDATE_DATETIME 
                    FROM LOCATION WITH (NOLOCK) WHERE CONSUMER_UUID = @id
                    UNION ALL
                    SELECT CONSUMER_UUID, LUPDATE_DATETIME 
                    FROM CONTACT WITH (NOLOCK) WHERE CONSUMER_UUID = @id
                    UNION ALL
                    SELECT CONSUMER_UUID, LUPDATE_DATETIME 
                    FROM CLIENT_CAREMANAGER WITH (NOLOCK) WHERE CONSUMER_UUID = @id
                    UNION ALL
                    SELECT A.CONSUMER_UUID, B.LUPDATE_DATETIME 
                    FROM CONTACT A WITH (NOLOCK) 
                    INNER JOIN PHONE B WITH (NOLOCK) ON A.CONTACT_UUID = B.CONTACT_UUID
                    WHERE A.CONSUMER_UUID = @id
                    UNION ALL
                    SELECT A.CONSUMER_UUID, B.LUPDATE_DATETIME 
                    FROM CONTACT A WITH (NOLOCK) 
                    INNER JOIN LOCATION B WITH (NOLOCK) ON A.CONTACT_UUID = B.CONTACT_UUID
                    WHERE A.CONSUMER_UUID = @id
                ) AS T
                GROUP BY CONSUMER_UUID
            )
            SELECT	  
                [C].[CONSUMER_UUID] as Id
                , [C].[AGE]
                , [C].[AGENCY_UUID]
                , [AG].[DESCRIPTION] as AgencyDescription
                , [C].[CREATE_DATETIME]
                , [C].[CREATE_USER]
                , [C].[DOB]
                , [C].[EMP_STATUS_CODE]
                , [C].[ETHNICITY]
                , [C].[GENDER]
                , [C].[HOME_PHONE]
                , [C].[IS_ABUSED_NEG_EXP]
                , [C].[IS_ACTIVE]
                , [C].[IS_CLIENT_GROUP]
                , [C].[IS_DISABLED]
                , [C].[IS_DUP_MAIL]
                , [C].[IS_FEMALE_HOHH]
                , [C].[IS_FRAIL]
                , [C].[IS_HOMEBOUND]
                , [C].[IS_IN_POVERTY]
                , [C].[IS_LIVES_ALONE]
                , [C].[IS_MEDICARE_ELIGIBLE]					
                , [C].[IS_RURAL]
                , [C].[IS_SSI]
                , [C].[IS_STATE_RESIDENT]
                , [C].[IS_TRIBAL]
                , [C].[IS_US_CITIZEN]
                , [C].[IS_USDAMEAL_ELIGIBLE]
                , [C].[IS_VETERAN]
                , [C].[IS_VETERAN_DEPENDENT]
                , [CD].[LUPDATE_DATETIME]
                , [C].[LUPDATE_USER]
                , [C].[MARITAL_STATUS_UUID]
                , [C].[MJM_ID]										
                , [C].[PRIMARY_PHONE]
                , [C].[REASON_CODE_UUID]
                , [C].[REFERRED_BY]
                , [C].[RES_ADDRESS1]
                , [C].[RES_ADDRESS2]
                , [C].[RES_COUNTY]
                , [C].[RES_MUNICIPALITY]
                , [C].[RES_STATE]
                , [C].[RES_TOWN_NAME]
                , [C].[RES_ZIP]
                , [C].[STATUS_DATE]
                , [C].[UNDERSTANDS_ENGLISH]
                , [L].[DESCRIPTION] as Language
                , [C].[UPT_CARD_ID]	as UptCardId
                , [CL].[CONSUMER_UUID] as Id
                , [CL].[CLIENT_ID] 
                , [CL].[CREATE_DATETIME]
                , [CL].[CREATE_USER]					
                , [CL].[EMAIL_ADDR]
                , [CL].[SALUTATION]
                , [CL].[SUFFIX]
                , [CL].[FIRST_NAME]
                , [CL].[FULL_NAME]
                , [CL].[LAST_NAME]
                , [CL].[LUPDATE_DATETIME]
                , [CL].[LUPDATE_USER]					
                , [CL].[MI]
                , [CL].[SSN]
                , [CL].[MEDICAID_NO] as MedicaIdNo                                    
            FROM [CONSUMER] AS [C] WITH (NOLOCK)
            LEFT JOIN [CLIENT] AS [CL] WITH (NOLOCK) ON [C].[CONSUMER_UUID] = [CL].[CONSUMER_UUID]
            LEFT JOIN [LANGUAGE] AS [L] WITH (NOLOCK) ON [C].LANGUAGE_UUID = [L].LANGUAGE_UUID
            LEFT JOIN [AGENCY] AS [AG] WITH (NOLOCK) ON [C].AGENCY_UUID = [AG].AGENCY_UUID
            INNER JOIN [CONSUMER_DATES] AS [CD] WITH (NOLOCK) ON [C].CONSUMER_UUID = [CD].CONSUMER_UUID
            WHERE [C].[CONSUMER_UUID] = @id
        ";
    }
}
