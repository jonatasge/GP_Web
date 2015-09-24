-- =============================================
-- Author:                   Willian Barbosa
-- Create date: 2013-09-19
-- Description: Authentication
-- =============================================
CREATE PROCEDURE [VirtualPlay].[pr_login_user] @email    VARCHAR(255)
                                             , @password VARCHAR(255)
AS
  BEGIN
      SET NOCOUNT ON;

      IF EXISTS(SELECT 1
                  FROM [VirtualPlay].[Sys_User](NOLOCK)
                 WHERE dsEmail = @email)
        BEGIN
            IF EXISTS(SELECT 1
                        FROM [VirtualPlay].[Sys_User](NOLOCK)
                       WHERE dsEmail    = @email
                         AND dsPassword = @password)
              BEGIN
                  SELECT idUser
                       , login_at
                       , name
                       , zipcode
                       , @session AS session
                  FROM [VirtualPlay].[Sys_User](NOLOCK)
                  WHERE
                    participant.cpf = @cpf
                    AND participant.password = @password

			INSERT INTO [VirtualPlay].[Sys_Login]
					   ([dtLogin]
					   ,[idUser]
					   ,[dsEmail]
					   ,[flSuccess]
					   ,[cdIPAddress]
					   ,[dsAgent]
					   ,[lGuid])
				 VALUES
					   (<dtLogin, datetime,>
					   ,<idUser, int,>
					   ,<dsEmail, varchar(100),>
					   ,<flSuccess, char(1),>
					   ,<cdIPAddress, char(15),>
					   ,<dsAgent, varchar(500),>
					   ,<lGuid, nvarchar(100),>)

              END
            ELSE
              BEGIN
                  /*Invalid password*/
                  SELECT Cast(-2 AS INT)         AS id
                         ,Cast(NULL AS DATETIME) AS login_at
                         ,Cast(NULL AS VARCHAR)  AS name
                         ,Cast(NULL AS VARCHAR)  AS zipcode
                         ,Cast(NULL AS VARCHAR)  AS session
              END
        END
      ELSE
        BEGIN
            /*Invalid CPF*/
            SELECT Cast(-1 AS INT)         AS id
                   ,Cast(NULL AS DATETIME) AS login_at
                   ,Cast(NULL AS VARCHAR)  AS name
                   ,Cast(NULL AS VARCHAR)  AS zipcode
                   ,Cast(NULL AS VARCHAR)  AS session
        END

      SET NOCOUNT OFF;
  END
GO