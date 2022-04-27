create procedure [dbo].[Resource_Update]
	@Id int,
	@Name varchar (40),
	@Type int,
	@Description varchar (500),
	@Price decimal (6, 0)
as
begin
	update dbo.[Resource]
	set
		[Name] = @Name,
		[Type] = @Type,
		[Description] = @Description,
		[Price] = @Price
	where Id = @Id
end
