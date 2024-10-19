create table ccUsers(
User_id	int identity(1,1) primary key,
Login	nvarchar(max),
Nombres	nvarchar(max),
ApellidoPaterno	nvarchar(max),
ApellidoMaterno	nvarchar(max),
Password	 nvarchar(max),
TipoUser_id	int,
Status	int,
fCreate	datetime,
IDArea	int,
LastLoginAttempt datetime
)

create table ccloglogin
(
	Log_id INT IDENTITY(1,1) PRIMARY KEY,
	User_id  int,
	Extension int,
	TipoMov int,
	fecha datetime,
	CONSTRAINT FK_ccloglogin_ccUsers FOREIGN KEY (User_id) REFERENCES ccUsers(User_id)
)

create table ccRIACat_Areas
(
IDArea int identity(1,1) primary key,
AreaName	nvarchar(max),
StatusArea	bit,
CreateDate datetime
)

ALTER TABLE ccUsers
ADD CONSTRAINT FK_ccUsers_ccRIACat_Areas 
FOREIGN KEY (IDArea) 
REFERENCES ccRIACat_Areas(IDArea);


