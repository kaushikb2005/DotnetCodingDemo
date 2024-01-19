USE [DotnetCodingDB]
GO
/****** Object:  Table [dbo].[ProductApprovalQueues]    Script Date: 1/19/2024 12:21:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductApprovalQueues](
	[ApprovalId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[RequestReason] [nvarchar](max) NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProductApprovalQueues] PRIMARY KEY CLUSTERED 
(
	[ApprovalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/19/2024 12:21:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[ApprovalStatus] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [State], [ApprovalStatus], [IsActive], [CreatedDate], [ModifiedDate], [DeletedDate], [IsDeleted]) VALUES (1, N'iPhone 15', N'iPhone 15', 2000, N'Update', N'Approved', 1, CAST(N'2024-01-19T16:44:25.4089459' AS DateTime2), CAST(N'2024-01-19T17:34:05.6162650' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [State], [ApprovalStatus], [IsActive], [CreatedDate], [ModifiedDate], [DeletedDate], [IsDeleted]) VALUES (2, N'Samsung S23', N'Samsung S23', 6000, N'Create', N'Approved', 1, CAST(N'2024-01-19T16:49:17.9669182' AS DateTime2), CAST(N'2024-01-19T16:49:17.9669186' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [State], [ApprovalStatus], [IsActive], [CreatedDate], [ModifiedDate], [DeletedDate], [IsDeleted]) VALUES (3, N'Nokia 20', N'Nokia S23', 1500, N'Delete', N'Approved', 0, CAST(N'2024-01-19T17:41:45.6057040' AS DateTime2), CAST(N'2024-01-19T17:41:45.6057045' AS DateTime2), CAST(N'2024-01-19T17:45:31.0718107' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[ProductApprovalQueues]  WITH CHECK ADD  CONSTRAINT [FK_ProductApprovalQueues_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductApprovalQueues] CHECK CONSTRAINT [FK_ProductApprovalQueues_Products_ProductId]
GO
