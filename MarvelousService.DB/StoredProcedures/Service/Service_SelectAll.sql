create procedure [dbo].[Service_SelectAll]
as
begin
	select
	 [Id],
	 [Name],
     [Type],
     [Description],
     [Price]
	from dbo.[Service]
end
