--
-- Script To Create dbo.Carrier Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Carrier Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[Carrier] (
   [Code] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [RefSupplier] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[Carrier] ADD CONSTRAINT [PK_Carrier] PRIMARY KEY CLUSTERED ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Carrier Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Carrier Table'
END
GO

--
-- Script To Update dbo.CycleCountMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.CycleCountMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[CycleCountMstr]
      ADD [EffectiveDate] [datetime] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.CycleCountMstr Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.CycleCountMstr Table'
END
GO

--
-- Script To Create dbo.Expense Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Expense Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[Expense] (
   [Code] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Carrier] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Amount] [decimal] (18, 8) NOT NULL,
   [CreateDate] [datetime] NOT NULL,
   [CreateUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Remark] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [IsReferenced] [bit] NOT NULL,
   [LastModifyDate] [datetime] NULL,
   [LastModifyUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Currency] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [IsIncludeTax] [bit] NOT NULL,
   [TaxCode] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[Expense] ADD CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Expense Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Expense Table'
END
GO


--
-- Script To Update dbo.FlowMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.FlowMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_FlowMstr_IsAsnUniqueReceipt')
      ALTER TABLE [dbo].[FlowMstr] DROP CONSTRAINT [DF_FlowMstr_IsAsnUniqueReceipt]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysindexes WHERE name = N'IX_Flow_1')
      DROP INDEX [IX_Flow_1] ON [dbo].[FlowMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[FlowMstr]
      ALTER COLUMN [IsAsnUniqueReceipt] [bit] NOT NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[FlowMstr]
      ADD [TPriceList] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[FlowMstr]
      ADD [TRoute] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[FlowMstr] ADD CONSTRAINT [DF_FlowMstr_IsAsnUniqueReceipt] DEFAULT ((1)) FOR [IsAsnUniqueReceipt]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.FlowMstr Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.FlowMstr Table'
END
GO


--
-- Script To Update dbo.IpMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.IpMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_IpMstr_printcount')
      ALTER TABLE [dbo].[IpMstr] DROP CONSTRAINT [DF_IpMstr_printcount]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysindexes WHERE name = N'IX_IpMstr_1')
      DROP INDEX [IX_IpMstr_1] ON [dbo].[IpMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[IpMstr]
      ADD [flow] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[IpMstr]
      ADD [isreferenced] [bit] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.IpMstr Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.IpMstr Table'
END
GO


--
-- Script To Update dbo.IpDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.IpDet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[IpDet]
      ADD [item] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[IpDet]
      ADD [locfrom] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[IpDet]
      ADD [locto] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.IpDet Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.IpDet Table'
END
GO

--
-- Script To Update dbo.Region Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Region Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[Region]
      ADD [Type] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Region Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Region Table'
END
GO

--
-- Script To Create dbo.TAddress Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TAddress Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TAddress] (
   [Id] [int] IDENTITY (1, 1) NOT NULL,
   [Country] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Province] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [City] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [District] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Address] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TAddress] ADD CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TAddress Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TAddress Table'
END
GO

--
-- Script To Create dbo.TRouteMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TRouteMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TRouteMstr] (
   [Code] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [IsActive] [bit] NOT NULL,
   [Description] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [ShipFrom] [int] NOT NULL,
   [ShipTo] [int] NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TRouteMstr] ADD CONSTRAINT [PK_TRoute] PRIMARY KEY CLUSTERED ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TRouteMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TRouteMstr Table'
END
GO


--
-- Script To Create dbo.TOrderMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TOrderMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TOrderMstr] (
   [OrderNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [TRoute] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [CreateDate] [datetime] NULL,
   [CreateUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [StartDate] [datetime] NULL,
   [StartUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [CompleteDate] [datetime] NULL,
   [CompleteUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [CloseDate] [datetime] NULL,
   [CloseUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [CancelDate] [datetime] NULL,
   [CancelUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [IsValuated] [bit] NOT NULL,
   [Status] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [VehicleCode] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Carrier] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [PallentCount] [int] NOT NULL,
   [RefPallentCount] [int] NOT NULL,
   [Expense] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [LastModifyDate] [datetime] NULL,
   [LastModifyUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [VehicleDriver] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Remark] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [BillAddr] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [PricingMethod] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [VehicleType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [PK_TOrder] PRIMARY KEY CLUSTERED ([OrderNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TOrderMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TOrderMstr Table'
END
GO


--
-- Script To Create dbo.TPriceListMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TPriceListMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TPriceListMstr] (
   [Code] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Party] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [IsActive] [bit] NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TPriceListMstr] ADD CONSTRAINT [PK_TPriceListMstr] PRIMARY KEY CLUSTERED ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TPriceListMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TPriceListMstr Table'
END
GO


--
-- Script To Create dbo.TPriceListDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TPriceListDet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TPriceListDet] (
   [Id] [int] IDENTITY (1, 1) NOT NULL,
   [TPriceList] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [StartDate] [datetime] NOT NULL,
   [EndDate] [datetime] NULL,
   [ShipFrom] [int] NULL,
   [ShipTo] [int] NULL,
   [Currency] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Uom] [varchar] (5) COLLATE Chinese_PRC_CI_AS NULL,
   [UnitPrice] [decimal] (18, 8) NOT NULL,
   [TaxCode] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [IsIncludeTax] [bit] NOT NULL,
   [IsProvEst] [bit] NOT NULL,
   [PricingMethod] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [VehicleType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [MinVolume] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TPriceListDet_MinVolume] DEFAULT ((0)),
   [ServiceCharge] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TPriceListDet_ServiceCharge] DEFAULT ((0)),
   [RoundUpOpt] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Type] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Remark] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [UC] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TPriceListDet_UC] DEFAULT ((0)),
   [BillingMethod] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Item] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TPriceListDet] ADD CONSTRAINT [PK_TPriceListDet] PRIMARY KEY CLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TPriceListDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TPriceListDet Table'
END
GO


--
-- Script To Create dbo.TActBill Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TActBill Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TActBill] (
   [Id] [int] IDENTITY (1, 1) NOT NULL,
   [OrderNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [RecNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [ExpenseNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [ExtRecNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [BillAddr] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [BillQty] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TActBill_BillQty] DEFAULT ((0)),
   [BilledQty] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TActBill_BilledQty] DEFAULT ((0)),
   [BillAmount] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TActBill_BillAmount] DEFAULT ((0)),
   [BilledAmount] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TActBill_BilledAmount] DEFAULT ((0)),
   [PriceList] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [PriceListDet] [int] NULL,
   [Currency] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [TaxCode] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [IsProvEst] [bit] NOT NULL CONSTRAINT [DF_TActBill_IsProvEst] DEFAULT ((0)),
   [CreateDate] [datetime] NOT NULL,
   [CreateUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [LastModifyDate] [datetime] NOT NULL,
   [LastModifyUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Status] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [EffDate] [datetime] NOT NULL,
   [TransType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Uom] [varchar] (5) COLLATE Chinese_PRC_CI_AS NULL,
   [UnitPrice] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TActBill_UnitPrice] DEFAULT ((0)),
   [IsIncludeTax] [bit] NOT NULL,
   [Item] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [PricingMethod] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [ShipFrom] [int] NULL,
   [ShipTo] [int] NULL,
   [VehicleType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [PK_TActBill] PRIMARY KEY CLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TActBill Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TActBill Table'
END
GO


--
-- Script To Create dbo.TBillMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TBillMstr Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TBillMstr] (
   [BillNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_TBillMstr_BillNo] DEFAULT ((0)),
   [ExtBillNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [RefBillNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Status] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [BillAddr] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Currency] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [IsIncludeTax] [bit] NOT NULL,
   [TaxCode] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Discount] [decimal] (18, 8) NOT NULL,
   [CreateDate] [datetime] NOT NULL,
   [CreateUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [LastModifyDate] [datetime] NOT NULL,
   [LastModifyUser] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [BillType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TBillMstr] ADD CONSTRAINT [PK_TBillMstr] PRIMARY KEY CLUSTERED ([BillNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TBillMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TBillMstr Table'
END
GO


--
-- Script To Create dbo.TBillDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TBillDet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TBillDet] (
   [Id] [int] IDENTITY (1, 1) NOT NULL,
   [BillNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [TransId] [int] NOT NULL,
   [BilledQty] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TBillDet_BilledQty] DEFAULT ((0)),
   [UnitPrice] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TBillDet_UnitPrice] DEFAULT ((0)),
   [Currency] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Discount] [decimal] (18, 8) NULL CONSTRAINT [DF_TBillDet_Discount] DEFAULT ((0)),
   [IsIncludeTax] [bit] NOT NULL,
   [TaxCode] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Amount] [decimal] (18, 8) NOT NULL CONSTRAINT [DF_TBillDet_Amount] DEFAULT ((0)),
   [TransType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TBillDet] ADD CONSTRAINT [PK_TBillDet] PRIMARY KEY NONCLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TBillDet] ADD CONSTRAINT [IX_TBillDet] UNIQUE CLUSTERED ([BillNo], [TransId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TBillDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TBillDet Table'
END
GO


--
-- Script To Create dbo.TOrderDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TOrderDet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TOrderDet] (
   [Id] [int] IDENTITY (1, 1) NOT NULL,
   [OrderNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [IpNo] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TOrderDet] ADD CONSTRAINT [PK_TOrderDet] PRIMARY KEY CLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TOrderDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TOrderDet Table'
END
GO


--
-- Script To Create dbo.TRouteDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TRouteDet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[TRouteDet] (
   [Id] [int] IDENTITY (1, 1) NOT NULL,
   [Seq] [int] NOT NULL,
   [TAddress] [int] NOT NULL,
   [TRoute] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[TRouteDet] ADD CONSTRAINT [PK_TRouteDet] PRIMARY KEY CLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TRouteDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TRouteDet Table'
END
GO


--
-- Script To Create dbo.Vehicle Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Vehicle Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[Vehicle] (
   [Code] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [IsActive] [bit] NOT NULL,
   [Carrier] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Type] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
   [Driver] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [Phone] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
   [MPhone] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[Vehicle] ADD CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Vehicle Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Vehicle Table'
END
GO

--
-- Script To Create dbo.Expense Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Expense Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_ACC_User_CreateUser')
      ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_ACC_User_CreateUser]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_ACC_User_CreateUser')
      ALTER TABLE [dbo].[Expense] ADD CONSTRAINT [FK_Expense_ACC_User_CreateUser] FOREIGN KEY ([CreateUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_ACC_User_LastModifyUser')
      ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_ACC_User_LastModifyUser]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_ACC_User_LastModifyUser')
      ALTER TABLE [dbo].[Expense] ADD CONSTRAINT [FK_Expense_ACC_User_LastModifyUser] FOREIGN KEY ([LastModifyUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_Carrier')
      ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_Carrier]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_Carrier')
      ALTER TABLE [dbo].[Expense] ADD CONSTRAINT [FK_Expense_Carrier] FOREIGN KEY ([Carrier]) REFERENCES [dbo].[Carrier] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_Currency')
      ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_Currency]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_Currency')
      ALTER TABLE [dbo].[Expense] ADD CONSTRAINT [FK_Expense_Currency] FOREIGN KEY ([Currency]) REFERENCES [dbo].[Currency] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_Expense')
      ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_Expense]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_Expense_Expense')
      ALTER TABLE [dbo].[Expense] ADD CONSTRAINT [FK_Expense_Expense] FOREIGN KEY ([Code]) REFERENCES [dbo].[Expense] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Expense Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Expense Table'
END
GO


--
-- Script To Update dbo.FlowMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.FlowMstr Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_FlowMstr_TPriceListMstr')
      ALTER TABLE [dbo].[FlowMstr] DROP CONSTRAINT [FK_FlowMstr_TPriceListMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_FlowMstr_TPriceListMstr')
      ALTER TABLE [dbo].[FlowMstr] ADD CONSTRAINT [FK_FlowMstr_TPriceListMstr] FOREIGN KEY ([TPriceList]) REFERENCES [dbo].[TPriceListMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_FlowMstr_TRouteMstr')
      ALTER TABLE [dbo].[FlowMstr] DROP CONSTRAINT [FK_FlowMstr_TRouteMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_FlowMstr_TRouteMstr')
      ALTER TABLE [dbo].[FlowMstr] ADD CONSTRAINT [FK_FlowMstr_TRouteMstr] FOREIGN KEY ([TRoute]) REFERENCES [dbo].[TRouteMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.FlowMstr Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.FlowMstr Table'
END
GO


--
-- Script To Update dbo.IpMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.IpMstr Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_IpMstr_FlowMstr')
      ALTER TABLE [dbo].[IpMstr] DROP CONSTRAINT [FK_IpMstr_FlowMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_IpMstr_FlowMstr')
      ALTER TABLE [dbo].[IpMstr] ADD CONSTRAINT [FK_IpMstr_FlowMstr] FOREIGN KEY ([flow]) REFERENCES [dbo].[FlowMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.IpMstr Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.IpMstr Table'
END
GO


--
-- Script To Create dbo.TRouteMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TRouteMstr Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteMstr_TAddress')
      ALTER TABLE [dbo].[TRouteMstr] DROP CONSTRAINT [FK_TRouteMstr_TAddress]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteMstr_TAddress')
      ALTER TABLE [dbo].[TRouteMstr] ADD CONSTRAINT [FK_TRouteMstr_TAddress] FOREIGN KEY ([ShipFrom]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteMstr_TAddress1')
      ALTER TABLE [dbo].[TRouteMstr] DROP CONSTRAINT [FK_TRouteMstr_TAddress1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteMstr_TAddress1')
      ALTER TABLE [dbo].[TRouteMstr] ADD CONSTRAINT [FK_TRouteMstr_TAddress1] FOREIGN KEY ([ShipTo]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TRouteMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TRouteMstr Table'
END
GO


--
-- Script To Create dbo.TOrderMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TOrderMstr Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_ACC_User]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_ACC_User] FOREIGN KEY ([CreateUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User1')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_ACC_User1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User1')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_ACC_User1] FOREIGN KEY ([StartUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User2')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_ACC_User2]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User2')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_ACC_User2] FOREIGN KEY ([CompleteUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User3')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_ACC_User3]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User3')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_ACC_User3] FOREIGN KEY ([CloseUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User4')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_ACC_User4]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_ACC_User4')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_ACC_User4] FOREIGN KEY ([LastModifyUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_Carrier')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_Carrier]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_Carrier')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_Carrier] FOREIGN KEY ([Carrier]) REFERENCES [dbo].[Carrier] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_Expense')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_Expense]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_Expense')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_Expense] FOREIGN KEY ([Expense]) REFERENCES [dbo].[Expense] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_PartyAddr')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_PartyAddr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_PartyAddr')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_PartyAddr] FOREIGN KEY ([BillAddr]) REFERENCES [dbo].[PartyAddr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_TRouteMstr')
      ALTER TABLE [dbo].[TOrderMstr] DROP CONSTRAINT [FK_TOrderMstr_TRouteMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderMstr_TRouteMstr')
      ALTER TABLE [dbo].[TOrderMstr] ADD CONSTRAINT [FK_TOrderMstr_TRouteMstr] FOREIGN KEY ([TRoute]) REFERENCES [dbo].[TRouteMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TOrderMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TOrderMstr Table'
END
GO


--
-- Script To Create dbo.TPriceListMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TPriceListMstr Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListMstr_Party')
      ALTER TABLE [dbo].[TPriceListMstr] DROP CONSTRAINT [FK_TPriceListMstr_Party]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListMstr_Party')
      ALTER TABLE [dbo].[TPriceListMstr] ADD CONSTRAINT [FK_TPriceListMstr_Party] FOREIGN KEY ([Party]) REFERENCES [dbo].[Party] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TPriceListMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TPriceListMstr Table'
END
GO


--
-- Script To Create dbo.TPriceListDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TPriceListDet Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_Currency')
      ALTER TABLE [dbo].[TPriceListDet] DROP CONSTRAINT [FK_TPriceListDet_Currency]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_Currency')
      ALTER TABLE [dbo].[TPriceListDet] ADD CONSTRAINT [FK_TPriceListDet_Currency] FOREIGN KEY ([Currency]) REFERENCES [dbo].[Currency] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_TAddress')
      ALTER TABLE [dbo].[TPriceListDet] DROP CONSTRAINT [FK_TPriceListDet_TAddress]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_TAddress')
      ALTER TABLE [dbo].[TPriceListDet] ADD CONSTRAINT [FK_TPriceListDet_TAddress] FOREIGN KEY ([ShipFrom]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_TAddress1')
      ALTER TABLE [dbo].[TPriceListDet] DROP CONSTRAINT [FK_TPriceListDet_TAddress1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_TAddress1')
      ALTER TABLE [dbo].[TPriceListDet] ADD CONSTRAINT [FK_TPriceListDet_TAddress1] FOREIGN KEY ([ShipTo]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_TPriceListMstr')
      ALTER TABLE [dbo].[TPriceListDet] DROP CONSTRAINT [FK_TPriceListDet_TPriceListMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_TPriceListMstr')
      ALTER TABLE [dbo].[TPriceListDet] ADD CONSTRAINT [FK_TPriceListDet_TPriceListMstr] FOREIGN KEY ([TPriceList]) REFERENCES [dbo].[TPriceListMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_Uom')
      ALTER TABLE [dbo].[TPriceListDet] DROP CONSTRAINT [FK_TPriceListDet_Uom]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TPriceListDet_Uom')
      ALTER TABLE [dbo].[TPriceListDet] ADD CONSTRAINT [FK_TPriceListDet_Uom] FOREIGN KEY ([Uom]) REFERENCES [dbo].[Uom] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TPriceListDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TPriceListDet Table'
END
GO


--
-- Script To Create dbo.TActBill Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TActBill Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_ACC_User')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_ACC_User]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_ACC_User')
      ALTER TABLE [dbo].[TActBill] WITH NOCHECK ADD CONSTRAINT [FK_TActBill_ACC_User] FOREIGN KEY ([CreateUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_ACC_User1')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_ACC_User1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_ACC_User1')
      ALTER TABLE [dbo].[TActBill] WITH NOCHECK ADD CONSTRAINT [FK_TActBill_ACC_User1] FOREIGN KEY ([LastModifyUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_Currency')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_Currency]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_Currency')
      ALTER TABLE [dbo].[TActBill] WITH NOCHECK ADD CONSTRAINT [FK_TActBill_Currency] FOREIGN KEY ([Currency]) REFERENCES [dbo].[Currency] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_Expense')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_Expense]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_Expense')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_Expense] FOREIGN KEY ([ExpenseNo]) REFERENCES [dbo].[Expense] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_PartyAddr')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_PartyAddr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_PartyAddr')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_PartyAddr] FOREIGN KEY ([BillAddr]) REFERENCES [dbo].[PartyAddr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_ReceiptMstr')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_ReceiptMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_ReceiptMstr')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_ReceiptMstr] FOREIGN KEY ([RecNo]) REFERENCES [dbo].[ReceiptMstr] ([RecNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TAddress')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_TAddress]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TAddress')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_TAddress] FOREIGN KEY ([ShipFrom]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TAddress1')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_TAddress1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TAddress1')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_TAddress1] FOREIGN KEY ([ShipTo]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TOrderMstr')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_TOrderMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TOrderMstr')
      ALTER TABLE [dbo].[TActBill] WITH NOCHECK ADD CONSTRAINT [FK_TActBill_TOrderMstr] FOREIGN KEY ([OrderNo]) REFERENCES [dbo].[TOrderMstr] ([OrderNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TPriceListDet')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_TPriceListDet]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TPriceListDet')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_TPriceListDet] FOREIGN KEY ([PriceListDet]) REFERENCES [dbo].[TPriceListDet] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TPriceListMstr')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_TPriceListMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_TPriceListMstr')
      ALTER TABLE [dbo].[TActBill] WITH NOCHECK ADD CONSTRAINT [FK_TActBill_TPriceListMstr] FOREIGN KEY ([PriceList]) REFERENCES [dbo].[TPriceListMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_Uom')
      ALTER TABLE [dbo].[TActBill] DROP CONSTRAINT [FK_TActBill_Uom]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TActBill_Uom')
      ALTER TABLE [dbo].[TActBill] ADD CONSTRAINT [FK_TActBill_Uom] FOREIGN KEY ([Uom]) REFERENCES [dbo].[Uom] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TActBill Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TActBill Table'
END
GO


--
-- Script To Create dbo.TBillMstr Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TBillMstr Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_ACC_User')
      ALTER TABLE [dbo].[TBillMstr] DROP CONSTRAINT [FK_TBillMstr_ACC_User]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_ACC_User')
      ALTER TABLE [dbo].[TBillMstr] WITH NOCHECK ADD CONSTRAINT [FK_TBillMstr_ACC_User] FOREIGN KEY ([CreateUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_ACC_User1')
      ALTER TABLE [dbo].[TBillMstr] DROP CONSTRAINT [FK_TBillMstr_ACC_User1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_ACC_User1')
      ALTER TABLE [dbo].[TBillMstr] WITH NOCHECK ADD CONSTRAINT [FK_TBillMstr_ACC_User1] FOREIGN KEY ([LastModifyUser]) REFERENCES [dbo].[ACC_User] ([USR_Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_Currency')
      ALTER TABLE [dbo].[TBillMstr] DROP CONSTRAINT [FK_TBillMstr_Currency]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_Currency')
      ALTER TABLE [dbo].[TBillMstr] ADD CONSTRAINT [FK_TBillMstr_Currency] FOREIGN KEY ([Currency]) REFERENCES [dbo].[Currency] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_Currency1')
      ALTER TABLE [dbo].[TBillMstr] DROP CONSTRAINT [FK_TBillMstr_Currency1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_Currency1')
      ALTER TABLE [dbo].[TBillMstr] ADD CONSTRAINT [FK_TBillMstr_Currency1] FOREIGN KEY ([Currency]) REFERENCES [dbo].[Currency] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_PartyAddr')
      ALTER TABLE [dbo].[TBillMstr] DROP CONSTRAINT [FK_TBillMstr_PartyAddr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_PartyAddr')
      ALTER TABLE [dbo].[TBillMstr] ADD CONSTRAINT [FK_TBillMstr_PartyAddr] FOREIGN KEY ([BillAddr]) REFERENCES [dbo].[PartyAddr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_PartyAddr1')
      ALTER TABLE [dbo].[TBillMstr] DROP CONSTRAINT [FK_TBillMstr_PartyAddr1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillMstr_PartyAddr1')
      ALTER TABLE [dbo].[TBillMstr] ADD CONSTRAINT [FK_TBillMstr_PartyAddr1] FOREIGN KEY ([BillAddr]) REFERENCES [dbo].[PartyAddr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TBillMstr Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TBillMstr Table'
END
GO


--
-- Script To Create dbo.TBillDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TBillDet Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillDet_BillMstr')
      ALTER TABLE [dbo].[TBillDet] DROP CONSTRAINT [FK_TBillDet_BillMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillDet_BillMstr')
      ALTER TABLE [dbo].[TBillDet] WITH NOCHECK ADD CONSTRAINT [FK_TBillDet_BillMstr] FOREIGN KEY ([BillNo]) REFERENCES [dbo].[TBillMstr] ([BillNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillDet_BillTrans')
      ALTER TABLE [dbo].[TBillDet] DROP CONSTRAINT [FK_TBillDet_BillTrans]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillDet_BillTrans')
      ALTER TABLE [dbo].[TBillDet] WITH NOCHECK ADD CONSTRAINT [FK_TBillDet_BillTrans] FOREIGN KEY ([TransId]) REFERENCES [dbo].[TActBill] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillDet_Currency')
      ALTER TABLE [dbo].[TBillDet] DROP CONSTRAINT [FK_TBillDet_Currency]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TBillDet_Currency')
      ALTER TABLE [dbo].[TBillDet] WITH NOCHECK ADD CONSTRAINT [FK_TBillDet_Currency] FOREIGN KEY ([Currency]) REFERENCES [dbo].[Currency] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TBillDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TBillDet Table'
END
GO


--
-- Script To Create dbo.TOrderDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TOrderDet Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderDet_IpMstr')
      ALTER TABLE [dbo].[TOrderDet] DROP CONSTRAINT [FK_TOrderDet_IpMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderDet_IpMstr')
      ALTER TABLE [dbo].[TOrderDet] ADD CONSTRAINT [FK_TOrderDet_IpMstr] FOREIGN KEY ([IpNo]) REFERENCES [dbo].[IpMstr] ([IpNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderDet_TOrderMstr')
      ALTER TABLE [dbo].[TOrderDet] DROP CONSTRAINT [FK_TOrderDet_TOrderMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TOrderDet_TOrderMstr')
      ALTER TABLE [dbo].[TOrderDet] ADD CONSTRAINT [FK_TOrderDet_TOrderMstr] FOREIGN KEY ([OrderNo]) REFERENCES [dbo].[TOrderMstr] ([OrderNo])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TOrderDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TOrderDet Table'
END
GO


--
-- Script To Create dbo.TRouteDet Table In 192.168.210.121.scms_yf
-- Generated 星期一, 五月 16, 2011, at 01:34 PM
--
-- Please backup 192.168.210.121.scms_yf before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.TRouteDet Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteDet_TAddress')
      ALTER TABLE [dbo].[TRouteDet] DROP CONSTRAINT [FK_TRouteDet_TAddress]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteDet_TAddress')
      ALTER TABLE [dbo].[TRouteDet] ADD CONSTRAINT [FK_TRouteDet_TAddress] FOREIGN KEY ([TAddress]) REFERENCES [dbo].[TAddress] ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteDet_TRouteMstr')
      ALTER TABLE [dbo].[TRouteDet] DROP CONSTRAINT [FK_TRouteDet_TRouteMstr]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_TRouteDet_TRouteMstr')
      ALTER TABLE [dbo].[TRouteDet] ADD CONSTRAINT [FK_TRouteDet_TRouteMstr] FOREIGN KEY ([TRoute]) REFERENCES [dbo].[TRouteMstr] ([Code])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.TRouteDet Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.TRouteDet Table'
END
GO
