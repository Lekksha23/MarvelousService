CREATE PROCEDURE [dbo].[Service_Update]
	@Name varchar,
	@Description varchar,
	@OneTimePrice decimal
AS
BEGIN
	update into dbo.[Service]
	([Name],
	 [Description],
	 [OneTimePrice])
	values
	(@Name,
	 @Description,
	 @OneTimePrice)
	select scope_identity()
end
