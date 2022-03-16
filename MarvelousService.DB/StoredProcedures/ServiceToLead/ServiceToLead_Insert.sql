create procedure [dbo].[ServiceToLead_Insert]
	@PeriodId int,
	@Price decimal,
	@Status int,
	@LeadId int,
	@ServiceId int
as
begin
	insert into dbo.[ServiceToLead]
		([PeriodId],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId])
	values
		(@PeriodId,
		 @Price,
		 @Status,
		 @LeadId,
		 @ServiceId)
	select scope_identity()
end