-- I often drop the table for testing and use this to re-create the table
create table Movies (
MovieId int not null primary key,
Title varchar(255) not null unique,
ReleaseDate bigint not null
);

create table Users (
UserId int not null primary key,
Username varchar(255) not null UNIQUE
);

create table Watchlist (
WatchlistId int not null primary key,
UserID int foreign key references Users(UserID),
MovieID int foreign key references Movies(MovieID)
);
