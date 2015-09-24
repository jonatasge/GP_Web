SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		VirtualPlay
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [VirtualPlay].[Sys_WebmailConfSelect]
	  @idWebMail		INT = -1
	, @idSequence		INT = -1
	, @flMenorSequence	BIT =  0
AS
BEGIN

	SET NOCOUNT ON;

	IF @flMenorSequence = 0
		BEGIN
		  SELECT [idWebmail]
			   , [idSequence]
			   , [dsFrom]
			   , [dsHost]
			   , [nbPort]
			   , [cdUserName]
			   , [cdPassword]
			   , [flEnableSsl]
			   , [flUseDefaultCredentials]
			   , [dtCreatedAt]
			   , [dtUpdatedAt]
			   , [flStatus]
			   , [nbAmountSend]
			   , [nbAmountLimit]
			FROM [VirtualPlay].[Sys_WebmailConf](NOLOCK)
		   WHERE [flStatus]    = 1
			 AND ([idWebmail]  = @idWebMail  OR @idWebMail  = -1)
			 AND ([idSequence] = @idSequence OR @idSequence = -1)
		END
	ELSE
		BEGIN
		  SELECT TOP 1
				 [idWebmail]
			   , [idSequence]
			   , [dsFrom]
			   , [dsHost]
			   , [nbPort]
			   , [cdUserName]
			   , [cdPassword]
			   , [flEnableSsl]
			   , [flUseDefaultCredentials]
			   , [dtCreatedAt]
			   , [dtUpdatedAt]
			   , [flStatus]
			   , [nbAmountSend]
			   , [nbAmountLimit]
			FROM [VirtualPlay].[Sys_WebmailConf]
		   WHERE [flStatus]		 =  1
			 AND [idWebmail]	 =  @idWebMail
			 AND [nbAmountSend] != -1
			 AND [nbAmountSend]  <  [nbAmountLimit]
		ORDER BY [idSequence]	ASC
			   , [nbAmountSend] ASC
		END

	SET NOCOUNT OFF;
END

GO

-- =============================================
-- Author:		VirtualPlay
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [VirtualPlay].[Sys_WebmailConfUpdate]
	  @idWebMail		INT
	, @idSequence		INT
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @idSequenceMin	INT = -1
		  , @idSequenceMax	INT = -1

		SELECT @idSequenceMin = MIN([idSequence])
		  FROM [VirtualPlay].[Sys_WebmailConf] (NOLOCK)
		 WHERE [idWebmail]     = @idWebMail
		   AND [idSequence]    = @idSequence

		SELECT @idSequenceMax = MAX([idSequence])
		  FROM [VirtualPlay].[Sys_WebmailConf] (NOLOCK)
		 WHERE [idWebmail]     = @idWebMail
		   AND [idSequence]    = @idSequence

		UPDATE [VirtualPlay].[Sys_WebmailConf]
		   SET [nbAmountSend] = [nbAmountSend] + 1
		 WHERE [idWebmail]    = @idWebMail
		   AND [idSequence]   = @idSequence

		IF EXISTS (SELECT 1
					 FROM [VirtualPlay].[Sys_WebmailConf]
					WHERE [idWebmail]     = @idWebMail
					  AND [idSequence]    = @idSequence
					  AND [nbAmountSend] >= [nbAmountLimit])
			BEGIN
				IF @idSequence < @idSequenceMax
					BEGIN
						UPDATE [VirtualPlay].[Sys_WebmailConf]
						   SET [nbAmountSend] = 0
						 WHERE [idWebmail]    = @idWebMail
						   AND [idSequence]   = @idSequence + 1
					END
				ELSE
					BEGIN
						UPDATE [VirtualPlay].[Sys_WebmailConf]
						   SET [nbAmountSend] = 0
						 WHERE [idWebmail]    = @idWebMail
						   AND [idSequence]   = @idSequenceMin
					END
			END

	SET NOCOUNT OFF;
END