create procedure [dbo].[Transaction_Insert] 
	@ServiceToLeadId int,
	@TransactionId int
as
begin
	insert into dbo.[Transaction]
	  (ServiceToLeadId,
	   TransactionId)
	values
	  (@ServiceToLeadId,
	   @TransactionId)
	select scope_identity()
end

