create procedure [dbo].[Resource_SoftDelete]
	@Id int,
	@IsDeleted bit
as
begin
	update [dbo].[Resource]
	set
		[IsDeleted] = @IsDeleted
	where Id = @Id
end
