CREATE PROCEDURE [dbo].[Service_Insert]
	@Name varchar(50),
	@ServiceType int,
	@Period int,
	@Price decimal,
	@Status int,
	@LeadId int,
	@TransactionId int
	
AS
BEGIN 
	insert into dbo.[Service]
		([Name], 
		 [ServiceType],
		 [Period],
	     [Price],
	     [Status],
		 [LeadId],
		 [TransactionId])
	values
		(@Name,
		 @ServiceType,
		 @Period,
		 @Price,
		 @Status,
		 @LeadId,
		 @TransactionId)
	select scope_identity()
END