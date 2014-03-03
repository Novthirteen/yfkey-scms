/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2010-8-12 10:58:31                           */
/*==============================================================*/


if exists (select 1
          from sysobjects
          where  id = object_id('GetNextSequence')
          and type in ('P','PC'))
   drop procedure GetNextSequence
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_Company') and o.name = 'FK_ACC_COMP_INDUSTRY__ACC_INDU')
alter table ACC_Company
   drop constraint FK_ACC_COMP_INDUSTRY__ACC_INDU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuCommon') and o.name = 'FK_Menu_MenuCommon')
alter table ACC_MenuCommon
   drop constraint FK_Menu_MenuCommon
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuCommon') and o.name = 'FK_PARENTMENU_MENUCOMMON')
alter table ACC_MenuCommon
   drop constraint FK_PARENTMENU_MENUCOMMON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuCompany') and o.name = 'FK_ACC_MENU_COMPANY_M_ACC_COMP')
alter table ACC_MenuCompany
   drop constraint FK_ACC_MENU_COMPANY_M_ACC_COMP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuCompany') and o.name = 'FK_Menu_MenuCompany')
alter table ACC_MenuCompany
   drop constraint FK_Menu_MenuCompany
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuCompany') and o.name = 'FK_ParentMenu_CompanyMenu')
alter table ACC_MenuCompany
   drop constraint FK_ParentMenu_CompanyMenu
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuIndustry') and o.name = 'FK_INDUSTRY_MENUINDUSTRY')
alter table ACC_MenuIndustry
   drop constraint FK_INDUSTRY_MENUINDUSTRY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuIndustry') and o.name = 'FK_ACC_MENU_MENUINDUSTRY')
alter table ACC_MenuIndustry
   drop constraint FK_ACC_MENU_MENUINDUSTRY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_MenuIndustry') and o.name = 'FK_ParentMenu_MenuIndustry')
alter table ACC_MenuIndustry
   drop constraint FK_ParentMenu_MenuIndustry
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_Permission') and o.name = 'FK_PermissionCategory_Permission')
alter table ACC_Permission
   drop constraint FK_PermissionCategory_Permission
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_RolePermission') and o.name = 'FK_ACC_RolePermission_ACC_Permission')
alter table ACC_RolePermission
   drop constraint FK_ACC_RolePermission_ACC_Permission
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_RolePermission') and o.name = 'FK_ACC_RolePermission_ACC_Role')
alter table ACC_RolePermission
   drop constraint FK_ACC_RolePermission_ACC_Role
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_User') and o.name = 'FK_ACC_User_ACC_User')
alter table ACC_User
   drop constraint FK_ACC_User_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_UserFav') and o.name = 'FK_Favorites_ACC_User')
alter table ACC_UserFav
   drop constraint FK_Favorites_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_UserPermission') and o.name = 'FK_ACC_UserPermission_ACC_Permission')
alter table ACC_UserPermission
   drop constraint FK_ACC_UserPermission_ACC_Permission
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_UserPermission') and o.name = 'FK_ACC_UserPermission_ACC_User')
alter table ACC_UserPermission
   drop constraint FK_ACC_UserPermission_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_UserPre') and o.name = 'FK_UserPreference_ACC_User')
alter table ACC_UserPre
   drop constraint FK_UserPreference_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ACC_UserRole') and o.name = 'FK_ACC_UserRole_ACC_User')
alter table ACC_UserRole
   drop constraint FK_ACC_UserRole_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_ACC_User')
alter table ActBill
   drop constraint FK_ActBill_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_ACC_User1')
alter table ActBill
   drop constraint FK_ActBill_ACC_User1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_Currency')
alter table ActBill
   drop constraint FK_ActBill_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_Item')
alter table ActBill
   drop constraint FK_ActBill_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_OrderMstr')
alter table ActBill
   drop constraint FK_ActBill_OrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_PartyAddr')
alter table ActBill
   drop constraint FK_ActBill_PartyAddr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_PriceListMstr')
alter table ActBill
   drop constraint FK_ActBill_PriceListMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_ReceiptMstr')
alter table ActBill
   drop constraint FK_ActBill_ReceiptMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ActBill') and o.name = 'FK_ActBill_Uom')
alter table ActBill
   drop constraint FK_ActBill_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('AutoOrderTrack') and o.name = 'FK_AutoOrderTrack_OrderDet')
alter table AutoOrderTrack
   drop constraint FK_AutoOrderTrack_OrderDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('AutoOrderTrack') and o.name = 'FK_AutoOrderTrack_OrderDet1')
alter table AutoOrderTrack
   drop constraint FK_AutoOrderTrack_OrderDet1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BatchJobParam') and o.name = 'FK_BatchJobParam_BatchJobDet')
alter table BatchJobParam
   drop constraint FK_BatchJobParam_BatchJobDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BatchRunLog') and o.name = 'FK_BatchRunLog_BatchJobDet')
alter table BatchRunLog
   drop constraint FK_BatchRunLog_BatchJobDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BatchRunLog') and o.name = 'FK_BatchRunLog_BatchTrigger')
alter table BatchRunLog
   drop constraint FK_BatchRunLog_BatchTrigger
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BatchTrigger') and o.name = 'FK_BatchTrigger_BatchJobDet')
alter table BatchTrigger
   drop constraint FK_BatchTrigger_BatchJobDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BatchTriggerParam') and o.name = 'FK_BatchTriggerParam_BatchTrigger')
alter table BatchTriggerParam
   drop constraint FK_BatchTriggerParam_BatchTrigger
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillDet') and o.name = 'FK_BillDet_BillMstr')
alter table BillDet
   drop constraint FK_BillDet_BillMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillDet') and o.name = 'FK_BillDet_BillTrans')
alter table BillDet
   drop constraint FK_BillDet_BillTrans
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillDet') and o.name = 'FK_BillDet_Currency')
alter table BillDet
   drop constraint FK_BillDet_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillMstr') and o.name = 'FK_BillMstr_ACC_User')
alter table BillMstr
   drop constraint FK_BillMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillMstr') and o.name = 'FK_BillMstr_ACC_User1')
alter table BillMstr
   drop constraint FK_BillMstr_ACC_User1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillMstr') and o.name = 'FK_BillMstr_Currency')
alter table BillMstr
   drop constraint FK_BillMstr_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BillMstr') and o.name = 'FK_BillMstr_PartyAddr')
alter table BillMstr
   drop constraint FK_BillMstr_PartyAddr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BomDet') and o.name = 'FK_BomDet_BomMstr')
alter table BomDet
   drop constraint FK_BomDet_BomMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BomDet') and o.name = 'FK_BomDet_Item')
alter table BomDet
   drop constraint FK_BomDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BomDet') and o.name = 'FK_BomDet_Location')
alter table BomDet
   drop constraint FK_BomDet_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BomDet') and o.name = 'FK_BomDet_Uom')
alter table BomDet
   drop constraint FK_BomDet_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BomMstr') and o.name = 'FK_BomMstr_Region')
alter table BomMstr
   drop constraint FK_BomMstr_Region
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BomMstr') and o.name = 'FK_BomMstr_Uom')
alter table BomMstr
   drop constraint FK_BomMstr_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClientLog') and o.name = 'FK_ClientLog_Client')
alter table ClientLog
   drop constraint FK_ClientLog_Client
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClientMonitor') and o.name = 'FK_ClientMonitor_Client')
alter table ClientMonitor
   drop constraint FK_ClientMonitor_Client
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClientOrderDet') and o.name = 'FK_ClientOrderDet_ClientOrderMstr')
alter table ClientOrderDet
   drop constraint FK_ClientOrderDet_ClientOrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClientOrderMstr') and o.name = 'FK_ClientOrderMstr_Client')
alter table ClientOrderMstr
   drop constraint FK_ClientOrderMstr_Client
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClientWorkingHours') and o.name = 'FK_ClientWorkingHours_ClientOrderMstr')
alter table ClientWorkingHours
   drop constraint FK_ClientWorkingHours_ClientOrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CurrencyExchange') and o.name = 'FK_CurrencyExchange_Currency')
alter table CurrencyExchange
   drop constraint FK_CurrencyExchange_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountDet') and o.name = 'FK_CycleCountDet_CycleCountMstr')
alter table CycleCountDet
   drop constraint FK_CycleCountDet_CycleCountMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountDet') and o.name = 'FK_CycleCountDet_HuDet')
alter table CycleCountDet
   drop constraint FK_CycleCountDet_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountDet') and o.name = 'FK_CycleCountDet_Item')
alter table CycleCountDet
   drop constraint FK_CycleCountDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountMstr') and o.name = 'FK_CycleCountMstr_ACC_User')
alter table CycleCountMstr
   drop constraint FK_CycleCountMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountMstr') and o.name = 'FK_CycleCountMstr_ACC_User1')
alter table CycleCountMstr
   drop constraint FK_CycleCountMstr_ACC_User1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountMstr') and o.name = 'FK_CycleCountMstr_ACC_User2')
alter table CycleCountMstr
   drop constraint FK_CycleCountMstr_ACC_User2
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountMstr') and o.name = 'FK_CycleCountMstr_ACC_User3')
alter table CycleCountMstr
   drop constraint FK_CycleCountMstr_ACC_User3
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountMstr') and o.name = 'FK_CycleCountMstr_ACC_User4')
alter table CycleCountMstr
   drop constraint FK_CycleCountMstr_ACC_User4
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountMstr') and o.name = 'FK_CycleCountMstr_Location')
alter table CycleCountMstr
   drop constraint FK_CycleCountMstr_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountResult') and o.name = 'FK_CycleCountResult_CycleCountMstr')
alter table CycleCountResult
   drop constraint FK_CycleCountResult_CycleCountMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountResult') and o.name = 'FK_CycleCountResult_HuDet')
alter table CycleCountResult
   drop constraint FK_CycleCountResult_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CycleCountResult') and o.name = 'FK_CycleCountResult_Item')
alter table CycleCountResult
   drop constraint FK_CycleCountResult_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DssExpHis') and o.name = 'FK_DssExpMstr_DssOutboundCtrl')
alter table DssExpHis
   drop constraint FK_DssExpMstr_DssOutboundCtrl
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DssExpHisDet') and o.name = 'FK_DssExpHisDet_DssExpHis')
alter table DssExpHisDet
   drop constraint FK_DssExpHisDet_DssExpHis
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DssOutboundCtrl') and o.name = 'FK_DssOutboundCtrl_DssSysMstr')
alter table DssOutboundCtrl
   drop constraint FK_DssOutboundCtrl_DssSysMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_Employee_ACC_User')
alter table Employee
   drop constraint FK_Employee_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowBinding') and o.name = 'FK_FlowBinding_FlowMstr_Mstr')
alter table FlowBinding
   drop constraint FK_FlowBinding_FlowMstr_Mstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowBinding') and o.name = 'FK_FlowBinding_FlowMstr_Slv')
alter table FlowBinding
   drop constraint FK_FlowBinding_FlowMstr_Slv
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_FlowDet_FlowMstr')
alter table FlowDet
   drop constraint FK_FlowDet_FlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_FlowDet_PriceListMstr')
alter table FlowDet
   drop constraint FK_FlowDet_PriceListMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_FlowDet_PriceListMstr1')
alter table FlowDet
   drop constraint FK_FlowDet_PriceListMstr1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_FlowDet_PriceListMstr2')
alter table FlowDet
   drop constraint FK_FlowDet_PriceListMstr2
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_FlowDet_Uom')
alter table FlowDet
   drop constraint FK_FlowDet_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_ItemFlowDet_BomMstr')
alter table FlowDet
   drop constraint FK_ItemFlowDet_BomMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_ItemFlowDet_Item')
alter table FlowDet
   drop constraint FK_ItemFlowDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_ItemFlowDet_Location')
alter table FlowDet
   drop constraint FK_ItemFlowDet_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_ItemFlowDet_Location1')
alter table FlowDet
   drop constraint FK_ItemFlowDet_Location1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_ItemFlowDet_PartyAddr')
alter table FlowDet
   drop constraint FK_ItemFlowDet_PartyAddr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowDet') and o.name = 'FK_ItemFlowDet_PartyAddr1')
alter table FlowDet
   drop constraint FK_ItemFlowDet_PartyAddr1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_ACC_User_CreateUser')
alter table FlowMstr
   drop constraint FK_FlowMstr_ACC_User_CreateUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_ACC_User_LastModifyUser')
alter table FlowMstr
   drop constraint FK_FlowMstr_ACC_User_LastModifyUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_Currency')
alter table FlowMstr
   drop constraint FK_FlowMstr_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_LocationFrom')
alter table FlowMstr
   drop constraint FK_FlowMstr_LocationFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_LocationTo')
alter table FlowMstr
   drop constraint FK_FlowMstr_LocationTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyAddr_BillFrom')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyAddr_BillFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyAddr_BillTo')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyAddr_BillTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyAddr_CarrierBillAddr')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyAddr_CarrierBillAddr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyAddr_ShipFrom')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyAddr_ShipFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyAddr_ShipTo')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyAddr_ShipTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyFrom')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PartyTo')
alter table FlowMstr
   drop constraint FK_FlowMstr_PartyTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_Party_Carrier')
alter table FlowMstr
   drop constraint FK_FlowMstr_Party_Carrier
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PriceListFrom')
alter table FlowMstr
   drop constraint FK_FlowMstr_PriceListFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_PriceListTo')
alter table FlowMstr
   drop constraint FK_FlowMstr_PriceListTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_RefFlowMstr')
alter table FlowMstr
   drop constraint FK_FlowMstr_RefFlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_ReturnRouting')
alter table FlowMstr
   drop constraint FK_FlowMstr_ReturnRouting
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowMstr') and o.name = 'FK_FlowMstr_Routing')
alter table FlowMstr
   drop constraint FK_FlowMstr_Routing
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_ACC_User')
alter table HuDet
   drop constraint FK_HuDet_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_HuDet')
alter table HuDet
   drop constraint FK_HuDet_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_Item')
alter table HuDet
   drop constraint FK_HuDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_Location')
alter table HuDet
   drop constraint FK_HuDet_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_Party')
alter table HuDet
   drop constraint FK_HuDet_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_StorageBin')
alter table HuDet
   drop constraint FK_HuDet_StorageBin
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuDet') and o.name = 'FK_HuDet_Uom')
alter table HuDet
   drop constraint FK_HuDet_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuOdd') and o.name = 'FK_HuOdd_ACC_CreateUser')
alter table HuOdd
   drop constraint FK_HuOdd_ACC_CreateUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuOdd') and o.name = 'FK_HuOdd_ACC_LastModifyUser')
alter table HuOdd
   drop constraint FK_HuOdd_ACC_LastModifyUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuOdd') and o.name = 'FK_HuOdd_LocationLotDet')
alter table HuOdd
   drop constraint FK_HuOdd_LocationLotDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HuOdd') and o.name = 'FK_HuOdd_OrderDet')
alter table HuOdd
   drop constraint FK_HuOdd_OrderDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectDet') and o.name = 'FK_InspectDet_InspectMstr')
alter table InspectDet
   drop constraint FK_InspectDet_InspectMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectDet') and o.name = 'FK_InspectDet_Item')
alter table InspectDet
   drop constraint FK_InspectDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectDet') and o.name = 'FK_InspectDet_LocationLotDet')
alter table InspectDet
   drop constraint FK_InspectDet_LocationLotDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectDet') and o.name = 'FK_InspectDet_Location_From')
alter table InspectDet
   drop constraint FK_InspectDet_Location_From
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectDet') and o.name = 'FK_InspectDet_Location_To')
alter table InspectDet
   drop constraint FK_InspectDet_Location_To
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectMstr') and o.name = 'FK_InspectMstr_ACC_User')
alter table InspectMstr
   drop constraint FK_InspectMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectMstr') and o.name = 'FK_InspectMstr_ACC_User1')
alter table InspectMstr
   drop constraint FK_InspectMstr_ACC_User1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('InspectMstr') and o.name = 'FK_InspectMstr_Party')
alter table InspectMstr
   drop constraint FK_InspectMstr_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_AsnDet_OrderLocTrans')
alter table IpDet
   drop constraint FK_AsnDet_OrderLocTrans
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_HuDet')
alter table IpDet
   drop constraint FK_IpDet_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_IpMstr')
alter table IpDet
   drop constraint FK_IpDet_IpMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_Item')
alter table IpDet
   drop constraint FK_IpDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_Location_From')
alter table IpDet
   drop constraint FK_IpDet_Location_From
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_Location_To')
alter table IpDet
   drop constraint FK_IpDet_Location_To
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_PlanBill')
alter table IpDet
   drop constraint FK_IpDet_PlanBill
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpDet') and o.name = 'FK_IpDet_Uom')
alter table IpDet
   drop constraint FK_IpDet_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_AsnMstr_ACC_User_CreateUser')
alter table IpMstr
   drop constraint FK_AsnMstr_ACC_User_CreateUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_AsnMstr_ACC_User_LastModifyDate')
alter table IpMstr
   drop constraint FK_AsnMstr_ACC_User_LastModifyDate
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_IpMstr_PartyAddr_ShipFrom')
alter table IpMstr
   drop constraint FK_IpMstr_PartyAddr_ShipFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_IpMstr_PartyAddr_ShipTo')
alter table IpMstr
   drop constraint FK_IpMstr_PartyAddr_ShipTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_IpMstr_PartyFrom')
alter table IpMstr
   drop constraint FK_IpMstr_PartyFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_IpMstr_PartyTo')
alter table IpMstr
   drop constraint FK_IpMstr_PartyTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpMstr') and o.name = 'FK_IpMstr_ReceiptMstr')
alter table IpMstr
   drop constraint FK_IpMstr_ReceiptMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpTrack') and o.name = 'FK_IpTrack_ACC_User')
alter table IpTrack
   drop constraint FK_IpTrack_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpTrack') and o.name = 'FK_IpTrack_IpMstr')
alter table IpTrack
   drop constraint FK_IpTrack_IpMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IpTrack') and o.name = 'FK_IpTrack_WorkCenter')
alter table IpTrack
   drop constraint FK_IpTrack_WorkCenter
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Item') and o.name = 'FK_Item_ACC_User')
alter table Item
   drop constraint FK_Item_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Item') and o.name = 'FK_Item_BomMstr')
alter table Item
   drop constraint FK_Item_BomMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Item') and o.name = 'FK_Item_Location')
alter table Item
   drop constraint FK_Item_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Item') and o.name = 'FK_Item_RoutingMstr')
alter table Item
   drop constraint FK_Item_RoutingMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Item') and o.name = 'FK_Item_UnitOfMeasure')
alter table Item
   drop constraint FK_Item_UnitOfMeasure
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemFlowPlanDet') and o.name = 'FK_ItemFlowPlanDet_ItemFlowPlanMstr')
alter table ItemFlowPlanDet
   drop constraint FK_ItemFlowPlanDet_ItemFlowPlanMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemFlowPlanMstr') and o.name = 'FK_FlowPlanMstr_FlowMstr')
alter table ItemFlowPlanMstr
   drop constraint FK_FlowPlanMstr_FlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemFlowPlanMstr') and o.name = 'FK_ItemFlowPlanMstr_FlowDet')
alter table ItemFlowPlanMstr
   drop constraint FK_ItemFlowPlanMstr_FlowDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemFlowPlanTrack') and o.name = 'FK_ItemFlowPlanTrack_OrderLocTrans')
alter table ItemFlowPlanTrack
   drop constraint FK_ItemFlowPlanTrack_OrderLocTrans
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemKit') and o.name = 'FK_ItemKit_Item_Child')
alter table ItemKit
   drop constraint FK_ItemKit_Item_Child
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemKit') and o.name = 'FK_ItemKit_Item_Parent')
alter table ItemKit
   drop constraint FK_ItemKit_Item_Parent
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemRef') and o.name = 'FK_ItemRef_Item')
alter table ItemRef
   drop constraint FK_ItemRef_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ItemRef') and o.name = 'FK_ItemRef_Party')
alter table ItemRef
   drop constraint FK_ItemRef_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Location') and o.name = 'FK_Location_Location')
alter table Location
   drop constraint FK_Location_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Location') and o.name = 'FK_Location_Region')
alter table Location
   drop constraint FK_Location_Region
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LocationLotDet') and o.name = 'FK_LocationLotDet_HuDet')
alter table LocationLotDet
   drop constraint FK_LocationLotDet_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LocationLotDet') and o.name = 'FK_LocationLotDet_Item')
alter table LocationLotDet
   drop constraint FK_LocationLotDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LocationLotDet') and o.name = 'FK_LocationLotDet_Location')
alter table LocationLotDet
   drop constraint FK_LocationLotDet_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LocationLotDet') and o.name = 'FK_LocationLotDet_PlanBill')
alter table LocationLotDet
   drop constraint FK_LocationLotDet_PlanBill
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LocationLotDet') and o.name = 'FK_LocationLotDet_StorageBin')
alter table LocationLotDet
   drop constraint FK_LocationLotDet_StorageBin
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MiscOrderDet') and o.name = 'FK_MiscOrderDet_HuDet')
alter table MiscOrderDet
   drop constraint FK_MiscOrderDet_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MiscOrderDet') and o.name = 'FK_MiscOrderDet_Item')
alter table MiscOrderDet
   drop constraint FK_MiscOrderDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MiscOrderDet') and o.name = 'FK_MiscOrderDet_MiscOrderMstr')
alter table MiscOrderDet
   drop constraint FK_MiscOrderDet_MiscOrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MiscOrderMstr') and o.name = 'FK_MiscOrderMstr_ACC_User')
alter table MiscOrderMstr
   drop constraint FK_MiscOrderMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MiscOrderMstr') and o.name = 'FK_MiscOrderMstr_Location')
alter table MiscOrderMstr
   drop constraint FK_MiscOrderMstr_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NamedQuery') and o.name = 'FK_NamedQuery_ACC_User')
alter table NamedQuery
   drop constraint FK_NamedQuery_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderBinding') and o.name = 'FK_OrderBinding_FlowMstr')
alter table OrderBinding
   drop constraint FK_OrderBinding_FlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderBinding') and o.name = 'FK_OrderBinding_Order')
alter table OrderBinding
   drop constraint FK_OrderBinding_Order
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderBinding') and o.name = 'FK_OrderBinding_Order_BindOrder')
alter table OrderBinding
   drop constraint FK_OrderBinding_Order_BindOrder
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_BomMstr')
alter table OrderDet
   drop constraint FK_OrderDet_BomMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_Location_From')
alter table OrderDet
   drop constraint FK_OrderDet_Location_From
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_Location_To')
alter table OrderDet
   drop constraint FK_OrderDet_Location_To
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_OrderMstr')
alter table OrderDet
   drop constraint FK_OrderDet_OrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_Party')
alter table OrderDet
   drop constraint FK_OrderDet_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_PartyAddr_BillFrom')
alter table OrderDet
   drop constraint FK_OrderDet_PartyAddr_BillFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_PartyAddr_BillTo')
alter table OrderDet
   drop constraint FK_OrderDet_PartyAddr_BillTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_PriceListDetFrom')
alter table OrderDet
   drop constraint FK_OrderDet_PriceListDetFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_PriceListDetTo')
alter table OrderDet
   drop constraint FK_OrderDet_PriceListDetTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_PriceListMstr_From')
alter table OrderDet
   drop constraint FK_OrderDet_PriceListMstr_From
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderDet_PriceListMstr_To')
alter table OrderDet
   drop constraint FK_OrderDet_PriceListMstr_To
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderItem_Item')
alter table OrderDet
   drop constraint FK_OrderItem_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDet') and o.name = 'FK_OrderItem_UnitOfMeasure')
alter table OrderDet
   drop constraint FK_OrderItem_UnitOfMeasure
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderLocTrans') and o.name = 'FK_OrderLocTrans_BomDet')
alter table OrderLocTrans
   drop constraint FK_OrderLocTrans_BomDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderLocTrans') and o.name = 'FK_OrderLocTrans_Item')
alter table OrderLocTrans
   drop constraint FK_OrderLocTrans_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderLocTrans') and o.name = 'FK_OrderLocTrans_Location')
alter table OrderLocTrans
   drop constraint FK_OrderLocTrans_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderLocTrans') and o.name = 'FK_OrderLocTrans_OrderDet')
alter table OrderLocTrans
   drop constraint FK_OrderLocTrans_OrderDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderLocTrans') and o.name = 'FK_OrderLocTrans_RejLocation')
alter table OrderLocTrans
   drop constraint FK_OrderLocTrans_RejLocation
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderLocTrans') and o.name = 'FK_OrderLocTrans_Uom')
alter table OrderLocTrans
   drop constraint FK_OrderLocTrans_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ACC_User_CancelUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_ACC_User_CancelUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ACC_User_CloseUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_ACC_User_CloseUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ACC_User_CreateUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_ACC_User_CreateUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ACC_User_LastModifyUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_ACC_User_LastModifyUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ACC_User_ReleaseUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_ACC_User_ReleaseUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ACC_User_StartUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_ACC_User_StartUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_Currency')
alter table OrderMstr
   drop constraint FK_OrderMstr_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_Flow')
alter table OrderMstr
   drop constraint FK_OrderMstr_Flow
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_LocationFrom')
alter table OrderMstr
   drop constraint FK_OrderMstr_LocationFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_LocationTo')
alter table OrderMstr
   drop constraint FK_OrderMstr_LocationTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_OrderMstr_CompleteUser')
alter table OrderMstr
   drop constraint FK_OrderMstr_OrderMstr_CompleteUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyAddr_BillFrom')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyAddr_BillFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyAddr_BillTo')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyAddr_BillTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyAddr_CarrierBillAddr')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyAddr_CarrierBillAddr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyAddr_ShipFrom')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyAddr_ShipFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyAddr_ShipTo')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyAddr_ShipTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyFrom')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PartyTo')
alter table OrderMstr
   drop constraint FK_OrderMstr_PartyTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_Party_Carrier')
alter table OrderMstr
   drop constraint FK_OrderMstr_Party_Carrier
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PriceListFrom')
alter table OrderMstr
   drop constraint FK_OrderMstr_PriceListFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_PriceListTo')
alter table OrderMstr
   drop constraint FK_OrderMstr_PriceListTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_Routing')
alter table OrderMstr
   drop constraint FK_OrderMstr_Routing
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderMstr') and o.name = 'FK_OrderMstr_ShiftMstr')
alter table OrderMstr
   drop constraint FK_OrderMstr_ShiftMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderOp') and o.name = 'FK_OrderOp_OrderMstr')
alter table OrderOp
   drop constraint FK_OrderOp_OrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderOp') and o.name = 'FK_OrderOp_WorkCenter')
alter table OrderOp
   drop constraint FK_OrderOp_WorkCenter
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Party') and o.name = 'FK_Party_Party')
alter table Party
   drop constraint FK_Party_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PartyAddr') and o.name = 'FK_PartyAddr_Party')
alter table PartyAddr
   drop constraint FK_PartyAddr_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PackListDet_Item')
alter table PickListDet
   drop constraint FK_PackListDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PackListDet_Location')
alter table PickListDet
   drop constraint FK_PackListDet_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PackListDet_PackListMstr')
alter table PickListDet
   drop constraint FK_PackListDet_PackListMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PackListDet_StorageArea')
alter table PickListDet
   drop constraint FK_PackListDet_StorageArea
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PackListDet_StorageBin')
alter table PickListDet
   drop constraint FK_PackListDet_StorageBin
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PickListDet_HuDet')
alter table PickListDet
   drop constraint FK_PickListDet_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListDet') and o.name = 'FK_PickListDet_OrderLocTrans')
alter table PickListDet
   drop constraint FK_PickListDet_OrderLocTrans
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PackListMstr_ACC_User_CreateUser')
alter table PickListMstr
   drop constraint FK_PackListMstr_ACC_User_CreateUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PackListMstr_ACC_User_LastModifyUser')
alter table PickListMstr
   drop constraint FK_PackListMstr_ACC_User_LastModifyUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PickListMstr_ACC_User')
alter table PickListMstr
   drop constraint FK_PickListMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PickListMstr_FlowMstr')
alter table PickListMstr
   drop constraint FK_PickListMstr_FlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PickListMstr_PartyAddr_ShipFrom')
alter table PickListMstr
   drop constraint FK_PickListMstr_PartyAddr_ShipFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PickListMstr_PartyAddr_ShipTo')
alter table PickListMstr
   drop constraint FK_PickListMstr_PartyAddr_ShipTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PickListMstr_PartyFrom')
alter table PickListMstr
   drop constraint FK_PickListMstr_PartyFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListMstr') and o.name = 'FK_PickListMstr_PartyTo')
alter table PickListMstr
   drop constraint FK_PickListMstr_PartyTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListResult') and o.name = 'FK_PackListResult_PackListDet')
alter table PickListResult
   drop constraint FK_PackListResult_PackListDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PickListResult') and o.name = 'FK_PickListResult_LocationLotDet')
alter table PickListResult
   drop constraint FK_PickListResult_LocationLotDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_ACC_User')
alter table PlanBill
   drop constraint FK_PlanBill_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_ACC_User1')
alter table PlanBill
   drop constraint FK_PlanBill_ACC_User1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_Currency')
alter table PlanBill
   drop constraint FK_PlanBill_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_HuDet')
alter table PlanBill
   drop constraint FK_PlanBill_HuDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_Item')
alter table PlanBill
   drop constraint FK_PlanBill_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_OrderMstr')
alter table PlanBill
   drop constraint FK_PlanBill_OrderMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_PartyAddr')
alter table PlanBill
   drop constraint FK_PlanBill_PartyAddr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_PriceListMstr')
alter table PlanBill
   drop constraint FK_PlanBill_PriceListMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_ReceiptMstr')
alter table PlanBill
   drop constraint FK_PlanBill_ReceiptMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PlanBill') and o.name = 'FK_PlanBill_Uom')
alter table PlanBill
   drop constraint FK_PlanBill_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PriceListDet') and o.name = 'FK_PriceListDet_Currency')
alter table PriceListDet
   drop constraint FK_PriceListDet_Currency
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PriceListDet') and o.name = 'FK_PriceListDet_Item')
alter table PriceListDet
   drop constraint FK_PriceListDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PriceListDet') and o.name = 'FK_PriceListDet_PriceList')
alter table PriceListDet
   drop constraint FK_PriceListDet_PriceList
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PriceListDet') and o.name = 'FK_PriceListDet_Uom')
alter table PriceListDet
   drop constraint FK_PriceListDet_Uom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PriceListMstr') and o.name = 'FK_PriceListMstr_Party')
alter table PriceListMstr
   drop constraint FK_PriceListMstr_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProdLineIp') and o.name = 'FK_ProdLineIp_ACC_User')
alter table ProdLineIp
   drop constraint FK_ProdLineIp_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProdLineIp') and o.name = 'FK_ProdLineIp_ACC_User1')
alter table ProdLineIp
   drop constraint FK_ProdLineIp_ACC_User1
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProdLineIp') and o.name = 'FK_ProdLineIp_FlowMstr')
alter table ProdLineIp
   drop constraint FK_ProdLineIp_FlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProdLineIp') and o.name = 'FK_ProdLineIp_Item')
alter table ProdLineIp
   drop constraint FK_ProdLineIp_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProdLineIp') and o.name = 'FK_ProdLineIp_Location')
alter table ProdLineIp
   drop constraint FK_ProdLineIp_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProdLineIp') and o.name = 'FK_ProdLineIp_PlanBill')
alter table ProdLineIp
   drop constraint FK_ProdLineIp_PlanBill
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptDet') and o.name = 'FK_ReceiptDet_OrderLocTrans')
alter table ReceiptDet
   drop constraint FK_ReceiptDet_OrderLocTrans
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptDet') and o.name = 'FK_ReceiptDet_PlanBill')
alter table ReceiptDet
   drop constraint FK_ReceiptDet_PlanBill
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptDet') and o.name = 'FK_ReceiptDet_ReceiptMstr')
alter table ReceiptDet
   drop constraint FK_ReceiptDet_ReceiptMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptIp') and o.name = 'FK_ReceiptIp_IpMstr')
alter table ReceiptIp
   drop constraint FK_ReceiptIp_IpMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptIp') and o.name = 'FK_ReceiptIp_ReceiptMstr')
alter table ReceiptIp
   drop constraint FK_ReceiptIp_ReceiptMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptMstr') and o.name = 'FK_ReceiptMstr_ACC_User')
alter table ReceiptMstr
   drop constraint FK_ReceiptMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptMstr') and o.name = 'FK_ReceiptMstr_PartyAddr_ShipFrom')
alter table ReceiptMstr
   drop constraint FK_ReceiptMstr_PartyAddr_ShipFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptMstr') and o.name = 'FK_ReceiptMstr_PartyAddr_ShipTo')
alter table ReceiptMstr
   drop constraint FK_ReceiptMstr_PartyAddr_ShipTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptMstr') and o.name = 'FK_ReceiptMstr_PartyFrom')
alter table ReceiptMstr
   drop constraint FK_ReceiptMstr_PartyFrom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReceiptMstr') and o.name = 'FK_ReceiptMstr_PartyTo')
alter table ReceiptMstr
   drop constraint FK_ReceiptMstr_PartyTo
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RepackDet') and o.name = 'FK_RepackDet_LocationLotDet')
alter table RepackDet
   drop constraint FK_RepackDet_LocationLotDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RepackDet') and o.name = 'FK_RepackDet_RepackMstr')
alter table RepackDet
   drop constraint FK_RepackDet_RepackMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RepackMstr') and o.name = 'FK_RepackMstr_ACC_User')
alter table RepackMstr
   drop constraint FK_RepackMstr_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RollingPlanDet') and o.name = 'FK_RollingPlanDet_Item')
alter table RollingPlanDet
   drop constraint FK_RollingPlanDet_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RollingPlanDet') and o.name = 'FK_RollingPlanDet_RollingPlanMstr')
alter table RollingPlanDet
   drop constraint FK_RollingPlanDet_RollingPlanMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RollingPlanMstr') and o.name = 'FK_RollingPlanMstr_FileUpload')
alter table RollingPlanMstr
   drop constraint FK_RollingPlanMstr_FileUpload
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RollingPlanMstr') and o.name = 'FK_RollingPlanMstr_FlowMstr')
alter table RollingPlanMstr
   drop constraint FK_RollingPlanMstr_FlowMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RoutingDet') and o.name = 'FK_RoutingDet_Location_From')
alter table RoutingDet
   drop constraint FK_RoutingDet_Location_From
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RoutingDet') and o.name = 'FK_RoutingDet_Routing')
alter table RoutingDet
   drop constraint FK_RoutingDet_Routing
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RoutingDet') and o.name = 'FK_RoutingDet_WorkCenter')
alter table RoutingDet
   drop constraint FK_RoutingDet_WorkCenter
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RoutingMstr') and o.name = 'FK_Routing_Region')
alter table RoutingMstr
   drop constraint FK_Routing_Region
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ShiftDet') and o.name = 'FK_ShiftDet_ShiftMstr')
alter table ShiftDet
   drop constraint FK_ShiftDet_ShiftMstr
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ShiftPlanSchedule') and o.name = 'FK_ShiftPlanSchedule_ACC_User')
alter table ShiftPlanSchedule
   drop constraint FK_ShiftPlanSchedule_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ShiftPlanSchedule') and o.name = 'FK_ShiftPlanSchedule_FlowDet')
alter table ShiftPlanSchedule
   drop constraint FK_ShiftPlanSchedule_FlowDet
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SpecialTime') and o.name = 'FK_SpecialTime_Region')
alter table SpecialTime
   drop constraint FK_SpecialTime_Region
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SpecialTime') and o.name = 'FK_SpecialTime_WorkCenter')
alter table SpecialTime
   drop constraint FK_SpecialTime_WorkCenter
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StorageArea') and o.name = 'FK_StorageArea_Location')
alter table StorageArea
   drop constraint FK_StorageArea_Location
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StorageBin') and o.name = 'FK_StorageBin_StorageArea')
alter table StorageBin
   drop constraint FK_StorageBin_StorageArea
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UomConv') and o.name = 'FK_UomConv_AltUom')
alter table UomConv
   drop constraint FK_UomConv_AltUom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UomConv') and o.name = 'FK_UomConv_BaseUom')
alter table UomConv
   drop constraint FK_UomConv_BaseUom
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UomConv') and o.name = 'FK_UomConv_Item')
alter table UomConv
   drop constraint FK_UomConv_Item
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkCenter') and o.name = 'FK_WorkCenter_Party')
alter table WorkCenter
   drop constraint FK_WorkCenter_Party
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Workday') and o.name = 'FK_Workday_Region')
alter table Workday
   drop constraint FK_Workday_Region
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Workday') and o.name = 'FK_Workday_WorkCenter')
alter table Workday
   drop constraint FK_Workday_WorkCenter
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkdayShift') and o.name = 'FK_WorkdayShift_Workday')
alter table WorkdayShift
   drop constraint FK_WorkdayShift_Workday
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkingHours') and o.name = 'FK_WorkingHours_ACC_User')
alter table WorkingHours
   drop constraint FK_WorkingHours_ACC_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkingHours') and o.name = 'FK_WorkingHours_Employee')
alter table WorkingHours
   drop constraint FK_WorkingHours_Employee
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkingHours') and o.name = 'FK_WorkingHours_ReceiptMstr')
alter table WorkingHours
   drop constraint FK_WorkingHours_ReceiptMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ActBillView')
            and   type = 'V')
   drop view ActBillView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BillAgingView')
            and   type = 'V')
   drop view BillAgingView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowView')
            and   type = 'V')
   drop view FlowView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('InvAgingView')
            and   type = 'V')
   drop view InvAgingView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IpDetTrackView')
            and   type = 'V')
   drop view IpDetTrackView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IpDetView')
            and   type = 'V')
   drop view IpDetView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LeanEngineView')
            and   type = 'V')
   drop view LeanEngineView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocBinDet')
            and   type = 'V')
   drop view LocBinDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocBinItemDet')
            and   type = 'V')
   drop view LocBinItemDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocBinMstr')
            and   type = 'V')
   drop view LocBinMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocLotDetView')
            and   type = 'V')
   drop view LocLotDetView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocTransView')
            and   type = 'V')
   drop view LocTransView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocationDet')
            and   type = 'V')
   drop view LocationDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MenuView')
            and   type = 'V')
   drop view MenuView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderDetView')
            and   type = 'V')
   drop view OrderDetView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderLocTransView')
            and   type = 'V')
   drop view OrderLocTransView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PlanBillView')
            and   type = 'V')
   drop view PlanBillView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SupplierLocationView')
            and   type = 'V')
   drop view SupplierLocationView
go

alter table ACC_Company
   drop constraint PK__ACC_Company__58920452
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_Company')
            and   type = 'U')
   drop table ACC_Company
go

alter table ACC_Industry
   drop constraint PK__ACC_Industry__56A9BBE0
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_Industry')
            and   type = 'U')
   drop table ACC_Industry
go

alter table ACC_Menu
   drop constraint PK__ACC_Menu__5A7A4CC4
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_Menu')
            and   type = 'U')
   drop table ACC_Menu
go

alter table ACC_MenuCommon
   drop constraint PK__ACC_MenuCommon__5C629536
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_MenuCommon')
            and   type = 'U')
   drop table ACC_MenuCommon
go

alter table ACC_MenuCompany
   drop constraint PK__ACC_MenuCompany__6033261A
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_MenuCompany')
            and   type = 'U')
   drop table ACC_MenuCompany
go

alter table ACC_MenuIndustry
   drop constraint PK__ACC_MenuIndustry__5E4ADDA8
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_MenuIndustry')
            and   type = 'U')
   drop table ACC_MenuIndustry
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ACC_Permission')
            and   name  = 'IX_ACC_Permission'
            and   indid > 0
            and   indid < 255)
   drop index ACC_Permission.IX_ACC_Permission
go

alter table ACC_Permission
   drop constraint PK_ACC_Permission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_Permission')
            and   type = 'U')
   drop table ACC_Permission
go

alter table ACC_PermissionCategory
   drop constraint PK_ACC_PermissionCategory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_PermissionCategory')
            and   type = 'U')
   drop table ACC_PermissionCategory
go

alter table ACC_Role
   drop constraint PK_ACC_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_Role')
            and   type = 'U')
   drop table ACC_Role
go

alter table ACC_RolePermission
   drop constraint PK_ACC_RolePermission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_RolePermission')
            and   type = 'U')
   drop table ACC_RolePermission
go

alter table ACC_User
   drop constraint IX_ACC_User
go

alter table ACC_User
   drop constraint PK_ACC_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_User')
            and   type = 'U')
   drop table ACC_User
go

alter table ACC_UserFav
   drop constraint IX_Favorites
go

alter table ACC_UserFav
   drop constraint PK_Favorites
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_UserFav')
            and   type = 'U')
   drop table ACC_UserFav
go

alter table ACC_UserPermission
   drop constraint PK_ACC_UserPermission_1
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_UserPermission')
            and   type = 'U')
   drop table ACC_UserPermission
go

alter table ACC_UserPre
   drop constraint PK_UserPreference
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_UserPre')
            and   type = 'U')
   drop table ACC_UserPre
go

alter table ACC_UserRole
   drop constraint PK_ACC_UserRole
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ACC_UserRole')
            and   type = 'U')
   drop table ACC_UserRole
go

alter table ActBill
   drop constraint PK_ActBill
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ActBill')
            and   type = 'U')
   drop table ActBill
go

alter table AutoOrderTrack
   drop constraint PK_AutoOrderTrack
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AutoOrderTrack')
            and   type = 'U')
   drop table AutoOrderTrack
go

alter table BatchJobDet
   drop constraint PK_BatchJobDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BatchJobDet')
            and   type = 'U')
   drop table BatchJobDet
go

alter table BatchJobParam
   drop constraint PK_BatchJobParam
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BatchJobParam')
            and   type = 'U')
   drop table BatchJobParam
go

alter table BatchRunLog
   drop constraint PK_BatchRunLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BatchRunLog')
            and   type = 'U')
   drop table BatchRunLog
go

alter table BatchTrigger
   drop constraint PK_BatchTrigger
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BatchTrigger')
            and   type = 'U')
   drop table BatchTrigger
go

alter table BatchTriggerParam
   drop constraint PK_BatchTriggerParam
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BatchTriggerParam')
            and   type = 'U')
   drop table BatchTriggerParam
go

alter table BillDet
   drop constraint PK_BillDet
go

alter table BillDet
   drop constraint IX_BillDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BillDet')
            and   type = 'U')
   drop table BillDet
go

alter table BillMstr
   drop constraint PK_BillMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BillMstr')
            and   type = 'U')
   drop table BillMstr
go

alter table BillTrans
   drop constraint PK_BillTrans
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BillTrans')
            and   type = 'U')
   drop table BillTrans
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BomDet')
            and   name  = 'IX_BomDet'
            and   indid > 0
            and   indid < 255)
   drop index BomDet.IX_BomDet
go

alter table BomDet
   drop constraint PK_BomDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BomDet')
            and   type = 'U')
   drop table BomDet
go

alter table BomMstr
   drop constraint PK_BomMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BomMstr')
            and   type = 'U')
   drop table BomMstr
go

alter table Client
   drop constraint PK_Client_1
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Client')
            and   type = 'U')
   drop table Client
go

alter table ClientLog
   drop constraint PK_ClientLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClientLog')
            and   type = 'U')
   drop table ClientLog
go

alter table ClientMonitor
   drop constraint PK_ClientMonitor
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClientMonitor')
            and   type = 'U')
   drop table ClientMonitor
go

alter table ClientOrderDet
   drop constraint PK_ClientOrderDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClientOrderDet')
            and   type = 'U')
   drop table ClientOrderDet
go

alter table ClientOrderMstr
   drop constraint PK_ClientOrderMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClientOrderMstr')
            and   type = 'U')
   drop table ClientOrderMstr
go

alter table ClientWorkingHours
   drop constraint PK_ClientWorkingHours
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClientWorkingHours')
            and   type = 'U')
   drop table ClientWorkingHours
go

alter table CodeMstr
   drop constraint PK_CodeMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CodeMstr')
            and   type = 'U')
   drop table CodeMstr
go

alter table Currency
   drop constraint PK_Currency
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Currency')
            and   type = 'U')
   drop table Currency
go

alter table CurrencyExchange
   drop constraint PK_ExchangeRate
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CurrencyExchange')
            and   type = 'U')
   drop table CurrencyExchange
go

alter table CycleCountDet
   drop constraint PK_CycleCountDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CycleCountDet')
            and   type = 'U')
   drop table CycleCountDet
go

alter table CycleCountMstr
   drop constraint PK_CycleCount
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CycleCountMstr')
            and   type = 'U')
   drop table CycleCountMstr
go

alter table CycleCountResult
   drop constraint PK_CycleCountResult
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CycleCountResult')
            and   type = 'U')
   drop table CycleCountResult
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DssExpHis')
            and   name  = 'IX_DssExpHis'
            and   indid > 0
            and   indid < 255)
   drop index DssExpHis.IX_DssExpHis
go

alter table DssExpHis
   drop constraint PK_DssExpHis
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssExpHis')
            and   type = 'U')
   drop table DssExpHis
go

alter table DssExpHisDet
   drop constraint PK_DssExpHisDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssExpHisDet')
            and   type = 'U')
   drop table DssExpHisDet
go

alter table DssFtpCtrl
   drop constraint PK_Dss
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssFtpCtrl')
            and   type = 'U')
   drop table DssFtpCtrl
go

alter table DssImpHis
   drop constraint PK_DssImpHis_1
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssImpHis')
            and   type = 'U')
   drop table DssImpHis
go

alter table DssInboundCtrl
   drop constraint PK_DssLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssInboundCtrl')
            and   type = 'U')
   drop table DssInboundCtrl
go

alter table DssObjectMapping
   drop constraint PK_DssObjectMapping
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssObjectMapping')
            and   type = 'U')
   drop table DssObjectMapping
go

alter table DssOutboundCtrl
   drop constraint PK_DssOutboundCtrl_1
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssOutboundCtrl')
            and   type = 'U')
   drop table DssOutboundCtrl
go

alter table DssSysMstr
   drop constraint PK_DssSysMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DssSysMstr')
            and   type = 'U')
   drop table DssSysMstr
go

alter table Employee
   drop constraint PK_Employee
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Employee')
            and   type = 'U')
   drop table Employee
go

alter table EntityOpt
   drop constraint PK_EntityPreference_1
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EntityOpt')
            and   type = 'U')
   drop table EntityOpt
go

alter table FileUpload
   drop constraint PK_FileUpload
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FileUpload')
            and   type = 'U')
   drop table FileUpload
go

alter table FlowBinding
   drop constraint PK_FlowBinding
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowBinding')
            and   type = 'U')
   drop table FlowBinding
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FlowDet')
            and   name  = 'IX_ITEM'
            and   indid > 0
            and   indid < 255)
   drop index FlowDet.IX_ITEM
go

alter table FlowDet
   drop constraint PK_ItemFlowDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowDet')
            and   type = 'U')
   drop table FlowDet
go

alter table FlowMstr
   drop constraint PK_ItemFlow
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowMstr')
            and   type = 'U')
   drop table FlowMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FlowPlan')
            and   name  = 'IX_FlowPlan_1'
            and   indid > 0
            and   indid < 255)
   drop index FlowPlan.IX_FlowPlan_1
go

alter table FlowPlan
   drop constraint PK_FlowPlan
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowPlan')
            and   type = 'U')
   drop table FlowPlan
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HuDet')
            and   name  = 'IX_ITEM'
            and   indid > 0
            and   indid < 255)
   drop index HuDet.IX_ITEM
go

alter table HuDet
   drop constraint PK_HuDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HuDet')
            and   type = 'U')
   drop table HuDet
go

alter table HuOdd
   drop constraint PK_HuOdd
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HuOdd')
            and   type = 'U')
   drop table HuOdd
go

alter table InspectDet
   drop constraint PK_InspectDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('InspectDet')
            and   type = 'U')
   drop table InspectDet
go

alter table InspectMstr
   drop constraint PK_InspectMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('InspectMstr')
            and   type = 'U')
   drop table InspectMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IpDet')
            and   name  = 'IX_IpDet'
            and   indid > 0
            and   indid < 255)
   drop index IpDet.IX_IpDet
go

alter table IpDet
   drop constraint PK_IpDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IpDet')
            and   type = 'U')
   drop table IpDet
go

alter table IpMstr
   drop constraint PK_AsnMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IpMstr')
            and   type = 'U')
   drop table IpMstr
go

alter table IpTrack
   drop constraint PK_IpTrack
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IpTrack')
            and   type = 'U')
   drop table IpTrack
go

alter table Item
   drop constraint PK_Item
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Item')
            and   type = 'U')
   drop table Item
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ItemFlowPlanDet')
            and   name  = 'IX_ItemFlowPlanDet'
            and   indid > 0
            and   indid < 255)
   drop index ItemFlowPlanDet.IX_ItemFlowPlanDet
go

alter table ItemFlowPlanDet
   drop constraint PK_ItemFlowPlanDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ItemFlowPlanDet')
            and   type = 'U')
   drop table ItemFlowPlanDet
go

alter table ItemFlowPlanMstr
   drop constraint PK_ItemFlowPlanMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ItemFlowPlanMstr')
            and   type = 'U')
   drop table ItemFlowPlanMstr
go

alter table ItemFlowPlanTrack
   drop constraint PK_ItemFlowPlanTrack
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ItemFlowPlanTrack')
            and   type = 'U')
   drop table ItemFlowPlanTrack
go

alter table ItemKit
   drop constraint PK_ItemKit
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ItemKit')
            and   type = 'U')
   drop table ItemKit
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ItemRef')
            and   name  = 'IX_ItemRef'
            and   indid > 0
            and   indid < 255)
   drop index ItemRef.IX_ItemRef
go

alter table ItemRef
   drop constraint PK_ItemRef
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ItemRef')
            and   type = 'U')
   drop table ItemRef
go

alter table LocTrans
   drop constraint PK_TransHist
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocTrans')
            and   type = 'U')
   drop table LocTrans
go

alter table Location
   drop constraint PK_Location
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Location')
            and   type = 'U')
   drop table Location
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LocationLotDet')
            and   name  = 'IX_LOCLOTDET_HUID'
            and   indid > 0
            and   indid < 255)
   drop index LocationLotDet.IX_LOCLOTDET_HUID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LocationLotDet')
            and   name  = 'IX_LOCLOTDET'
            and   indid > 0
            and   indid < 255)
   drop index LocationLotDet.IX_LOCLOTDET
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LocationLotDet')
            and   name  = '_dta_index_LocationLotDet_21_1801773476__K2_K4_K1_K5_K3_K9_6_7_8_10'
            and   indid > 0
            and   indid < 255)
   drop index LocationLotDet._dta_index_LocationLotDet_21_1801773476__K2_K4_K1_K5_K3_K9_6_7_8_10
go

alter table LocationLotDet
   drop constraint PK_LocationLotDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocationLotDet')
            and   type = 'U')
   drop table LocationLotDet
go

alter table MiscOrderDet
   drop constraint PK_MiscOrderDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MiscOrderDet')
            and   type = 'U')
   drop table MiscOrderDet
go

alter table MiscOrderMstr
   drop constraint PK_MiscOrderMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MiscOrderMstr')
            and   type = 'U')
   drop table MiscOrderMstr
go

alter table NamedQuery
   drop constraint PK_NamedQuery
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NamedQuery')
            and   type = 'U')
   drop table NamedQuery
go

alter table NumCtrl
   drop constraint PK_NumCtrl
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NumCtrl')
            and   type = 'U')
   drop table NumCtrl
go

alter table OrderBinding
   drop constraint PK_OrderBinding
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderBinding')
            and   type = 'U')
   drop table OrderBinding
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderDet')
            and   name  = 'IX_ORDDET'
            and   indid > 0
            and   indid < 255)
   drop index OrderDet.IX_ORDDET
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderDet')
            and   name  = 'IX_ITEM'
            and   indid > 0
            and   indid < 255)
   drop index OrderDet.IX_ITEM
go

alter table OrderDet
   drop constraint PK_OrderDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderDet')
            and   type = 'U')
   drop table OrderDet
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderLocTrans')
            and   name  = 'IX_ORDERLOCTRANS'
            and   indid > 0
            and   indid < 255)
   drop index OrderLocTrans.IX_ORDERLOCTRANS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderLocTrans')
            and   name  = 'IX_LOCITEM'
            and   indid > 0
            and   indid < 255)
   drop index OrderLocTrans.IX_LOCITEM
go

alter table OrderLocTrans
   drop constraint PK_OrderLocTrans
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderLocTrans')
            and   type = 'U')
   drop table OrderLocTrans
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderMstr')
            and   name  = 'IX_ORD_TYPE'
            and   indid > 0
            and   indid < 255)
   drop index OrderMstr.IX_ORD_TYPE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderMstr')
            and   name  = 'IX_ORD_STATUS'
            and   indid > 0
            and   indid < 255)
   drop index OrderMstr.IX_ORD_STATUS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderMstr')
            and   name  = 'IX_ORD_FLOW'
            and   indid > 0
            and   indid < 255)
   drop index OrderMstr.IX_ORD_FLOW
go

alter table OrderMstr
   drop constraint PK_OrderMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderMstr')
            and   type = 'U')
   drop table OrderMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderOp')
            and   name  = 'IX_OrderOp'
            and   indid > 0
            and   indid < 255)
   drop index OrderOp.IX_OrderOp
go

alter table OrderOp
   drop constraint PK_OrderOp
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderOp')
            and   type = 'U')
   drop table OrderOp
go

alter table OrderPlanBackflush
   drop constraint PK_OrderPlanBackflush
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderPlanBackflush')
            and   type = 'U')
   drop table OrderPlanBackflush
go

alter table Party
   drop constraint PK_Party
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Party')
            and   type = 'U')
   drop table Party
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PartyAddr')
            and   name  = 'IX_PARTYADDR'
            and   indid > 0
            and   indid < 255)
   drop index PartyAddr.IX_PARTYADDR
go

alter table PartyAddr
   drop constraint PK_PartyAddr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PartyAddr')
            and   type = 'U')
   drop table PartyAddr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PickListDet')
            and   name  = 'IX_ITEM'
            and   indid > 0
            and   indid < 255)
   drop index PickListDet.IX_ITEM
go

alter table PickListDet
   drop constraint PK_PackListDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PickListDet')
            and   type = 'U')
   drop table PickListDet
go

alter table PickListMstr
   drop constraint PK_PackListMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PickListMstr')
            and   type = 'U')
   drop table PickListMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PickListResult')
            and   name  = 'IX_PIKRST'
            and   indid > 0
            and   indid < 255)
   drop index PickListResult.IX_PIKRST
go

alter table PickListResult
   drop constraint PK_PackListResult
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PickListResult')
            and   type = 'U')
   drop table PickListResult
go

alter table PlanBill
   drop constraint PK_PlanBill
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PlanBill')
            and   type = 'U')
   drop table PlanBill
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PriceListDet')
            and   name  = 'IX_PriceListDet'
            and   indid > 0
            and   indid < 255)
   drop index PriceListDet.IX_PriceListDet
go

alter table PriceListDet
   drop constraint PK_PriceListDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PriceListDet')
            and   type = 'U')
   drop table PriceListDet
go

alter table PriceListMstr
   drop constraint PK_PriceList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PriceListMstr')
            and   type = 'U')
   drop table PriceListMstr
go

alter table ProdLineIp
   drop constraint PK_ProdLineIp
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProdLineIp')
            and   type = 'U')
   drop table ProdLineIp
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReceiptDet')
            and   name  = 'IX_RECEIPTDET'
            and   indid > 0
            and   indid < 255)
   drop index ReceiptDet.IX_RECEIPTDET
go

alter table ReceiptDet
   drop constraint PK_ReceiptDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReceiptDet')
            and   type = 'U')
   drop table ReceiptDet
go

alter table ReceiptIp
   drop constraint PK_ReceiptIp
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReceiptIp')
            and   type = 'U')
   drop table ReceiptIp
go

alter table ReceiptMstr
   drop constraint PK_ReceiptMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReceiptMstr')
            and   type = 'U')
   drop table ReceiptMstr
go

alter table RepackDet
   drop constraint PK_RepackDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RepackDet')
            and   type = 'U')
   drop table RepackDet
go

alter table RepackMstr
   drop constraint PK_RepackMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RepackMstr')
            and   type = 'U')
   drop table RepackMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RollingPlanDet')
            and   name  = 'IX_RollingPlanDet'
            and   indid > 0
            and   indid < 255)
   drop index RollingPlanDet.IX_RollingPlanDet
go

alter table RollingPlanDet
   drop constraint PK_RollingPlanDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RollingPlanDet')
            and   type = 'U')
   drop table RollingPlanDet
go

alter table RollingPlanMstr
   drop constraint PK_RollingPlanMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RollingPlanMstr')
            and   type = 'U')
   drop table RollingPlanMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RoutingDet')
            and   name  = 'IN_RoutingDet'
            and   indid > 0
            and   indid < 255)
   drop index RoutingDet.IN_RoutingDet
go

alter table RoutingDet
   drop constraint PK_RoutingDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RoutingDet')
            and   type = 'U')
   drop table RoutingDet
go

alter table RoutingMstr
   drop constraint PK_Routing
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RoutingMstr')
            and   type = 'U')
   drop table RoutingMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SCMSQAD')
            and   type = 'U')
   drop table SCMSQAD
go

alter table ShiftDet
   drop constraint PK_ShiftDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ShiftDet')
            and   type = 'U')
   drop table ShiftDet
go

alter table ShiftMstr
   drop constraint PK_ShiftMstr
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ShiftMstr')
            and   type = 'U')
   drop table ShiftMstr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ShiftPlanSchedule')
            and   name  = 'IX_ShiftPlanSchedule'
            and   indid > 0
            and   indid < 255)
   drop index ShiftPlanSchedule.IX_ShiftPlanSchedule
go

alter table ShiftPlanSchedule
   drop constraint PK_ShiftPlan
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ShiftPlanSchedule')
            and   type = 'U')
   drop table ShiftPlanSchedule
go

alter table SpecialTime
   drop constraint PK_TimeManagement
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SpecialTime')
            and   type = 'U')
   drop table SpecialTime
go

alter table StorageArea
   drop constraint PK_StorageArea
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StorageArea')
            and   type = 'U')
   drop table StorageArea
go

alter table StorageBin
   drop constraint PK_StorageBin
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StorageBin')
            and   type = 'U')
   drop table StorageBin
go

alter table SubjectList
   drop constraint PK_SubjectList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SubjectList')
            and   type = 'U')
   drop table SubjectList
go

alter table Tax
   drop constraint PK_Tax
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Tax')
            and   type = 'U')
   drop table Tax
go

alter table Uom
   drop constraint PK_UnitOfMeasure
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Uom')
            and   type = 'U')
   drop table Uom
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UomConv')
            and   name  = 'IX_UomConv'
            and   indid > 0
            and   indid < 255)
   drop index UomConv.IX_UomConv
go

alter table UomConv
   drop constraint PK_UomConv
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UomConv')
            and   type = 'U')
   drop table UomConv
go

alter table WorkCenter
   drop constraint PK12
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkCenter')
            and   type = 'U')
   drop table WorkCenter
go

alter table Workday
   drop constraint PK_WorkCalendar
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Workday')
            and   type = 'U')
   drop table Workday
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WorkdayShift')
            and   name  = 'IX_WorkdayShift'
            and   indid > 0
            and   indid < 255)
   drop index WorkdayShift.IX_WorkdayShift
go

alter table WorkdayShift
   drop constraint PK_WorkdayDet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkdayShift')
            and   type = 'U')
   drop table WorkdayShift
go

alter table WorkingHours
   drop constraint PK_WorkingHours
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkingHours')
            and   type = 'U')
   drop table WorkingHours
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cyclecountdet_bak')
            and   type = 'U')
   drop table cyclecountdet_bak
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cyclecountresult_bak')
            and   type = 'U')
   drop table cyclecountresult_bak
go

/*==============================================================*/
/* Table: ACC_Company                                           */
/*==============================================================*/
create table ACC_Company (
   Code                 varchar(255)         collate Chinese_PRC_CI_AS not null,
   Title                varchar(255)         collate Chinese_PRC_CI_AS not null,
   Desc_                varchar(255)         collate Chinese_PRC_CI_AS null,
   IndustryCode         varchar(255)         collate Chinese_PRC_CI_AS null,
   LogoUrl              varchar(255)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   CreateDate           datetime             null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_Company
   add constraint PK__ACC_Company__58920452 primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_Industry                                          */
/*==============================================================*/
create table ACC_Industry (
   Code                 varchar(255)         collate Chinese_PRC_CI_AS not null,
   Title                varchar(255)         collate Chinese_PRC_CI_AS not null,
   Desc_                varchar(255)         collate Chinese_PRC_CI_AS null,
   LogoUrl              varchar(255)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_Industry
   add constraint PK__ACC_Industry__56A9BBE0 primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_Menu                                              */
/*==============================================================*/
create table ACC_Menu (
   id                   varchar(50)          not null,
   Code                 varchar(255)         collate Chinese_PRC_CI_AS not null,
   Version              int                  not null,
   Title                varchar(255)         collate Chinese_PRC_CI_AS null,
   Desc_                varchar(255)         collate Chinese_PRC_CI_AS null,
   PageUrl              varchar(255)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   ImageUrl             varchar(255)         collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null,
   Remark               varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_Menu
   add constraint PK__ACC_Menu__5A7A4CC4 primary key (id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_MenuCommon                                        */
/*==============================================================*/
create table ACC_MenuCommon (
   Id                   int                  identity(1,1) not for replication,
   ACC_id               varchar(50)          null,
   MenuId               varchar(50)          not null,
   ParentMenuId         varchar(50)          null,
   Level_               int                  not null,
   Seq                  int                  not null,
   IsActive             bit                  not null,
   CreateDate           datetime             null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_MenuCommon
   add constraint PK__ACC_MenuCommon__5C629536 primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_MenuCompany                                       */
/*==============================================================*/
create table ACC_MenuCompany (
   Id                   int                  identity(1,1),
   CompanyCode          varchar(255)         collate Chinese_PRC_CI_AS not null,
   MenuId               varchar(50)          not null,
   ParentMenuId         varchar(50)          null,
   Level_               int                  not null,
   Seq                  int                  not null,
   IsActive             bit                  not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_MenuCompany
   add constraint PK__ACC_MenuCompany__6033261A primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_MenuIndustry                                      */
/*==============================================================*/
create table ACC_MenuIndustry (
   Id                   int                  identity(1,1),
   ACC_id               varchar(50)          null,
   IndustryCode         varchar(255)         collate Chinese_PRC_CI_AS not null,
   MenuId               varchar(50)          not null,
   ParentMenuId         varchar(50)          null,
   Level_               int                  not null,
   Seq                  int                  not null,
   IsActive             bit                  not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_MenuIndustry
   add constraint PK__ACC_MenuIndustry__5E4ADDA8 primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_Permission                                        */
/*==============================================================*/
create table ACC_Permission (
   PM_ID                int                  identity(1, 1),
   PM_Code              nvarchar(255)        collate Chinese_PRC_CI_AS not null,
   PM_Desc              nvarchar(255)        collate Chinese_PRC_CI_AS not null,
   PM_CateCode          nvarchar(20)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table ACC_Permission
   add constraint PK_ACC_Permission primary key nonclustered (PM_ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ACC_Permission                                     */
/*==============================================================*/
create unique clustered index IX_ACC_Permission on ACC_Permission (
PM_Code ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_PermissionCategory                                */
/*==============================================================*/
create table ACC_PermissionCategory (
   PMC_Code             nvarchar(20)         collate Chinese_PRC_CI_AS not null,
   PMC_Desc             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PMC_Type             nvarchar(50)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table ACC_PermissionCategory
   add constraint PK_ACC_PermissionCategory primary key (PMC_Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_Role                                              */
/*==============================================================*/
create table ACC_Role (
   ROLE_Code            varchar(50)          collate Chinese_PRC_CI_AS not null,
   ROLE_Desc            varchar(50)          collate Chinese_PRC_CI_AS not null,
   ROLE_AllowDel        bit                  null constraint DF_ACC_Role_ROLE_AllowDel default (1)
)
on "PRIMARY"
go

alter table ACC_Role
   add constraint PK_ACC_Role primary key (ROLE_Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_RolePermission                                    */
/*==============================================================*/
create table ACC_RolePermission (
   RP_ID                int                  identity(1, 1),
   RP_RoleCode          varchar(50)          collate Chinese_PRC_CI_AS not null,
   RP_PMID              int                  not null
)
on "PRIMARY"
go

alter table ACC_RolePermission
   add constraint PK_ACC_RolePermission primary key (RP_ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_User                                              */
/*==============================================================*/
create table ACC_User (
   USR_Code             varchar(50)          collate Chinese_PRC_CI_AS not null,
   USR_Pwd              varchar(50)          collate Chinese_PRC_CI_AS not null,
   USR_FirstName        varchar(255)         collate Chinese_PRC_CI_AS not null,
   USR_LastName         varchar(50)          collate Chinese_PRC_CI_AS not null,
   USR_Email            varchar(50)          collate Chinese_PRC_CI_AS not null,
   USR_Address          varchar(100)         collate Chinese_PRC_CI_AS null,
   USR_Sex              varchar(50)          collate Chinese_PRC_CI_AS not null,
   USR_Phone            varchar(50)          collate Chinese_PRC_CI_AS null,
   USR_MPhone           varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   LastModifyDate       datetime             null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_User
   add constraint PK_ACC_User primary key (USR_Code)
      on "PRIMARY"
go

alter table ACC_User
   add constraint IX_ACC_User unique (USR_Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_UserFav                                           */
/*==============================================================*/
create table ACC_UserFav (
   Id                   int                  identity(1, 1),
   USR_Code             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS null,
   PageName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   PageUrl              varchar(250)         collate Chinese_PRC_CI_AS null,
   PageImg              varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ACC_UserFav
   add constraint PK_Favorites primary key (Id)
      on "PRIMARY"
go

alter table ACC_UserFav
   add constraint IX_Favorites unique (USR_Code, Type, PageName)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_UserPermission                                    */
/*==============================================================*/
create table ACC_UserPermission (
   UP_USRCode           varchar(50)          collate Chinese_PRC_CI_AS not null,
   UP_PMID              int                  not null,
   UP_ID                int                  identity(1, 1)
)
on "PRIMARY"
go

alter table ACC_UserPermission
   add constraint PK_ACC_UserPermission_1 primary key (UP_ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_UserPre                                           */
/*==============================================================*/
create table ACC_UserPre (
   USR_Code             varchar(50)          collate Chinese_PRC_CI_AS not null,
   PreCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   PreValue             varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table ACC_UserPre
   add constraint PK_UserPreference primary key (USR_Code, PreCode)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ACC_UserRole                                          */
/*==============================================================*/
create table ACC_UserRole (
   UR_ID                int                  identity(1, 1),
   UR_USRCode           varchar(50)          collate Chinese_PRC_CI_AS not null,
   UR_RoleCode          varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table ACC_UserRole
   add constraint PK_ACC_UserRole primary key (UR_ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ActBill                                               */
/*==============================================================*/
create table ActBill (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtRecNo             varchar(50)          collate Chinese_PRC_CI_AS null,
   TransType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   BillAddr             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   BillQty              decimal(18,8)        not null,
   BilledQty            decimal(18,8)        not null,
   UnitPrice            decimal(18,8)        not null,
   BillAmount           decimal(18,8)        null,
   BilledAmount         decimal(18,8)        null,
   PriceList            varchar(50)          collate Chinese_PRC_CI_AS null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsIncludeTax         bit                  not null,
   TaxCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   IsProvEst            bit                  not null constraint DF_ActBill_IsProvEst default (0),
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   EffDate              datetime             not null,
   UC                   decimal(18,8)        not null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ActBill
   add constraint PK_ActBill primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: AutoOrderTrack                                        */
/*==============================================================*/
create table AutoOrderTrack (
   Id                   int                  identity(1, 1),
   OrderDetId           int                  not null,
   RefOrderDetId        int                  not null,
   IOType               varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderQty             decimal(18,8)        not null,
   Qty                  decimal(18,8)        not null
)
on "PRIMARY"
go

alter table AutoOrderTrack
   add constraint PK_AutoOrderTrack primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BatchJobDet                                           */
/*==============================================================*/
create table BatchJobDet (
   Id                   int                  identity(1, 1),
   Name                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   ServiceName          varchar(255)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table BatchJobDet
   add constraint PK_BatchJobDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BatchJobParam                                         */
/*==============================================================*/
create table BatchJobParam (
   Id                   int                  identity(1, 1),
   JobId                int                  not null,
   ParamName            varchar(50)          collate Chinese_PRC_CI_AS not null,
   ParamValue           varchar(255)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table BatchJobParam
   add constraint PK_BatchJobParam primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BatchRunLog                                           */
/*==============================================================*/
create table BatchRunLog (
   Id                   int                  identity(1, 1),
   JobId                int                  null,
   TriggerId            int                  null,
   StartTime            datetime             not null,
   EndTime              datetime             null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   Message              varchar(2000)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table BatchRunLog
   add constraint PK_BatchRunLog primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BatchTrigger                                          */
/*==============================================================*/
create table BatchTrigger (
   Id                   int                  identity(1, 1),
   Name                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   JobId                int                  not null,
   NextFireTime         datetime             null,
   PrevFireTime         datetime             null,
   RepeatCount          int                  not null,
   Interval             int                  not null,
   IntervalType         varchar(50)          collate Chinese_PRC_CI_AS not null,
   TimesTriggered       bigint               not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table BatchTrigger
   add constraint PK_BatchTrigger primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BatchTriggerParam                                     */
/*==============================================================*/
create table BatchTriggerParam (
   Id                   int                  identity(1, 1),
   TriggerId            int                  not null,
   ParamName            varchar(50)          collate Chinese_PRC_CI_AS not null,
   ParamValue           varchar(Max)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table BatchTriggerParam
   add constraint PK_BatchTriggerParam primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BillDet                                               */
/*==============================================================*/
create table BillDet (
   Id                   int                  identity(1, 1),
   BillNo               varchar(50)          collate Chinese_PRC_CI_AS not null,
   TransId              int                  not null,
   BilledQty            decimal(18,8)        not null,
   UnitPrice            decimal(18,8)        not null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Discount             decimal(18,8)        null,
   IsIncludeTax         bit                  not null,
   TaxCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderAmount          decimal(18,8)        not null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table BillDet
   add constraint IX_BillDet unique clustered (BillNo, TransId)
      on "PRIMARY"
go

alter table BillDet
   add constraint PK_BillDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BillMstr                                              */
/*==============================================================*/
create table BillMstr (
   BillNo               varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtBillNo            varchar(50)          collate Chinese_PRC_CI_AS null,
   RefBillNo            varchar(50)          collate Chinese_PRC_CI_AS null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   TransType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   BillAddr             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsIncludeTax         bit                  not null,
   TaxCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   Discount             decimal(18,8)        null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   BillType             varchar(50)          collate Chinese_PRC_CI_AS not null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table BillMstr
   add constraint PK_BillMstr primary key (BillNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BillTrans                                             */
/*==============================================================*/
create table BillTrans (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtRecNo             varchar(50)          collate Chinese_PRC_CI_AS null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   ItemDesc             varchar(255)         collate Chinese_PRC_CI_AS null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   BillAddr             varchar(50)          collate Chinese_PRC_CI_AS not null,
   BillAddrDesc         varchar(255)         collate Chinese_PRC_CI_AS null,
   Loc                  varchar(50)          collate Chinese_PRC_CI_AS null,
   LocName              varchar(50)          collate Chinese_PRC_CI_AS null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   BatchNo              int                  null,
   Qty                  decimal(18,8)        not null,
   EffDate              datetime             not null,
   TransType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PlanBill             int                  not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ActBill              int                  not null,
   Party                varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyName            varchar(255)         collate Chinese_PRC_CI_AS null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table BillTrans
   add constraint PK_BillTrans primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: BomDet                                                */
/*==============================================================*/
create table BomDet (
   Id                   int                  identity(1, 1),
   Bom                  varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Op                   int                  not null,
   Ref                  varchar(50)          collate Chinese_PRC_CI_AS null,
   StruType             varchar(50)          collate Chinese_PRC_CI_AS not null,
   StartDate            datetime             not null,
   EndDate              datetime             null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   RateQty              decimal(18,8)        not null,
   ScrapPct             decimal(18,8)        not null,
   Priority             int                  null,
   NeedPrint            bit                  not null,
   Loc                  varchar(50)          collate Chinese_PRC_CI_AS null,
   HuLotSize            int                  null,
   IsShipScan           bit                  not null constraint DF_BomDet_IsShipScan default (0),
   BackFlushMethod      varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table BomDet
   add constraint PK_BomDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_BomDet                                             */
/*==============================================================*/
create unique clustered index IX_BomDet on BomDet (
Bom ASC,
Item ASC,
Op ASC,
Ref ASC,
StartDate ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: BomMstr                                               */
/*==============================================================*/
create table BomMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS null,
   Region               varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null constraint DF_BomMstr_IsActive default (1)
)
on "PRIMARY"
go

alter table BomMstr
   add constraint PK_BomMstr primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Client                                                */
/*==============================================================*/
create table Client (
   ClientId             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Description          varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null
)
on "PRIMARY"
go

alter table Client
   add constraint PK_Client_1 primary key (ClientId)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ClientLog                                             */
/*==============================================================*/
create table ClientLog (
   Id                   int                  identity(1, 1),
   ClientId             varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS null,
   Operation            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Result               varchar(50)          collate Chinese_PRC_CI_AS not null,
   Message              varchar(500)         collate Chinese_PRC_CI_AS null,
   SynTime              datetime             not null
)
on "PRIMARY"
go

alter table ClientLog
   add constraint PK_ClientLog primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ClientMonitor                                         */
/*==============================================================*/
create table ClientMonitor (
   Id                   int                  identity(1, 1),
   ClientId             varchar(50)          collate Chinese_PRC_CI_AS not null,
   BeatTime             datetime             not null
)
on "PRIMARY"
go

alter table ClientMonitor
   add constraint PK_ClientMonitor primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ClientOrderDet                                        */
/*==============================================================*/
create table ClientOrderDet (
   Id                   int                  identity(1, 1),
   Seq                  int                  null,
   OrderHeadId          varchar(50)          collate Chinese_PRC_CI_AS not null,
   ItemCode             varchar(50)          collate Chinese_PRC_CI_AS null,
   ItemDescription      varchar(255)         collate Chinese_PRC_CI_AS null,
   UomCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   UomDescription       varchar(255)         collate Chinese_PRC_CI_AS null,
   UnitCount            decimal(18,8)        null,
   OrderedQty           decimal(18,8)        null,
   ShippedQty           decimal(18,8)        null,
   ReceivedQty          decimal(18,8)        null,
   ReceiveQty           decimal(18,8)        null,
   RejectQty            decimal(18,8)        null,
   ScrapQty             decimal(18,8)        null
)
on "PRIMARY"
go

alter table ClientOrderDet
   add constraint PK_ClientOrderDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ClientOrderMstr                                       */
/*==============================================================*/
create table ClientOrderMstr (
   Id                   varchar(50)          collate Chinese_PRC_CI_AS not null,
   ClientId             varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS null,
   UserCode             varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Flow                 varchar(50)          collate Chinese_PRC_CI_AS null,
   SynStatus            varchar(50)          collate Chinese_PRC_CI_AS not null,
   SynTime              datetime             null
)
on "PRIMARY"
go

alter table ClientOrderMstr
   add constraint PK_ClientOrderMstr primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ClientWorkingHours                                    */
/*==============================================================*/
create table ClientWorkingHours (
   Id                   int                  identity(1, 1),
   OrderHeadId          varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Employee             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Hours                decimal(18,8)        not null
)
on "PRIMARY"
go

alter table ClientWorkingHours
   add constraint PK_ClientWorkingHours primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: CodeMstr                                              */
/*==============================================================*/
create table CodeMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   CodeValue            varchar(255)         collate Chinese_PRC_CI_AS not null,
   Seq                  int                  not null,
   IsDefault            bit                  not null constraint DF_CodeMstr_IsDefault default (0),
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table CodeMstr
   add constraint PK_CodeMstr primary key (Code, CodeValue)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Currency                                              */
/*==============================================================*/
create table Currency (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsDefault            bit                  not null constraint DF_Currency_IsDefault default (0)
)
on "PRIMARY"
go

alter table Currency
   add constraint PK_Currency primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: CurrencyExchange                                      */
/*==============================================================*/
create table CurrencyExchange (
   Id                   int                  identity(1, 1),
   Currency             varchar(50)          collate Chinese_PRC_CI_AS not null,
   StartDate            datetime             not null,
   BaseQty              decimal(18,8)        not null,
   ExchangeQty          decimal(18,8)        not null
)
on "PRIMARY"
go

alter table CurrencyExchange
   add constraint PK_ExchangeRate primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: CycleCountDet                                         */
/*==============================================================*/
create table CycleCountDet (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Qty                  decimal(18,8)        not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table CycleCountDet
   add constraint PK_CycleCountDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: CycleCountMstr                                        */
/*==============================================================*/
create table CycleCountMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS not null,
   EffDate              datetime             not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   ReleaseUser          varchar(50)          collate Chinese_PRC_CI_AS null,
   ReleaseDate          datetime             null,
   CancelUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   CancelDate           datetime             null,
   CloseUser            varchar(50)          collate Chinese_PRC_CI_AS null,
   CloseDate            datetime             null
)
on "PRIMARY"
go

alter table CycleCountMstr
   add constraint PK_CycleCount primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: CycleCountResult                                      */
/*==============================================================*/
create table CycleCountResult (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   InvQty               decimal(18,8)        not null,
   DiffQty              decimal(18,8)        not null,
   DiffReason           varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null,
   RefLocation          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table CycleCountResult
   add constraint PK_CycleCountResult primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssExpHis                                             */
/*==============================================================*/
create table DssExpHis (
   Id                   int                  identity(1, 1),
   DssOutboundCtrl      int                  not null,
   EventCode            varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null,
   KeyCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   RefKeyCode           varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS null,
   RefLocation          varchar(50)          collate Chinese_PRC_CI_AS null,
   EffDate              datetime             null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Uom                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null constraint DF_DssExpHis_Qty default (0),
   Comments             varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr1              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr2              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr3              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr4              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr5              varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr1            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr2            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr3            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr4            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr5            varchar(255)         collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             not null,
   TransNo              varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table DssExpHis
   add constraint PK_DssExpHis primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_DssExpHis                                          */
/*==============================================================*/
create index IX_DssExpHis on DssExpHis (
IsActive ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: DssExpHisDet                                          */
/*==============================================================*/
create table DssExpHisDet (
   Id                   int                  identity(1, 1),
   MstrId               int                  not null,
   KeyCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS null,
   RefLocation          varchar(50)          collate Chinese_PRC_CI_AS null,
   EffDate              datetime             null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Uom                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   Comments             varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr1              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr2              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr3              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr4              varchar(255)         collate Chinese_PRC_CI_AS null,
   DefStr5              varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr1            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr2            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr3            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr4            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr5            varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table DssExpHisDet
   add constraint PK_DssExpHisDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssFtpCtrl                                            */
/*==============================================================*/
create table DssFtpCtrl (
   Id                   int                  identity(1, 1),
   FtpServer            varchar(255)         collate Chinese_PRC_CI_AS not null,
   FtpPort              int                  null,
   FtpUser              varchar(255)         collate Chinese_PRC_CI_AS not null,
   FtpPass              varchar(255)         collate Chinese_PRC_CI_AS not null,
   FtpTempFolder        varchar(255)         collate Chinese_PRC_CI_AS null,
   FtpFolder            varchar(255)         collate Chinese_PRC_CI_AS null,
   FilePattern          varchar(255)         collate Chinese_PRC_CI_AS null,
   LocalTempFolder      varchar(255)         collate Chinese_PRC_CI_AS null,
   LocalFolder          varchar(255)         collate Chinese_PRC_CI_AS null,
   IOType               varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table DssFtpCtrl
   add constraint PK_Dss primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssImpHis                                             */
/*==============================================================*/
create table DssImpHis (
   Id                   int                  identity(1, 1),
   DssInboundCtrl       int                  not null,
   EventCode            varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null,
   KeyCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null constraint DF_DssImpHis_Qty default (0),
   CreateDate           datetime             not null,
   Memo                 varchar(255)         collate Chinese_PRC_CI_AS null,
   data0                varchar(255)         collate Chinese_PRC_CI_AS null,
   data1                varchar(255)         collate Chinese_PRC_CI_AS null,
   data2                varchar(255)         collate Chinese_PRC_CI_AS null,
   data3                varchar(255)         collate Chinese_PRC_CI_AS null,
   data4                varchar(255)         collate Chinese_PRC_CI_AS null,
   data5                varchar(255)         collate Chinese_PRC_CI_AS null,
   data6                varchar(255)         collate Chinese_PRC_CI_AS null,
   data7                varchar(255)         collate Chinese_PRC_CI_AS null,
   data8                varchar(255)         collate Chinese_PRC_CI_AS null,
   data9                varchar(255)         collate Chinese_PRC_CI_AS null,
   data10               varchar(255)         collate Chinese_PRC_CI_AS null,
   data11               varchar(255)         collate Chinese_PRC_CI_AS null,
   data12               varchar(255)         collate Chinese_PRC_CI_AS null,
   data13               varchar(255)         collate Chinese_PRC_CI_AS null,
   data14               varchar(255)         collate Chinese_PRC_CI_AS null,
   data15               varchar(255)         collate Chinese_PRC_CI_AS null,
   data16               varchar(255)         collate Chinese_PRC_CI_AS null,
   data17               varchar(255)         collate Chinese_PRC_CI_AS null,
   data18               varchar(255)         collate Chinese_PRC_CI_AS null,
   data19               varchar(255)         collate Chinese_PRC_CI_AS null,
   data20               varchar(255)         collate Chinese_PRC_CI_AS null,
   ErrCount             int                  null
)
on "PRIMARY"
go

alter table DssImpHis
   add constraint PK_DssImpHis_1 primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssInboundCtrl                                        */
/*==============================================================*/
create table DssInboundCtrl (
   Id                   int                  identity(1, 1),
   InFloder             varchar(255)         collate Chinese_PRC_CI_AS not null,
   FilePattern          varchar(255)         collate Chinese_PRC_CI_AS null,
   ServiceName          varchar(255)         collate Chinese_PRC_CI_AS not null,
   ArchiveFloder        varchar(255)         collate Chinese_PRC_CI_AS not null,
   ErrorFloder          varchar(255)         collate Chinese_PRC_CI_AS not null,
   SeqNo                int                  not null,
   FileEncoding         varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table DssInboundCtrl
   add constraint PK_DssLog primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssObjectMapping                                      */
/*==============================================================*/
create table DssObjectMapping (
   Id                   int                  identity(1, 1),
   Entity               varchar(50)          collate Chinese_PRC_CI_AS not null,
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtSys               varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtEntity            varchar(50)          collate Chinese_PRC_CI_AS null,
   ExtCode              varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table DssObjectMapping
   add constraint PK_DssObjectMapping primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssOutboundCtrl                                       */
/*==============================================================*/
create table DssOutboundCtrl (
   Id                   int                  identity(1, 1),
   ExtSysCode           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtObjectCode        varchar(50)          collate Chinese_PRC_CI_AS null,
   OutFolder            varchar(255)         collate Chinese_PRC_CI_AS not null,
   ServiceName          varchar(255)         collate Chinese_PRC_CI_AS not null,
   ArchiveFolder        varchar(255)         collate Chinese_PRC_CI_AS not null,
   SeqNo                int                  not null,
   TempFolder           varchar(255)         collate Chinese_PRC_CI_AS not null,
   FileEncoding         varchar(50)          collate Chinese_PRC_CI_AS null,
   SysAlias             varchar(50)          collate Chinese_PRC_CI_AS null,
   FilePrefix           varchar(50)          collate Chinese_PRC_CI_AS null,
   FileSuffix           varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   Mark                 int                  not null constraint DF_DssOutboundCtrl_Mark default (0),
   UndefStr1            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr2            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr3            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr4            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr5            varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table DssOutboundCtrl
   add constraint PK_DssOutboundCtrl_1 primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: DssSysMstr                                            */
/*==============================================================*/
create table DssSysMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   SysAlias             varchar(50)          collate Chinese_PRC_CI_AS null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Flag                 varchar(50)          collate Chinese_PRC_CI_AS null,
   UndefStr1            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr2            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr3            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr4            varchar(255)         collate Chinese_PRC_CI_AS null,
   UndefStr5            varchar(255)         collate Chinese_PRC_CI_AS null,
   Prefix1              varchar(50)          collate Chinese_PRC_CI_AS null,
   Prefix2              varchar(50)          collate Chinese_PRC_CI_AS null,
   Prefix3              varchar(50)          collate Chinese_PRC_CI_AS null,
   Prefix4              varchar(50)          collate Chinese_PRC_CI_AS null,
   Prefix5              varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table DssSysMstr
   add constraint PK_DssSysMstr primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Employee                                              */
/*==============================================================*/
create table Employee (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Gender               varchar(50)          collate Chinese_PRC_CI_AS null,
   Department           varchar(50)          collate Chinese_PRC_CI_AS null,
   WorkGroup            varchar(50)          collate Chinese_PRC_CI_AS null,
   Post                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Memo                 varchar(255)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   LastModifyDate       datetime             null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table Employee
   add constraint PK_Employee primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: EntityOpt                                             */
/*==============================================================*/
create table EntityOpt (
   PreCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   PreValue             varchar(255)         collate Chinese_PRC_CI_AS null,
   CodeDesc             varchar(255)         collate Chinese_PRC_CI_AS not null,
   Seq                  int                  null
)
on "PRIMARY"
go

alter table EntityOpt
   add constraint PK_EntityPreference_1 primary key (PreCode)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: FileUpload                                            */
/*==============================================================*/
create table FileUpload (
   FileUploadId         int                  identity(1, 1),
   FileName             varchar(255)         collate Chinese_PRC_CI_AS not null,
   FileExt              varchar(50)          collate Chinese_PRC_CI_AS null,
   FileDesc             varchar(255)         collate Chinese_PRC_CI_AS null,
   FullPath             varchar(255)         collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   CreateUser           nvarchar(20)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table FileUpload
   add constraint PK_FileUpload primary key (FileUploadId)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: FlowBinding                                           */
/*==============================================================*/
create table FlowBinding (
   Id                   int                  identity(1, 1),
   MstrFlow             varchar(50)          collate Chinese_PRC_CI_AS not null,
   SlvFlow              varchar(50)          collate Chinese_PRC_CI_AS not null,
   BindType             varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table FlowBinding
   add constraint PK_FlowBinding primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: FlowDet                                               */
/*==============================================================*/
create table FlowDet (
   Id                   int                  identity(1, 1),
   Flow                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   UC                   decimal(18,8)        not null,
   Bom                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Seq                  int                  not null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS null,
   BillFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   BillTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListFrom        varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListTo          varchar(50)          collate Chinese_PRC_CI_AS null,
   IsAutoCreate         bit                  not null,
   SafeStock            decimal(18,8)        null,
   MaxStock             decimal(18,8)        null,
   MinLotSize           decimal(18,8)        null,
   OrderLotSize         decimal(18,8)        null,
   OrderGrLotSize       decimal(18,8)        null,
   BatchSize            decimal              null,
   RoundUpOpt           varchar(50)          collate Chinese_PRC_CI_AS null,
   HuLotSize            int                  null,
   PackVol              decimal(18,8)        null,
   PackType             varchar(50)          collate Chinese_PRC_CI_AS null,
   ProjectDesc          varchar(50)          collate Chinese_PRC_CI_AS null,
   Remark               varchar(50)          collate Chinese_PRC_CI_AS null,
   BillSettleTerm       varchar(50)          collate Chinese_PRC_CI_AS null,
   Customer             varchar(50)          collate Chinese_PRC_CI_AS null,
   TimeUnit             varchar(50)          collate Chinese_PRC_CI_AS null,
   NeedInspect          bit                  not null,
   IdMark               varchar(50)          collate Chinese_PRC_CI_AS null,
   BarCodeType          varchar(50)          collate Chinese_PRC_CI_AS null,
   StartDate            datetime             null,
   EndDate              datetime             null,
   OddShipOpt           varchar(50)          collate Chinese_PRC_CI_AS null,
   CustomerItemCode     varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField5           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField6           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField7           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField8           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   NumField5            decimal(18,8)        null,
   NumField6            decimal(18,8)        null,
   NumField7            decimal(18,8)        null,
   NumField8            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null,
   DateField3           datetime             null,
   DateField4           datetime             null
)
on "PRIMARY"
go

alter table FlowDet
   add constraint PK_ItemFlowDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ITEM                                               */
/*==============================================================*/
create index IX_ITEM on FlowDet (
Item ASC,
Uom ASC,
UC ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: FlowMstr                                              */
/*==============================================================*/
create table FlowMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   RefFlow              varchar(50)          collate Chinese_PRC_CI_AS null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShipFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS null,
   BillFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   BillTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListFrom        varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListTo          varchar(50)          collate Chinese_PRC_CI_AS null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS null,
   DockDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   Routing              varchar(50)          collate Chinese_PRC_CI_AS null,
   ReturnRouting        varchar(50)          collate Chinese_PRC_CI_AS null,
   FlowStrategy         varchar(50)          collate Chinese_PRC_CI_AS null,
   LotGroup             varchar(50)          collate Chinese_PRC_CI_AS null,
   LeadTime             decimal(18,8)        null,
   EmTime               decimal(18,8)        null,
   MaxCirTime           decimal(18,8)        null,
   WinTime1             varchar(255)         collate Chinese_PRC_CI_AS null,
   WinTime2             varchar(255)         collate Chinese_PRC_CI_AS null,
   WinTime3             varchar(255)         collate Chinese_PRC_CI_AS null,
   WinTime4             varchar(255)         collate Chinese_PRC_CI_AS null,
   WinTime5             varchar(255)         collate Chinese_PRC_CI_AS null,
   WinTime6             varchar(255)         collate Chinese_PRC_CI_AS null,
   WinTime7             varchar(255)         collate Chinese_PRC_CI_AS null,
   NextOrderTime        datetime             null,
   NextWinTime          datetime             null,
   WeekInterval         int                  null,
   IsAutoCreate         bit                  not null,
   IsAutoRelease        bit                  not null,
   IsAutoStart          bit                  not null,
   IsAutoShip           bit                  not null constraint DF_FlowMstr_IsAutoShip default (0),
   IsAutoReceive        bit                  not null constraint DF_FlowMstr_IsAutoReceive default (0),
   IsAutoBill           bit                  not null,
   IsListDet            bit                  not null constraint DF_FlowMstr_ListDetInOrder default (1),
   IsShowPrice          bit                  not null,
   CheckDetOpt          varchar(50)          collate Chinese_PRC_CI_AS not null constraint DF_FlowMstr_IsCheckDetSource default (0),
   StartLatency         decimal(18,8)        null,
   CompleteLatency      decimal(18,8)        null,
   NeedPrintOrder       bit                  not null,
   NeedPrintAsn         bit                  not null,
   NeedPrintRcpt        bit                  not null,
   GrGapTo              varchar(50)          collate Chinese_PRC_CI_AS null,
   AllowExceed          bit                  not null,
   FulfillUC            bit                  not null constraint DF_FlowMstr_FulfillUC default (0),
   RecTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   OrderTemplate        varchar(100)         collate Chinese_PRC_CI_AS null,
   AsnTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   Carrier              varchar(50)          collate Chinese_PRC_CI_AS null,
   CarrierBillAddr      varchar(50)          collate Chinese_PRC_CI_AS null,
   AllowCreateDetail    bit                  null,
   BillSettleTerm       varchar(50)          collate Chinese_PRC_CI_AS null,
   IsShipScan           bit                  not null constraint DF_FlowMstr_IsShipScan default (0),
   IsRecScan            bit                  not null constraint DF_FlowMstr_IsRecScan default (0),
   CreateHuOpt          varchar(50)          collate Chinese_PRC_CI_AS not null constraint DF_FlowMstr_CreateHuOpt default 'None',
   AutoPrintHu          bit                  not null constraint DF_FlowMstr_AutoPrintHu default (1),
   IsOddCreateHu        bit                  not null constraint DF_FlowMstr_IsOddCreateHu default (0),
   IsAutoCreatePL       bit                  not null constraint DF_FlowMstr_IsAutoCreatePL default (0),
   NeedInspect          bit                  not null,
   IsGrFifo             bit                  not null,
   AntiResolveHu        varchar(50)          collate Chinese_PRC_CI_AS null,
   MaxOnlineQty         int                  null,
   HuTemplate           varchar(100)         collate Chinese_PRC_CI_AS null,
   AllowRepeatlyExceed  bit                  not null,
   IsPickFromBin        bit                  not null,
   IsShipByOrder        bit                  not null,
   IsAsnUniqueReceipt   bit                  null constraint DF_FlowMstr_IsAsnUniqueReceipt default (0),
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField5           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField6           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField7           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField8           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   NumField5            decimal(18,8)        null,
   NumField6            decimal(18,8)        null,
   NumField7            decimal(18,8)        null,
   NumField8            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null,
   DateField3           datetime             null,
   DateField4           datetime             null
)
on "PRIMARY"
go

alter table FlowMstr
   add constraint PK_ItemFlow primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: FlowPlan                                              */
/*==============================================================*/
create table FlowPlan (
   Id                   int                  identity(1, 1),
   FlowDetId            int                  not null,
   TimePeriodType       varchar(50)          collate Chinese_PRC_CI_AS not null,
   ReqDate              datetime             not null,
   PlanQty              decimal              not null
)
on "PRIMARY"
go

alter table FlowPlan
   add constraint PK_FlowPlan primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_FlowPlan_1                                         */
/*==============================================================*/
create index IX_FlowPlan_1 on FlowPlan (
FlowDetId ASC,
TimePeriodType ASC,
ReqDate ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: HuDet                                                 */
/*==============================================================*/
create table HuDet (
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   QualityLevel         varchar(50)          collate Chinese_PRC_CI_AS not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   UC                   decimal(18,8)        not null,
   UnitQty              decimal(18,8)        not null,
   Qty                  decimal(18,8)        not null,
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   ManufactureDate      datetime             not null,
   ManufactureParty     varchar(50)          collate Chinese_PRC_CI_AS null,
   Remark               varchar(255)         collate Chinese_PRC_CI_AS null,
   ParentHuId           varchar(50)          collate Chinese_PRC_CI_AS null,
   PrintCount           int                  not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExpireDate           datetime             null,
   Version              varchar(50)          collate Chinese_PRC_CI_AS null,
   LotSize              decimal(18,8)        not null,
   AntiResolveHu        varchar(50)          collate Chinese_PRC_CI_AS null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS null,
   CustomerItemCode     varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table HuDet
   add constraint PK_HuDet primary key (HuId)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ITEM                                               */
/*==============================================================*/
create index IX_ITEM on HuDet (
Item ASC,
Uom ASC,
UC ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: HuOdd                                                 */
/*==============================================================*/
create table HuOdd (
   Id                   int                  identity(1, 1),
   OrderDetId           int                  not null,
   LocLotDetId          int                  not null,
   OddQty               decimal(18,8)        not null,
   CreateQty            decimal(18,8)        not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table HuOdd
   add constraint PK_HuOdd primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: InspectDet                                            */
/*==============================================================*/
create table InspectDet (
   Id                   int                  identity(1, 1),
   InspNo               varchar(50)          collate Chinese_PRC_CI_AS not null,
   LocLotDetId          int                  not null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS not null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   InspQty              decimal(18,8)        not null,
   QualifyQty           decimal(18,8)        null,
   RejectQty            decimal(18,8)        null,
   DefectClassification varchar(50)          collate Chinese_PRC_CI_AS null,
   Disposition          varchar(50)          collate Chinese_PRC_CI_AS null,
   FGCode               varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table InspectDet
   add constraint PK_InspectDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: InspectMstr                                           */
/*==============================================================*/
create table InspectMstr (
   InspNo               varchar(50)          collate Chinese_PRC_CI_AS not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsDetHasHu           bit                  not null,
   IsPrinted            bit                  not null,
   Region               varchar(50)          collate Chinese_PRC_CI_AS null,
   ReceiptNo            varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table InspectMstr
   add constraint PK_InspectMstr primary key (InspNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: IpDet                                                 */
/*==============================================================*/
create table IpDet (
   Id                   int                  identity(1, 1),
   IpNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderLocTransId      int                  not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   IsCS                 bit                  not null constraint DF_IpDet_IsCS default (0),
   PlanBillId           int                  null,
   RecQty               decimal(18,8)        null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   CustomerItemCode     varchar(50)          collate Chinese_PRC_CI_AS null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS null,
   UC                   decimal(18,8)        null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table IpDet
   add constraint PK_IpDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_IpDet                                              */
/*==============================================================*/
create index IX_IpDet on IpDet (
IpNo ASC,
OrderLocTransId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: IpMstr                                                */
/*==============================================================*/
create table IpMstr (
   IpNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   CurrOp               int                  null,
   CurrAct              varchar(50)          collate Chinese_PRC_CI_AS null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShipFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   DockDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   GapRecNo             varchar(50)          collate Chinese_PRC_CI_AS null,
   IsShipScan           bit                  not null constraint DF_IpMstr_IsShipScan default (0),
   IsRecScan            bit                  not null,
   IsAutoReceive        bit                  not null,
   CompleteLatency      decimal(18,8)        null,
   GrGapTo              varchar(50)          collate Chinese_PRC_CI_AS null,
   AsnTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   RecTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   IsDetHasHu           bit                  null constraint DF_IpMstr_IsDetHasHu default (0),
   HuTemplate           varchar(100)         collate Chinese_PRC_CI_AS null,
   Disposition          varchar(100)         collate Chinese_PRC_CI_AS null,
   RefOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   NeedPrintAsn         bit                  not null,
   IsPrinted            bit                  not null,
   IsAsnUniqueReceipt   bit                  null constraint DF_IpMstr_IsAsnUniqueReceipt default (0),
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table IpMstr
   add constraint PK_AsnMstr primary key (IpNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: IpTrack                                               */
/*==============================================================*/
create table IpTrack (
   Id                   int                  identity(1, 1),
   IpNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Op                   int                  not null,
   Activity             varchar(50)          collate Chinese_PRC_CI_AS not null,
   WorkCenter           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ActDate              datetime             null,
   ActUser              varchar(50)          collate Chinese_PRC_CI_AS null,
   Remark               varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table IpTrack
   add constraint PK_IpTrack primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Item                                                  */
/*==============================================================*/
create table Item (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(20)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   Desc2                varchar(255)         collate Chinese_PRC_CI_AS null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   UC                   decimal(18,8)        not null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS null,
   ImageUrl             varchar(255)         collate Chinese_PRC_CI_AS null,
   Bom                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Routing              varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   Memo                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table Item
   add constraint PK_Item primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ItemFlowPlanDet                                       */
/*==============================================================*/
create table ItemFlowPlanDet (
   Id                   int                  identity(1, 1),
   ItemFlowPlanId       int                  not null,
   TimePeriodType       varchar(50)          collate Chinese_PRC_CI_AS not null,
   ReqDate              datetime             not null,
   PlanQty              decimal(18,8)        not null
)
on "PRIMARY"
go

alter table ItemFlowPlanDet
   add constraint PK_ItemFlowPlanDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ItemFlowPlanDet                                    */
/*==============================================================*/
create unique clustered index IX_ItemFlowPlanDet on ItemFlowPlanDet (
ItemFlowPlanId ASC,
TimePeriodType ASC,
ReqDate ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: ItemFlowPlanMstr                                      */
/*==============================================================*/
create table ItemFlowPlanMstr (
   Id                   int                  identity(1, 1),
   Flow                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   FlowDetId            int                  not null,
   PlanType             varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ItemFlowPlanMstr
   add constraint PK_ItemFlowPlanMstr primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ItemFlowPlanTrack                                     */
/*==============================================================*/
create table ItemFlowPlanTrack (
   Id                   int                  identity(1, 1),
   ItemFlowPlanDetId    int                  not null,
   RefOrderLocTransId   int                  null,
   RefPlanDetId         int                  null,
   Rate                 decimal(18,8)        not null constraint DF_ItemFlowPlanTrack_Rate default (1)
)
on "PRIMARY"
go

alter table ItemFlowPlanTrack
   add constraint PK_ItemFlowPlanTrack primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ItemKit                                               */
/*==============================================================*/
create table ItemKit (
   ParentItem           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ChildItem            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Qty                  decimal(18,8)        not null,
   IsActive             bit                  not null
)
on "PRIMARY"
go

alter table ItemKit
   add constraint PK_ItemKit primary key (ParentItem, ChildItem)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ItemRef                                               */
/*==============================================================*/
create table ItemRef (
   Id                   int                  identity(1, 1),
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Party                varchar(50)          collate Chinese_PRC_CI_AS null,
   RefCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   Remark               varchar(255)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null
)
on "PRIMARY"
go

alter table ItemRef
   add constraint PK_ItemRef primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ItemRef                                            */
/*==============================================================*/
create unique clustered index IX_ItemRef on ItemRef (
Item ASC,
Party ASC,
RefCode ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: LocTrans                                              */
/*==============================================================*/
create table LocTrans (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS null,
   ExtOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   RefOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   IpNo                 varchar(50)          collate Chinese_PRC_CI_AS null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   BillTransId          int                  null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   BatchNo              int                  null,
   TransType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   ItemDesc             varchar(255)         collate Chinese_PRC_CI_AS null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   Qty                  decimal(18,8)        not null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyFromName        varchar(255)         collate Chinese_PRC_CI_AS null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyToName          varchar(255)         collate Chinese_PRC_CI_AS null,
   ShipFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipFromAddr         varchar(255)         collate Chinese_PRC_CI_AS null,
   ShipTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipToAddr           varchar(255)         collate Chinese_PRC_CI_AS null,
   Loc                  varchar(50)          collate Chinese_PRC_CI_AS null,
   LocName              varchar(50)          collate Chinese_PRC_CI_AS null,
   Area                 varchar(50)          collate Chinese_PRC_CI_AS null,
   AreaDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CS_AS null,
   BinDesc              varchar(50)          collate Chinese_PRC_CS_AS null,
   LocIOReason          varchar(50)          collate Chinese_PRC_CI_AS null,
   LocIOReasonDesc      varchar(255)         collate Chinese_PRC_CI_AS null,
   DockDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   Carrier              varchar(50)          collate Chinese_PRC_CI_AS null,
   CarrierBillCode      varchar(50)          collate Chinese_PRC_CI_AS null,
   CarrierBillAddr      varchar(255)         collate Chinese_PRC_CI_AS null,
   EffDate              datetime             not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   CostCenterCode       varchar(50)          collate Chinese_PRC_CI_AS null,
   SubjectCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderDetId           int                  null,
   OrderLocTransId      int                  null,
   IsSubcontract        bit                  null,
   RefLoc               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RefLocName           nvarchar(50)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table LocTrans
   add constraint PK_TransHist primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Location                                              */
/*==============================================================*/
create table Location (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Region               varchar(50)          collate Chinese_PRC_CI_AS not null constraint DF_Location_Region default 'a',
   Volume               decimal(18,8)        null constraint DF_Location_Volume default (0),
   IsActive             bit                  not null constraint DF_Location_IsActive default (1),
   ActLocation          varchar(50)          collate Chinese_PRC_CI_AS null,
   AllowNegaInv         bit                  not null constraint DF_Location_AllowNegaInv default (1),
   EnableAdvWM          bit                  not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table Location
   add constraint PK_Location primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: LocationLotDet                                        */
/*==============================================================*/
create table LocationLotDet (
   Id                   int                  identity(1, 1),
   Location             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   IsCS                 bit                  not null constraint DF_LocationLotDet_IsCS default (0),
   PlanBillId           int                  null,
   CreateDate           datetime             not null,
   LastModifyDate       datetime             null
)
on "PRIMARY"
go

alter table LocationLotDet
   add constraint PK_LocationLotDet primary key (Id)
      on "PRIMARY"
go

/*============================================================================*/
/* Index: _dta_index_LocationLotDet_21_1801773476__K2_K4_K1_K5_K3_K9_6_7_8_10 */
/*============================================================================*/
create index _dta_index_LocationLotDet_21_1801773476__K2_K4_K1_K5_K3_K9_6_7_8_10 on LocationLotDet (
Location ASC,
Item ASC,
Id ASC,
HuId ASC,
Bin ASC,
PlanBillId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_LOCLOTDET                                          */
/*==============================================================*/
create index IX_LOCLOTDET on LocationLotDet (
Location ASC,
Item ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_LOCLOTDET_HUID                                     */
/*==============================================================*/
create index IX_LOCLOTDET_HUID on LocationLotDet (
HuId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: MiscOrderDet                                          */
/*==============================================================*/
create table MiscOrderDet (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null
)
on "PRIMARY"
go

alter table MiscOrderDet
   add constraint PK_MiscOrderDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: MiscOrderMstr                                         */
/*==============================================================*/
create table MiscOrderMstr (
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   RefOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS not null,
   SubjectListId        int                  not null,
   EffDate              datetime             not null,
   Remark               varchar(255)         collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   Reason               varchar(50)          collate Chinese_PRC_CI_AS null,
   ProjectCode          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table MiscOrderMstr
   add constraint PK_MiscOrderMstr primary key (OrderNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: NamedQuery                                            */
/*==============================================================*/
create table NamedQuery (
   UserCode             varchar(50)          collate Chinese_PRC_CI_AS not null,
   QueryName            varchar(50)          collate Chinese_PRC_CI_AS not null,
   UserControlPath      varchar(50)          collate Chinese_PRC_CI_AS not null,
   ModuleParameter      varchar(255)         collate Chinese_PRC_CI_AS not null,
   ActionParameter      varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table NamedQuery
   add constraint PK_NamedQuery primary key (UserCode, QueryName)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: NumCtrl                                               */
/*==============================================================*/
create table NumCtrl (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   IntValue             int                  null,
   StrValue             varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table NumCtrl
   add constraint PK_NumCtrl primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: OrderBinding                                          */
/*==============================================================*/
create table OrderBinding (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   BindFlow             varchar(50)          collate Chinese_PRC_CI_AS not null,
   BindType             varchar(50)          collate Chinese_PRC_CI_AS not null,
   BindOrderNo          varchar(50)          collate Chinese_PRC_CI_AS null,
   Remark               varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table OrderBinding
   add constraint PK_OrderBinding primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: OrderDet                                              */
/*==============================================================*/
create table OrderDet (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   Seq                  int                  not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   UC                   decimal(18,8)        not null,
   ReqQty               decimal(18,8)        not null,
   OrderQty             decimal(18,8)        not null,
   ShipQty              decimal(18,8)        null,
   RecQty               decimal(18,8)        null,
   RejQty               decimal(18,8)        null,
   ScrapQty             decimal(18,8)        null,
   OrderGrLotSize       decimal(18,8)        null,
   BatchSize            decimal(18,8)        null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS null,
   BillFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   BillTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListFrom        varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListTo          varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListDetFrom     int                  null,
   PriceListDetTo       int                  null,
   DiscountFrom         decimal(18,8)        null,
   DiscountTo           decimal              null,
   AmountFrom           decimal(18,8)        null,
   AmountTo             decimal(18,8)        null,
   Bom                  varchar(50)          collate Chinese_PRC_CI_AS null,
   HuLotSize            int                  null,
   BillSettleTerm       varchar(50)          collate Chinese_PRC_CI_AS null,
   Customer             varchar(50)          collate Chinese_PRC_CI_AS null,
   PackVol              decimal              null,
   PackType             varchar(50)          collate Chinese_PRC_CI_AS null,
   NeedInspect          bit                  not null,
   IdMark               varchar(50)          collate Chinese_PRC_CI_AS null,
   BarCodeType          varchar(50)          collate Chinese_PRC_CI_AS null,
   ItemVersion          varchar(50)          collate Chinese_PRC_CI_AS null,
   OddShipOpt           varchar(50)          collate Chinese_PRC_CI_AS null,
   CustomerItemCode     varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField5           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField6           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField7           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField8           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   NumField5            decimal(18,8)        null,
   NumField6            decimal(18,8)        null,
   NumField7            decimal(18,8)        null,
   NumField8            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null,
   DateField3           datetime             null,
   DateField4           datetime             null
)
on "PRIMARY"
go

alter table OrderDet
   add constraint PK_OrderDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ITEM                                               */
/*==============================================================*/
create index IX_ITEM on OrderDet (
Item ASC,
Uom ASC,
UC ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ORDDET                                             */
/*==============================================================*/
create index IX_ORDDET on OrderDet (
OrderNo ASC,
Item ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: OrderLocTrans                                         */
/*==============================================================*/
create table OrderLocTrans (
   Id                   int                  identity(1, 1),
   OrderDetId           int                  not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   ItemDesc             varchar(255)         collate Chinese_PRC_CI_AS null,
   BomDet               int                  null,
   IsAssemble           bit                  not null constraint DF_OrderLocTrans_IsAssemble default (1),
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   Op                   int                  null,
   IOType               varchar(50)          collate Chinese_PRC_CI_AS not null,
   TransType            varchar(50)          collate Chinese_PRC_CI_AS null,
   UnitQty              decimal(18,8)        not null,
   OrderQty             decimal(18,8)        not null,
   AccumQty             decimal(18,8)        null,
   AccumRejQty          decimal(18,8)        null,
   AccumScrapQty        decimal(18,8)        null,
   Loc                  varchar(50)          collate Chinese_PRC_CI_AS null,
   RejLoc               varchar(50)          collate Chinese_PRC_CI_AS null,
   HuLotSize            int                  null,
   NeedPrint            bit                  not null,
   IsShipScan           bit                  not null constraint DF_OrderLocTrans_IsShipScan default (0),
   BackFlushMethod      varchar(50)          collate Chinese_PRC_CI_AS null,
   ItemVersion          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table OrderLocTrans
   add constraint PK_OrderLocTrans primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_LOCITEM                                            */
/*==============================================================*/
create index IX_LOCITEM on OrderLocTrans (
Loc ASC,
Item ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ORDERLOCTRANS                                      */
/*==============================================================*/
create index IX_ORDERLOCTRANS on OrderLocTrans (
OrderDetId ASC,
IOType ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: OrderMstr                                             */
/*==============================================================*/
create table OrderMstr (
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   RefOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   ExtOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   Flow                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Routing              varchar(50)          collate Chinese_PRC_CI_AS null,
   Seq                  int                  null,
   StartTime            datetime             not null,
   WindowTime           datetime             not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   Priority             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   SubType              varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShipFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS null,
   DockDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   Carrier              varchar(50)          collate Chinese_PRC_CI_AS null,
   CarrierBillAddr      varchar(50)          collate Chinese_PRC_CI_AS null,
   BillFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   BillTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListFrom        varchar(50)          collate Chinese_PRC_CI_AS null,
   PriceListTo          varchar(50)          collate Chinese_PRC_CI_AS null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS null,
   DiscountFrom         decimal(18,8)        null,
   DiscountTo           decimal              null,
   IsAutoRelease        bit                  not null,
   IsAutoStart          bit                  not null,
   IsAutoShip           bit                  not null constraint DF_OrderMstr_IsAutoShip default (0),
   IsAutoReceive        bit                  not null constraint DF_OrderMstr_IsAutoReceive default (0),
   IsAutoBill           bit                  not null,
   IsShowPrice          bit                  null,
   StartLatency         decimal(18,8)        null,
   CompleteLatency      decimal(18,8)        null,
   NeedPrintOrder       bit                  not null,
   NeedPrintAsn         bit                  not null,
   NeedPrintRcpt        bit                  not null,
   GrGapTo              varchar(50)          collate Chinese_PRC_CI_AS null,
   AllowExceed          bit                  not null,
   FulfillUC            bit                  not null constraint DF_OrderMstr_FulfillUC default (0),
   IsPrinted            bit                  not null,
   RecTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   OrderTemplate        varchar(100)         collate Chinese_PRC_CI_AS null,
   AsnTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   ReleaseDate          datetime             null,
   ReleaseUser          varchar(50)          collate Chinese_PRC_CI_AS null,
   StartDate            datetime             null,
   StartUser            varchar(50)          collate Chinese_PRC_CI_AS null,
   CompleteDate         datetime             null,
   CompleteUser         varchar(50)          collate Chinese_PRC_CI_AS null,
   CloseDate            datetime             null,
   CloseUser            varchar(50)          collate Chinese_PRC_CI_AS null,
   CancelDate           datetime             null,
   CancelUser           varchar(50)          collate Chinese_PRC_CI_AS null,
   CancelReason         varchar(255)         collate Chinese_PRC_CI_AS null,
   Memo                 varchar(255)         collate Chinese_PRC_CI_AS null,
   CheckDetOpt          varchar(50)          collate Chinese_PRC_CI_AS not null,
   AllowCreateDetail    bit                  null,
   BillSettleTerm       varchar(50)          collate Chinese_PRC_CI_AS null,
   IsShipScan           bit                  not null,
   IsRecScan            bit                  not null,
   CreateHuOpt          varchar(50)          collate Chinese_PRC_CI_AS not null,
   AutoPrintHu          bit                  not null,
   IsOddCreateHu        bit                  not null,
   IsAutoCreatePL       bit                  not null constraint DF_OrderMstr_IsAutoCreatePL default (0),
   NeedInspect          bit                  not null,
   Shift                varchar(50)          collate Chinese_PRC_CI_AS null,
   IsGrFifo             bit                  not null,
   AntiResolveHu        varchar(50)          collate Chinese_PRC_CI_AS null,
   MaxOnlineQty         int                  null,
   IsNewItem            bit                  null,
   HuTemplate           varchar(100)         collate Chinese_PRC_CI_AS null,
   AllowRepeatlyExceed  bit                  not null,
   IsPickFromBin        bit                  not null,
   IsShipByOrder        bit                  null constraint DF_OrderMstr_IsShipByOrder default (0),
   IsAsnUniqueReceipt   bit                  null constraint DF_OrderMstr_IsAsnUniqueReceipt default (0),
   IsSubcontract        bit                  null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField5           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField6           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField7           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField8           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   NumField5            decimal(18,8)        null,
   NumField6            decimal(18,8)        null,
   NumField7            decimal(18,8)        null,
   NumField8            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null,
   DateField3           datetime             null,
   DateField4           datetime             null
)
on "PRIMARY"
go

alter table OrderMstr
   add constraint PK_OrderMstr primary key (OrderNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ORD_FLOW                                           */
/*==============================================================*/
create index IX_ORD_FLOW on OrderMstr (
Flow ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ORD_STATUS                                         */
/*==============================================================*/
create index IX_ORD_STATUS on OrderMstr (
Status ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ORD_TYPE                                           */
/*==============================================================*/
create index IX_ORD_TYPE on OrderMstr (
Type ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: OrderOp                                               */
/*==============================================================*/
create table OrderOp (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Op                   int                  not null,
   Ref                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Activity             varchar(50)          collate Chinese_PRC_CI_AS not null,
   WorkCenter           varchar(50)          collate Chinese_PRC_CI_AS not null,
   UnitTime             decimal(18,8)        null,
   WorkTime             decimal(18,8)        null
)
on "PRIMARY"
go

alter table OrderOp
   add constraint PK_OrderOp primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_OrderOp                                            */
/*==============================================================*/
create unique clustered index IX_OrderOp on OrderOp (
OrderNo ASC,
Op ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: OrderPlanBackflush                                    */
/*==============================================================*/
create table OrderPlanBackflush (
   Id                   int                  identity(1, 1),
   OrderLocTransId      int                  not null,
   IpNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   PlanQty              decimal(18,8)        not null,
   IsActive             bit                  not null
)
on "PRIMARY"
go

alter table OrderPlanBackflush
   add constraint PK_OrderPlanBackflush primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Party                                                 */
/*==============================================================*/
create table Party (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Name                 varchar(255)         collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   ParentCode           varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   BarCodeType          varchar(50)          collate Chinese_PRC_CI_AS null,
   PaymentTerm          varchar(100)         collate Chinese_PRC_CI_AS null,
   TradeTerm            varchar(100)         collate Chinese_PRC_CI_AS null,
   Country              varchar(50)          collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table Party
   add constraint PK_Party primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: PartyAddr                                             */
/*==============================================================*/
create table PartyAddr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null constraint DF_PartyAddr_PartyAddrId default newid(),
   PartyCode            varchar(50)          collate Chinese_PRC_CI_AS not null,
   AddrType             varchar(50)          collate Chinese_PRC_CI_AS not null,
   SeqNo                int                  not null,
   IsPrimary            bit                  not null,
   Address              varchar(255)         collate Chinese_PRC_CI_AS null,
   PostalCode           varchar(50)          collate Chinese_PRC_CI_AS null,
   PostalCodeExt        varchar(50)          collate Chinese_PRC_CI_AS null,
   TelNumber            varchar(50)          collate Chinese_PRC_CI_AS null,
   MobilePhone          varchar(50)          collate Chinese_PRC_CI_AS null,
   ContactPsnName       varchar(100)         collate Chinese_PRC_CI_AS null,
   Fax                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Email                varchar(50)          collate Chinese_PRC_CI_AS null,
   WebSite              varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table PartyAddr
   add constraint PK_PartyAddr primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_PARTYADDR                                          */
/*==============================================================*/
create index IX_PARTYADDR on PartyAddr (
PartyCode ASC,
AddrType ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: PickListDet                                           */
/*==============================================================*/
create table PickListDet (
   Id                   int                  identity(1, 1),
   PLNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderLocTransId      int                  null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Area                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   UC                   decimal(18,8)        not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   Memo                 varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table PickListDet
   add constraint PK_PackListDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ITEM                                               */
/*==============================================================*/
create index IX_ITEM on PickListDet (
Item ASC,
Uom ASC,
UC ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: PickListMstr                                          */
/*==============================================================*/
create table PickListMstr (
   PLNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   PickBy               varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShipFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   DockDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsShipScan           bit                  not null constraint DF_PickListMstr_IsShipScan default (0),
   IsRecScan            bit                  not null constraint DF_PickListMstr_IsRecScan default (0),
   IsAutoReceive        bit                  not null constraint DF_PickListMstr_IsAutoReceive default (0),
   CompleteLatency      decimal(18,8)        null,
   GrGapTo              varchar(50)          collate Chinese_PRC_CI_AS null,
   AsnTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   RecTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   Flow                 varchar(50)          collate Chinese_PRC_CI_AS null,
   StartDate            datetime             null,
   StartUser            varchar(50)          collate Chinese_PRC_CI_AS null,
   IsPrinted            bit                  not null,
   WinTime              datetime             not null,
   IsAsnUniqueReceipt   bit                  null constraint DF_PickListMstr_IsAsnUniqueReceipt default (0),
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table PickListMstr
   add constraint PK_PackListMstr primary key (PLNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: PickListResult                                        */
/*==============================================================*/
create table PickListResult (
   Id                   int                  identity(1, 1),
   PLDetId              int                  not null,
   LocLotDetId          int                  not null,
   Qty                  decimal(18,8)        not null
)
on "PRIMARY"
go

alter table PickListResult
   add constraint PK_PackListResult primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_PIKRST                                             */
/*==============================================================*/
create index IX_PIKRST on PickListResult (
PLDetId ASC,
LocLotDetId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: PlanBill                                              */
/*==============================================================*/
create table PlanBill (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtRecNo             varchar(50)          collate Chinese_PRC_CI_AS null,
   TransType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   BillAddr             varchar(50)          collate Chinese_PRC_CI_AS not null,
   SettleTerm           varchar(50)          collate Chinese_PRC_CI_AS not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   PlanQty              decimal(18,8)        not null,
   ActQty               decimal(18,8)        null,
   UnitQty              decimal(18,8)        not null,
   UnitPrice            decimal(18,8)        not null,
   PriceList            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsIncludeTax         bit                  not null,
   TaxCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   IsProvEst            bit                  not null constraint DF_PlanBill_IsProvEst default (0),
   PlanAmount           decimal(18,8)        not null,
   ActAmount            decimal(18,8)        null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsAutoBill           bit                  not null,
   UC                   decimal(18,8)        not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table PlanBill
   add constraint PK_PlanBill primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: PriceListDet                                          */
/*==============================================================*/
create table PriceListDet (
   Id                   int                  identity(1, 1),
   PriceList            varchar(50)          collate Chinese_PRC_CI_AS not null,
   StartDate            datetime             not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   EndDate              datetime             null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS not null,
   UnitPrice            decimal(18,8)        not null,
   TaxCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   IsIncludeTax         bit                  not null,
   IsProvEst            bit                  not null constraint DF_PriceListDet_IsProv default (0),
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table PriceListDet
   add constraint PK_PriceListDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_PriceListDet                                       */
/*==============================================================*/
create unique clustered index IX_PriceListDet on PriceListDet (
PriceList ASC,
StartDate ASC,
Item ASC,
Currency ASC,
Uom ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: PriceListMstr                                         */
/*==============================================================*/
create table PriceListMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Party                varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table PriceListMstr
   add constraint PK_PriceList primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ProdLineIp                                            */
/*==============================================================*/
create table ProdLineIp (
   Id                   int                  identity(1, 1),
   ProdLine             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Op                   int                  null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   BackflushQty         decimal(18,8)        null,
   IsCS                 bit                  not null,
   PlanBillId           int                  null,
   Status               varchar(50)          collate Chinese_PRC_CI_AS not null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null,
   LastModifyDate       datetime             not null
)
on "PRIMARY"
go

alter table ProdLineIp
   add constraint PK_ProdLineIp primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ReceiptDet                                            */
/*==============================================================*/
create table ReceiptDet (
   Id                   int                  identity(1, 1),
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderLocTransId      int                  not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   IsCS                 bit                  not null,
   PlanBillId           int                  null,
   ShipQty              decimal(18,8)        null,
   RecQty               decimal(18,8)        null,
   RejQty               decimal(18,8)        null,
   ScrapQty             decimal(18,8)        null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS null,
   RefItemCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   CustomerItemCode     varchar(50)          collate Chinese_PRC_CI_AS null,
   Uom                  varchar(5)           collate Chinese_PRC_CI_AS null,
   UC                   decimal(18,8)        null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null,
   LocTo                varchar(50)          collate Chinese_PRC_CI_AS null,
   IpDetId              int                  null
)
on "PRIMARY"
go

alter table ReceiptDet
   add constraint PK_ReceiptDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_RECEIPTDET                                         */
/*==============================================================*/
create index IX_RECEIPTDET on ReceiptDet (
RecNo ASC,
OrderLocTransId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: ReceiptIp                                             */
/*==============================================================*/
create table ReceiptIp (
   Id                   int                  identity(1, 1),
   IpNo                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table ReceiptIp
   add constraint PK_ReceiptIp primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ReceiptMstr                                           */
/*==============================================================*/
create table ReceiptMstr (
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   OrderType            varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ExtRecNo             varchar(50)          collate Chinese_PRC_CI_AS null,
   RecTemplate          varchar(100)         collate Chinese_PRC_CI_AS null,
   PartyFrom            varchar(50)          collate Chinese_PRC_CI_AS not null,
   PartyTo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShipFrom             varchar(50)          collate Chinese_PRC_CI_AS null,
   ShipTo               varchar(50)          collate Chinese_PRC_CI_AS null,
   DockDesc             varchar(50)          collate Chinese_PRC_CI_AS null,
   RefIpNo              varchar(255)         collate Chinese_PRC_CI_AS null,
   HuTemplate           varchar(100)         collate Chinese_PRC_CI_AS null,
   TextField1           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField2           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField3           varchar(255)         collate Chinese_PRC_CI_AS null,
   TextField4           varchar(255)         collate Chinese_PRC_CI_AS null,
   NumField1            decimal(18,8)        null,
   NumField2            decimal(18,8)        null,
   NumField3            decimal(18,8)        null,
   NumField4            decimal(18,8)        null,
   DateField1           datetime             null,
   DateField2           datetime             null
)
on "PRIMARY"
go

alter table ReceiptMstr
   add constraint PK_ReceiptMstr primary key (RecNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: RepackDet                                             */
/*==============================================================*/
create table RepackDet (
   Id                   int                  identity(1, 1),
   RepNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   LocLotDetId          int                  not null,
   Qty                  decimal(18,8)        not null,
   IOType               varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table RepackDet
   add constraint PK_RepackDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: RepackMstr                                            */
/*==============================================================*/
create table RepackMstr (
   RepNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   CreateDate           datetime             not null,
   CreateUser           varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table RepackMstr
   add constraint PK_RepackMstr primary key (RepNo)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: RollingPlanDet                                        */
/*==============================================================*/
create table RollingPlanDet (
   Id                   int                  identity(1, 1),
   MstrId               int                  not null,
   ScheduleDate         datetime             not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        null
)
on "PRIMARY"
go

alter table RollingPlanDet
   add constraint PK_RollingPlanDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_RollingPlanDet                                     */
/*==============================================================*/
create unique clustered index IX_RollingPlanDet on RollingPlanDet (
MstrId ASC,
ScheduleDate ASC,
Item ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: RollingPlanMstr                                       */
/*==============================================================*/
create table RollingPlanMstr (
   Id                   int                  identity(1, 1),
   Flow                 varchar(50)          collate Chinese_PRC_CI_AS null,
   ExtOrderNo           varchar(50)          collate Chinese_PRC_CI_AS null,
   ReleaseDate          datetime             null,
   FileUploadId         int                  null,
   Status               nvarchar(20)         collate Chinese_PRC_CI_AS null,
   LastModifyDate       datetime             null,
   LastModifyUser       int                  null,
   ActiveStatus         nvarchar(20)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table RollingPlanMstr
   add constraint PK_RollingPlanMstr primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: RoutingDet                                            */
/*==============================================================*/
create table RoutingDet (
   Id                   int                  identity(1, 1),
   Routing              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Op                   int                  not null,
   Ref                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Activity             varchar(50)          collate Chinese_PRC_CI_AS not null,
   StartDate            datetime             not null,
   EndDate              datetime             null,
   WorkCenter           varchar(50)          collate Chinese_PRC_CI_AS not null,
   SetupTime            decimal(18,8)        not null,
   RunTime              decimal(18,8)        not null,
   MoveTime             decimal(18,8)        not null,
   TactTime             decimal(18,8)        not null,
   LocFrom              varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table RoutingDet
   add constraint PK_RoutingDet primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IN_RoutingDet                                         */
/*==============================================================*/
create unique clustered index IN_RoutingDet on RoutingDet (
Routing ASC,
Op ASC,
Ref ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: RoutingMstr                                           */
/*==============================================================*/
create table RoutingMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS not null,
   Region               varchar(50)          collate Chinese_PRC_CI_AS null,
   IsActive             bit                  not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table RoutingMstr
   add constraint PK_Routing primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: SCMSQAD                                               */
/*==============================================================*/
create table SCMSQAD (
   Id                   int                  identity(1, 1),
   TransType            varchar(50)          collate Chinese_PRC_CI_AS null,
   ItemCode             varchar(50)          collate Chinese_PRC_CI_AS null,
   CreateDate           datetime             null,
   SCMSQty              decimal(18,8)        null,
   QADQty               decimal(18,8)        null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: ShiftDet                                              */
/*==============================================================*/
create table ShiftDet (
   Id                   int                  identity(1, 1),
   Shift                varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShiftTime            varchar(255)         collate Chinese_PRC_CI_AS not null,
   StartDate            datetime             null,
   EndDate              datetime             null
)
on "PRIMARY"
go

alter table ShiftDet
   add constraint PK_ShiftDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ShiftMstr                                             */
/*==============================================================*/
create table ShiftMstr (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   ShiftName            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Memo                 varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table ShiftMstr
   add constraint PK_ShiftMstr primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: ShiftPlanSchedule                                     */
/*==============================================================*/
create table ShiftPlanSchedule (
   Id                   int                  identity(1, 1),
   FlowDetId            int                  not null,
   ReqDate              datetime             not null,
   Shift                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Seq                  int                  not null,
   PlanQty              decimal              not null,
   LastModifyDate       datetime             not null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table ShiftPlanSchedule
   add constraint PK_ShiftPlan primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ShiftPlanSchedule                                  */
/*==============================================================*/
create clustered index IX_ShiftPlanSchedule on ShiftPlanSchedule (
FlowDetId ASC,
ReqDate ASC,
Shift ASC,
Seq ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: SpecialTime                                           */
/*==============================================================*/
create table SpecialTime (
   ID                   int                  identity(1, 1),
   Region               varchar(50)          collate Chinese_PRC_CI_AS null,
   WorkCenter           varchar(50)          collate Chinese_PRC_CI_AS null,
   StartTime            datetime             not null,
   EndTime              datetime             not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table SpecialTime
   add constraint PK_TimeManagement primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: StorageArea                                           */
/*==============================================================*/
create table StorageArea (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Location             varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null
)
on "PRIMARY"
go

alter table StorageArea
   add constraint PK_StorageArea primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: StorageBin                                            */
/*==============================================================*/
create table StorageBin (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Area                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null,
   Seq                  int                  not null
)
on "PRIMARY"
go

alter table StorageBin
   add constraint PK_StorageBin primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: SubjectList                                           */
/*==============================================================*/
create table SubjectList (
   Id                   int                  identity(1, 1),
   SubjectCode          varchar(50)          collate Chinese_PRC_CI_AS not null,
   AccountCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   CostCenterCode       varchar(50)          collate Chinese_PRC_CI_AS not null,
   ProjectCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   AccountType          varchar(50)          collate Chinese_PRC_CI_AS null,
   SubjectName          varchar(100)         collate Chinese_PRC_CI_AS null,
   CostCenterName       varchar(100)         collate Chinese_PRC_CI_AS null,
   Reason               varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table SubjectList
   add constraint PK_SubjectList primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Tax                                                   */
/*==============================================================*/
create table Tax (
   TaxCode              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(50)          collate Chinese_PRC_CI_AS not null,
   TaxRate              decimal(18,8)        not null
)
on "PRIMARY"
go

alter table Tax
   add constraint PK_Tax primary key (TaxCode)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Uom                                                   */
/*==============================================================*/
create table Uom (
   Code                 varchar(5)           collate Chinese_PRC_CI_AS not null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table Uom
   add constraint PK_UnitOfMeasure primary key (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: UomConv                                               */
/*==============================================================*/
create table UomConv (
   Id                   int                  identity(11, 1),
   Item                 varchar(50)          collate Chinese_PRC_CI_AS null,
   AltUom               varchar(5)           collate Chinese_PRC_CI_AS not null,
   BaseUom              varchar(5)           collate Chinese_PRC_CI_AS not null,
   AltQty               decimal(18,8)        not null,
   BaseQty              decimal(18,8)        not null
)
on "PRIMARY"
go

alter table UomConv
   add constraint PK_UomConv primary key nonclustered (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_UomConv                                            */
/*==============================================================*/
create unique clustered index IX_UomConv on UomConv (
Item ASC,
AltUom ASC,
BaseUom ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: WorkCenter                                            */
/*==============================================================*/
create table WorkCenter (
   Code                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   IsActive             bit                  not null,
   Party                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   LbrBdnPer            decimal(18,8)        null,
   LbrBdnRate           decimal(18,8)        null,
   SetupBdnPer          decimal(18,8)        null,
   SetupBdnRate         decimal(18,8)        null,
   LaborRate            decimal(18,8)        null,
   Machine              varchar(50)          collate Chinese_PRC_CI_AS null,
   MachQty              decimal(18,8)        null,
   MachBdnRate          decimal(18,8)        null,
   MachSetupBdnRate     decimal(18,8)        null,
   RunCrew              decimal(18,8)        null,
   SetupCrew            decimal(18,8)        null,
   SetupRate            decimal(18,8)        null,
   QueueTime            decimal(18,8)        null,
   WaitTime             decimal(18,8)        null,
   PercentEfficiency    decimal(18,8)        null,
   PercentUtilization   decimal(18,8)        null
)
on "PRIMARY"
go

alter table WorkCenter
   add constraint PK12 primary key nonclustered (Code)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Workday                                               */
/*==============================================================*/
create table Workday (
   Id                   int                  identity(1, 1),
   Region               varchar(50)          collate Chinese_PRC_CI_AS null,
   WorkCenter           varchar(50)          collate Chinese_PRC_CI_AS null,
   DayOfWeek            varchar(50)          collate Chinese_PRC_CI_AS not null,
   Desc1                varchar(255)         collate Chinese_PRC_CI_AS null,
   Type                 varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table Workday
   add constraint PK_WorkCalendar primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: WorkdayShift                                          */
/*==============================================================*/
create table WorkdayShift (
   Id                   int                  identity(1, 1),
   WorkdayId            int                  not null,
   Shift                varchar(50)          collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

alter table WorkdayShift
   add constraint PK_WorkdayDet primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_WorkdayShift                                       */
/*==============================================================*/
create unique index IX_WorkdayShift on WorkdayShift (
WorkdayId ASC,
Shift ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: WorkingHours                                          */
/*==============================================================*/
create table WorkingHours (
   Id                   int                  identity(1, 1),
   RecNo                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Employee             varchar(50)          collate Chinese_PRC_CI_AS not null,
   Hours                decimal(18,8)        not null,
   LastModifyDate       datetime             null,
   LastModifyUser       varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

alter table WorkingHours
   add constraint PK_WorkingHours primary key (Id)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: cyclecountdet_bak                                     */
/*==============================================================*/
create table cyclecountdet_bak (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   Qty                  decimal(18,8)        not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: cyclecountresult_bak                                  */
/*==============================================================*/
create table cyclecountresult_bak (
   Id                   int                  identity(1, 1),
   OrderNo              varchar(50)          collate Chinese_PRC_CI_AS not null,
   Item                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   HuId                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LotNo                varchar(50)          collate Chinese_PRC_CI_AS null,
   Qty                  decimal(18,8)        not null,
   InvQty               decimal(18,8)        not null,
   DiffQty              decimal(18,8)        not null,
   DiffReason           varchar(50)          collate Chinese_PRC_CI_AS null,
   Bin                  varchar(50)          collate Chinese_PRC_CI_AS null,
   RefLocation          varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* View: ActBillView                                            */
/*==============================================================*/
create view ActBillView as
select
   MAX(Id) as Id,
   OrderNo,
   RecNo,
   ExtRecNo,
   TransType,
   BillAddr,
   Item,
   Uom,
   UC,
   EffDate,
   SUM(BillQty - ISNULL(BilledQty, 0)) as Qty
from
   ActBill
where
   (BillQty > 0)
   AND ( BillQty > BilledQty)
   OR ( BillQty < 0)
   AND ( BillQty < BilledQty)
group by
   OrderNo,
   RecNo,
   ExtRecNo,
   TransType,
   BillAddr,
   Item,
   Uom,
   UC,
   EffDate
go

/*==============================================================*/
/* View: BillAgingView                                          */
/*==============================================================*/
create view BillAgingView as
select
   MAX(Id) as ID,
   TransType,
   BillAddr,
   Item,
   Uom,
   UC,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) <= 30) THEN billqty - isnull(billedqty, 0)                        ELSE 0 END) as Qty1,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) <= 30) THEN ((billqty - isnull(billedqty, 0)) * UnitPrice) ELSE 0 END) as Amount1,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 30 AND datediff(day, effdate, getdate()) <= 60) THEN billqty - isnull(billedqty, 0)                        ELSE 0 END) as Qty2,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 30 AND datediff(day, effdate, getdate()) <= 60) THEN ((billqty - isnull(billedqty,                        0)) * UnitPrice) ELSE 0 END) as Amount2,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 60 AND datediff(day, effdate, getdate()) <= 90)                        THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty3,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 60 AND datediff(day, effdate, getdate())                        <= 90) THEN ((billqty - isnull(billedqty, 0)) * UnitPrice) ELSE 0 END) as Amount3,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 90 AND                        datediff(day, effdate, getdate()) <= 120) THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty4,
   SUM(CASE WHEN (datediff(day, effdate, getdate())                        > 90 AND datediff(day, effdate, getdate()) <= 120) THEN ((billqty - isnull(billedqty, 0)) * UnitPrice) ELSE 0 END) as Amount4,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 120 AND datediff(day, effdate, getdate()) <= 150) THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty5,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 120 AND datediff(day, effdate, getdate()) <= 150) THEN ((billqty - isnull(billedqty, 0))                        * UnitPrice) ELSE 0 END) as Amount5,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 150 AND datediff(day, effdate, getdate()) <= 180)                        THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty6,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 150 AND datediff(day, effdate, getdate())                        <= 180) THEN ((billqty - isnull(billedqty, 0)) * UnitPrice) ELSE 0 END) as Amount6,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 180 AND                        datediff(day, effdate, getdate()) <= 210) THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty7,
   SUM(CASE WHEN (datediff(day, effdate, getdate())                        > 180 AND datediff(day, effdate, getdate()) <= 210) THEN ((billqty - isnull(billedqty, 0)) * UnitPrice) ELSE 0 END) as Amount7,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 210 AND datediff(day, effdate, getdate()) <= 360) THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty8,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 210 AND datediff(day, effdate, getdate()) <= 360) THEN ((billqty - isnull(billedqty, 0))                        * UnitPrice) ELSE 0 END) as Amount8,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 360) THEN billqty - isnull(billedqty, 0) ELSE 0 END) as Qty9,
   SUM(CASE WHEN (datediff(day, effdate, getdate()) > 360) THEN ((billqty - isnull(billedqty, 0)) * UnitPrice) ELSE 0 END) as Amount9
from
   ActBill
where
   (BillQty > 0)
   AND ( BillQty > BilledQty)
   OR ( BillQty < 0)
   AND ( BillQty < BilledQty)
group by
   TransType,
   BillAddr,
   Item,
   Uom,
   UC
go

/*==============================================================*/
/* View: FlowView                                               */
/*==============================================================*/
create view FlowView as
SELECT     dbo.FlowDet.Id, dbo.FlowDet.Id AS FlowDetId, dbo.FlowDet.Flow, dbo.FlowDet.IsAutoCreate & dbo.FlowMstr.IsAutoCreate AS IsAutoCreate, 
                      ISNULL(dbo.FlowDet.LocFrom, dbo.FlowMstr.LocFrom) AS LocFrom, ISNULL(dbo.FlowDet.LocTo, dbo.FlowMstr.LocTo) AS LocTo, 
                      dbo.FlowMstr.RefFlow
FROM         dbo.FlowDet INNER JOIN
                      dbo.FlowMstr ON dbo.FlowDet.Flow = dbo.FlowMstr.Code
WHERE     (dbo.FlowMstr.IsActive = 1) AND (dbo.FlowDet.StartDate IS NULL OR
                      dbo.FlowDet.StartDate <= GETDATE()) AND (dbo.FlowDet.EndDate IS NULL OR
                      dbo.FlowDet.EndDate >= GETDATE())
UNION
SELECT     FlowDet_1.Id, FlowDet_1.Id AS FlowDetId, FlowMstr_2.Code AS Flow, FlowDet_1.IsAutoCreate & FlowMstr_2.IsAutoCreate AS IsAutoCreate, 
                      FlowMstr_2.LocFrom, FlowMstr_2.LocTo, FlowMstr_2.RefFlow
FROM         dbo.FlowDet AS FlowDet_1 INNER JOIN
                      dbo.FlowMstr AS FlowMstr_1 ON FlowDet_1.Flow = FlowMstr_1.Code INNER JOIN
                      dbo.FlowMstr AS FlowMstr_2 ON FlowMstr_2.RefFlow = FlowMstr_1.Code AND FlowMstr_2.RefFlow IS NOT NULL
WHERE     (FlowMstr_1.IsActive = 1) AND (FlowMstr_2.IsActive = 1) AND (FlowDet_1.StartDate IS NULL OR
                      FlowDet_1.StartDate <= GETDATE()) AND (FlowDet_1.EndDate IS NULL OR
                      FlowDet_1.EndDate >= GETDATE())
go

/*==============================================================*/
/* View: InvAgingView                                           */
/*==============================================================*/
create view InvAgingView as
SELECT     MAX(dbo.LocationLotDet.Id) AS Id, dbo.LocationLotDet.Location, dbo.LocationLotDet.Item, SUM(dbo.LocationLotDet.Qty) AS Qty, 
                      dbo.LocationLotDet.CreateDate, dbo.Location.Region
FROM         dbo.LocationLotDet INNER JOIN
                      dbo.Location ON dbo.LocationLotDet.Location = dbo.Location.Code
WHERE     (dbo.LocationLotDet.Qty <> 0)
GROUP BY dbo.LocationLotDet.Location, dbo.LocationLotDet.Item, dbo.LocationLotDet.CreateDate, dbo.Location.Region
go

/*==============================================================*/
/* View: IpDetTrackView                                         */
/*==============================================================*/
create view IpDetTrackView as
SELECT     MAX(dbo.IpDet.Id) AS Id, dbo.OrderMstr.Flow, dbo.IpMstr.CurrOp, dbo.OrderDet.Id AS OrderDetId, SUM(dbo.IpDet.Qty) AS Qty
FROM         dbo.IpDet INNER JOIN
                      dbo.IpMstr ON dbo.IpDet.IpNo = dbo.IpMstr.IpNo INNER JOIN
                      dbo.OrderLocTrans ON dbo.IpDet.OrderLocTransId = dbo.OrderLocTrans.Id INNER JOIN
                      dbo.OrderDet ON dbo.OrderLocTrans.OrderDetId = dbo.OrderDet.Id INNER JOIN
                      dbo.OrderMstr ON dbo.OrderDet.OrderNo = dbo.OrderMstr.OrderNo
WHERE     (dbo.IpMstr.Status = 'Create') AND (dbo.IpMstr.Type = 'Nml')
GROUP BY dbo.OrderMstr.Flow, dbo.IpMstr.CurrOp, dbo.OrderDet.Id
go

/*==============================================================*/
/* View: IpDetView                                              */
/*==============================================================*/
create view IpDetView as
SELECT     MAX(dbo.IpDet.Id) AS Id, dbo.IpDet.IpNo, dbo.OrderDet.Id AS OrderDetailId, SUM(dbo.IpDet.Qty) AS Qty
FROM         dbo.IpDet INNER JOIN
                      dbo.IpMstr ON dbo.IpDet.IpNo = dbo.IpMstr.IpNo INNER JOIN
                      dbo.OrderLocTrans ON dbo.IpDet.OrderLocTransId = dbo.OrderLocTrans.Id INNER JOIN
                      dbo.OrderDet ON dbo.OrderLocTrans.OrderDetId = dbo.OrderDet.Id
WHERE     (dbo.IpMstr.Status = 'Create') AND (dbo.IpMstr.Type = 'Nml')
GROUP BY dbo.IpDet.IpNo, dbo.OrderDet.Id
go

/*==============================================================*/
/* View: LeanEngineView                                         */
/*==============================================================*/
create view LeanEngineView as
SELECT     dbo.FlowView.FlowDetId, dbo.FlowView.Flow, dbo.FlowView.IsAutoCreate, dbo.FlowView.LocFrom, dbo.FlowView.LocTo, dbo.FlowDet.Item, 
                      dbo.FlowDet.Uom, dbo.FlowDet.UC, dbo.FlowDet.HuLotSize, dbo.FlowDet.Bom, dbo.FlowDet.SafeStock, dbo.FlowDet.MaxStock, 
                      dbo.FlowDet.MinLotSize, dbo.FlowDet.OrderLotSize, dbo.FlowDet.BatchSize, dbo.FlowDet.RoundUpOpt, dbo.FlowMstr.Type, dbo.FlowMstr.PartyFrom, 
                      dbo.FlowMstr.PartyTo, dbo.FlowMstr.FlowStrategy, dbo.FlowMstr.LeadTime, dbo.FlowMstr.EmTime, dbo.FlowMstr.MaxCirTime, 
                      dbo.FlowMstr.WinTime1, dbo.FlowMstr.WinTime2, dbo.FlowMstr.WinTime3, dbo.FlowMstr.WinTime4, dbo.FlowMstr.WinTime5, 
                      dbo.FlowMstr.WinTime6, dbo.FlowMstr.WinTime7, dbo.FlowMstr.NextOrderTime, dbo.FlowMstr.NextWinTime, dbo.FlowMstr.WeekInterval
FROM         dbo.FlowView INNER JOIN
                      dbo.FlowDet ON dbo.FlowView.FlowDetId = dbo.FlowDet.Id INNER JOIN
                      dbo.FlowMstr ON dbo.FlowDet.Flow = dbo.FlowMstr.Code
go

/*==============================================================*/
/* View: LocBinDet                                              */
/*==============================================================*/
create view LocBinDet as
SELECT     dbo.LocationLotDet.Id, dbo.LocationLotDet.Location, dbo.LocationLotDet.Bin, dbo.LocationLotDet.Item, dbo.Item.Desc1 + dbo.Item.Desc2 AS ItemDesc, 
                      dbo.Item.UC, dbo.Item.Uom, dbo.LocationLotDet.Qty, dbo.LocationLotDet.HuId, dbo.LocationLotDet.LotNo
FROM         dbo.LocationLotDet INNER JOIN
                      dbo.Item ON dbo.LocationLotDet.Item = dbo.Item.Code
WHERE     (dbo.LocationLotDet.Qty <> 0)
go

/*==============================================================*/
/* View: LocBinItemDet                                          */
/*==============================================================*/
create view LocBinItemDet as
SELECT     MAX(dbo.LocationLotDet.Id) AS Id, dbo.LocationLotDet.Location, dbo.LocationLotDet.Bin, dbo.LocationLotDet.Item, 
                      dbo.Item.Desc1 + dbo.Item.Desc2 AS ItemDesc, dbo.Item.UC, dbo.Item.Uom, SUM(dbo.LocationLotDet.Qty) AS Qty
FROM         dbo.LocationLotDet INNER JOIN
                      dbo.Item ON dbo.LocationLotDet.Item = dbo.Item.Code
WHERE     (dbo.LocationLotDet.Qty <> 0)
GROUP BY dbo.LocationLotDet.Location, dbo.LocationLotDet.Bin, dbo.LocationLotDet.Item, dbo.Item.Desc1 + dbo.Item.Desc2, dbo.Item.UC, dbo.Item.Uom
go

/*==============================================================*/
/* View: LocBinMstr                                             */
/*==============================================================*/
create view LocBinMstr as
SELECT     MAX(dbo.LocationLotDet.Id) AS Id, dbo.Location.Region, dbo.LocationLotDet.Location, dbo.Location.Name AS LocationName, dbo.StorageBin.Area, 
                      dbo.StorageArea.Desc1 AS AreaDesc, dbo.LocationLotDet.Bin, dbo.StorageBin.Desc1 AS BinDesc, COUNT(DISTINCT dbo.LocationLotDet.Item) 
                      AS ItemCount, COUNT(dbo.LocationLotDet.HuId) AS HuCount
FROM         dbo.LocationLotDet INNER JOIN
                      dbo.Location ON dbo.LocationLotDet.Location = dbo.Location.Code LEFT OUTER JOIN
                      dbo.StorageBin ON dbo.LocationLotDet.Bin = dbo.StorageBin.Code LEFT OUTER JOIN
                      dbo.StorageArea ON dbo.StorageBin.Area = dbo.StorageArea.Code
WHERE     (dbo.LocationLotDet.Qty <> 0)
GROUP BY dbo.Location.Region, dbo.LocationLotDet.Location, dbo.Location.Name, dbo.StorageBin.Area, dbo.StorageArea.Desc1, dbo.LocationLotDet.Bin, 
                      dbo.StorageBin.Desc1
go

/*==============================================================*/
/* View: LocLotDetView                                          */
/*==============================================================*/
create view LocLotDetView as
select
   MAX(Id) as Id,
   Location,
   Bin,
   Item,
   LotNo,
   SUM(Qty) as Qty
from
   LocationLotDet
group by
   Location,
   Bin,
   Item,
   LotNo
having
   (SUM(Qty) <> 0)
go

/*==============================================================*/
/* View: LocTransView                                           */
/*==============================================================*/
create view LocTransView as
select
   MAX(Id) as Id,
   OrderNo,
   ExtOrderNo,
   RefOrderNo,
   IpNo,
   RecNo,
   BillTransId,
   TransType,
   Item,
   ItemDesc,
   Uom,
   SUM(Qty) as Qty,
   PartyFrom,
   PartyFromName,
   PartyTo,
   PartyToName,
   ShipFrom,
   ShipFromAddr,
   ShipTo,
   ShipToAddr,
   Loc,
   LocName,
   LocIOReason,
   LocIOReasonDesc,
   EffDate,
   CreateUser
from
   LocTrans
group by
   OrderNo,
   ExtOrderNo,
   RefOrderNo,
   IpNo,
   RecNo,
   BillTransId,
   TransType,
   Item,
   ItemDesc,
   Uom,
   PartyFrom,
   PartyFromName,
   PartyTo,
   PartyToName,
   ShipFrom,
   ShipFromAddr,
   ShipTo,
   ShipToAddr,
   Loc,
   LocName,
   LocIOReason,
   LocIOReasonDesc,
   EffDate,
   CreateUser
go

/*==============================================================*/
/* View: LocationDet                                            */
/*==============================================================*/
create view LocationDet as
select
   MAX(Id) as Id,
   Location,
   Item,
   SUM(CASE WHEN IsCS = 1 THEN Qty ELSE 0 END) as CsQty,
   SUM(CASE WHEN IsCS = 0 THEN Qty ELSE 0 END) as NmlQty,
   SUM(Qty) as Qty,
   COUNT_BIG(*) as COUNT
from
   LocationLotDet
group by
   Location,
   Item
go

/*==============================================================*/
/* View: MenuView                                               */
/*==============================================================*/
create view MenuView as
SELECT     Menu1.Id, Menu1.Code, Menu1.Version, Menu1.Title, Menu1.Desc_, Menu1.PageUrl, Menu1.IsActive, Menu1.ImageUrl, Menu1.Remark, 
                      ACC_MenuCommon.Id AS MenuRelationId, 'ACC_MenuCommon' AS Type, '' AS IndustryOrCompanyCode, 
                      ACC_MenuCommon.ParentMenuId AS ParentId, ParentMenu1.Code AS ParentCode, ParentMenu1.Version AS ParenVersion, 
                      ACC_MenuCommon.Level_, ACC_MenuCommon.Seq, ACC_MenuCommon.IsActive AS MenuRelationIsActive, 
                      ACC_MenuCommon.CreateDate, ACC_MenuCommon.CreateUser, ACC_MenuCommon.LastModifyDate, 
                      ACC_MenuCommon.LastModifyUser
FROM         ACC_MenuCommon INNER JOIN
                      ACC_Menu AS Menu1 ON Menu1.Id = ACC_MenuCommon.MenuId LEFT OUTER JOIN
                      ACC_Menu AS ParentMenu1 ON ACC_MenuCommon.ParentMenuId = ParentMenu1.Id
WHERE     (NOT EXISTS
                          (SELECT     1 AS Expr1
                            FROM          ACC_MenuIndustry INNER JOIN
                                                   ACC_Industry ON ACC_MenuIndustry.IndustryCode = ACC_Industry.Code INNER JOIN
                                                   ACC_Company ON ACC_Industry.Code = ACC_Company.IndustryCode INNER JOIN
                                                   EntityOpt ON ACC_Company.Code = EntityOpt.PreValue INNER JOIN
                                                   ACC_Menu ON ACC_MenuIndustry.MenuId = ACC_Menu.Id
                            WHERE      (EntityOpt.PreCode = 'CompanyCode') AND (ACC_Menu.Code = Menu1.Code))) AND (NOT EXISTS
                          (SELECT     1 AS Expr1
                            FROM          ACC_MenuCompany INNER JOIN
                                                   ACC_Company AS ACC_Company_3 ON ACC_MenuCompany.CompanyCode = ACC_Company_3.Code INNER JOIN
                                                   EntityOpt AS EntityOpt_3 ON ACC_Company_3.Code = EntityOpt_3.PreValue INNER JOIN
                                                   ACC_Menu AS ACC_Menu_2 ON ACC_MenuCompany.MenuId = ACC_Menu_2.Id
                            WHERE      (EntityOpt_3.PreCode = 'CompanyCode') AND (Menu1.Code = ACC_Menu_2.Code)))
UNION
SELECT     Menu2.Id, Menu2.Code, Menu2.Version, Menu2.Title, Menu2.Desc_, Menu2.PageUrl, Menu2.IsActive, Menu2.ImageUrl, Menu2.Remark, 
                      ACC_MenuIndustry_1.Id AS MenuRelationId, 'ACC_MenuIndustry' AS Type, ACC_MenuIndustry_1.IndustryCode AS IndustryOrCompanyCode, 
                      ACC_MenuIndustry_1.ParentMenuId AS ParentId, ParentMenu2.Code AS ParentCode, ParentMenu2.Version AS ParentVersion, 
                      ACC_MenuIndustry_1.Level_, ACC_MenuIndustry_1.Seq, ACC_MenuIndustry_1.IsActive AS MenuRelationIsActive, ACC_MenuIndustry_1.CreateDate, 
                      ACC_MenuIndustry_1.CreateUser, ACC_MenuIndustry_1.LastModifyDate, ACC_MenuIndustry_1.LastModifyUser
FROM         ACC_MenuIndustry AS ACC_MenuIndustry_1 INNER JOIN
                      ACC_Menu AS Menu2 ON Menu2.Id = ACC_MenuIndustry_1.MenuId LEFT OUTER JOIN
                      ACC_Menu AS ParentMenu2 ON ACC_MenuIndustry_1.ParentMenuId = ParentMenu2.Id
WHERE     (NOT EXISTS
                          (SELECT     1 AS Expr1
                            FROM          ACC_MenuCompany AS ACC_MenuCompany_2 INNER JOIN
                                                   ACC_Company AS ACC_Company_2 ON ACC_MenuCompany_2.CompanyCode = ACC_Company_2.Code INNER JOIN
                                                   EntityOpt AS EntityOpt_2 ON ACC_Company_2.Code = EntityOpt_2.PreValue INNER JOIN
                                                   ACC_Menu AS ACC_Menu_1 ON ACC_MenuCompany_2.MenuId = ACC_Menu_1.Id
                            WHERE      (EntityOpt_2.PreCode = 'CompanyCode') AND (ACC_Menu_1.Code = Menu2.Code)))
UNION
SELECT     Menu3.Id, Menu3.Code, Menu3.Version, Menu3.Title, Menu3.Desc_, Menu3.PageUrl, Menu3.IsActive, Menu3.ImageUrl, Menu3.Remark, 
                      ACC_MenuCompany_1.Id AS MenuRelationId, 'ACC_MenuCompany' AS Type, ACC_MenuCompany_1.CompanyCode AS IndustryOrCompanyCode, 
                      ACC_MenuCompany_1.ParentMenuId AS ParentId, ParentMenu3.Code AS ParentCode, ParentMenu3.Version AS ParentVersion, 
                      ACC_MenuCompany_1.Level_, ACC_MenuCompany_1.Seq, ACC_MenuCompany_1.IsActive AS MenuRelationIsActive, 
                      ACC_MenuCompany_1.CreateDate, ACC_MenuCompany_1.CreateUser, ACC_MenuCompany_1.LastModifyDate, 
                      ACC_MenuCompany_1.LastModifyUser
FROM         ACC_MenuCompany AS ACC_MenuCompany_1 INNER JOIN
                      ACC_Menu AS Menu3 ON ACC_MenuCompany_1.MenuId = Menu3.Id INNER JOIN
                      ACC_Company AS ACC_Company_1 ON ACC_MenuCompany_1.CompanyCode = ACC_Company_1.Code INNER JOIN
                      EntityOpt AS EntityOpt_1 ON ACC_Company_1.Code = EntityOpt_1.PreValue LEFT OUTER JOIN
                      ACC_Menu AS ParentMenu3 ON ACC_MenuCompany_1.ParentMenuId = ParentMenu3.Id
WHERE     (EntityOpt_1.PreCode = 'CompanyCode')
go

/*==============================================================*/
/* View: OrderDetView                                           */
/*==============================================================*/
create view OrderDetView as
SELECT     MAX(dbo.OrderDet.Id) AS Id, dbo.OrderMstr.Flow, dbo.FlowMstr.Desc1, dbo.OrderMstr.Type, dbo.OrderMstr.PartyFrom, dbo.OrderMstr.PartyTo, 
                      CONVERT(datetime, CONVERT(varchar(8), dbo.OrderMstr.StartTime, 112)) AS EffDate, dbo.OrderMstr.Shift, dbo.OrderDet.Item, dbo.OrderDet.Uom, 
                      SUM(dbo.OrderDet.ReqQty) AS ReqQty, SUM(dbo.OrderDet.OrderQty) AS OrderQty, ISNULL(SUM(dbo.OrderDet.ShipQty), 0) AS ShipQty, 
                      ISNULL(SUM(dbo.OrderDet.RecQty), 0) AS RecQty, ISNULL(SUM(dbo.OrderDet.RejQty), 0) AS RejQty, ISNULL(SUM(dbo.OrderDet.ScrapQty), 0) 
                      AS ScrapQty, dbo.OrderMstr.Status
FROM         dbo.OrderDet INNER JOIN
                      dbo.OrderMstr ON dbo.OrderDet.OrderNo = dbo.OrderMstr.OrderNo INNER JOIN
                      dbo.FlowMstr ON dbo.OrderMstr.Flow = dbo.FlowMstr.Code
GROUP BY dbo.OrderMstr.Flow, dbo.FlowMstr.Desc1, dbo.OrderMstr.Type, dbo.OrderMstr.PartyFrom, dbo.OrderMstr.PartyTo, CONVERT(varchar(8), 
                      dbo.OrderMstr.StartTime, 112), dbo.OrderMstr.Shift, dbo.OrderDet.Item, dbo.OrderDet.Uom, dbo.OrderMstr.Status
go

/*==============================================================*/
/* View: OrderLocTransView                                      */
/*==============================================================*/
create view OrderLocTransView as
SELECT     dbo.OrderLocTrans.Id, dbo.OrderMstr.OrderNo, dbo.OrderMstr.Type, dbo.OrderMstr.Flow, dbo.OrderMstr.Status, dbo.OrderMstr.StartTime, 
                      dbo.OrderMstr.WindowTime, dbo.OrderMstr.PartyFrom, dbo.OrderMstr.PartyTo, dbo.OrderLocTrans.Loc, dbo.OrderDet.Item AS ItemCode, 
                      dbo.Item.Desc1 + dbo.Item.Desc2 AS ItemDesc, dbo.OrderDet.Uom, dbo.OrderDet.ReqQty, dbo.OrderDet.OrderQty, ISNULL(dbo.OrderDet.ShipQty, 0) 
                      AS ShipQty, ISNULL(dbo.OrderDet.RecQty, 0) AS RecQty, dbo.OrderLocTrans.Item, dbo.OrderLocTrans.IOType, dbo.OrderLocTrans.UnitQty, 
                      dbo.OrderLocTrans.OrderQty AS PlanQty, ISNULL(dbo.OrderLocTrans.AccumQty, 0) AS AccumQty
FROM         dbo.OrderDet INNER JOIN
                      dbo.OrderMstr ON dbo.OrderDet.OrderNo = dbo.OrderMstr.OrderNo INNER JOIN
                      dbo.OrderLocTrans ON dbo.OrderDet.Id = dbo.OrderLocTrans.OrderDetId INNER JOIN
                      dbo.Item ON dbo.OrderDet.Item = dbo.Item.Code
go

/*==============================================================*/
/* View: PlanBillView                                           */
/*==============================================================*/
create view PlanBillView as
SELECT     dbo.OrderMstr.Flow, dbo.PlanBill.BillAddr, dbo.PlanBill.Item, dbo.PlanBill.Uom, dbo.PlanBill.UC, dbo.PlanBill.TransType, 
                      SUM(dbo.PlanBill.PlanQty - ISNULL(dbo.PlanBill.ActQty, 0)) AS PlanQty, MAX(dbo.PlanBill.Id) AS Id
FROM         dbo.PlanBill INNER JOIN
                      dbo.OrderMstr ON dbo.PlanBill.OrderNo = dbo.OrderMstr.OrderNo
WHERE     (dbo.PlanBill.PlanQty > ISNULL(dbo.PlanBill.ActQty, 0))
GROUP BY dbo.PlanBill.BillAddr, dbo.PlanBill.Item, dbo.PlanBill.Uom, dbo.PlanBill.UC, dbo.PlanBill.TransType, dbo.OrderMstr.Flow
go

/*==============================================================*/
/* View: SupplierLocationView                                   */
/*==============================================================*/
create view SupplierLocationView as
SELECT     MAX(dbo.LocationLotDet.Id) AS Id, dbo.LocationLotDet.Item, SUM(dbo.LocationLotDet.Qty) AS Qty, dbo.LocationLotDet.Location, 
                      dbo.OrderMstr.PartyFrom, dbo.OrderMstr.PartyTo
FROM         dbo.LocationLotDet INNER JOIN
                      dbo.PlanBill ON dbo.LocationLotDet.PlanBillId = dbo.PlanBill.Id INNER JOIN
                      dbo.OrderMstr ON dbo.PlanBill.OrderNo = dbo.OrderMstr.OrderNo
WHERE     (dbo.LocationLotDet.IsCS = 1) AND (dbo.PlanBill.TransType = 'PO')
GROUP BY dbo.LocationLotDet.Item, dbo.LocationLotDet.Location, dbo.OrderMstr.PartyFrom, dbo.OrderMstr.PartyTo
go

alter table ACC_Company
   add constraint FK_ACC_COMP_INDUSTRY__ACC_INDU foreign key (IndustryCode)
      references ACC_Industry (Code)
go

alter table ACC_MenuCommon
   add constraint FK_Menu_MenuCommon foreign key (ACC_id)
      references ACC_Menu (id)
go

alter table ACC_MenuCommon
   add constraint FK_PARENTMENU_MENUCOMMON foreign key (ParentMenuId)
      references ACC_Menu (id)
go

alter table ACC_MenuCompany
   add constraint FK_ACC_MENU_COMPANY_M_ACC_COMP foreign key (CompanyCode)
      references ACC_Company (Code)
go

alter table ACC_MenuCompany
   add constraint FK_Menu_MenuCompany foreign key (MenuId)
      references ACC_Menu (id)
go

alter table ACC_MenuCompany
   add constraint FK_ParentMenu_CompanyMenu foreign key (ParentMenuId)
      references ACC_Menu (id)
go

alter table ACC_MenuIndustry
   add constraint FK_INDUSTRY_MENUINDUSTRY foreign key (IndustryCode)
      references ACC_Industry (Code)
go

alter table ACC_MenuIndustry
   add constraint FK_ACC_MENU_MENUINDUSTRY foreign key (ACC_id)
      references ACC_Menu (id)
go

alter table ACC_MenuIndustry
   add constraint FK_ParentMenu_MenuIndustry foreign key (ParentMenuId)
      references ACC_Menu (id)
go

alter table ACC_Permission
   add constraint FK_PermissionCategory_Permission foreign key (PM_CateCode)
      references ACC_PermissionCategory (PMC_Code)
go

alter table ACC_RolePermission
   add constraint FK_ACC_RolePermission_ACC_Permission foreign key (RP_PMID)
      references ACC_Permission (PM_ID)
go

alter table ACC_RolePermission
   add constraint FK_ACC_RolePermission_ACC_Role foreign key (RP_RoleCode)
      references ACC_Role (ROLE_Code)
go

alter table ACC_User
   add constraint FK_ACC_User_ACC_User foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table ACC_UserFav
   add constraint FK_Favorites_ACC_User foreign key (USR_Code)
      references ACC_User (USR_Code)
go

alter table ACC_UserPermission
   add constraint FK_ACC_UserPermission_ACC_Permission foreign key (UP_PMID)
      references ACC_Permission (PM_ID)
go

alter table ACC_UserPermission
   add constraint FK_ACC_UserPermission_ACC_User foreign key (UP_USRCode)
      references ACC_User (USR_Code)
go

alter table ACC_UserPre
   add constraint FK_UserPreference_ACC_User foreign key (USR_Code)
      references ACC_User (USR_Code)
go

alter table ACC_UserRole
   add constraint FK_ACC_UserRole_ACC_User foreign key (UR_USRCode)
      references ACC_User (USR_Code)
go

alter table ActBill
   add constraint FK_ActBill_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table ActBill
   add constraint FK_ActBill_ACC_User1 foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table ActBill
   add constraint FK_ActBill_Currency foreign key (Currency)
      references Currency (Code)
go

alter table ActBill
   add constraint FK_ActBill_Item foreign key (Item)
      references Item (Code)
go

alter table ActBill
   add constraint FK_ActBill_OrderMstr foreign key (OrderNo)
      references OrderMstr (OrderNo)
go

alter table ActBill
   add constraint FK_ActBill_PartyAddr foreign key (BillAddr)
      references PartyAddr (Code)
go

alter table ActBill
   add constraint FK_ActBill_PriceListMstr foreign key (PriceList)
      references PriceListMstr (Code)
go

alter table ActBill
   add constraint FK_ActBill_ReceiptMstr foreign key (RecNo)
      references ReceiptMstr (RecNo)
go

alter table ActBill
   add constraint FK_ActBill_Uom foreign key (Uom)
      references Uom (Code)
go

alter table AutoOrderTrack
   add constraint FK_AutoOrderTrack_OrderDet foreign key (OrderDetId)
      references OrderDet (Id)
go

alter table AutoOrderTrack
   add constraint FK_AutoOrderTrack_OrderDet1 foreign key (RefOrderDetId)
      references OrderDet (Id)
go

alter table BatchJobParam
   add constraint FK_BatchJobParam_BatchJobDet foreign key (JobId)
      references BatchJobDet (Id)
go

alter table BatchRunLog
   add constraint FK_BatchRunLog_BatchJobDet foreign key (JobId)
      references BatchJobDet (Id)
go

alter table BatchRunLog
   add constraint FK_BatchRunLog_BatchTrigger foreign key (TriggerId)
      references BatchTrigger (Id)
go

alter table BatchTrigger
   add constraint FK_BatchTrigger_BatchJobDet foreign key (JobId)
      references BatchJobDet (Id)
go

alter table BatchTriggerParam
   add constraint FK_BatchTriggerParam_BatchTrigger foreign key (TriggerId)
      references BatchTrigger (Id)
go

alter table BillDet
   add constraint FK_BillDet_BillMstr foreign key (BillNo)
      references BillMstr (BillNo)
go

alter table BillDet
   add constraint FK_BillDet_BillTrans foreign key (TransId)
      references ActBill (Id)
go

alter table BillDet
   add constraint FK_BillDet_Currency foreign key (Currency)
      references Currency (Code)
go

alter table BillMstr
   add constraint FK_BillMstr_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table BillMstr
   add constraint FK_BillMstr_ACC_User1 foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table BillMstr
   add constraint FK_BillMstr_Currency foreign key (Currency)
      references Currency (Code)
go

alter table BillMstr
   add constraint FK_BillMstr_PartyAddr foreign key (BillAddr)
      references PartyAddr (Code)
go

alter table BomDet
   add constraint FK_BomDet_BomMstr foreign key (Bom)
      references BomMstr (Code)
go

alter table BomDet
   add constraint FK_BomDet_Item foreign key (Item)
      references Item (Code)
go

alter table BomDet
   add constraint FK_BomDet_Location foreign key (Loc)
      references Location (Code)
go

alter table BomDet
   add constraint FK_BomDet_Uom foreign key (Uom)
      references Uom (Code)
go

alter table BomMstr
   add constraint FK_BomMstr_Region foreign key (Region)
      references Party (Code)
go

alter table BomMstr
   add constraint FK_BomMstr_Uom foreign key (Uom)
      references Uom (Code)
go

alter table ClientLog
   add constraint FK_ClientLog_Client foreign key (ClientId)
      references Client (ClientId)
go

alter table ClientMonitor
   add constraint FK_ClientMonitor_Client foreign key (ClientId)
      references Client (ClientId)
go

alter table ClientOrderDet
   add constraint FK_ClientOrderDet_ClientOrderMstr foreign key (OrderHeadId)
      references ClientOrderMstr (Id)
go

alter table ClientOrderMstr
   add constraint FK_ClientOrderMstr_Client foreign key (ClientId)
      references Client (ClientId)
go

alter table ClientWorkingHours
   add constraint FK_ClientWorkingHours_ClientOrderMstr foreign key (OrderHeadId)
      references ClientOrderMstr (Id)
go

alter table CurrencyExchange
   add constraint FK_CurrencyExchange_Currency foreign key (Currency)
      references Currency (Code)
go

alter table CycleCountDet
   add constraint FK_CycleCountDet_CycleCountMstr foreign key (OrderNo)
      references CycleCountMstr (Code)
go

alter table CycleCountDet
   add constraint FK_CycleCountDet_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table CycleCountDet
   add constraint FK_CycleCountDet_Item foreign key (Item)
      references Item (Code)
go

alter table CycleCountMstr
   add constraint FK_CycleCountMstr_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table CycleCountMstr
   add constraint FK_CycleCountMstr_ACC_User1 foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table CycleCountMstr
   add constraint FK_CycleCountMstr_ACC_User2 foreign key (ReleaseUser)
      references ACC_User (USR_Code)
go

alter table CycleCountMstr
   add constraint FK_CycleCountMstr_ACC_User3 foreign key (CloseUser)
      references ACC_User (USR_Code)
go

alter table CycleCountMstr
   add constraint FK_CycleCountMstr_ACC_User4 foreign key (CancelUser)
      references ACC_User (USR_Code)
go

alter table CycleCountMstr
   add constraint FK_CycleCountMstr_Location foreign key (Location)
      references Location (Code)
go

alter table CycleCountResult
   add constraint FK_CycleCountResult_CycleCountMstr foreign key (OrderNo)
      references CycleCountMstr (Code)
go

alter table CycleCountResult
   add constraint FK_CycleCountResult_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table CycleCountResult
   add constraint FK_CycleCountResult_Item foreign key (Item)
      references Item (Code)
go

alter table DssExpHis
   add constraint FK_DssExpMstr_DssOutboundCtrl foreign key (DssOutboundCtrl)
      references DssOutboundCtrl (Id)
go

alter table DssExpHisDet
   add constraint FK_DssExpHisDet_DssExpHis foreign key (MstrId)
      references DssExpHis (Id)
go

alter table DssOutboundCtrl
   add constraint FK_DssOutboundCtrl_DssSysMstr foreign key (ExtSysCode)
      references DssSysMstr (Code)
go

alter table Employee
   add constraint FK_Employee_ACC_User foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table FlowBinding
   add constraint FK_FlowBinding_FlowMstr_Mstr foreign key (MstrFlow)
      references FlowMstr (Code)
go

alter table FlowBinding
   add constraint FK_FlowBinding_FlowMstr_Slv foreign key (SlvFlow)
      references FlowMstr (Code)
go

alter table FlowDet
   add constraint FK_FlowDet_FlowMstr foreign key (Flow)
      references FlowMstr (Code)
go

alter table FlowDet
   add constraint FK_FlowDet_PriceListMstr foreign key (PriceListFrom)
      references PriceListMstr (Code)
go

alter table FlowDet
   add constraint FK_FlowDet_PriceListMstr1 foreign key (PriceListFrom)
      references PriceListMstr (Code)
go

alter table FlowDet
   add constraint FK_FlowDet_PriceListMstr2 foreign key (PriceListTo)
      references PriceListMstr (Code)
go

alter table FlowDet
   add constraint FK_FlowDet_Uom foreign key (Uom)
      references Uom (Code)
go

alter table FlowDet
   add constraint FK_ItemFlowDet_BomMstr foreign key (Bom)
      references BomMstr (Code)
go

alter table FlowDet
   add constraint FK_ItemFlowDet_Item foreign key (Item)
      references Item (Code)
go

alter table FlowDet
   add constraint FK_ItemFlowDet_Location foreign key (LocFrom)
      references Location (Code)
go

alter table FlowDet
   add constraint FK_ItemFlowDet_Location1 foreign key (LocTo)
      references Location (Code)
go

alter table FlowDet
   add constraint FK_ItemFlowDet_PartyAddr foreign key (BillFrom)
      references PartyAddr (Code)
go

alter table FlowDet
   add constraint FK_ItemFlowDet_PartyAddr1 foreign key (BillTo)
      references PartyAddr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_ACC_User_CreateUser foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_ACC_User_LastModifyUser foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_Currency foreign key (Currency)
      references Currency (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_LocationFrom foreign key (LocFrom)
      references Location (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_LocationTo foreign key (LocTo)
      references Location (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyAddr_BillFrom foreign key (BillFrom)
      references PartyAddr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyAddr_BillTo foreign key (BillTo)
      references PartyAddr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyAddr_CarrierBillAddr foreign key (CarrierBillAddr)
      references PartyAddr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyAddr_ShipFrom foreign key (ShipFrom)
      references PartyAddr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyAddr_ShipTo foreign key (ShipTo)
      references PartyAddr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyFrom foreign key (PartyFrom)
      references Party (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PartyTo foreign key (PartyTo)
      references Party (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_Party_Carrier foreign key (Carrier)
      references Party (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PriceListFrom foreign key (PriceListFrom)
      references PriceListMstr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_PriceListTo foreign key (PriceListTo)
      references PriceListMstr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_RefFlowMstr foreign key (RefFlow)
      references FlowMstr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_ReturnRouting foreign key (ReturnRouting)
      references RoutingMstr (Code)
go

alter table FlowMstr
   add constraint FK_FlowMstr_Routing foreign key (Routing)
      references RoutingMstr (Code)
go

alter table HuDet
   add constraint FK_HuDet_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table HuDet
   add constraint FK_HuDet_HuDet foreign key (ParentHuId)
      references HuDet (HuId)
go

alter table HuDet
   add constraint FK_HuDet_Item foreign key (Item)
      references Item (Code)
go

alter table HuDet
   add constraint FK_HuDet_Location foreign key (Location)
      references Location (Code)
go

alter table HuDet
   add constraint FK_HuDet_Party foreign key (ManufactureParty)
      references Party (Code)
go

alter table HuDet
   add constraint FK_HuDet_StorageBin foreign key (Bin)
      references StorageBin (Code)
go

alter table HuDet
   add constraint FK_HuDet_Uom foreign key (Uom)
      references Uom (Code)
go

alter table HuOdd
   add constraint FK_HuOdd_ACC_CreateUser foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table HuOdd
   add constraint FK_HuOdd_ACC_LastModifyUser foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table HuOdd
   add constraint FK_HuOdd_LocationLotDet foreign key (LocLotDetId)
      references LocationLotDet (Id)
go

alter table HuOdd
   add constraint FK_HuOdd_OrderDet foreign key (OrderDetId)
      references OrderDet (Id)
go

alter table InspectDet
   add constraint FK_InspectDet_InspectMstr foreign key (InspNo)
      references InspectMstr (InspNo)
go

alter table InspectDet
   add constraint FK_InspectDet_Item foreign key (FGCode)
      references Item (Code)
go

alter table InspectDet
   add constraint FK_InspectDet_LocationLotDet foreign key (LocLotDetId)
      references LocationLotDet (Id)
go

alter table InspectDet
   add constraint FK_InspectDet_Location_From foreign key (LocFrom)
      references Location (Code)
go

alter table InspectDet
   add constraint FK_InspectDet_Location_To foreign key (LocTo)
      references Location (Code)
go

alter table InspectMstr
   add constraint FK_InspectMstr_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table InspectMstr
   add constraint FK_InspectMstr_ACC_User1 foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table InspectMstr
   add constraint FK_InspectMstr_Party foreign key (Region)
      references Party (Code)
go

alter table IpDet
   add constraint FK_AsnDet_OrderLocTrans foreign key (OrderLocTransId)
      references OrderLocTrans (Id)
go

alter table IpDet
   add constraint FK_IpDet_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table IpDet
   add constraint FK_IpDet_IpMstr foreign key (IpNo)
      references IpMstr (IpNo)
go

alter table IpDet
   add constraint FK_IpDet_Item foreign key (Item)
      references Item (Code)
go

alter table IpDet
   add constraint FK_IpDet_Location_From foreign key (LocFrom)
      references Location (Code)
go

alter table IpDet
   add constraint FK_IpDet_Location_To foreign key (LocTo)
      references Location (Code)
go

alter table IpDet
   add constraint FK_IpDet_PlanBill foreign key (PlanBillId)
      references PlanBill (Id)
go

alter table IpDet
   add constraint FK_IpDet_Uom foreign key (Uom)
      references Uom (Code)
go

alter table IpMstr
   add constraint FK_AsnMstr_ACC_User_CreateUser foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table IpMstr
   add constraint FK_AsnMstr_ACC_User_LastModifyDate foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table IpMstr
   add constraint FK_IpMstr_PartyAddr_ShipFrom foreign key (ShipFrom)
      references PartyAddr (Code)
go

alter table IpMstr
   add constraint FK_IpMstr_PartyAddr_ShipTo foreign key (ShipTo)
      references PartyAddr (Code)
go

alter table IpMstr
   add constraint FK_IpMstr_PartyFrom foreign key (PartyFrom)
      references Party (Code)
go

alter table IpMstr
   add constraint FK_IpMstr_PartyTo foreign key (PartyTo)
      references Party (Code)
go

alter table IpMstr
   add constraint FK_IpMstr_ReceiptMstr foreign key (GapRecNo)
      references ReceiptMstr (RecNo)
go

alter table IpTrack
   add constraint FK_IpTrack_ACC_User foreign key (ActUser)
      references ACC_User (USR_Code)
go

alter table IpTrack
   add constraint FK_IpTrack_IpMstr foreign key (IpNo)
      references IpMstr (IpNo)
go

alter table IpTrack
   add constraint FK_IpTrack_WorkCenter foreign key (WorkCenter)
      references WorkCenter (Code)
go

alter table Item
   add constraint FK_Item_ACC_User foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table Item
   add constraint FK_Item_BomMstr foreign key (Bom)
      references BomMstr (Code)
go

alter table Item
   add constraint FK_Item_Location foreign key (Location)
      references Location (Code)
go

alter table Item
   add constraint FK_Item_RoutingMstr foreign key (Routing)
      references RoutingMstr (Code)
go

alter table Item
   add constraint FK_Item_UnitOfMeasure foreign key (Uom)
      references Uom (Code)
go

alter table ItemFlowPlanDet
   add constraint FK_ItemFlowPlanDet_ItemFlowPlanMstr foreign key (ItemFlowPlanId)
      references ItemFlowPlanMstr (Id)
         on delete cascade
go

alter table ItemFlowPlanMstr
   add constraint FK_FlowPlanMstr_FlowMstr foreign key (Flow)
      references FlowMstr (Code)
         on delete cascade
go

alter table ItemFlowPlanMstr
   add constraint FK_ItemFlowPlanMstr_FlowDet foreign key (FlowDetId)
      references FlowDet (Id)
         on delete cascade
go

alter table ItemFlowPlanTrack
   add constraint FK_ItemFlowPlanTrack_OrderLocTrans foreign key (RefOrderLocTransId)
      references OrderLocTrans (Id)
go

alter table ItemKit
   add constraint FK_ItemKit_Item_Child foreign key (ChildItem)
      references Item (Code)
go

alter table ItemKit
   add constraint FK_ItemKit_Item_Parent foreign key (ParentItem)
      references Item (Code)
go

alter table ItemRef
   add constraint FK_ItemRef_Item foreign key (Item)
      references Item (Code)
go

alter table ItemRef
   add constraint FK_ItemRef_Party foreign key (Party)
      references Party (Code)
go

alter table Location
   add constraint FK_Location_Location foreign key (ActLocation)
      references Location (Code)
go

alter table Location
   add constraint FK_Location_Region foreign key (Region)
      references Party (Code)
go

alter table LocationLotDet
   add constraint FK_LocationLotDet_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table LocationLotDet
   add constraint FK_LocationLotDet_Item foreign key (Item)
      references Item (Code)
go

alter table LocationLotDet
   add constraint FK_LocationLotDet_Location foreign key (Location)
      references Location (Code)
go

alter table LocationLotDet
   add constraint FK_LocationLotDet_PlanBill foreign key (PlanBillId)
      references PlanBill (Id)
go

alter table LocationLotDet
   add constraint FK_LocationLotDet_StorageBin foreign key (Bin)
      references StorageBin (Code)
go

alter table MiscOrderDet
   add constraint FK_MiscOrderDet_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table MiscOrderDet
   add constraint FK_MiscOrderDet_Item foreign key (Item)
      references Item (Code)
go

alter table MiscOrderDet
   add constraint FK_MiscOrderDet_MiscOrderMstr foreign key (OrderNo)
      references MiscOrderMstr (OrderNo)
go

alter table MiscOrderMstr
   add constraint FK_MiscOrderMstr_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table MiscOrderMstr
   add constraint FK_MiscOrderMstr_Location foreign key (Location)
      references Location (Code)
go

alter table NamedQuery
   add constraint FK_NamedQuery_ACC_User foreign key (UserCode)
      references ACC_User (USR_Code)
go

alter table OrderBinding
   add constraint FK_OrderBinding_FlowMstr foreign key (BindFlow)
      references FlowMstr (Code)
go

alter table OrderBinding
   add constraint FK_OrderBinding_Order foreign key (OrderNo)
      references OrderMstr (OrderNo)
go

alter table OrderBinding
   add constraint FK_OrderBinding_Order_BindOrder foreign key (BindOrderNo)
      references OrderMstr (OrderNo)
go

alter table OrderDet
   add constraint FK_OrderDet_BomMstr foreign key (Bom)
      references BomMstr (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_Location_From foreign key (LocFrom)
      references Location (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_Location_To foreign key (LocTo)
      references Location (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_OrderMstr foreign key (OrderNo)
      references OrderMstr (OrderNo)
go

alter table OrderDet
   add constraint FK_OrderDet_Party foreign key (Customer)
      references Party (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_PartyAddr_BillFrom foreign key (BillFrom)
      references PartyAddr (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_PartyAddr_BillTo foreign key (BillTo)
      references PartyAddr (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_PriceListDetFrom foreign key (PriceListDetFrom)
      references PriceListDet (Id)
go

alter table OrderDet
   add constraint FK_OrderDet_PriceListDetTo foreign key (PriceListDetTo)
      references PriceListDet (Id)
go

alter table OrderDet
   add constraint FK_OrderDet_PriceListMstr_From foreign key (PriceListFrom)
      references PriceListMstr (Code)
go

alter table OrderDet
   add constraint FK_OrderDet_PriceListMstr_To foreign key (PriceListTo)
      references PriceListMstr (Code)
go

alter table OrderDet
   add constraint FK_OrderItem_Item foreign key (Item)
      references Item (Code)
go

alter table OrderDet
   add constraint FK_OrderItem_UnitOfMeasure foreign key (Uom)
      references Uom (Code)
go

alter table OrderLocTrans
   add constraint FK_OrderLocTrans_BomDet foreign key (BomDet)
      references BomDet (Id)
go

alter table OrderLocTrans
   add constraint FK_OrderLocTrans_Item foreign key (Item)
      references Item (Code)
go

alter table OrderLocTrans
   add constraint FK_OrderLocTrans_Location foreign key (Loc)
      references Location (Code)
go

alter table OrderLocTrans
   add constraint FK_OrderLocTrans_OrderDet foreign key (OrderDetId)
      references OrderDet (Id)
go

alter table OrderLocTrans
   add constraint FK_OrderLocTrans_RejLocation foreign key (RejLoc)
      references Location (Code)
go

alter table OrderLocTrans
   add constraint FK_OrderLocTrans_Uom foreign key (Uom)
      references Uom (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ACC_User_CancelUser foreign key (CancelUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ACC_User_CloseUser foreign key (CloseUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ACC_User_CreateUser foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ACC_User_LastModifyUser foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ACC_User_ReleaseUser foreign key (ReleaseUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ACC_User_StartUser foreign key (StartUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_Currency foreign key (Currency)
      references Currency (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_Flow foreign key (Flow)
      references FlowMstr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_LocationFrom foreign key (LocFrom)
      references Location (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_LocationTo foreign key (LocTo)
      references Location (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_OrderMstr_CompleteUser foreign key (CompleteUser)
      references ACC_User (USR_Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyAddr_BillFrom foreign key (BillFrom)
      references PartyAddr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyAddr_BillTo foreign key (BillTo)
      references PartyAddr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyAddr_CarrierBillAddr foreign key (CarrierBillAddr)
      references PartyAddr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyAddr_ShipFrom foreign key (ShipFrom)
      references PartyAddr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyAddr_ShipTo foreign key (ShipTo)
      references PartyAddr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyFrom foreign key (PartyFrom)
      references Party (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PartyTo foreign key (PartyTo)
      references Party (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_Party_Carrier foreign key (Carrier)
      references Party (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PriceListFrom foreign key (PriceListFrom)
      references PriceListMstr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_PriceListTo foreign key (PriceListTo)
      references PriceListMstr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_Routing foreign key (Routing)
      references RoutingMstr (Code)
go

alter table OrderMstr
   add constraint FK_OrderMstr_ShiftMstr foreign key (Shift)
      references ShiftMstr (Code)
go

alter table OrderOp
   add constraint FK_OrderOp_OrderMstr foreign key (OrderNo)
      references OrderMstr (OrderNo)
go

alter table OrderOp
   add constraint FK_OrderOp_WorkCenter foreign key (WorkCenter)
      references WorkCenter (Code)
go

alter table Party
   add constraint FK_Party_Party foreign key (ParentCode)
      references Party (Code)
go

alter table PartyAddr
   add constraint FK_PartyAddr_Party foreign key (PartyCode)
      references Party (Code)
go

alter table PickListDet
   add constraint FK_PackListDet_Item foreign key (Item)
      references Item (Code)
go

alter table PickListDet
   add constraint FK_PackListDet_Location foreign key (Location)
      references Location (Code)
go

alter table PickListDet
   add constraint FK_PackListDet_PackListMstr foreign key (PLNo)
      references PickListMstr (PLNo)
go

alter table PickListDet
   add constraint FK_PackListDet_StorageArea foreign key (Area)
      references StorageArea (Code)
go

alter table PickListDet
   add constraint FK_PackListDet_StorageBin foreign key (Bin)
      references StorageBin (Code)
go

alter table PickListDet
   add constraint FK_PickListDet_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table PickListDet
   add constraint FK_PickListDet_OrderLocTrans foreign key (OrderLocTransId)
      references OrderLocTrans (Id)
go

alter table PickListMstr
   add constraint FK_PackListMstr_ACC_User_CreateUser foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table PickListMstr
   add constraint FK_PackListMstr_ACC_User_LastModifyUser foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table PickListMstr
   add constraint FK_PickListMstr_ACC_User foreign key (StartUser)
      references ACC_User (USR_Code)
go

alter table PickListMstr
   add constraint FK_PickListMstr_FlowMstr foreign key (Flow)
      references FlowMstr (Code)
go

alter table PickListMstr
   add constraint FK_PickListMstr_PartyAddr_ShipFrom foreign key (ShipFrom)
      references PartyAddr (Code)
go

alter table PickListMstr
   add constraint FK_PickListMstr_PartyAddr_ShipTo foreign key (ShipTo)
      references PartyAddr (Code)
go

alter table PickListMstr
   add constraint FK_PickListMstr_PartyFrom foreign key (PartyFrom)
      references Party (Code)
go

alter table PickListMstr
   add constraint FK_PickListMstr_PartyTo foreign key (PartyTo)
      references Party (Code)
go

alter table PickListResult
   add constraint FK_PackListResult_PackListDet foreign key (PLDetId)
      references PickListDet (Id)
go

alter table PickListResult
   add constraint FK_PickListResult_LocationLotDet foreign key (LocLotDetId)
      references LocationLotDet (Id)
go

alter table PlanBill
   add constraint FK_PlanBill_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table PlanBill
   add constraint FK_PlanBill_ACC_User1 foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table PlanBill
   add constraint FK_PlanBill_Currency foreign key (Currency)
      references Currency (Code)
go

alter table PlanBill
   add constraint FK_PlanBill_HuDet foreign key (HuId)
      references HuDet (HuId)
go

alter table PlanBill
   add constraint FK_PlanBill_Item foreign key (Item)
      references Item (Code)
go

alter table PlanBill
   add constraint FK_PlanBill_OrderMstr foreign key (OrderNo)
      references OrderMstr (OrderNo)
go

alter table PlanBill
   add constraint FK_PlanBill_PartyAddr foreign key (BillAddr)
      references PartyAddr (Code)
go

alter table PlanBill
   add constraint FK_PlanBill_PriceListMstr foreign key (PriceList)
      references PriceListMstr (Code)
go

alter table PlanBill
   add constraint FK_PlanBill_ReceiptMstr foreign key (RecNo)
      references ReceiptMstr (RecNo)
go

alter table PlanBill
   add constraint FK_PlanBill_Uom foreign key (Uom)
      references Uom (Code)
go

alter table PriceListDet
   add constraint FK_PriceListDet_Currency foreign key (Currency)
      references Currency (Code)
go

alter table PriceListDet
   add constraint FK_PriceListDet_Item foreign key (Item)
      references Item (Code)
go

alter table PriceListDet
   add constraint FK_PriceListDet_PriceList foreign key (PriceList)
      references PriceListMstr (Code)
go

alter table PriceListDet
   add constraint FK_PriceListDet_Uom foreign key (Uom)
      references Uom (Code)
go

alter table PriceListMstr
   add constraint FK_PriceListMstr_Party foreign key (Party)
      references Party (Code)
go

alter table ProdLineIp
   add constraint FK_ProdLineIp_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table ProdLineIp
   add constraint FK_ProdLineIp_ACC_User1 foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table ProdLineIp
   add constraint FK_ProdLineIp_FlowMstr foreign key (ProdLine)
      references FlowMstr (Code)
go

alter table ProdLineIp
   add constraint FK_ProdLineIp_Item foreign key (Item)
      references Item (Code)
go

alter table ProdLineIp
   add constraint FK_ProdLineIp_Location foreign key (LocFrom)
      references Location (Code)
go

alter table ProdLineIp
   add constraint FK_ProdLineIp_PlanBill foreign key (PlanBillId)
      references PlanBill (Id)
go

alter table ReceiptDet
   add constraint FK_ReceiptDet_OrderLocTrans foreign key (OrderLocTransId)
      references OrderLocTrans (Id)
go

alter table ReceiptDet
   add constraint FK_ReceiptDet_PlanBill foreign key (PlanBillId)
      references PlanBill (Id)
go

alter table ReceiptDet
   add constraint FK_ReceiptDet_ReceiptMstr foreign key (RecNo)
      references ReceiptMstr (RecNo)
go

alter table ReceiptIp
   add constraint FK_ReceiptIp_IpMstr foreign key (IpNo)
      references IpMstr (IpNo)
go

alter table ReceiptIp
   add constraint FK_ReceiptIp_ReceiptMstr foreign key (RecNo)
      references ReceiptMstr (RecNo)
go

alter table ReceiptMstr
   add constraint FK_ReceiptMstr_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table ReceiptMstr
   add constraint FK_ReceiptMstr_PartyAddr_ShipFrom foreign key (ShipFrom)
      references PartyAddr (Code)
go

alter table ReceiptMstr
   add constraint FK_ReceiptMstr_PartyAddr_ShipTo foreign key (ShipTo)
      references PartyAddr (Code)
go

alter table ReceiptMstr
   add constraint FK_ReceiptMstr_PartyFrom foreign key (PartyFrom)
      references Party (Code)
go

alter table ReceiptMstr
   add constraint FK_ReceiptMstr_PartyTo foreign key (PartyTo)
      references Party (Code)
go

alter table RepackDet
   add constraint FK_RepackDet_LocationLotDet foreign key (LocLotDetId)
      references LocationLotDet (Id)
go

alter table RepackDet
   add constraint FK_RepackDet_RepackMstr foreign key (RepNo)
      references RepackMstr (RepNo)
go

alter table RepackMstr
   add constraint FK_RepackMstr_ACC_User foreign key (CreateUser)
      references ACC_User (USR_Code)
go

alter table RollingPlanDet
   add constraint FK_RollingPlanDet_Item foreign key (Item)
      references Item (Code)
go

alter table RollingPlanDet
   add constraint FK_RollingPlanDet_RollingPlanMstr foreign key (MstrId)
      references RollingPlanMstr (Id)
go

alter table RollingPlanMstr
   add constraint FK_RollingPlanMstr_FileUpload foreign key (FileUploadId)
      references FileUpload (FileUploadId)
go

alter table RollingPlanMstr
   add constraint FK_RollingPlanMstr_FlowMstr foreign key (Flow)
      references FlowMstr (Code)
go

alter table RoutingDet
   add constraint FK_RoutingDet_Location_From foreign key (LocFrom)
      references Location (Code)
go

alter table RoutingDet
   add constraint FK_RoutingDet_Routing foreign key (Routing)
      references RoutingMstr (Code)
go

alter table RoutingDet
   add constraint FK_RoutingDet_WorkCenter foreign key (WorkCenter)
      references WorkCenter (Code)
go

alter table RoutingMstr
   add constraint FK_Routing_Region foreign key (Region)
      references Party (Code)
go

alter table ShiftDet
   add constraint FK_ShiftDet_ShiftMstr foreign key (Shift)
      references ShiftMstr (Code)
go

alter table ShiftPlanSchedule
   add constraint FK_ShiftPlanSchedule_ACC_User foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table ShiftPlanSchedule
   add constraint FK_ShiftPlanSchedule_FlowDet foreign key (FlowDetId)
      references FlowDet (Id)
go

alter table SpecialTime
   add constraint FK_SpecialTime_Region foreign key (Region)
      references Party (Code)
go

alter table SpecialTime
   add constraint FK_SpecialTime_WorkCenter foreign key (WorkCenter)
      references WorkCenter (Code)
go

alter table StorageArea
   add constraint FK_StorageArea_Location foreign key (Location)
      references Location (Code)
go

alter table StorageBin
   add constraint FK_StorageBin_StorageArea foreign key (Area)
      references StorageArea (Code)
go

alter table UomConv
   add constraint FK_UomConv_AltUom foreign key (AltUom)
      references Uom (Code)
go

alter table UomConv
   add constraint FK_UomConv_BaseUom foreign key (BaseUom)
      references Uom (Code)
go

alter table UomConv
   add constraint FK_UomConv_Item foreign key (Item)
      references Item (Code)
go

alter table WorkCenter
   add constraint FK_WorkCenter_Party foreign key (Party)
      references Party (Code)
go

alter table Workday
   add constraint FK_Workday_Region foreign key (Region)
      references Party (Code)
go

alter table Workday
   add constraint FK_Workday_WorkCenter foreign key (WorkCenter)
      references WorkCenter (Code)
go

alter table WorkdayShift
   add constraint FK_WorkdayShift_Workday foreign key (WorkdayId)
      references Workday (Id)
go

alter table WorkingHours
   add constraint FK_WorkingHours_ACC_User foreign key (LastModifyUser)
      references ACC_User (USR_Code)
go

alter table WorkingHours
   add constraint FK_WorkingHours_Employee foreign key (Employee)
      references Employee (Code)
go

alter table WorkingHours
   add constraint FK_WorkingHours_ReceiptMstr foreign key (RecNo)
      references ReceiptMstr (RecNo)
go


create procedure GetNextSequence @CodePrefix varchar(50),
 	@NextSequence int OUTPUT as
Begin Tran
	Declare @invValue int;
	select  @invValue = IntValue FROM NumCtrl WITH (UPDLOCK, ROWLOCK) where Code = @CodePrefix;
	if @invValue is null
	begin
		if @NextSequence is not null
		begin 
			insert into NumCtrl(Code, IntValue) values (@CodePrefix, @NextSequence + 1);
		end	
		else
		begin
			set @NextSequence = 1;
			insert into NumCtrl(Code, IntValue) values (@CodePrefix, 2);
		end
	end 
	else
	begin
		if @NextSequence is not null
		begin 
			if @invValue <= @NextSequence
			begin
				update NumCtrl set IntValue = @NextSequence + 1 where Code = @CodePrefix;
			end
		end
		else
		begin
			set @NextSequence = @invValue + 1;
			update NumCtrl set IntValue = @NextSequence where Code = @CodePrefix;
		end
	end	
Commit tran
go

