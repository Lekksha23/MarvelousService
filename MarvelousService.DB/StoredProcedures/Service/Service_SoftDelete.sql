CREATE PROCEDURE [dbo].[Service_SoftDelete]
	@Id int,
	@IsDeleted bit
AS
BEGIN
	insert into dbo.[Service]
	([IsDeleted])
	values
	(@IsDeleted)
end
