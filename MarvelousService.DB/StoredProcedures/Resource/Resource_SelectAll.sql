create procedure [dbo].[Resource_SelectAll]
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
	where IsDeleted = 0
end
