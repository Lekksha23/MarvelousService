create procedure [dbo].[ServiceToLead_Insert]
	@ServiceName int,
	@ServiceType int,
	@Period int,
	@Price decimal,
	@Description varchar(300),
	@Status int,
	@LeadId int,
	@TransactionId int
as
begin
	insert into dbo.[ServiceToLead]
		([Name], 
		 [Type],
		 [Period],
	     [Price],
		 [Description],
	     [Status],
		 [LeadId],
		 [TransactionId])
	values
		(@ServiceName,
		 @ServiceType,
		 @Period,
		 @Price,
		 @Description,
		 @Status,
		 @LeadId,
		 @TransactionId)
	select scope_identity()
end