use KasperChat
go
create table Users(
idUser int primary key,
userName nvarchar(25),
[password] nvarchar(25),
);
create table Rooms(
idRoom int primary key,
roomName nvarchar(25),
);
create table Link(
idRoom int,
idUser int,
[role] nvarchar(25),
primary key (idRoom, idUser)
);