create procedure [dbo].[ResourcePayment_SelectByLeadResource]
		@LeadResourceId int
as
begin
	select
		rp.Id,
		rp.TransactionId,
		lr.Id,
		lr.[Period],
	    lr.[Price],
	    lr.[Status],
		lr.[LeadId]
    from dbo.[ResourcePayment] rp inner join dbo.[LeadResource] lr ON rp.LeadResourceId = lr.Id
	where rp.LeadResourceId = @LeadResourceId
end
