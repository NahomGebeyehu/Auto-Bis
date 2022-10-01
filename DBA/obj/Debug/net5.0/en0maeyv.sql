IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Passengers] (
    [username] nvarchar(450) NOT NULL,
    [password] nvarchar(max) NULL,
    [phone_no] bigint NOT NULL,
    CONSTRAINT [PK_Passengers] PRIMARY KEY ([username])
);
GO

CREATE TABLE [Routes] (
    [route_id] bigint NOT NULL IDENTITY,
    [route_name] nvarchar(max) NULL,
    [no_of_passengers] int NOT NULL,
    [no_of_buses] int NOT NULL,
    CONSTRAINT [PK_Routes] PRIMARY KEY ([route_id])
);
GO

CREATE TABLE [VAMs] (
    [vam_id] bigint NOT NULL IDENTITY,
    [password] nvarchar(max) NULL,
    CONSTRAINT [PK_VAMs] PRIMARY KEY ([vam_id])
);
GO

CREATE TABLE [Buses] (
    [bus_id] nvarchar(450) NOT NULL,
    [is_active] bit NOT NULL,
    [route_id] bigint NULL,
    [route_id1] bigint NULL,
    CONSTRAINT [PK_Buses] PRIMARY KEY ([bus_id]),
    CONSTRAINT [FK_Buses_Routes_route_id1] FOREIGN KEY ([route_id1]) REFERENCES [Routes] ([route_id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Buses_route_id1] ON [Buses] ([route_id1]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220924080126_init', N'5.0.17');
GO

COMMIT;
GO

