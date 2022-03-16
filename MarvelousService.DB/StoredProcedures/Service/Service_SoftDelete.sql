create procedure [dbo].[Service_SoftDelete]
	@Id int,
	@IsDeleted bit
as
begin
	update [dbo].[Service]
	set
		[IsDeleted] = @IsDeleted
	where Id = @Id
end
