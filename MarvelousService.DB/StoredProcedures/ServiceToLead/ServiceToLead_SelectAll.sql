create procedure [dbo].[ServiceToLead_SelectAll]
as
begin
	select
		 [Type],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId]
	from dbo.[ServiceToLead]
end