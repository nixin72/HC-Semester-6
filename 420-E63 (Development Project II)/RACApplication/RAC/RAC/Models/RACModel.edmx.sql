
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/21/2018 10:39:04
-- Generated from EDMX file: C:\Users\1435792\Source\Workspaces\RACApplication\RAC\RAC\Models\RACModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RAC2017TEST];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RACRequestCompetencyElementsRACRequestCompetency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequestCompetencyElements] DROP CONSTRAINT [FK_RACRequestCompetencyElementsRACRequestCompetency];
GO
IF OBJECT_ID(N'[dbo].[FK_RACRequestCompetencyRACRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequestCompetencies] DROP CONSTRAINT [FK_RACRequestCompetencyRACRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_RACRequestCompetencyCompetencyComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequestCompetencyComment] DROP CONSTRAINT [FK_RACRequestCompetencyCompetencyComment];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateRACRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Candidate] DROP CONSTRAINT [FK_CandidateRACRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCompetencyComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequestCompetencyComment] DROP CONSTRAINT [FK_UserCompetencyComment];
GO
IF OBJECT_ID(N'[dbo].[FK_RACRequestUploadedDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UploadedDocument] DROP CONSTRAINT [FK_RACRequestUploadedDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_RACAdvisorNotificationsCandidate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACAdvisorNotifications] DROP CONSTRAINT [FK_RACAdvisorNotificationsCandidate];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateFinalizedRACContracts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinalizedRACContracts] DROP CONSTRAINT [FK_CandidateFinalizedRACContracts];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseMinistryDataCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_CourseMinistryDataCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_CourseProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramMinistryDataProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programs] DROP CONSTRAINT [FK_ProgramMinistryDataProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetencyMinistryDataCompetency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Competencies] DROP CONSTRAINT [FK_CompetencyMinistryDataCompetency];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetencyProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Competencies] DROP CONSTRAINT [FK_CompetencyProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramRACRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequest] DROP CONSTRAINT [FK_ProgramRACRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetencyRACRequestCompetency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequestCompetencies] DROP CONSTRAINT [FK_CompetencyRACRequestCompetency];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetencyCompetencyElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompetencyElement] DROP CONSTRAINT [FK_CompetencyCompetencyElement];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetencyElementRACRequestCompetencyElements]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RACRequestCompetencyElements] DROP CONSTRAINT [FK_CompetencyElementRACRequestCompetencyElements];
GO
IF OBJECT_ID(N'[dbo].[FK_ElementMinistryDataCompetencyElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompetencyElement] DROP CONSTRAINT [FK_ElementMinistryDataCompetencyElement];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseCompetencyCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseCompetencies] DROP CONSTRAINT [FK_CourseCompetencyCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseCompetencyCompetency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseCompetencies] DROP CONSTRAINT [FK_CourseCompetencyCompetency];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__34BEB830]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Membership] DROP CONSTRAINT [FK__aspnet_Me__Appli__34BEB830];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pa__Appli__6DF7358C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Paths] DROP CONSTRAINT [FK__aspnet_Pa__Appli__6DF7358C];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__5713D034]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Roles] DROP CONSTRAINT [FK__aspnet_Ro__Appli__5713D034];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__20B7BF83]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Users] DROP CONSTRAINT [FK__aspnet_Us__Appli__20B7BF83];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__35B2DC69]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Membership] DROP CONSTRAINT [FK__aspnet_Me__UserI__35B2DC69];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__75985754]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers] DROP CONSTRAINT [FK__aspnet_Pe__PathI__75985754];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__7B5130AA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] DROP CONSTRAINT [FK__aspnet_Pe__PathI__7B5130AA];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__UserI__7C4554E3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] DROP CONSTRAINT [FK__aspnet_Pe__UserI__7C4554E3];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pr__UserI__4BA21D88]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Profile] DROP CONSTRAINT [FK__aspnet_Pr__UserI__4BA21D88];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_aspnet_UsersInRoles_aspnet_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_aspnet_UsersInRoles_aspnet_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRole];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_Candidate_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Candidate] DROP CONSTRAINT [FK_Candidate_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ContentSpecialist_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_ContentSpecialist] DROP CONSTRAINT [FK_ContentSpecialist_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RACRequest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RACRequest];
GO
IF OBJECT_ID(N'[dbo].[UploadedDocument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UploadedDocument];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[Programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programs];
GO
IF OBJECT_ID(N'[dbo].[Competencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Competencies];
GO
IF OBJECT_ID(N'[dbo].[RACRequestCompetencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RACRequestCompetencies];
GO
IF OBJECT_ID(N'[dbo].[RACRequestCompetencyElements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RACRequestCompetencyElements];
GO
IF OBJECT_ID(N'[dbo].[CompetencyElement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompetencyElement];
GO
IF OBJECT_ID(N'[dbo].[RACRequestCompetencyComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RACRequestCompetencyComment];
GO
IF OBJECT_ID(N'[dbo].[ValidationKeys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidationKeys];
GO
IF OBJECT_ID(N'[dbo].[FinalizedRACContracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinalizedRACContracts];
GO
IF OBJECT_ID(N'[dbo].[RACAdvisorNotifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RACAdvisorNotifications];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[CourseCompetencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseCompetencies];
GO
IF OBJECT_ID(N'[dbo].[ElementMinistryDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElementMinistryDatas];
GO
IF OBJECT_ID(N'[dbo].[CompetencyMinistryDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompetencyMinistryDatas];
GO
IF OBJECT_ID(N'[dbo].[ProgramMinistryData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramMinistryData];
GO
IF OBJECT_ID(N'[dbo].[CourseMinistryData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseMinistryData];
GO
IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Applications];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Membership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Membership];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Paths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Paths];
GO
IF OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_PersonalizationAllUsers];
GO
IF OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_PersonalizationPerUser];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Profile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Profile];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Roles];
GO
IF OBJECT_ID(N'[dbo].[aspnet_SchemaVersions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_SchemaVersions];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Users];
GO
IF OBJECT_ID(N'[dbo].[aspnet_WebEvent_Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_WebEvent_Events];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[User_Candidate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_Candidate];
GO
IF OBJECT_ID(N'[dbo].[User_ContentSpecialist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_ContentSpecialist];
GO
IF OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_UsersInRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RACRequest'
CREATE TABLE [dbo].[RACRequest] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubmissionDate] datetime  NULL,
    [StartDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [IsGenEdOnly] bit  NOT NULL,
    [ProgramId] int  NOT NULL
);
GO

-- Creating table 'UploadedDocument'
CREATE TABLE [dbo].[UploadedDocument] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Binary] varbinary(max)  NOT NULL,
    [RACRequestId] int  NOT NULL,
    [DateUploaded] datetime  NOT NULL,
    [IsAddedByRACAdvisor] bit  NOT NULL,
    [IsOpened] bit  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [HomePhone] nvarchar(max)  NULL,
    [WorkPhone] nvarchar(max)  NULL,
    [UserType] int  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Salt] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NOT NULL,
    [ProgramMinistryDataId] int  NOT NULL
);
GO

-- Creating table 'Competencies'
CREATE TABLE [dbo].[Competencies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompetencyMinistryDataId] int  NOT NULL,
    [ProgramId] int  NOT NULL
);
GO

-- Creating table 'RACRequestCompetencies'
CREATE TABLE [dbo].[RACRequestCompetencies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RACRequestId] int  NOT NULL,
    [CompetencyId] int  NOT NULL
);
GO

-- Creating table 'RACRequestCompetencyElements'
CREATE TABLE [dbo].[RACRequestCompetencyElements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Answer] int  NOT NULL,
    [RACRequestCompetencyId] int  NOT NULL,
    [CompetencyElementId] int  NOT NULL
);
GO

-- Creating table 'CompetencyElement'
CREATE TABLE [dbo].[CompetencyElement] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompetencyId] int  NOT NULL,
    [ElementMinistryDataId] int  NOT NULL,
    [DateAdded] datetime  NOT NULL,
    [DateExpired] datetime  NULL
);
GO

-- Creating table 'RACRequestCompetencyComment'
CREATE TABLE [dbo].[RACRequestCompetencyComment] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [RACRequestCompetencyId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'ValidationKeys'
CREATE TABLE [dbo].[ValidationKeys] (
    [Id] int  NOT NULL,
    [UniqueKey] nvarchar(max)  NOT NULL,
    [DateAdded] datetime  NOT NULL
);
GO

-- Creating table 'FinalizedRACContracts'
CREATE TABLE [dbo].[FinalizedRACContracts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Binary] varbinary(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateUploaded] datetime  NOT NULL,
    [Candidate_Id] int  NOT NULL
);
GO

-- Creating table 'RACAdvisorNotifications'
CREATE TABLE [dbo].[RACAdvisorNotifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NotificationType] smallint  NOT NULL,
    [CandidateId] int  NOT NULL,
    [Time] datetime  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CourseMinistryDataId] int  NOT NULL,
    [ProgramId] int  NOT NULL
);
GO

-- Creating table 'CourseCompetencies'
CREATE TABLE [dbo].[CourseCompetencies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CourseId] int  NOT NULL,
    [CompetencyId] int  NOT NULL
);
GO

-- Creating table 'ElementMinistryDatas'
CREATE TABLE [dbo].[ElementMinistryDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [MinistryId] int  NULL,
    [MinistryCode] nvarchar(max)  NOT NULL,
    [DateAdded] datetime  NOT NULL,
    [DateExpired] datetime  NULL
);
GO

-- Creating table 'CompetencyMinistryDatas'
CREATE TABLE [dbo].[CompetencyMinistryDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsGenEd] bit  NOT NULL,
    [MinistryId] int  NOT NULL,
    [MinistryCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProgramMinistryData'
CREATE TABLE [dbo].[ProgramMinistryData] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [MinistryId] int  NULL,
    [MinistryCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CourseMinistryData'
CREATE TABLE [dbo].[CourseMinistryData] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [MinistryId] int  NULL,
    [MinistryCode] nvarchar(max)  NOT NULL,
    [CourseType] int  NOT NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'aspnet_Applications'
CREATE TABLE [dbo].[aspnet_Applications] (
    [ApplicationName] nvarchar(256)  NOT NULL,
    [LoweredApplicationName] nvarchar(256)  NOT NULL,
    [ApplicationId] uniqueidentifier  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'aspnet_Membership'
CREATE TABLE [dbo].[aspnet_Membership] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [PasswordFormat] int  NOT NULL,
    [PasswordSalt] nvarchar(128)  NOT NULL,
    [MobilePIN] nvarchar(16)  NULL,
    [Email] nvarchar(256)  NULL,
    [LoweredEmail] nvarchar(256)  NULL,
    [PasswordQuestion] nvarchar(256)  NULL,
    [PasswordAnswer] nvarchar(128)  NULL,
    [IsApproved] bit  NOT NULL,
    [IsLockedOut] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NOT NULL,
    [LastPasswordChangedDate] datetime  NOT NULL,
    [LastLockoutDate] datetime  NOT NULL,
    [FailedPasswordAttemptCount] int  NOT NULL,
    [FailedPasswordAttemptWindowStart] datetime  NOT NULL,
    [FailedPasswordAnswerAttemptCount] int  NOT NULL,
    [FailedPasswordAnswerAttemptWindowStart] datetime  NOT NULL,
    [Comment] nvarchar(max)  NULL
);
GO

-- Creating table 'aspnet_Paths'
CREATE TABLE [dbo].[aspnet_Paths] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NOT NULL,
    [Path] nvarchar(256)  NOT NULL,
    [LoweredPath] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'aspnet_PersonalizationAllUsers'
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers] (
    [PathId] uniqueidentifier  NOT NULL,
    [PageSettings] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_PersonalizationPerUser'
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser] (
    [Id] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NULL,
    [UserId] uniqueidentifier  NULL,
    [PageSettings] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_Profile'
CREATE TABLE [dbo].[aspnet_Profile] (
    [UserId] uniqueidentifier  NOT NULL,
    [PropertyNames] nvarchar(max)  NOT NULL,
    [PropertyValuesString] nvarchar(max)  NOT NULL,
    [PropertyValuesBinary] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_Roles'
CREATE TABLE [dbo].[aspnet_Roles] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [RoleId] uniqueidentifier  NOT NULL,
    [RoleName] nvarchar(256)  NOT NULL,
    [LoweredRoleName] nvarchar(256)  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'aspnet_SchemaVersions'
CREATE TABLE [dbo].[aspnet_SchemaVersions] (
    [Feature] nvarchar(128)  NOT NULL,
    [CompatibleSchemaVersion] nvarchar(128)  NOT NULL,
    [IsCurrentVersion] bit  NOT NULL
);
GO

-- Creating table 'aspnet_Users'
CREATE TABLE [dbo].[aspnet_Users] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [LoweredUserName] nvarchar(256)  NOT NULL,
    [MobileAlias] nvarchar(16)  NULL,
    [IsAnonymous] bit  NOT NULL,
    [LastActivityDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_WebEvent_Events'
CREATE TABLE [dbo].[aspnet_WebEvent_Events] (
    [EventId] char(32)  NOT NULL,
    [EventTimeUtc] datetime  NOT NULL,
    [EventTime] datetime  NOT NULL,
    [EventType] nvarchar(256)  NOT NULL,
    [EventSequence] decimal(19,0)  NOT NULL,
    [EventOccurrence] decimal(19,0)  NOT NULL,
    [EventCode] int  NOT NULL,
    [EventDetailCode] int  NOT NULL,
    [Message] nvarchar(1024)  NULL,
    [ApplicationPath] nvarchar(256)  NULL,
    [ApplicationVirtualPath] nvarchar(256)  NULL,
    [MachineName] nvarchar(256)  NOT NULL,
    [RequestUrl] nvarchar(1024)  NULL,
    [ExceptionType] nvarchar(256)  NULL,
    [Details] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'User_Candidate'
CREATE TABLE [dbo].[User_Candidate] (
    [Street] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [Province] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [PreferedMethodOfContact] int  NULL,
    [IsAccountValidated] bit  NULL,
    [IsGenEdOnly] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [IsArchived] bit  NOT NULL,
    [IsPrivacyPolicyAccepted] bit  NOT NULL,
    [DateRegistered] datetime  NOT NULL,
    [RegistrationIP] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [RACRequest_Id] int  NOT NULL
);
GO

-- Creating table 'User_ContentSpecialist'
CREATE TABLE [dbo].[User_ContentSpecialist] (
    [DepartmentId] int  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'aspnet_UsersInRoles'
CREATE TABLE [dbo].[aspnet_UsersInRoles] (
    [aspnet_Roles_RoleId] uniqueidentifier  NOT NULL,
    [aspnet_Users_UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RACRequest'
ALTER TABLE [dbo].[RACRequest]
ADD CONSTRAINT [PK_RACRequest]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UploadedDocument'
ALTER TABLE [dbo].[UploadedDocument]
ADD CONSTRAINT [PK_UploadedDocument]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Competencies'
ALTER TABLE [dbo].[Competencies]
ADD CONSTRAINT [PK_Competencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RACRequestCompetencies'
ALTER TABLE [dbo].[RACRequestCompetencies]
ADD CONSTRAINT [PK_RACRequestCompetencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RACRequestCompetencyElements'
ALTER TABLE [dbo].[RACRequestCompetencyElements]
ADD CONSTRAINT [PK_RACRequestCompetencyElements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompetencyElement'
ALTER TABLE [dbo].[CompetencyElement]
ADD CONSTRAINT [PK_CompetencyElement]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RACRequestCompetencyComment'
ALTER TABLE [dbo].[RACRequestCompetencyComment]
ADD CONSTRAINT [PK_RACRequestCompetencyComment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ValidationKeys'
ALTER TABLE [dbo].[ValidationKeys]
ADD CONSTRAINT [PK_ValidationKeys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FinalizedRACContracts'
ALTER TABLE [dbo].[FinalizedRACContracts]
ADD CONSTRAINT [PK_FinalizedRACContracts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RACAdvisorNotifications'
ALTER TABLE [dbo].[RACAdvisorNotifications]
ADD CONSTRAINT [PK_RACAdvisorNotifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CourseCompetencies'
ALTER TABLE [dbo].[CourseCompetencies]
ADD CONSTRAINT [PK_CourseCompetencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ElementMinistryDatas'
ALTER TABLE [dbo].[ElementMinistryDatas]
ADD CONSTRAINT [PK_ElementMinistryDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompetencyMinistryDatas'
ALTER TABLE [dbo].[CompetencyMinistryDatas]
ADD CONSTRAINT [PK_CompetencyMinistryDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramMinistryData'
ALTER TABLE [dbo].[ProgramMinistryData]
ADD CONSTRAINT [PK_ProgramMinistryData]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CourseMinistryData'
ALTER TABLE [dbo].[CourseMinistryData]
ADD CONSTRAINT [PK_CourseMinistryData]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [ApplicationId] in table 'aspnet_Applications'
ALTER TABLE [dbo].[aspnet_Applications]
ADD CONSTRAINT [PK_aspnet_Applications]
    PRIMARY KEY CLUSTERED ([ApplicationId] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [PK_aspnet_Membership]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [PathId] in table 'aspnet_Paths'
ALTER TABLE [dbo].[aspnet_Paths]
ADD CONSTRAINT [PK_aspnet_Paths]
    PRIMARY KEY CLUSTERED ([PathId] ASC);
GO

-- Creating primary key on [PathId] in table 'aspnet_PersonalizationAllUsers'
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]
ADD CONSTRAINT [PK_aspnet_PersonalizationAllUsers]
    PRIMARY KEY CLUSTERED ([PathId] ASC);
GO

-- Creating primary key on [Id] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [PK_aspnet_PersonalizationPerUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [PK_aspnet_Profile]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'aspnet_Roles'
ALTER TABLE [dbo].[aspnet_Roles]
ADD CONSTRAINT [PK_aspnet_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Feature], [CompatibleSchemaVersion] in table 'aspnet_SchemaVersions'
ALTER TABLE [dbo].[aspnet_SchemaVersions]
ADD CONSTRAINT [PK_aspnet_SchemaVersions]
    PRIMARY KEY CLUSTERED ([Feature], [CompatibleSchemaVersion] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [PK_aspnet_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [EventId] in table 'aspnet_WebEvent_Events'
ALTER TABLE [dbo].[aspnet_WebEvent_Events]
ADD CONSTRAINT [PK_aspnet_WebEvent_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'User_Candidate'
ALTER TABLE [dbo].[User_Candidate]
ADD CONSTRAINT [PK_User_Candidate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User_ContentSpecialist'
ALTER TABLE [dbo].[User_ContentSpecialist]
ADD CONSTRAINT [PK_User_ContentSpecialist]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [aspnet_Roles_RoleId], [aspnet_Users_UserId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [PK_aspnet_UsersInRoles]
    PRIMARY KEY CLUSTERED ([aspnet_Roles_RoleId], [aspnet_Users_UserId] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RACRequestCompetencyId] in table 'RACRequestCompetencyElements'
ALTER TABLE [dbo].[RACRequestCompetencyElements]
ADD CONSTRAINT [FK_RACRequestCompetencyElementsRACRequestCompetency]
    FOREIGN KEY ([RACRequestCompetencyId])
    REFERENCES [dbo].[RACRequestCompetencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RACRequestCompetencyElementsRACRequestCompetency'
CREATE INDEX [IX_FK_RACRequestCompetencyElementsRACRequestCompetency]
ON [dbo].[RACRequestCompetencyElements]
    ([RACRequestCompetencyId]);
GO

-- Creating foreign key on [RACRequestId] in table 'RACRequestCompetencies'
ALTER TABLE [dbo].[RACRequestCompetencies]
ADD CONSTRAINT [FK_RACRequestCompetencyRACRequest]
    FOREIGN KEY ([RACRequestId])
    REFERENCES [dbo].[RACRequest]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RACRequestCompetencyRACRequest'
CREATE INDEX [IX_FK_RACRequestCompetencyRACRequest]
ON [dbo].[RACRequestCompetencies]
    ([RACRequestId]);
GO

-- Creating foreign key on [RACRequestCompetencyId] in table 'RACRequestCompetencyComment'
ALTER TABLE [dbo].[RACRequestCompetencyComment]
ADD CONSTRAINT [FK_RACRequestCompetencyCompetencyComment]
    FOREIGN KEY ([RACRequestCompetencyId])
    REFERENCES [dbo].[RACRequestCompetencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RACRequestCompetencyCompetencyComment'
CREATE INDEX [IX_FK_RACRequestCompetencyCompetencyComment]
ON [dbo].[RACRequestCompetencyComment]
    ([RACRequestCompetencyId]);
GO

-- Creating foreign key on [RACRequest_Id] in table 'User_Candidate'
ALTER TABLE [dbo].[User_Candidate]
ADD CONSTRAINT [FK_CandidateRACRequest]
    FOREIGN KEY ([RACRequest_Id])
    REFERENCES [dbo].[RACRequest]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateRACRequest'
CREATE INDEX [IX_FK_CandidateRACRequest]
ON [dbo].[User_Candidate]
    ([RACRequest_Id]);
GO

-- Creating foreign key on [UserId] in table 'RACRequestCompetencyComment'
ALTER TABLE [dbo].[RACRequestCompetencyComment]
ADD CONSTRAINT [FK_UserCompetencyComment]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCompetencyComment'
CREATE INDEX [IX_FK_UserCompetencyComment]
ON [dbo].[RACRequestCompetencyComment]
    ([UserId]);
GO

-- Creating foreign key on [RACRequestId] in table 'UploadedDocument'
ALTER TABLE [dbo].[UploadedDocument]
ADD CONSTRAINT [FK_RACRequestUploadedDocument]
    FOREIGN KEY ([RACRequestId])
    REFERENCES [dbo].[RACRequest]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RACRequestUploadedDocument'
CREATE INDEX [IX_FK_RACRequestUploadedDocument]
ON [dbo].[UploadedDocument]
    ([RACRequestId]);
GO

-- Creating foreign key on [CandidateId] in table 'RACAdvisorNotifications'
ALTER TABLE [dbo].[RACAdvisorNotifications]
ADD CONSTRAINT [FK_RACAdvisorNotificationsCandidate]
    FOREIGN KEY ([CandidateId])
    REFERENCES [dbo].[User_Candidate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RACAdvisorNotificationsCandidate'
CREATE INDEX [IX_FK_RACAdvisorNotificationsCandidate]
ON [dbo].[RACAdvisorNotifications]
    ([CandidateId]);
GO

-- Creating foreign key on [Candidate_Id] in table 'FinalizedRACContracts'
ALTER TABLE [dbo].[FinalizedRACContracts]
ADD CONSTRAINT [FK_CandidateFinalizedRACContracts]
    FOREIGN KEY ([Candidate_Id])
    REFERENCES [dbo].[User_Candidate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateFinalizedRACContracts'
CREATE INDEX [IX_FK_CandidateFinalizedRACContracts]
ON [dbo].[FinalizedRACContracts]
    ([Candidate_Id]);
GO

-- Creating foreign key on [CourseMinistryDataId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_CourseMinistryDataCourse]
    FOREIGN KEY ([CourseMinistryDataId])
    REFERENCES [dbo].[CourseMinistryData]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseMinistryDataCourse'
CREATE INDEX [IX_FK_CourseMinistryDataCourse]
ON [dbo].[Courses]
    ([CourseMinistryDataId]);
GO

-- Creating foreign key on [ProgramId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_CourseProgram]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseProgram'
CREATE INDEX [IX_FK_CourseProgram]
ON [dbo].[Courses]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramMinistryDataId] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [FK_ProgramMinistryDataProgram]
    FOREIGN KEY ([ProgramMinistryDataId])
    REFERENCES [dbo].[ProgramMinistryData]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramMinistryDataProgram'
CREATE INDEX [IX_FK_ProgramMinistryDataProgram]
ON [dbo].[Programs]
    ([ProgramMinistryDataId]);
GO

-- Creating foreign key on [CompetencyMinistryDataId] in table 'Competencies'
ALTER TABLE [dbo].[Competencies]
ADD CONSTRAINT [FK_CompetencyMinistryDataCompetency]
    FOREIGN KEY ([CompetencyMinistryDataId])
    REFERENCES [dbo].[CompetencyMinistryDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetencyMinistryDataCompetency'
CREATE INDEX [IX_FK_CompetencyMinistryDataCompetency]
ON [dbo].[Competencies]
    ([CompetencyMinistryDataId]);
GO

-- Creating foreign key on [ProgramId] in table 'Competencies'
ALTER TABLE [dbo].[Competencies]
ADD CONSTRAINT [FK_CompetencyProgram]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetencyProgram'
CREATE INDEX [IX_FK_CompetencyProgram]
ON [dbo].[Competencies]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramId] in table 'RACRequest'
ALTER TABLE [dbo].[RACRequest]
ADD CONSTRAINT [FK_ProgramRACRequest]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramRACRequest'
CREATE INDEX [IX_FK_ProgramRACRequest]
ON [dbo].[RACRequest]
    ([ProgramId]);
GO

-- Creating foreign key on [CompetencyId] in table 'RACRequestCompetencies'
ALTER TABLE [dbo].[RACRequestCompetencies]
ADD CONSTRAINT [FK_CompetencyRACRequestCompetency]
    FOREIGN KEY ([CompetencyId])
    REFERENCES [dbo].[Competencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetencyRACRequestCompetency'
CREATE INDEX [IX_FK_CompetencyRACRequestCompetency]
ON [dbo].[RACRequestCompetencies]
    ([CompetencyId]);
GO

-- Creating foreign key on [CompetencyId] in table 'CompetencyElement'
ALTER TABLE [dbo].[CompetencyElement]
ADD CONSTRAINT [FK_CompetencyCompetencyElement]
    FOREIGN KEY ([CompetencyId])
    REFERENCES [dbo].[Competencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetencyCompetencyElement'
CREATE INDEX [IX_FK_CompetencyCompetencyElement]
ON [dbo].[CompetencyElement]
    ([CompetencyId]);
GO

-- Creating foreign key on [CompetencyElementId] in table 'RACRequestCompetencyElements'
ALTER TABLE [dbo].[RACRequestCompetencyElements]
ADD CONSTRAINT [FK_CompetencyElementRACRequestCompetencyElements]
    FOREIGN KEY ([CompetencyElementId])
    REFERENCES [dbo].[CompetencyElement]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetencyElementRACRequestCompetencyElements'
CREATE INDEX [IX_FK_CompetencyElementRACRequestCompetencyElements]
ON [dbo].[RACRequestCompetencyElements]
    ([CompetencyElementId]);
GO

-- Creating foreign key on [ElementMinistryDataId] in table 'CompetencyElement'
ALTER TABLE [dbo].[CompetencyElement]
ADD CONSTRAINT [FK_ElementMinistryDataCompetencyElement]
    FOREIGN KEY ([ElementMinistryDataId])
    REFERENCES [dbo].[ElementMinistryDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElementMinistryDataCompetencyElement'
CREATE INDEX [IX_FK_ElementMinistryDataCompetencyElement]
ON [dbo].[CompetencyElement]
    ([ElementMinistryDataId]);
GO

-- Creating foreign key on [CourseId] in table 'CourseCompetencies'
ALTER TABLE [dbo].[CourseCompetencies]
ADD CONSTRAINT [FK_CourseCompetencyCourse]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseCompetencyCourse'
CREATE INDEX [IX_FK_CourseCompetencyCourse]
ON [dbo].[CourseCompetencies]
    ([CourseId]);
GO

-- Creating foreign key on [CompetencyId] in table 'CourseCompetencies'
ALTER TABLE [dbo].[CourseCompetencies]
ADD CONSTRAINT [FK_CourseCompetencyCompetency]
    FOREIGN KEY ([CompetencyId])
    REFERENCES [dbo].[Competencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseCompetencyCompetency'
CREATE INDEX [IX_FK_CourseCompetencyCompetency]
ON [dbo].[CourseCompetencies]
    ([CompetencyId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [FK__aspnet_Me__Appli__34BEB830]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Me__Appli__34BEB830'
CREATE INDEX [IX_FK__aspnet_Me__Appli__34BEB830]
ON [dbo].[aspnet_Membership]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Paths'
ALTER TABLE [dbo].[aspnet_Paths]
ADD CONSTRAINT [FK__aspnet_Pa__Appli__6DF7358C]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pa__Appli__6DF7358C'
CREATE INDEX [IX_FK__aspnet_Pa__Appli__6DF7358C]
ON [dbo].[aspnet_Paths]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Roles'
ALTER TABLE [dbo].[aspnet_Roles]
ADD CONSTRAINT [FK__aspnet_Ro__Appli__5713D034]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Ro__Appli__5713D034'
CREATE INDEX [IX_FK__aspnet_Ro__Appli__5713D034]
ON [dbo].[aspnet_Roles]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [FK__aspnet_Us__Appli__20B7BF83]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Us__Appli__20B7BF83'
CREATE INDEX [IX_FK__aspnet_Us__Appli__20B7BF83]
ON [dbo].[aspnet_Users]
    ([ApplicationId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [FK__aspnet_Me__UserI__35B2DC69]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PathId] in table 'aspnet_PersonalizationAllUsers'
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]
ADD CONSTRAINT [FK__aspnet_Pe__PathI__75985754]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[aspnet_Paths]
        ([PathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PathId] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [FK__aspnet_Pe__PathI__7B5130AA]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[aspnet_Paths]
        ([PathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pe__PathI__7B5130AA'
CREATE INDEX [IX_FK__aspnet_Pe__PathI__7B5130AA]
ON [dbo].[aspnet_PersonalizationPerUser]
    ([PathId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [FK__aspnet_Pe__UserI__7C4554E3]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pe__UserI__7C4554E3'
CREATE INDEX [IX_FK__aspnet_Pe__UserI__7C4554E3]
ON [dbo].[aspnet_PersonalizationPerUser]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [FK__aspnet_Pr__UserI__4BA21D88]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [aspnet_Roles_RoleId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Roles]
    FOREIGN KEY ([aspnet_Roles_RoleId])
    REFERENCES [dbo].[aspnet_Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [aspnet_Users_UserId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Users]
    FOREIGN KEY ([aspnet_Users_UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_aspnet_UsersInRoles_aspnet_Users'
CREATE INDEX [IX_FK_aspnet_UsersInRoles_aspnet_Users]
ON [dbo].[aspnet_UsersInRoles]
    ([aspnet_Users_UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [Id] in table 'User_Candidate'
ALTER TABLE [dbo].[User_Candidate]
ADD CONSTRAINT [FK_Candidate_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'User_ContentSpecialist'
ALTER TABLE [dbo].[User_ContentSpecialist]
ADD CONSTRAINT [FK_ContentSpecialist_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------