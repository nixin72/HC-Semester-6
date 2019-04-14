SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspInsertManager]
	-- Add the parameters for the stored procedure here
	@IDUser AS INTEGER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IsValid INT;
	SET @IsValid = (
		SELECT CASE WHEN COUNT(IDUser) > 0 THEN 1 ELSE 0 END
		FROM [Applications].[UserRole] ur1
		WHERE (select COUNT(*) from [Applications].[UserRole] ur2 where ur1.IDUser = ur2.IDUser) > 0
		AND IDUser = @IDUser
		AND IsActive = 1
		AND IDRole NOT IN (SELECT IDRole FROM .[Applications].[Role] WHERE Code IN ('ASM', 'ASA'))
	)
    -- Insert statements for procedure here
	IF @IsValid = 1
		INSERT INTO [Applications].[UserRole]
		VALUES (@IDUser, (SELECT IDRole FROM [Applications].[Role] WHERE Code LIKE 'ASM'), 1)
	ELSE
		RETURN -1
END

GO
GRANT EXECUTE ON  [dbo].[uspInsertManager] TO [CSAdmin]
GO
