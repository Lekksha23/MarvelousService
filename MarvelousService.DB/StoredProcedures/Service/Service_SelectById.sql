create procedure [dbo].[Service_SelectById]
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
	from dbo.[Service]
	where Id = @Id
end