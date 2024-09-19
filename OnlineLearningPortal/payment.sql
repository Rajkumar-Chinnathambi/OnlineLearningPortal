create table Payment(
	paymentId int primary key identity(1,1),
	courseId int,
	userId int,
	paymentMode varchar(20),
	Foreign key (courseId) references Courses(CourseID),
	Foreign key (userId) references UserDetails(UserId),
	createAt datetime not null default getdate()
	)

	
create procedure SPI_Payement
	@courseId int,
	@userId int,
	@paymentMode varchar(20)
as 
begin 
	insert into Payment(courseId,userId,paymentMode) values(@courseId,@userId,@paymentMode)
end

create procedure GetPaymentByUserCourse
	@courseId int,
	@userId int
as 
begin 
	select * from Payment where courseId = @courseId and userId = @userId
end
exec GetPaymentByUserCourse 12,2

delete from Payment where paymentId = 2

select * from Payment

create procedure GetAllPayments
as
begin 
	select ud.UserName,c.Coursename,p.paymentMode,p.createAt from Payment p left Join UserDetails ud on p.userId = ud.UserId left join Courses c on p.courseId =c.CourseID 
end

exec GetAllPayments