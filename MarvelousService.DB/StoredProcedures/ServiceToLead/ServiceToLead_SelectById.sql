CREATE PROCEDURE [dbo].[ServiceToLead_SelectById]
            @Id int
as
begin
    select
         [Period],
         [Price]
    from dbo.[ServiceToLead]
    where Id = @Id
end
