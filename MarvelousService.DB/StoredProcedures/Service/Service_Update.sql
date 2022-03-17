create procedure [dbo].[Service_Update]
	@Id int,
	@Name varchar (50),
	@Type int,
	@Description varchar (1000),
	@Price decimal (10, 0)
as
begin
	update dbo.[Service]
	set
		[Name] = @Name,
		[Type] = @Type,
		[Description] = @Description,
		[Price] = @Price
	where Id = @Id
end
