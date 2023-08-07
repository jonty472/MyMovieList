-- I often drop the table for testing and use this to re-create the table
create table Movies (
ID int not null UNIQUE,
MovieTitle varchar(255) not null unique,
ReleaseYear int not null
);
