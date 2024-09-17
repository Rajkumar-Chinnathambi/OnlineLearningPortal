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
	@Courseimage varbinary(max),
	@CourseType varchar(10)
as
begin
	insert into Courses(CourseCatagory,Coursename,Coursesrc,Coursedesc,Courseimage,CourseType) values(@CourseCatagory,@Coursename,@Coursesrc,@Coursedesc,@Courseimage,@CourseType)
end

select * from Courses
exec SPI_Course 'Front End', 'HTML Basics', 'https://www.youtube.com/embed/dD2EISBDjWM', 'Introduction to HTML for beginners', 0x89504E470D0A1A0A0000000D49484452, 'Free'

exec SPI_Course 'Back End', 'Node.js Introduction', 'https://www.youtube.com/embed/TlB_eWDSMt4', 'Learn the basics of Node.js and how it works', 0x89504E470D0A1A0A0000000D49484452, 'Free'

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

insert into Quize(courseID,quize,option1,option2,option3,option4,answer) values	

(7,'Which is the correct HTML element for inserting a line break?', '<break>', '<br>', '<lb>', '<hr>', '<br>'),

(7,'What is the correct HTML for creating a hyperlink?', '<a href="http://www.example.com">Example</a>', '<link>http://www.example.com</link>', '<a>http://www.example.com</a>', '<url>http://www.example.com</url>', '<a href="http://www.example.com">Example</a>'),

(7,'Which of these elements are all <table> elements?', '<table>, <tr>, <tt>', '<table>, <tr>, <td>', '<thead>, <body>, <tr>', '<table>, <head>, <tfoot>', '<table>, <tr>, <td>'),

(7,'How can you make a numbered list in HTML?', '<ul>', '<ol>', '<dl>', '<list>', '<ol>'),

(7,'Which HTML tag is used to define an image?', '<img>', '<picture>', '<src>', '<image>', '<img>'),

(7,'How can you create a checkbox in HTML?', '<input type="checkbox">', '<input type="check">', '<input type="box">', '<checkbox>', '<input type="checkbox">'),

(7,'Which HTML tag is used to display the largest heading?', '<h1>', '<h6>', '<heading>', '<head>', '<h1>');



create procedure SPR_QuizeByCourseId
	@courseID int
as
begin
	select * from Quize where courseID=@courseID
end

exec SPR_QuizeByCourseId 4