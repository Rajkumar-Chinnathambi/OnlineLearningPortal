create table  Courses(
	CourseID int Primary key identity(1,1),
	CourseCatagory varchar(100),
	Coursename varchar(50),
	Coursesrc varchar(max),
	Coursedesc varchar(max)
	)

create procedure SPI_Course
	@CourseCatagory varchar(100),
	@Coursename varchar(50),
	@Coursesrc varchar(max),
	@Coursedesc varchar(max)
as
begin
	insert into Courses(CourseCatagory,Coursename,Coursesrc,Coursedesc) values(@CourseCatagory,@Coursename,@Coursesrc,@Coursedesc)
end

exec SPI_Course 'Full Stack','Java','https://youtu.be/BGTx91t8q50?si=cnSsV1q_-KI1vNMP','Java is backend programming Language'

create procedure SPU_Course
	@CourseID int,
	@CourseCatagory varchar(100),
	@Coursename varchar(50),
	@Coursesrc varchar(max),
	@Coursedesc varchar(max)
as
begin
	update Courses set CourseCatagory=@CourseCatagory,Coursename=@Coursename,Coursesrc=@Coursesrc,Coursedesc=@Coursedesc where CourseID=@CourseID
end

create procedure SPD_Course
	@CourseID int
as
begin
	delete from Courses where CourseID=@CourseID
end

create procedure SPR_AllCourses
as
begin
	select * from Courses
end

exec SPR_AllCourses

create procedure SPI_SingleCourse
	@CourseID int
as
begin
	select * from Courses where CourseID=@CourseID
end

exec SPI_SingleCourse 2

create procedure SPR_CourseCount
as
begin
	select count(*) from Courses 
end
exec SPR_CourseCount

