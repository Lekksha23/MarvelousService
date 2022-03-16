CREATE PROCEDURE [dbo].[TransactionFromervice_Insert]
	@OneTimePrice decimal,
	@TransactionId int
AS
BEGIN
	insert into dbo.[TransactionFromervice_Insert]
	([OneTimePrice],
	 [TransactionId])
	 values
	 (@OneTimePrice,
	  @TransactionId)
select scope_identity()
end