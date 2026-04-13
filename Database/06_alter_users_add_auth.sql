IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'email')
    BEGIN
        ALTER TABLE Users ADD email VARCHAR(150) NOT NULL DEFAULT ''
    END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'password_hash')
    BEGIN
        ALTER TABLE Users ADD password_hash VARCHAR(255) NOT NULL DEFAULT ''
    END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('Users') AND name = 'UQ_Users_Email')
    BEGIN
        CREATE UNIQUE INDEX UQ_Users_Email ON Users(email)
    END
GO


UPDATE Users SET email = 'john.doe@company.com' WHERE name = 'John Doe'
UPDATE Users SET email = 'jane.smith@company.com' WHERE name = 'Jane Smith'
UPDATE Users SET email = 'alex.pop@company.com' WHERE name = 'Alex Pop'