create procedure [dbo].[Service_Insert]
	@Name varchar(50),
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
		([Name], 
		 [ServiceType],
		 [Period],
	     [Price],
		 [Description],
	     [Status],
		 [LeadId],
		 [TransactionId])
	values
		(@Name,
		 @ServiceType,
		 @Period,
		 @Price,
		 @Description,
		 @Status,
		 @LeadId,
		 @TransactionId)
	select scope_identity()
end