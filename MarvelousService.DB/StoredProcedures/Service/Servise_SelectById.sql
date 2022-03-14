CREATE PROCEDURE [dbo].[Servise_SelectById]
	 @Id int
AS
BEGIN
	SELECT 
	 [Name],
	 [Description],
	 [OneTimePrice]
	FROM dbo.[Service]
	WHERE Id = @Id
END
