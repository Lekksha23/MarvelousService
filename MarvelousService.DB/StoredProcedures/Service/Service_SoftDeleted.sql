CREATE PROCEDURE [dbo].[Service_SoftDeleted]
	@Id int,
	@IsDeleted bit
AS
BEGIN
	insert into dbo.[Service]
	([IsDeleted])
	values
	(@IsDeleted)
	select scope_identity()
end
