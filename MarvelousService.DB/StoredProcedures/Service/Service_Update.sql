CREATE PROCEDURE [dbo].[Service_Update]
	@Name varchar,
	@Description varchar,
	@Price decimal
AS
BEGIN
	insert into dbo.[Service]
	([Name],
	 [Description],
	 [Price])
	values
	(@Name,
	 @Description,
	 @Price)
end
