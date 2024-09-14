create table  Courses(
	CourseID int Primary key identity(1,1),
	CourseCatagory varchar(100),
	Coursename varchar(50),
	Coursesrc varchar(max),
	Coursedesc varchar(max),
	Courseimage varbinary(max)
	)
alter table Courses add Courseimage varbinary(max)

create procedure SPI_Course
	@CourseCatagory varchar(100),
	@Coursename varchar(50),
	@Coursesrc varchar(max),
	@Coursedesc varchar(max),
	@Courseimage varbinary(max)
as
begin
	insert into Courses(CourseCatagory,Coursename,Coursesrc,Coursedesc,Courseimage) values(@CourseCatagory,@Coursename,@Coursesrc,@Coursedesc,@Courseimage)
end

select * from Courses
exec SPI_Course 'Full Stack','Java','https://youtu.be/BGTx91t8q50?si=cnSsV1q_-KI1vNMP','Java is backend programming Language'

create procedure SPU_Course
	@CourseID int,
	@CourseCatagory varchar(100),
	@Coursename varchar(50),
	@Coursesrc varchar(max),
	@Coursedesc varchar(max),
	@Courseimage varbinary(max)
as
begin
	update Courses set CourseCatagory=@CourseCatagory,Coursename=@Coursename,Coursesrc=@Coursesrc,Coursedesc=@Coursedesc,Courseimage=@Courseimage where CourseID=@CourseID
end

create procedure SPD_Course
	@CourseID int
as
begin
	delete from Courses where CourseID=@CourseID
end


create procedure SPR_GetCourseById
	@CourseId int
as
begin
	select * from Courses where CourseID=@CourseId
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


create table Quize(
	quizeId int Primary key identity(1,1),
	courseID int,
	quize varchar(200) ,
	option1 varchar(200),
	option2 varchar(200),
	option3 varchar(200),
	option4 varchar(200),
	answer varchar(200),
	foreign key (courseID) references Courses(CourseID)
	)

select * from  Courses
insert into Quize(courseID,quize,option1,option2,option3,option4,answer) values
	(4,'What is the correct command to create a new React project?','npx create-react-app myReactApp','npm create-react-app myReactApp','npm create-react-app','npx create-react-app','npx create-react-app myReactApp')

create procedure SPR_QuizeByCourseId
	@courseID int
as
begin
	select * from Quize where courseID=@courseID
end

exec SPR_QuizeByCourseId 4