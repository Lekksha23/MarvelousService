create procedure [dbo].[Resource_Insert] 
	@Name varchar (40),
	@Type int,
	@Description varchar (500),
	@Price decimal (6, 0),
	@IsDeleted bit
as
begin
	insert into dbo.[Resource]
	([Name],
	 [Type],
	 [Description],
	 [Price],
	 [IsDeleted])
	values
	(@Name,
	 @Type,
	 @Description,
	 @Price,
	 @IsDeleted)
	select scope_identity()
end
