create procedure [dbo].[TransactionFromService_Insert]
	@ServiceToLeadId int,
	@TransactionId int,
	@Price decimal(10,0)
as
begin
	insert into dbo.[TransactionFromService]
		([ServiceToLeadId],
		 [TransactionId],
		 [Price])
	values
		(@ServiceToLeadId,
		 @TransactionId,
		 @Price)
    select scope_identity()
end

