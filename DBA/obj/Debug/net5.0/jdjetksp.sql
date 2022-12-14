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

CREATE TABLE [Routes] (
    [route_id] bigint NOT NULL IDENTITY,
    [point_A] bigint NOT NULL,
    [point_B] bigint NOT NULL,
    [no_of_passengers] int NOT NULL,
    [no_of_buses] int NOT NULL,
    CONSTRAINT [PK_Routes] PRIMARY KEY ([route_id])
);
GO

CREATE TABLE [Stations] (
    [station_id] bigint NOT NULL IDENTITY,
    [location] nvarchar(max) NULL,
    CONSTRAINT [PK_Stations] PRIMARY KEY ([station_id])
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

CREATE TABLE [Passengers] (
    [username] nvarchar(450) NOT NULL,
    [password] nvarchar(max) NULL,
    [source] bigint NOT NULL,
    [destination] bigint NOT NULL,
    [phone_no] bigint NOT NULL,
    [bus_id] bigint NOT NULL,
    [bus_id1] nvarchar(450) NULL,
    [route_id] bigint NULL,
    CONSTRAINT [PK_Passengers] PRIMARY KEY ([username]),
    CONSTRAINT [FK_Passengers_Buses_bus_id1] FOREIGN KEY ([bus_id1]) REFERENCES [Buses] ([bus_id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Passengers_Routes_route_id] FOREIGN KEY ([route_id]) REFERENCES [Routes] ([route_id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Buses_route_id1] ON [Buses] ([route_id1]);
GO

CREATE INDEX [IX_Passengers_bus_id1] ON [Passengers] ([bus_id1]);
GO

CREATE INDEX [IX_Passengers_route_id] ON [Passengers] ([route_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220920112540_initial-migration', N'5.0.17');
GO

COMMIT;
GO

