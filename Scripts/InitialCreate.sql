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
CREATE TABLE [Languages] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
);

CREATE TABLE [Municipalities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Phone] nvarchar(20) NULL,
    [Address] nvarchar(250) NULL,
    CONSTRAINT [PK_Municipalities] PRIMARY KEY ([Id])
);

CREATE TABLE [WasteCollectionSites] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(20) NOT NULL,
    [Address] nvarchar(250) NULL,
    [MunicipalityId] int NOT NULL,
    CONSTRAINT [PK_WasteCollectionSites] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WasteCollectionSites_Municipalities_MunicipalityId] FOREIGN KEY ([MunicipalityId]) REFERENCES [Municipalities] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(150) NOT NULL,
    [Phone] nvarchar(20) NULL,
    [SiteId] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_WasteCollectionSites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [WasteCollectionSites] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [SiteTranslations] (
    [SiteId] int NOT NULL,
    [LanguageId] int NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_SiteTranslations] PRIMARY KEY ([SiteId], [LanguageId]),
    CONSTRAINT [FK_SiteTranslations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SiteTranslations_WasteCollectionSites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [WasteCollectionSites] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] ON;
INSERT INTO [Languages] ([Id], [Code], [Name])
VALUES (1, N'ar', N'العربية'),
(2, N'en', N'English');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name', N'Phone') AND [object_id] = OBJECT_ID(N'[Municipalities]'))
    SET IDENTITY_INSERT [Municipalities] ON;
INSERT INTO [Municipalities] ([Id], [Address], [Name], [Phone])
VALUES (1, N'Nablus, Palestine', N'Nablus Municipality', N'09-2384000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name', N'Phone') AND [object_id] = OBJECT_ID(N'[Municipalities]'))
    SET IDENTITY_INSERT [Municipalities] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Code', N'MunicipalityId') AND [object_id] = OBJECT_ID(N'[WasteCollectionSites]'))
    SET IDENTITY_INSERT [WasteCollectionSites] ON;
INSERT INTO [WasteCollectionSites] ([Id], [Address], [Code], [MunicipalityId])
VALUES (1, N'Rafidia, Nablus', N'NBL-RAF', 1),
(2, N'Al-Makhfiya, Nablus', N'NBL-MAK', 1),
(3, N'Old City, Nablus', N'NBL-OLD', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Code', N'MunicipalityId') AND [object_id] = OBJECT_ID(N'[WasteCollectionSites]'))
    SET IDENTITY_INSERT [WasteCollectionSites] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FullName', N'Phone', N'SiteId') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] ON;
INSERT INTO [Employees] ([Id], [FullName], [Phone], [SiteId])
VALUES (1, N'Ahmad Khalil', N'0599111111', 1),
(2, N'Omar Nasser', N'0599222222', 2),
(3, N'Lina Saleh', N'0599333333', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FullName', N'Phone', N'SiteId') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'LanguageId', N'SiteId', N'Name') AND [object_id] = OBJECT_ID(N'[SiteTranslations]'))
    SET IDENTITY_INSERT [SiteTranslations] ON;
INSERT INTO [SiteTranslations] ([LanguageId], [SiteId], [Name])
VALUES (1, 1, N'موقع رفيديا لجمع النفايات'),
(2, 1, N'Rafidia Waste Collection Site'),
(1, 2, N'موقع المخفية لجمع النفايات'),
(2, 2, N'Al-Makhfiya Waste Collection Site'),
(1, 3, N'موقع البلدة القديمة لجمع النفايات'),
(2, 3, N'Old City Waste Collection Site');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'LanguageId', N'SiteId', N'Name') AND [object_id] = OBJECT_ID(N'[SiteTranslations]'))
    SET IDENTITY_INSERT [SiteTranslations] OFF;

CREATE UNIQUE INDEX [IX_Employees_SiteId] ON [Employees] ([SiteId]);

CREATE UNIQUE INDEX [IX_Languages_Code] ON [Languages] ([Code]);

CREATE INDEX [IX_SiteTranslations_LanguageId] ON [SiteTranslations] ([LanguageId]);

CREATE UNIQUE INDEX [IX_WasteCollectionSites_Code] ON [WasteCollectionSites] ([Code]);

CREATE INDEX [IX_WasteCollectionSites_MunicipalityId] ON [WasteCollectionSites] ([MunicipalityId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261019140818_InitialCreate', N'10.0.13');

COMMIT;
GO

