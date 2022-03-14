CREATE PROCEDURE [dbo].[ServiceToLead_SelectById]
            @Id int
as
begin
    select
         [Name], 
         [Type],
         [Period],
         [Price],
         [Description]
    from dbo.[ServiceToLead]
    where Id = @Id
end
