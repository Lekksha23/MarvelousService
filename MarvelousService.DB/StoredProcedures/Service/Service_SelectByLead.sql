﻿create procedure [dbo].[Service_SelectByLead]
		@LeadId int
as
begin
	select
		 [Name], 
		 [Type],
		 [Period],
	     [Price],
		 [Description],
	     [Status],
		 [LeadId],
		 [TransactionId]
	from dbo.[Service]
	where LeadId = @LeadId
end