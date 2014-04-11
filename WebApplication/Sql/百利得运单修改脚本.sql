--运单增加字段
alter table TPriceListDet add StartQty decimal(18,8) null,EndQty decimal(18,8) null,MinPrice decimal(18,8),MaxPrice decimal(18,8)

insert into codemstr values('PricingMethod','LadderStere',40,0,'阶梯(立方米)')
update codemstr set desc1='重量(公斤)' where code='PricingMethod' and codevalue='KG'
update codemstr set desc1='体积（立方米）' where code='PricingMethod' and codevalue='M3'
update codemstr set desc1='包车' where code='PricingMethod' and codevalue='SHIPT'

