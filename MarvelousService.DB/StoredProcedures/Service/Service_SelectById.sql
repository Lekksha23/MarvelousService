CREATE PROCEDURE [dbo].[Service_SelectById]
	 @Id int
AS
BEGIN
	SELECT 
	 [Name],
	 [Description],
	 [Price]
	FROM dbo.[Service]
	WHERE Id = @Id
END
