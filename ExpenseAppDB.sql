-- Create the database
CREATE DATABASE ExpenseAppDB;
GO

-- Use the database
USE ExpenseAppDB;
GO

-- Create the Expenses table
CREATE TABLE Expenses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Description NVARCHAR(50) NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    DateAdded DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Create the Trigger table
CREATE TABLE TriggerLog (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TableName NVARCHAR(50) NOT NULL,
    OperationType NVARCHAR(10) NOT NULL,
    DateAdded DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Create a trigger on the Expenses table to log any INSERT, UPDATE or DELETE operations
CREATE TRIGGER ExpensesTrigger
ON Expenses
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @operationType NVARCHAR(10);
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @operationType = 'UPDATE';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @operationType = 'INSERT';
    ELSE
        SET @operationType = 'DELETE';

    INSERT INTO TriggerLog (TableName, OperationType)
    VALUES ('Expenses', @operationType);
END;
GO