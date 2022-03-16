CREATE PROCEDURE [dbo].[Period_Insert]
	@Season varchar
AS
BEGIN
	insert into dbo.Period_Insert
	([Season])
	values
	(@Season)
	select scope_identity()
end
