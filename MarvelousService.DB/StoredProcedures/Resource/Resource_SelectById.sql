create procedure [dbo].[Resource_SelectById]
	 @Id int
as
begin
	select
		[Id],
		[Name],
		[Type],
		[Description],
		[Price],
		[IsDeleted]
	from dbo.[Resource]
	where Id = @Id
end