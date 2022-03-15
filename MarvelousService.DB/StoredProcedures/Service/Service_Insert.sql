CREATE PROCEDURE [dbo].[Service_Insert] 
	@Name varchar (50),
	@Description varchar (500),
	@OneTimePrice decimal (10, 0)
AS
BEGIN
	insert into dbo.[Service]
	([Name],
	 [Description],
	 [OneTimePrice])
	values
	(@Name,
	 @Description,
	 @OneTimePrice)
	select scope_identity()
end
