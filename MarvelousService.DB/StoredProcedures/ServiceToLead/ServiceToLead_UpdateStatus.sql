create procedure [dbo].[ServiceToLead_UpdateStatus]
	@Id int,
	@Status int
as
begin
	update dbo.ServiceToLead
	set 
		[Status] = @Status
	where Id = @Id
end
