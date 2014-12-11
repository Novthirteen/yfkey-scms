SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProdLineIpBackflushLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProdLineIpId] [int] NULL,
	[Item] [varchar](50) NULL,
	[ProdLine] [varchar](50) NULL,
	[IpNo] [varchar](50) NULL,
	[LocFrom] [varchar](50) NULL,
	[HuId] [varchar](50) NULL,
	[RemainQty] [decimal](18, 8) NULL,
	[Lvl] [tinyint] NULL,
	[Msg] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


