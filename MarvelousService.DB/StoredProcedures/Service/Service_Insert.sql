create procedure [dbo].[Service_Insert]
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
	insert into dbo.[Service]
		([ServiceName], 
		 [ServiceType],
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