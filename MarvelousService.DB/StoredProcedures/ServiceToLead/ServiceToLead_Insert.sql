create procedure [dbo].[ServiceToLead_Insert]
	@Type int,
	@Period int,
	@Price decimal,
	@Status int,
	@LeadId int,
	@ServiceId int,
	@TransactionId int
as
begin
	insert into dbo.[ServiceToLead]
		([Type],
		 [Period],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId],
		 [TransactionId])
	values
		(@Type,
		 @Period,
		 @Price,
		 @Status,
		 @LeadId,
		 @ServiceId,
		 @TransactionId)
	select scope_identity()
end