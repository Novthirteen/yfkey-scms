--�˵������ֶ�
alter table TPriceListDet add StartQty decimal(18,8) null,EndQty decimal(18,8) null,MinPrice decimal(18,8),MaxPrice decimal(18,8)

insert into codemstr values('PricingMethod','LadderStere',40,0,'����(������)')
update codemstr set desc1='����(����)' where code='PricingMethod' and codevalue='KG'
update codemstr set desc1='����������ף�' where code='PricingMethod' and codevalue='M3'
update codemstr set desc1='����' where code='PricingMethod' and codevalue='SHIPT'

