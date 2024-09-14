--UserCourse
create table UserCourse(
	id int primary key identity(1,1),
	UserId int,
	CourseId int,
	EnrollStatus varchar(20),
	Foreign key (UserId) references UserDetails(UserId),
	Foreign key (CourseId) references Courses(CourseID)
)

insert into UserCourse values(3,1)
select * from UserCourse
create procedure SPI_UserCourse
	@UserId int,
	@CourseId int,
	@EnrollStatus varchar(20)
	
as
begin 
	insert into UserCourse(UserId,CourseId,EnrollStatus) values(@UserId,@CourseId,@EnrollStatus)
end

--check user already enroll or not
create procedure SPR_ChecKUserByCourse
	@UserId int,
	@CourseId int
as
begin
 select * from UserCourse where UserId = @UserId and CourseId=@CourseId
end

select * from UserCourse
create procedure SPR_GetCourseByUser
	@UserId int
as
begin
	select c.CourseID,c.Coursesrc,c.Coursename,c.CourseCatagory,c.Coursedesc from UserCourse as us full outer join UserDetails as ud on us.UserId=ud.UserId full outer join Courses as c on us.CourseId = c.CourseId where ud.UserId=@UserId
end
exec SPR_GetCourseByUser @UserId=4;
select * from UserCourse

drop procedure SPR_GetCourseByUser

select * from UserDetails
select * from Courses
select * from UserCourse
create procedure SPR_UnEnrollCourse
	@UserId int
as
begin
select * from Courses where CourseID not in (select COALESCE(c.CourseID,0) from UserCourse as us full outer join UserDetails as ud on us.UserId=ud.UserId full outer join Courses as c on us.CourseId = c.CourseId where ud.UserId=4)
end
drop procedure SPR_UnEnrollCourse
exec SPR_UnEnrollCourse 4

create procedure SPD_UserCourse
	@CourseID int,
	@UserId int
as
begin
	delete from UserCourse where CourseId = @CourseID and UserId = @UserId
end

create table VideoTable(
	videoId int identity(1,1),
	videoName varchar(50),
	videoData varbinary(max),
	fileExt varchar(10)
)

create procedure SPI_Video
	@videoName varchar(50),
	@videoData varbinary(max),
	@fileExt varchar(10)
as
begin
	insert into VideoTable(videoName,videoData,fileExt) values(@videoName,@videoData,@fileExt)
end

create procedure SPR_Video
	@videoId int
as
begin
	select * from VideoTable where videoId = @videoId
end

create procedure SPR_GetAllVideo
as
begin
	select * from VideoTable
end

select * from VideoTable

delete from VideoTable where videoId=2

create procedure GetPendingApprovalCourse
as
begin
	select uc.id Id,uc.UserId UserId,uc.CourseId CourseId,ud.UserName UserName,c.Coursename CourseName  from UserCourse uc inner join UserDetails ud on uc.UserId=ud.UserId
	inner join Courses c on uc.CourseId=c.CourseID where uc.EnrollStatus='Pending'
end

create procedure UpdateApprovalCourse
	@id int,
	@EnrollStatus varchar(20)
as
begin
	update UserCourse set EnrollStatus=@EnrollStatus where id=@id
end

select * from UserCourse;

drop procedure SPR_GetCourseByUser

create procedure SPR_GetCourseByUser
	@UserId int
as
begin
	select uc.CourseId CourseId,c.Coursename CourseName,c.Courseimage Courseimage,c.Coursedesc Coursedesc,uc.EnrollStatus Enroll from UserCourse uc inner join UserDetails ud on uc.UserId=ud.UserId
	inner join Courses c on uc.CourseId=c.CourseID where uc.UserId=@UserId
end

exec SPR_GetCourseByUser 2

select * from UserDetails