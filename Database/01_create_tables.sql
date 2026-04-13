IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DeviceManagementSystem')
BEGIN
    CREATE DATABASE DeviceManagementSystem
END
GO

USE DeviceManagementSystem
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
CREATE TABLE Users(
                      id int primary key identity (1,1),
                      name varchar(100) not null,
                      role varchar(50) not null,
                      location varchar(100) not null
)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Devices')
BEGIN
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
END
GO