create procedure [dbo].[ServicePeriod_SelectById]
        @Id int,
	    @Period varchar (20)
as
begin
    select
        [Id],
        [Period]
    from dbo.[ServicePeriod]
	where Id = @Id
end