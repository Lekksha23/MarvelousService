create procedure [dbo].[Service_SelectAll]
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
end
