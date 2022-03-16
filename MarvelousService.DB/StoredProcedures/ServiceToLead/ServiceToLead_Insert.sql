create procedure [dbo].[ServiceToLead_Insert]
	@Type int,
	@Period int,
	@Price decimal,
	@Status int,
	@LeadId int,
	@ServiceId int
as
begin
	insert into dbo.[ServiceToLead]
		([Type],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId])
	values
		(@Type,
		 @Period,
		 @Price,
		 @Status,
		 @LeadId,
		 @ServiceId)
	select scope_identity()
end