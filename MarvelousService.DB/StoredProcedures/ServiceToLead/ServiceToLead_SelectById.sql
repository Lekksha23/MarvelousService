CREATE PROCEDURE [dbo].[ServiceToLead_SelectById]
            @Id int
as
begin
    select
         [Type],
         [Price]
    from dbo.[ServiceToLead]
    where Id = @Id
end
