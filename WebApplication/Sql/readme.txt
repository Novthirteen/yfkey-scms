
1.初始化数据库,按照次顺序执行数据库脚本文件:

	sconittrunk20100812.sql

	data_user_role_userpre.sql

	data_permissioncategory.sql

	data_permission.sql

	data_menu.sql

	data_codemstr.sql

	data_entityopt.sql

	data_batch.sql

2.开发时候添加的字段请加入到上述脚本文件中,并且把增量放入 update.sql 中,并且包含初始值,例如:
	ALTER  TABLE   OrderMstr
			ADD	[TextField1] [varchar](255) NULL;
	UPDATE OrderMstr SET TextField1= '初始值';

