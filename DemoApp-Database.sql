USE [DemoApp]
GO
/****** Object:  Table [dbo].[rntbanner]    Script Date: 02-01-2025 13:44:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntbanner](
	[bannerId] [bigint] IDENTITY(1,1) NOT NULL,
	[bannerType] [nvarchar](50) NOT NULL,
	[bannerName] [nvarchar](100) NULL,
	[bannerDescription] [nvarchar](500) NULL,
	[imagePath] [nvarchar](200) NOT NULL,
	[link] [nvarchar](200) NULL,
	[status] [nvarchar](50) NOT NULL,
	[createdBy] [numeric](18, 0) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [numeric](18, 0) NULL,
	[updatedDate] [datetime] NULL,
 CONSTRAINT [PK_rntbanner] PRIMARY KEY CLUSTERED 
(
	[bannerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntCity]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntCity](
	[CityId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StateId] [numeric](18, 0) NOT NULL,
	[CityName] [nvarchar](50) NOT NULL,
	[CityCode] [nvarchar](5) NULL,
	[CityPhoneCode] [nvarchar](7) NULL,
	[Description] [nvarchar](1000) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntCMS]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntCMS](
	[cmsId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[CmsType] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Contents] [ntext] NOT NULL,
	[ParentId] [numeric](18, 0) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CMS] PRIMARY KEY CLUSTERED 
(
	[cmsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntContactUs]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntContactUs](
	[ContactId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NULL,
	[EmailId] [nvarchar](100) NULL,
	[Subject] [nvarchar](200) NULL,
	[Message] [nvarchar](1000) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntCountry]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntCountry](
	[CountryId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
	[CountryCode] [nvarchar](10) NULL,
	[CountryPhoneCode] [nvarchar](7) NULL,
	[Description] [nvarchar](1000) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntCustomer]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntCustomer](
	[CustomerId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[EmailId] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IP] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntfaq]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntfaq](
	[faqId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[faqCategory] [nvarchar](20) NULL,
	[question] [nvarchar](500) NULL,
	[solution] [nvarchar](2000) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_rntfaq] PRIMARY KEY CLUSTERED 
(
	[faqId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntState]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntState](
	[StateId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CountryId] [numeric](18, 0) NOT NULL,
	[StateName] [nvarchar](50) NOT NULL,
	[StateCode] [nvarchar](5) NULL,
	[Description] [nvarchar](1000) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rntUser]    Script Date: 02-01-2025 13:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rntUser](
	[UserId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[usertype] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[EmailId] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [numeric](18, 0) NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IP] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[rntbanner] ADD  CONSTRAINT [DF_rntbanner_CreatedDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[rntbanner] ADD  CONSTRAINT [DF_rntbanner_UpdatedDate]  DEFAULT (getdate()) FOR [updatedDate]
GO
ALTER TABLE [dbo].[rntCity] ADD  CONSTRAINT [DF_rntcity_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntCity] ADD  CONSTRAINT [DF_rntcity_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[rntContactUs] ADD  CONSTRAINT [DF_rntContactUs_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntContactUs] ADD  CONSTRAINT [DF_rntContactUs_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[rntCountry] ADD  CONSTRAINT [DF_rntCountry_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntCountry] ADD  CONSTRAINT [DF_rntCountry_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[rntCustomer] ADD  CONSTRAINT [DF_rntCustomer_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntCustomer] ADD  CONSTRAINT [DF_rntCustomer_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[rntfaq] ADD  CONSTRAINT [DF_rntfaq_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntfaq] ADD  CONSTRAINT [DF_rntfaq_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[rntState] ADD  CONSTRAINT [DF_rntState_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntState] ADD  CONSTRAINT [DF_rntState_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[rntUser] ADD  CONSTRAINT [DF_rntUser_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[rntUser] ADD  CONSTRAINT [DF_rntUser_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
