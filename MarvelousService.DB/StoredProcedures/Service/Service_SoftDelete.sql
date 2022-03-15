CREATE PROCEDURE [dbo].[Service_SoftDelete]
	@IsDeleted bit
AS
BEGIN
	insert into dbo.[Service]
	([IsDeleted])
	values
	(@IsDeleted)
end
