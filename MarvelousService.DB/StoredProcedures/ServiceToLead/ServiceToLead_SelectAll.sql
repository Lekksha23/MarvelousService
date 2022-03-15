create procedure [dbo].[ServiceToLead_SelectAll]
as
begin
	select
		 [Type],
		 [Period],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId]
	from dbo.[ServiceToLead]
end