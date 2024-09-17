create table UserDetails(
	UserId int primary key identity(1,1),
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	DateOfBith date not null,
	Gender varchar(20)not null,
	Mobile bigint,
	Email varchar(50) not null unique,
	Address varchar(50),
	City varchar(50),
	State varchar(50),
	UserName varchar(50) not null unique,
	Password varchar(max) not null,
	UserType varchar(20) not null
	);
	alter table UserDetails alter column Password varchar(max);
	create procedure SPI_User
	(
	
		@FirstName varchar(50),
		@LastName varchar(50),
		@DateOfBith date,
		@Gender varchar(20),
		@Mobile bigint,
		@Email varchar(50),
		@Address varchar(50),
		@City varchar(50),
		@State varchar(50),
		@UserName varchar(50),
		@Password varchar(50),
		@UserType varchar(20)
	)
	as
	begin
		insert into UserDetails values(@FirstName,@LastName,@DateOfBith,@Gender,@Mobile,@Email,@Address,@City,@State,@UserName,@Password,@UserType)
		
	end
	
	exec SPI_User 'testAdmin','testAdmin','2020-06-10','Male',9874561230,'testAdmin@gmail.com','Natrampalayam','Anchetty','TN','testAdmin123','testAdmin@123','Admin'
	exec SPI_User 'testUser1','testUser1','2020-06-10','Male',9874561230,'testuser@gmail.com','Natrampalayam','Anchetty','TN','testuser123','testuser@123','User'

	create procedure SPR_SingleUser
		
		@Email varchar(50),
		@Password varchar(50)		
	
	as
	begin
		select * from UserDetails where Email=@Email and Password=@Password		
	end

	



	create procedure SPU_User
	(
		@UserId int,
		@FirstName varchar(50),
		@LastName varchar(50),
		@DateOfBith date,
		@Gender varchar(20),
		@Mobile bigint,
		@Email varchar(50),
		@Address varchar(50),
		@City varchar(50),
		@State varchar(50),
		@UserName varchar(50),
		@Password varchar(50)
	)
	as
	begin
		update  UserDetails set FirstName=@FirstName,LastName=@LastName,DateOfBith=@DateOfBith,Gender=@Gender,Mobile=@Mobile,Email=@Email,Address=@Address,City=@City,State=@State,UserName=@UserName,Password=@Password where UserId=@UserId;
		
	end

	exec SPU_User	2,'testusere','test','2002-02-02','FeMale',9874563210,'srilatha@gmail.com','Karimangalam','Dharmapuri','TN','Sri','sri@12345'

	select * from UserDetails

	create procedure SPD_User
		@UserId int
	as
	begin
		delete from UserDetails where UserId = @UserId
	end

	update UserDetails set UserType="Admin" where Use
	
	create procedure SPR_AllUser
	as
	begin
		select * from UserDetails
	end
	exec SPR_AllUser

	update UserDetails set Password='Raj@12345' where UserId=1

	create procedure SPR_AllUserCount
	as
	begin
		select count(*) from UserDetails
	end

	create procedure SPR_GetUserById
		@UserId int
	as
	begin
		select * from UserDetails where UserId=@UserId
	end

	exec SPR_GetUserById 2

	select * from UserDetails