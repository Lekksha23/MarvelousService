CREATE PROCEDURE [dbo].[Service_Insert] 
	@Name varchar,
	@Description varchar,
	@OneTimePrice decimal,
	@IsDeleted bit
AS
BEGIN
	insert into dbo.[Service]
	([Name],
	 [Description],
	 [OneTimePrice],
	 [IsDeleted])
	values
	(@Name,
	 @Description,
	 @OneTimePrice,
	 @IsDeleted)
	select scope_identity()
end
