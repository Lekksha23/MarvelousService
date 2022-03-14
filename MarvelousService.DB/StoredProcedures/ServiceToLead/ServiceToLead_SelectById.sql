CREATE PROCEDURE [dbo].[ServiceToLead_SelectById]
            @Id int
as
begin
    select
         [Type],
         [Period],
         [Price]
    from dbo.[ServiceToLead]
    where Id = @Id
end
