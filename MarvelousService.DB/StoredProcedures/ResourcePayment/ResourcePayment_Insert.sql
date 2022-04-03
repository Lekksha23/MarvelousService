create procedure [dbo].[ResourcePayment_Insert]
	@LeadResourceId int,
	@TransactionId int
as
begin
	insert into dbo.[ResourcePayment]
		([LeadResourceId],
		 [TransactionId])
	values
		(@LeadResourceId,
		 @TransactionId)
    select scope_identity()
end

