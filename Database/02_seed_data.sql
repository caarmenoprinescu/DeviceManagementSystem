USE DeviceManagementSystem
GO


IF NOT EXISTS (SELECT 1 FROM Users WHERE name = 'John Doe')
    INSERT INTO Users (name, role, location)
    VALUES ('John Doe', 'employee', 'Cluj-Napoca')

IF NOT EXISTS (SELECT 1 FROM Users WHERE name = 'Jane Smith')
    INSERT INTO Users (name, role, location)
    VALUES ('Jane Smith', 'admin', 'Bucharest')

IF NOT EXISTS (SELECT 1 FROM Users WHERE name = 'Alex Pop')
    INSERT INTO Users (name, role, location)
    VALUES ('Alex Pop', 'employee', 'Timisoara')
GO


IF NOT EXISTS (SELECT 1 FROM Devices WHERE name = 'iPhone 15 Pro')
    INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id)
    VALUES ('iPhone 15 Pro', 'phone', 'iOS', '17.0', 'A17 Pro', 8, 'Apple flagship phone', 1)

IF NOT EXISTS (SELECT 1 FROM Devices WHERE name = 'Samsung Galaxy S24')
    INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id)
    VALUES ('Samsung Galaxy S24', 'phone', 'Android', '14.0', 'Snapdragon 8 Gen 3', 12, 'Samsung flagship phone', NULL)

IF NOT EXISTS (SELECT 1 FROM Devices WHERE name = 'iPad Pro 12.9')
    INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id)
    VALUES ('iPad Pro 12.9', 'tablet', 'iPadOS', '17.0', 'M2', 16, 'Apple pro tablet', 2)

IF NOT EXISTS (SELECT 1 FROM Devices WHERE name = 'Google Pixel 8')
    INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id)
    VALUES ('Google Pixel 8', 'phone', 'Android', '14.0', 'Google Tensor G3', 8, 'Google stock Android phone', 3)

IF NOT EXISTS (SELECT 1 FROM Devices WHERE name = 'Samsung Galaxy Tab S9')
    INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id)
    VALUES ('Samsung Galaxy Tab S9', 'tablet', 'Android', '14.0', 'Snapdragon 8 Gen 2', 12, 'Samsung Android tablet', NULL)
GO