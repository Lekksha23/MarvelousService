CREATE PROCEDURE [dbo].[Service_SelectById]
            @Id int
as
begin
    select
         [ServiceName], 
         [ServiceType],
         [Period],
         [Price],
         [Description]
    from dbo.[Service]
    where Id = @Id
end
