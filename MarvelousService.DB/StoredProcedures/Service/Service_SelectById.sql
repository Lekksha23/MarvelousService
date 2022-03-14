CREATE PROCEDURE [dbo].[Service_SelectById]
            @Id int
as
begin
    select
         [Name], 
         [Type],
         [Period],
         [Price],
         [Description]
    from dbo.[Service]
    where Id = @Id
end
