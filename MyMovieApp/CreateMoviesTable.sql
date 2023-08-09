-- I often drop the table for testing and use this to re-create the table
create table Movies (
MovieID int not null primary key,
MovieTitle varchar(255) not null unique,
ReleaseYear int not null
);

create table Users (
UserID int not null primary key,
Username varchar(255) not null UNIQUE
);

create table Watchlist (
WatchlistID int not null primary key,
UserID int foreign key references Users(UserID),
MovieID int foreign key references Movies(MovieID)
);
