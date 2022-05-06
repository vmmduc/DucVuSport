create proc acc_Login
	@email nvarchar(50),
	@passwd nvarchar(50)
	as
	begin
	declare @count int
	declare @res bit
	select @count = COUNT(*) from tbUsers where email = @email and passwordHash = @passwd
	if @count > 0
		set @res = 1
	else 
	set @res = 0
	select @res
end
