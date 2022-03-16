create procedure [dbo].[ServiceToLead_Insert]
	@Period int,
	@Price decimal,
	@Status int,
	@LeadId int,
	@ServiceId int
as
begin
	insert into dbo.[ServiceToLead]
		([Type],
		([Period],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId])
	values
		(@Period,
		 @Price,
		 @Status,
		 @LeadId,
		 @ServiceId)
	select scope_identity()
end