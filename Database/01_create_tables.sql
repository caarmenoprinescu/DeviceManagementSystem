CREATE DATABASE DeviceManagementSystem
Use DeviceManagementSystem
go

CREATE TABLE Users(
                      id int primary key identity (1,1),
                      name varchar(100) not null,
                      role varchar(50) not null,
                      location varchar(100) not null

)
go

CREATE TABLE Devices(
                        id int primary key identity (1,1),
                        name varchar(100) not null,
                        d_type varchar(10) not null,
                        os varchar(30) not null,
                        os_version varchar(30) not null,
                        processor varchar(30) not null,
                        ram int not null,
                        description varchar(255) not null,
                        current_user_id int NULL references Users(id)
)
go