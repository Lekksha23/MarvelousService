create procedure [dbo].[ServicePayment_Insert]
	@ServiceToLeadId int,
	@TransactionId int
as
begin
	insert into dbo.[ServicePayment]
		([ServiceToLeadId],
		 [TransactionId])
	values
		(@ServiceToLeadId,
		 @TransactionId)
    select scope_identity()
end

