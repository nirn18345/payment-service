USE PaymentsDB
GO



-- ===========================================
-- Tabla: Customer
-- ===========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Customer' AND type = 'U')
BEGIN
    CREATE TABLE Customer (
        CustomerId      UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        FullName        NVARCHAR(200) NOT NULL,
        Email           NVARCHAR(200) NOT NULL UNIQUE,
        PhoneNumber     NVARCHAR(50)  NOT NULL,
        IsActive        BIT DEFAULT 1,
        CreatedAt       DATETIME2 DEFAULT SYSUTCDATETIME(),
        CreatedBy       NVARCHAR(100),
        UpdatedAt       DATETIME2,
        UpdatedBy       NVARCHAR(100)
    );
END
GO


-- ===========================================
-- Tabla: Payment
-- ===========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Payment' AND type = 'U')
BEGIN
    CREATE TABLE Payment (
        PaymentId       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        CustomerId      UNIQUEIDENTIFIER NOT NULL,
        ServiceProvider NVARCHAR(250) NOT NULL,
        Amount          DECIMAL(18,2) NOT NULL,
		Currency		CHAR(3) NOT NULL,
		Status			NVARCHAR(200) NOT NULL,
        IsActive        BIT DEFAULT 1,
        CreatedAt       DATETIME2 DEFAULT SYSUTCDATETIME(),
        CreatedBy       NVARCHAR(100),
        UpdatedAt       DATETIME2,
        UpdatedBy       NVARCHAR(100),

        CONSTRAINT FK_Payment_Customer FOREIGN KEY (CustomerId)
            REFERENCES Customer(CustomerId)
    );
END
GO



-- ===========================================
-- Inserts Customer
-- ===========================================

INSERT INTO Customer (CustomerId, FullName, Email, PhoneNumber, IsActive, CreatedAt)
VALUES 
(NEWID(), 'Juan Pérez', 'juan.perez@example.com', '78912345', 1, SYSUTCDATETIME());

INSERT INTO Customer (CustomerId, FullName, Email, PhoneNumber, IsActive, CreatedAt)
VALUES 
(NEWID(), 'María Gómez', 'maria.gomez@example.com', '71234567', 1, SYSUTCDATETIME());

INSERT INTO Customer (CustomerId, FullName, Email, PhoneNumber, IsActive, CreatedAt)
VALUES 
(NEWID(), 'Carlos Ortega', 'carlos.ortega@example.com', '76549821', 1, SYSUTCDATETIME());


