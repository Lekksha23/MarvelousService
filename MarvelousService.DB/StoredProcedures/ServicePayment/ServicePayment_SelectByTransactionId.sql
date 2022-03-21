create procedure [dbo].[ServicePayment_SelectByTransactionId]
		@TransactionId int
as
begin
	select
		sp.Id,
		sp.TransactionId,
		sl.Id,
		sl.[Period],
	    sl.[Price],
	    sl.[Status],
		sl.[LeadId]
    from dbo.[ServicePayment] sp inner join dbo.[ServiceToLead] sl ON sp.ServiceToLeadId = sl.Id
	where sp.TransactionId = @TransactionId
end
