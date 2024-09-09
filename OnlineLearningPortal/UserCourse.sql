--UserCourse
create table UserCourse(
	id int primary key identity(1,1),
	UserId int,
	CourseId int,
	Foreign key (UserId) references UserDetails(UserId),
	Foreign key (CourseId) references Courses(CourseID)
)

insert into UserCourse values(3,1)

select * from UserCourse as us full outer join UserDetails as ud on us.UserId=ud.UserId full outer join Courses as c on us.CourseId = c.CourseId where ud.UserId=3