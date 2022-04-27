create procedure [dbo].[LeadResource_Insert]
	@Period int,
	@Price decimal,
	@Status int,
	@LeadId int,
	@ResourceId int
as
begin
	insert into dbo.[LeadResource]
		([Period],
	     [Price],
	     [Status],
		 [LeadId],
		 [ResourceId],
		 [StartDate])
	values
		(@Period,
		 @Price,
		 @Status,
		 @LeadId,
		 @ResourceId,
		 getdate())
	select scope_identity()
end