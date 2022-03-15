CREATE PROCEDURE [dbo].[Service_SoftDeleted]
	@Id int,
	@IsDeleted bit
AS
BEGIN
	update into dbo.[Service]
	([IsDeleted])
	values
	(@IsDeleted)
	select scope_identity()
end
