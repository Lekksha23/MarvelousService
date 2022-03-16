create procedure [dbo].[Service_Insert] 
	@Name varchar (50),
	@Type int,
	@Description varchar (500),
	@Price decimal (10, 0)
as
begin
	insert into dbo.[Service]
	([Name],
	 [Type],
	 [Description],
	 [Price])
	values
	(@Name,
	 @Type,
	 @Description,
	 @Price)
	select scope_identity()
end
