create procedure [dbo].[LeadResource_UpdateStatus]
	@Id int,
	@Status int
as
begin
	update dbo.LeadResource
	set 
		[Status] = @Status
	where Id = @Id
end
