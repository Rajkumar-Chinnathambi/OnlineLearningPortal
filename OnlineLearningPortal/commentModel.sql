create table tblContactComment(
	commentid int primary key identity(1,1),
	name varchar(30),
	email varchar(50),
	country varchar(50),
	message varchar(100)
)

create procedure SPI_ContactComment
	@name varchar(30),
	@email varchar(50),
	@country varchar(50),
	@message varchar(100)
as
begin
	insert into tblContactComment(name,email,country,message) values(@name,@email,@country,@message)
end

create procedure SPR_AllContactComment
as
begin
	select * from tblContactComment
end

exec SPR_AllContactComment