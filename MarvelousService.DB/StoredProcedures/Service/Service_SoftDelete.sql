CREATE PROCEDURE [dbo].[Service_SoftDelete]
	@IsDeleted bit
AS
BEGIN
	update into dbo.[Service]
	([IsDeleted])
	values
	(@IsDeleted)
end
