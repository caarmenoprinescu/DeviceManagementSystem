ALTER TABLE Devices ADD manufacturer VARCHAR(100) NULL
UPDATE Devices SET manufacturer = 'Unknown'
ALTER TABLE Devices ALTER COLUMN manufacturer VARCHAR(100) NOT NULL