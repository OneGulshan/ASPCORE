USE [JobPortalCOREDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cities]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[SId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EId] [int] IDENTITY(1,1) NOT NULL,
	[Mobno] [bigint] NOT NULL,
	[Gender] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[JobProfileId] [int] NOT NULL,
	[SkillsId] [int] NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Age] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobProfile]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobProfile](
	[JPId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_JobProfile] PRIMARY KEY CLUSTERED 
(
	[JPId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Skills]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[SklId] [int] IDENTITY(1,1) NOT NULL,
	[JPId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[SklId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[States]    Script Date: 5/5/2023 12:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[SId] [int] IDENTITY(1,1) NOT NULL,
	[CId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[SId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221214034637_Addtblemployee', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230503090955_Crtgender', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230503113446_CrtprofileSkills', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230504073654_AddJobIdSklId', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230504093357_Addimage', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230504101709_RemoveAge', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230505062019_CrtStateCity', N'7.0.5')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a496ad6d-8377-4778-9eb0-7c87c2c175ac', N'gulshankumar.mailid01@gmail.com', N'GULSHANKUMAR.MAILID01@GMAIL.COM', N'gulshankumar.mailid01@gmail.com', N'GULSHANKUMAR.MAILID01@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAECDFyGxYVz48ueejwQwui8zTJTUIOARoFJph/j9sZhyTRfmy9x7T74r6zuht5AcvnQ==', N'AMBFUQDQYLOV5W2INM4MV6IYI3NH3Y4Z', N'0e7f153f-fa8f-4b21-af06-a91408403ba0', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (1, 1, N'New Delhi')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (2, 1, N'Mehrauli')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (3, 1, N'Firozabad')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (4, 2, N'Lucknow')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (5, 2, N'Agra')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (6, 2, N'Varanasi')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (7, 3, N'Ludhiyana')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (8, 3, N'Amritsar')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (9, 3, N'Mohali')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (10, 4, N'Alampur Safarabad')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (11, 4, N'Bahedi')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (12, 4, N'Bhadpura')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (13, 5, N'Gomti Nagar')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (14, 5, N'Jankipuram')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (15, 5, N'Indira Nagar')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (16, 6, N'Shahabad')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (17, 6, N'Sawayajpur')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (18, 6, N'Bilgram')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (19, 7, N'Los Angeles')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (20, 7, N'San Francisco')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (21, 7, N'San Diego')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (22, 8, N'Hauston')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (23, 8, N'Austin')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (24, 8, N'Dallas')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (25, 9, N'Miami')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (26, 9, N'Orlando')
INSERT [dbo].[Cities] ([CityId], [SId], [Name]) VALUES (27, 9, N'Tampa')
SET IDENTITY_INSERT [dbo].[Cities] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CId], [Name]) VALUES (1, N'India')
INSERT [dbo].[Countries] ([CId], [Name]) VALUES (2, N'Shrilanka')
INSERT [dbo].[Countries] ([CId], [Name]) VALUES (3, N'USA')
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EId], [Mobno], [Gender], [Name], [JobProfileId], [SkillsId], [ImagePath], [Age], [CityId], [CountryId], [StateId]) VALUES (2, 2132111, 1, N'Gulshan', -1, 0, N'1f40f7ba-51c3-497c-b884-ed8937182006-Bread.jpg', 0, 0, 2, 4)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[States] ON 

INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (1, 1, N'Delhi')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (2, 1, N'Uttar Pradesh')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (3, 1, N'Punjab')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (4, 2, N'Bareli')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (5, 2, N'Lucknow')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (6, 2, N'Hardoi')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (7, 3, N'California')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (8, 3, N'Texas')
INSERT [dbo].[States] ([SId], [CId], [Name]) VALUES (9, 3, N'Florida')
SET IDENTITY_INSERT [dbo].[States] OFF
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [JobProfileId]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [SkillsId]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [Age]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [CityId]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [CountryId]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [StateId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
