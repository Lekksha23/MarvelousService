CREATE PROCEDURE [dbo].[ServicePeriod_Insert]
	@Period varchar (20)
AS
BEGIN
	insert into [dbo].[ServicePeriod]
		([Period])
	values
		(@Period)
	select scope_identity()
end
