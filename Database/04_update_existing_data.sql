UPDATE Devices
SET manufacturer = 'Apple'
WHERE name LIKE '%iPhone%' OR name LIKE '%iPad%';

UPDATE Devices
SET manufacturer = 'Samsung'
WHERE name LIKE '%Samsung%';

UPDATE Devices
SET manufacturer = 'Google'
WHERE name LIKE '%Google%' OR name LIKE '%Pixel%';