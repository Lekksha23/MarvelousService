CREATE PROCEDURE [dbo].[Service_Insert] 
	@Name varchar (50),
	@Description varchar (500),
	@Price decimal (10, 0)
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
	select scope_identity()
end
