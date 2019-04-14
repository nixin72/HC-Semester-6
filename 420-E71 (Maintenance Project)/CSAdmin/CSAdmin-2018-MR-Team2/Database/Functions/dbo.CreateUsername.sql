SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[CreateUsername] 
(
	@firstName nvarchar(max),
	@lastName nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @Username nvarchar(max);

	--parse for special characters
	SET @Username = LOWER(SUBSTRING(REPLACE(@firstName,'Dr. ',''),1,1)) + 
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(LOWER(@lastName), ' ', '%')--replace space with % for later
				, CHAR(39), '') --remove single quote
       			, '-', '%') --replace hyphen with % for later
       			, 'é','e')
       			, 'î', 'i')
       			, 'û', 'u')
       			, 'â', 'a')
       			, 'ô', 'o')
       			, 'è','e')
       	
    --cut off username at the % character (spaces or dashes)
    IF CHARINDEX('%',@Username) = 0
		RETURN @Username --needs to be uncommented in production
		--RETURN 'cstest' -- This needs to be removed in production. It is included so that no spam emails are sent
		
		
	SET @Username = substring(@Username, 1, CHARINDEX('%',@Username)-1)
	-- Remove the part after the % which was used to replace a space or hyphen
	-- Return the result of the function
	
	RETURN @Username --needs to be uncommented in production
	--RETURN 'cstest' -- This needs to be removed in production. It is included so that no spam emails are sent

END
GO
