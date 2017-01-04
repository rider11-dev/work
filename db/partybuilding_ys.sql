/*
Navicat MySQL Data Transfer

Source Server         : mysql_local
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : partybuilding_ys

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2017-01-04 17:59:03
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for auth_group
-- ----------------------------
DROP TABLE IF EXISTS `auth_group`;
CREATE TABLE `auth_group` (
  `gp_id` varchar(32) NOT NULL COMMENT '组织id',
  `gp_code` varchar(40) NOT NULL COMMENT '组织编号，唯一',
  `gp_name` varchar(40) NOT NULL COMMENT '组织名称',
  `gp_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统',
  `gp_parent` varchar(32) DEFAULT NULL COMMENT '上级组织编号',
  `gp_sort` varchar(10) NOT NULL DEFAULT '10' COMMENT '排序号',
  PRIMARY KEY (`gp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of auth_group
-- ----------------------------
INSERT INTO `auth_group` VALUES ('313cb71fa40b4c12b05ff34b89c9a652', 'cx_sljz', '曹县孙老家镇党组织', '\0', 'cxxwzzb', '0103');
INSERT INTO `auth_group` VALUES ('4d299198b4d0416eb9deebeaadbaf365', 'cxwjzxlc', '曹县王集镇徐楼村党组织', '\0', 'cx_wagnjizhen', '010201');
INSERT INTO `auth_group` VALUES ('4d93696b61cf4ea5acbda0a6e7122c89', 'cxccbsc', '曹城办事处党组织', '\0', 'cxxwzzb', '0101');
INSERT INTO `auth_group` VALUES ('6953306b35ab47f8a040dbd1d8893b13', 'cxxwzzb', '曹县县委组织部', '\0', null, '01');
INSERT INTO `auth_group` VALUES ('69a15308cc55479bb94e97b5c5ed376c', 'sys', '系统管理', '', '', '3123');
INSERT INTO `auth_group` VALUES ('7b82c887950c491fa0fd2122dfe623d6', 'cx_wagnjizhen', '曹县王集镇党组织', '\0', 'cxxwzzb', '0102');
INSERT INTO `auth_group` VALUES ('admin', 'admin', '超级管理员', '', null, '00');

-- ----------------------------
-- Table structure for auth_permission
-- ----------------------------
DROP TABLE IF EXISTS `auth_permission`;
CREATE TABLE `auth_permission` (
  `per_id` varchar(32) NOT NULL DEFAULT '',
  `per_code` varchar(40) NOT NULL COMMENT '权限编号',
  `per_name` varchar(40) NOT NULL COMMENT '权限名称',
  `per_type` varchar(20) NOT NULL COMMENT '权限类别:Func、Opt',
  `per_parent` varchar(32) DEFAULT NULL COMMENT '上级权限code',
  `per_sort` varchar(10) NOT NULL DEFAULT '10' COMMENT '排序',
  `per_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统预制',
  `per_remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `per_uri` varchar(255) DEFAULT '' COMMENT '功能对应uri',
  `per_method` varchar(255) DEFAULT NULL COMMENT '操作对应方法名称',
  `per_icon` varchar(255) DEFAULT NULL COMMENT '功能菜单图标资源文件uri',
  `per_halign` varchar(255) DEFAULT NULL COMMENT '功能菜单水平居？',
  PRIMARY KEY (`per_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='权限控制——权限列表';

-- ----------------------------
-- Records of auth_permission
-- ----------------------------
INSERT INTO `auth_permission` VALUES ('061f1473a00411e6a0a014dda9275f65', 'func_party_org_actplace', '组织活动场所管理', 'Func', 'func_party_org', '1009', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/ActivityPlacePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('1046a79cc87e4b4399263b138241edfb', 'opt_cq_table_edit', '修改表', 'Opt', 'func_cq_table', '220102', '', null, null, 'Edit', null, null);
INSERT INTO `auth_permission` VALUES ('15077f0d1fa340819f041905516a9c57', 'func_party_org_orgstruct', '组织架构', 'Func', 'func_party_org', '1001', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/OrgStructPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('1b83bc5a188c44aebbb47d67d79420a2', 'func_cq_table', '查询表管理', 'Func', 'func_cq', '2201', '', null, '/MyNet.CustomQuery.Client;component/Pages/Base/TablesMngPage.xaml', null, null, null);
INSERT INTO `auth_permission` VALUES ('1d9195a7a00411e6a0a014dda9275f65', 'func_party_org_query', '查询统计', 'Func', 'func_party_org', '1010', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/QueryPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('1d999f9625ec403b84bbe9eefa2be065', 'func_party_learn', '党建学习', 'Func', null, '13', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_learn.png', 'Center');
INSERT INTO `auth_permission` VALUES ('31c2dafb78f24accbe8a24e714aaf160', 'opt_cq_field_init', '初始化', 'Opt', 'func_cq_field', '220204', '', null, null, 'Init', null, null);
INSERT INTO `auth_permission` VALUES ('3828bf62a00311e6a0a014dda9275f65', 'func_party_org_2new', '两新组织', 'Func', 'func_party_org', '1002', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/Org2NewPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('468f84c5a00511e6a0a014dda9275f65', 'func_party_daily_taskrec', '我的任务', 'Func', 'func_party_daily', '1102', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/TaskReceivePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('499d06a121bc45c3bf22ab2b3a15e2e1', 'opt_cq_table_init', '初始化', 'Opt', 'func_cq_table', '220104', '', '初始化查询表及查询字段', null, 'Init', null, null);
INSERT INTO `auth_permission` VALUES ('50c8ccbf9853454080aa80c6feab88fc', 'func_party_sys_eval_proj', '考核项目设置', 'Func', 'func_party_eval', '1205', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Evaluation/EvaluateProjectPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('54bd6fd248d345eeab8cb51ed844aa3a', 'func_party_sys_eval_projassign', '考核项目分配', 'Func', 'func_party_eval', '1206', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Evaluation/EvalProjAssignPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('5a6203d3a00511e6a0a014dda9275f65', 'func_party_daily_notice', '通知管理', 'Func', 'func_party_daily', '1103', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/NoticePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('5c3f2bd9a00b11e6a0a014dda9275f65', 'func_party_sys_learn_channel', '栏目设置', 'Func', 'func_party_learn', '1305', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Learn/ChannelSetPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('63d9d648a00311e6a0a014dda9275f65', 'func_party_org_mem', '党员管理', 'Func', 'func_party_org', '1003', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartyMemberPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('65d42e4bed0c406d9b6a135cda7adc90', 'opt_cq_field_add', '新增字段', 'Opt', 'func_cq_field', '220201', '', null, null, 'Add', null, null);
INSERT INTO `auth_permission` VALUES ('676e985f4e3c4d53b6930c79ec13845b', 'test', '测试', 'Func', null, '100', '\0', null, null, null, null, null);
INSERT INTO `auth_permission` VALUES ('69872d59a00511e6a0a014dda9275f65', 'func_party_daily_inforelease', '信息发布', 'Func', 'func_party_daily', '1104', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/InfoReleasePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('7392abf3a00b11e6a0a014dda9275f65', 'func_party_sys_learn_article', '文章发布', 'Func', 'func_party_learn', '1306', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Learn/ArticlesPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('760de1580c9d48608e94da6324f35933', 'func_party_daily_taskdisp', '任务派遣', 'Func', 'func_party_daily', '1101', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/TaskDispatchPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('7668a42fa00311e6a0a014dda9275f65', 'func_party_org_memaddbook', '党员通讯录', 'Func', 'func_party_org', '1004', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartymemAddrBookPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('809ef779a9ff43bf902a9f2d3ef6b207', 'func_party_daily', '日常管理', 'Func', null, '11', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_daily.png', 'Center');
INSERT INTO `auth_permission` VALUES ('81944bfca00511e6a0a014dda9275f65', 'func_party_daily_partyactrecord', '党内组织生活', 'Func', 'func_party_daily', '1105', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/PartyActRecordPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('9044474dae97439893c1a769202aee70', 'test_1', '测试1', 'Func', 'test', '10001', '\0', 'asfdf', 'uri', 'action', null, null);
INSERT INTO `auth_permission` VALUES ('9101a0ff08c14d93af6debba00789da4', 'opt_cq_table_del', '删除表', 'Opt', 'func_cq_table', '220103', '', null, null, 'Delete', null, null);
INSERT INTO `auth_permission` VALUES ('91e3bcd4a00311e6a0a014dda9275f65', 'func_party_org_memdues', '党费管理', 'Func', 'func_party_org', '1005', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartyMemDuesPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ac72bb5ba00311e6a0a014dda9275f65', 'func_party_org_villagecadres', '村干部管理', 'Func', 'func_party_org', '1006', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/VillageCadresPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('afb342a647e24f3987679f8eab6914f8', 'opt_cq_table_add', '新增表', 'Opt', 'func_cq_table', '220101', '', null, null, 'Add', null, null);
INSERT INTO `auth_permission` VALUES ('b236bc1414a44f5c882080c7f28b418c', 'func_cq_query', '查询执行', 'Func', 'func_cq', '2203', '', null, '/MyNet.CustomQuery.Client;component/Pages/ExecQueryPage.xaml', null, null, null);
INSERT INTO `auth_permission` VALUES ('b393d5078e9e451dace2d369769d46b9', 'func_party_eval', '考核管理', 'Func', null, '12', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_evaluation.png', 'Center');
INSERT INTO `auth_permission` VALUES ('b3da249ba00911e6a0a014dda9275f65', 'func_party_learn_cleangov', '廉政建设', 'Func', 'func_party_learn', '1301', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('b876985564374a15947ee344e102501d', 'opt_cq_field_edit', '修改字段', 'Opt', 'func_cq_field', '220202', '', null, null, 'Edit', null, null);
INSERT INTO `auth_permission` VALUES ('b8dd0e22258146a49c089f3cc3fcbbc2', 'opt_cq_field_del', '删除字段', 'Opt', 'func_cq_field', '220203', '', null, null, 'Delete', null, null);
INSERT INTO `auth_permission` VALUES ('bb058684485b47218e552c6347091b2e', 'func_cq', '自定义查询', 'Func', null, '22', '', null, null, null, '/MyNet.Client;component/Resources/img/auth.png', 'Center');
INSERT INTO `auth_permission` VALUES ('c3a2fc90a00811e6a0a014dda9275f65', 'func_party_eval_evaluate', '考核评价', 'Func', 'func_party_eval', '1202', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluatePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('c6df3d18a00911e6a0a014dda9275f65', 'func_party_learn_theory', '理论制度', 'Func', 'func_party_learn', '1302', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('cdabebee2c0f4dcc9520d7045e0e161f', 'func_party_org', '党组织管理', 'Func', null, '10', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_base.png', 'Center');
INSERT INTO `auth_permission` VALUES ('cdb3623e5fc14c1abcea94eeed2f1fb2', 'func_cq_field', '查询字段管理', 'Func', 'func_cq', '2202', '', null, '/MyNet.CustomQuery.Client;component/Pages/Base/FieldsMngPage.xaml', null, null, null);
INSERT INTO `auth_permission` VALUES ('d2afbb0da00311e6a0a014dda9275f65', 'func_party_org_collstuofficer', '大学生村官管理', 'Func', 'func_party_org', '1007', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/CollegeStuOfficerPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('d3f5a15359804094a6ed3073f1e76cf7', 'd', 'd', 'Func', 'test_1', '123', '\0', null, null, null, null, null);
INSERT INTO `auth_permission` VALUES ('d581b580a00911e6a0a014dda9275f65', 'func_party_learn_school', '网上党校', 'Func', 'func_party_learn', '1303', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('df2d7f48a00811e6a0a014dda9275f65', 'func_party_eval_evaldetail', '考核情况查询', 'Func', 'func_party_eval', '1203', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluateDetailPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ebadfada1d2c49408132f732b6d96b1e', 'func_party_eval_upload', '资料上传', 'Func', 'func_party_eval', '1201', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/FileuploadPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ecd4b83aa00311e6a0a014dda9275f65', 'func_party_org_firstsecretary', '第一书记管理', 'Func', 'func_party_org', '1008', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/FirstSecretaryPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ef3a914ca00811e6a0a014dda9275f65', 'func_party_eval_evalscore', '考核分数统计', 'Func', 'func_party_eval', '1204', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluateScorePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('f044577ea00911e6a0a014dda9275f65', 'func_party_learn_pubedu', '宣传教育', 'Func', 'func_party_learn', '1304', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_auth', 'func_auth', '权限管理', 'Func', '', '20', '', '', '', null, '/MyNet.Client;component/Resources/img/auth.png', 'Center');
INSERT INTO `auth_permission` VALUES ('func_auth_group', 'func_auth_group', '组织管理', 'Func', 'func_auth', '200', '', null, '/MyNet.Client;component/Pages/Auth/GroupMngPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_auth_per', 'func_auth_per', '权限管理', 'Func', 'func_auth', '202', '', '', '/MyNet.Client;component/Pages/Auth/PermissionMngPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_auth_usr', 'func_auth_usr', '用户管理', 'Func', 'func_auth', '201', '', '', '/MyNet.Client;component/Pages/Auth/UserMngPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_changepwd', 'func_changepwd', '密码修改', 'Func', 'func_myaccount', '302', '', 'asdfasdf', '/MyNet.Client;component/Pages/Account/ChangePwdPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_myaccount', 'func_myaccount', '我的账户', 'Func', '', '30', '', null, '', null, '/MyNet.Client;component/Resources/img/account.png', 'Center');
INSERT INTO `auth_permission` VALUES ('func_mydetail', 'func_mydetail', '我的信息', 'Func', 'func_myaccount', '301', '', null, '/MyNet.Client;component/Pages/Account/DetailPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_group_add', 'opt_group_add', '新增组织', 'Opt', 'func_auth_group', '20001', '', null, null, 'Add', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_group_del', 'opt_group_del', '删除组织', 'Opt', 'func_auth_group', '20003', '', null, null, 'Delete', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_group_edit', 'opt_group_edit', '修改组织', 'Opt', 'func_auth_group', '20002', '', null, null, 'Edit', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_myinfo_save', 'opt_myinfo_save', '保存我的信息', 'Opt', 'func_myinfo', '30101', '', '', '', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_per_add', 'opt_per_add', '新增权限', 'Opt', 'func_auth_per', '20201', '', '', '', 'Add', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_per_del', 'opt_per_del', '删除权限', 'Opt', 'func_auth_per', '20203', '', '', '', 'Delete', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_per_edit', 'opt_per_edit', '修改权限', 'Opt', 'func_auth_per', '20202', '', '', '', 'Edit', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_add', 'opt_usr_add', '新增用户', 'Opt', 'func_auth_usr', '20101', '', '', '', 'Add', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_assign_per', 'opt_usr_assign_per', '分配权限', 'Opt', 'func_auth_usr', '20104', '', '', '', 'Assign', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_del', 'opt_usr_del', '删除用户', 'Opt', 'func_auth_usr', '20103', '', '', '', 'Delete', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_edit', 'opt_usr_edit', '修改用户', 'Opt', 'func_auth_usr', '20102', '', '', '', 'Edit', null, 'Left');

-- ----------------------------
-- Table structure for auth_user
-- ----------------------------
DROP TABLE IF EXISTS `auth_user`;
CREATE TABLE `auth_user` (
  `user_id` varchar(32) NOT NULL,
  `user_name` varchar(10) NOT NULL,
  `user_pwd` varchar(60) NOT NULL,
  `user_idcard` varchar(18) NOT NULL COMMENT '身份证号',
  `user_truename` varchar(10) DEFAULT NULL COMMENT '真实姓名',
  `user_regioncode` varchar(10) NOT NULL COMMENT '区域编码',
  `user_group` varchar(32) DEFAULT NULL COMMENT '所属组织id',
  `user_creator` varchar(32) DEFAULT NULL COMMENT '创建人',
  `user_remark` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='权限控制——用户';

-- ----------------------------
-- Records of auth_user
-- ----------------------------
INSERT INTO `auth_user` VALUES ('41d70ea65a044acfb9f8d119358eaccd', 'cxxwzzb', '5cadbc6fecfdaa9903144a25f842a03f', '22222222222222222X', '张三【县委】', '3729', '6953306b35ab47f8a040dbd1d8893b13', 'admin', null);
INSERT INTO `auth_user` VALUES ('admin', 'admin', '0b4e7a0e5fe84ad35fb5f95b9ceeac79', '372924198708265138', '管理员', '372924', 'admin', null, '管理员dfasd\r\n继续加油！！！\r\n啊哈撒打发斯蒂');
INSERT INTO `auth_user` VALUES ('test', 'test', '0b4e7a0e5fe84ad35fb5f95b9ceeac79', '111111111111111111', '测试', '372924', '6953306b35ab47f8a040dbd1d8893b13', 'admin', '阿斯蒂芬');

-- ----------------------------
-- Table structure for auth_user_permission
-- ----------------------------
DROP TABLE IF EXISTS `auth_user_permission`;
CREATE TABLE `auth_user_permission` (
  `rel_id` varchar(32) NOT NULL,
  `rel_userid` varchar(32) NOT NULL,
  `rel_permissionid` varchar(32) NOT NULL,
  PRIMARY KEY (`rel_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='权限控制——用户权限关联表';

-- ----------------------------
-- Records of auth_user_permission
-- ----------------------------
INSERT INTO `auth_user_permission` VALUES ('074fff28bf504684ab67272eb03c9261', 'admin', 'func_auth');
INSERT INTO `auth_user_permission` VALUES ('09ed1b30399240a88e08aba14fc84620', 'a3656545383e40ae990c4113c61cb6bd', '5a6203d3a00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('0a50d9c84d6d427cab14b58df6b59308', 'a3656545383e40ae990c4113c61cb6bd', '3828bf62a00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('0a943e6d9bdc40ff853508bbbfa5e431', 'e4831f52908b4c3e90d50032d4ffb043', 'func_changepwd');
INSERT INTO `auth_user_permission` VALUES ('0b15492d624f4b4c8b9c6f60008699aa', 'test', 'func_mydetail');
INSERT INTO `auth_user_permission` VALUES ('0f37de158a884dbb8a1eefc3f0f8e4fb', 'admin', '54bd6fd248d345eeab8cb51ed844aa3a');
INSERT INTO `auth_user_permission` VALUES ('108a94c00be0430bb3ed29823ce8184c', 'admin', 'opt_per_edit');
INSERT INTO `auth_user_permission` VALUES ('1134536781714f04beea78ff13799c08', 'admin', '7668a42fa00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('1547ca64af734c808e4d0ce3616b12a3', 'admin', 'b3da249ba00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('1acbcd74299742bab4957d45c6c37ad2', 'admin', '760de1580c9d48608e94da6324f35933');
INSERT INTO `auth_user_permission` VALUES ('1c5addc0a33c41c29af4e2e1f6bfec52', 'admin', '91e3bcd4a00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('1ee2c2089ff94132ba2b07b4990e730b', 'admin', '7392abf3a00b11e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('1f3b5486b7ba4abaae961aa38c06767b', 'a3656545383e40ae990c4113c61cb6bd', 'f044577ea00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('1fa42a0b69014d6f9b0638899ccd60c2', 'a3656545383e40ae990c4113c61cb6bd', 'd2afbb0da00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('217ff9497fb14c728160bf883a3f81f6', 'test', 'func_myaccount');
INSERT INTO `auth_user_permission` VALUES ('21a5e48924744eba90a27eec23c4ef91', 'a3656545383e40ae990c4113c61cb6bd', 'b393d5078e9e451dace2d369769d46b9');
INSERT INTO `auth_user_permission` VALUES ('227879a3444743089f0a7c5df5f9feec', 'admin', '468f84c5a00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('23fb17765b3e477b87efd9da3382bb2b', 'a3656545383e40ae990c4113c61cb6bd', '1d9195a7a00411e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('248d4292779a44948e41c94c560bc2e1', 'admin', '65d42e4bed0c406d9b6a135cda7adc90');
INSERT INTO `auth_user_permission` VALUES ('28ec54f7611b4c608f792caf22738f20', 'admin', '3828bf62a00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('293375369d214a0d8a1f3c62d5910dc9', 'a3656545383e40ae990c4113c61cb6bd', '7392abf3a00b11e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('296a13ec77b641ccb6bff4ba1daf3317', 'admin', 'func_mydetail');
INSERT INTO `auth_user_permission` VALUES ('2aedb870ec74473785f02c3bc473f72c', 'admin', 'func_myaccount');
INSERT INTO `auth_user_permission` VALUES ('2ea7e6baf42a414eaf80d76ab1a0433e', 'e4831f52908b4c3e90d50032d4ffb043', 'func_myaccount');
INSERT INTO `auth_user_permission` VALUES ('2efbe825efcb4fad8243ca05abdaf3e8', 'admin', 'ac72bb5ba00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('2fd5c76a835b4e6fbc58e119f4d00f7c', 'admin', '1046a79cc87e4b4399263b138241edfb');
INSERT INTO `auth_user_permission` VALUES ('30607e5a0b8549ccb28f7fab0f413280', 'a3656545383e40ae990c4113c61cb6bd', '15077f0d1fa340819f041905516a9c57');
INSERT INTO `auth_user_permission` VALUES ('3292e121545f49ba98555361ff093cc5', 'a3656545383e40ae990c4113c61cb6bd', '468f84c5a00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('3301c7ff11a342808cdbfca12ffd0695', 'a3656545383e40ae990c4113c61cb6bd', '5c3f2bd9a00b11e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('3371ef8719b2426ba9e359e9845a150b', 'a3656545383e40ae990c4113c61cb6bd', 'func_changepwd');
INSERT INTO `auth_user_permission` VALUES ('36034108f2084bbf8ced8cca1269719f', 'admin', '9101a0ff08c14d93af6debba00789da4');
INSERT INTO `auth_user_permission` VALUES ('361c750d18ec4f92ab47e87f800d9ba8', 'a3656545383e40ae990c4113c61cb6bd', 'c3a2fc90a00811e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('3bf92e8ad5c745a0984dbd84dcea3f7d', 'admin', 'df2d7f48a00811e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('3dc8e9cb4f49443fa47661306870951d', 'admin', '061f1473a00411e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('418bc62da08d4caba0a3806db871c1e3', 'a3656545383e40ae990c4113c61cb6bd', 'ef3a914ca00811e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('4480883cea9c41e4b00a1c371175169a', 'admin', 'ecd4b83aa00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('461ec88820374bb584120292bfff56b4', 'admin', 'opt_per_del');
INSERT INTO `auth_user_permission` VALUES ('468bf6279fe647e8b26a9e239cfb46e3', 'admin', 'opt_usr_assign_per');
INSERT INTO `auth_user_permission` VALUES ('47937fecf58e49839ced703e7a481660', 'admin', 'bb058684485b47218e552c6347091b2e');
INSERT INTO `auth_user_permission` VALUES ('4df3598d0f564a389778f976c9567dce', 'admin', 'afb342a647e24f3987679f8eab6914f8');
INSERT INTO `auth_user_permission` VALUES ('4e43dd86516444b5897347fa97270d15', 'admin', 'b236bc1414a44f5c882080c7f28b418c');
INSERT INTO `auth_user_permission` VALUES ('504c46a2b297430aba9abe760ddbe8b1', 'admin', 'opt_group_del');
INSERT INTO `auth_user_permission` VALUES ('553e38d4de0f47ce97b753effe8acf8c', 'admin', 'ef3a914ca00811e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('56d17cf90009425d9c1f5eb7e00c9216', 'a3656545383e40ae990c4113c61cb6bd', '7668a42fa00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('598ee2026d2c4ee7833acfe7ddbf8108', 'a3656545383e40ae990c4113c61cb6bd', '81944bfca00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('6015f50c30d64ee3abc155c28d9a3591', 'admin', 'opt_usr_edit');
INSERT INTO `auth_user_permission` VALUES ('607cc7ab5a7d41b2842722dc1c9691e5', 'a3656545383e40ae990c4113c61cb6bd', '54bd6fd248d345eeab8cb51ed844aa3a');
INSERT INTO `auth_user_permission` VALUES ('61941a1c977f466594c036a14a3d60ba', 'admin', 'c6df3d18a00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('6587430741c14e82ad526d029bebde2f', 'admin', '50c8ccbf9853454080aa80c6feab88fc');
INSERT INTO `auth_user_permission` VALUES ('6b15cebd86be40c9ace6a88cf1dec151', 'a3656545383e40ae990c4113c61cb6bd', 'ecd4b83aa00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('6fc67cab9cdd4057b4f21478f0ce52a6', 'admin', '1d999f9625ec403b84bbe9eefa2be065');
INSERT INTO `auth_user_permission` VALUES ('70236564381c43bbb3c12ece7f9e41b5', 'a3656545383e40ae990c4113c61cb6bd', 'func_myaccount');
INSERT INTO `auth_user_permission` VALUES ('7539c547e6534bd9bbd73d92bae3cb1e', 'a3656545383e40ae990c4113c61cb6bd', '1d999f9625ec403b84bbe9eefa2be065');
INSERT INTO `auth_user_permission` VALUES ('79dddc49bfd24af2abbe0969b8e30d43', 'admin', '81944bfca00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('7aaf6347504e49318af9ce14d24d7b1b', 'admin', '499d06a121bc45c3bf22ab2b3a15e2e1');
INSERT INTO `auth_user_permission` VALUES ('835077f8f89544cc98ea4284575b0993', 'admin', 'opt_per_add');
INSERT INTO `auth_user_permission` VALUES ('87638a802b6d482b8196f8ddedb474c5', 'a3656545383e40ae990c4113c61cb6bd', 'df2d7f48a00811e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('89fa5b6d7fc84f4db9a24af796041ad4', 'admin', 'func_auth_usr');
INSERT INTO `auth_user_permission` VALUES ('8a412b8752ca438fa002e47651bc95af', 'admin', '1d9195a7a00411e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('8bf4f0712faf457f87632c35adf2428e', 'admin', '31c2dafb78f24accbe8a24e714aaf160');
INSERT INTO `auth_user_permission` VALUES ('8c0f2547c6b8414fbf83b8894c225d4c', 'test', 'func_changepwd');
INSERT INTO `auth_user_permission` VALUES ('8e1ef049c396492abbf737e74668467a', 'admin', 'f044577ea00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('9065fdc05cc24d9a8d47d88338eb0644', 'admin', 'opt_group_add');
INSERT INTO `auth_user_permission` VALUES ('914ee48c562d490ea9e29be3466ee5d5', 'admin', 'opt_group_edit');
INSERT INTO `auth_user_permission` VALUES ('95945a56fd7b4c9487e8dc9470514bfc', 'admin', '5c3f2bd9a00b11e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('967b5eed1c52431288f3c61ffcb4fca5', 'admin', 'func_auth_group');
INSERT INTO `auth_user_permission` VALUES ('97109ec33ad0424f85ab4189ef1c8c1f', 'admin', 'd2afbb0da00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('98c0734458424d35a289f61467741bd2', 'admin', '63d9d648a00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('99478692c30e4d16b48daedf420c1bfb', 'e4831f52908b4c3e90d50032d4ffb043', 'func_mydetail');
INSERT INTO `auth_user_permission` VALUES ('9cd5ac43a7d046fd88d2b87690e92e7f', 'a3656545383e40ae990c4113c61cb6bd', 'func_mydetail');
INSERT INTO `auth_user_permission` VALUES ('9ea12fe41f324c38afadec12d616da0d', 'a3656545383e40ae990c4113c61cb6bd', 'ebadfada1d2c49408132f732b6d96b1e');
INSERT INTO `auth_user_permission` VALUES ('9f5438d0573c4c98ae1580336d1afe55', 'admin', 'cdb3623e5fc14c1abcea94eeed2f1fb2');
INSERT INTO `auth_user_permission` VALUES ('a1084615852d4de5b5e8e820e5dc95ec', 'admin', 'b8dd0e22258146a49c089f3cc3fcbbc2');
INSERT INTO `auth_user_permission` VALUES ('a3376d436dd84c4789c51e824256b8de', 'a3656545383e40ae990c4113c61cb6bd', '061f1473a00411e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('a338ac8c61a44b53963970ef81ba29ae', 'a3656545383e40ae990c4113c61cb6bd', '91e3bcd4a00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('a4204453d1184afaa1d7dacf505d1022', 'admin', 'cdabebee2c0f4dcc9520d7045e0e161f');
INSERT INTO `auth_user_permission` VALUES ('a4922359d04c48fea5fa3b4212c676da', 'a3656545383e40ae990c4113c61cb6bd', '809ef779a9ff43bf902a9f2d3ef6b207');
INSERT INTO `auth_user_permission` VALUES ('a9227da939a74d8ca51a9d7e5d59b763', 'a3656545383e40ae990c4113c61cb6bd', 'cdabebee2c0f4dcc9520d7045e0e161f');
INSERT INTO `auth_user_permission` VALUES ('a9a07bdd5f764bb29ae247941cd71e44', 'admin', 'func_changepwd');
INSERT INTO `auth_user_permission` VALUES ('ad107b96699e4319a5d8abef417360c8', 'admin', 'c3a2fc90a00811e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('afa660b333a04285a6f64a2f48457f1a', 'admin', 'opt_usr_del');
INSERT INTO `auth_user_permission` VALUES ('b4f293eb3faf495880fd2c2ddd5d25ab', 'admin', '1b83bc5a188c44aebbb47d67d79420a2');
INSERT INTO `auth_user_permission` VALUES ('b7fd43d482704e57b48ca2e9f399e74e', 'a3656545383e40ae990c4113c61cb6bd', '63d9d648a00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('bb1432f8544348f0a42e5ce3d6ce3d47', 'a3656545383e40ae990c4113c61cb6bd', '760de1580c9d48608e94da6324f35933');
INSERT INTO `auth_user_permission` VALUES ('c1cbb69574154d4c804eacc4db5b95c9', 'a3656545383e40ae990c4113c61cb6bd', '69872d59a00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('c368abe8453547bcb138558df2268e16', 'admin', 'func_auth_per');
INSERT INTO `auth_user_permission` VALUES ('c57c6f96b3e54e99863127998c6ab652', 'a3656545383e40ae990c4113c61cb6bd', '50c8ccbf9853454080aa80c6feab88fc');
INSERT INTO `auth_user_permission` VALUES ('c62825444846419a952335e850d35b29', 'a3656545383e40ae990c4113c61cb6bd', 'd581b580a00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('c8cd4dd89b92493eb480a95c86c75277', 'admin', '809ef779a9ff43bf902a9f2d3ef6b207');
INSERT INTO `auth_user_permission` VALUES ('d06f1182da0a48b59347dd596283636a', 'admin', 'opt_usr_add');
INSERT INTO `auth_user_permission` VALUES ('d141333b234648b6b0eb6b81137b22ad', 'a3656545383e40ae990c4113c61cb6bd', 'ac72bb5ba00311e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('d1737d9147a54c899f841c2c00cb4aa7', 'admin', 'b393d5078e9e451dace2d369769d46b9');
INSERT INTO `auth_user_permission` VALUES ('d727218dc451446282a76d3fc7af7a24', 'admin', '5a6203d3a00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('deae2cf2458b4536828deb93e2ee9347', 'a3656545383e40ae990c4113c61cb6bd', 'c6df3d18a00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('df8068218dd848dea879970b1d2e7449', 'admin', 'b876985564374a15947ee344e102501d');
INSERT INTO `auth_user_permission` VALUES ('e03c7d74a89f4f8bbc6ef8350f37df48', 'admin', '69872d59a00511e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('e9f0c270eb2146a6b7400497827f0639', 'admin', 'ebadfada1d2c49408132f732b6d96b1e');
INSERT INTO `auth_user_permission` VALUES ('ef68b3d77951446eb81dd9a7cf6b6682', 'a3656545383e40ae990c4113c61cb6bd', 'b3da249ba00911e6a0a014dda9275f65');
INSERT INTO `auth_user_permission` VALUES ('f68f5b4181064f6480a82edfc0b2acda', 'admin', '15077f0d1fa340819f041905516a9c57');
INSERT INTO `auth_user_permission` VALUES ('fb592b3eb4674eacb25fe6d2b3c54251', 'admin', 'd581b580a00911e6a0a014dda9275f65');

-- ----------------------------
-- Table structure for base_dict
-- ----------------------------
DROP TABLE IF EXISTS `base_dict`;
CREATE TABLE `base_dict` (
  `dict_id` varchar(32) NOT NULL,
  `dict_code` varchar(40) NOT NULL COMMENT '字典编号，相同类型下，编号不能为空',
  `dict_name` varchar(20) NOT NULL COMMENT '字典名称',
  `dict_type` varchar(40) NOT NULL COMMENT '字典类型',
  `dict_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统预制(系统预制的不允许删除）',
  `dict_default` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否默认值',
  `dict_order` int(10) NOT NULL DEFAULT '0' COMMENT '排序号',
  PRIMARY KEY (`dict_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of base_dict
-- ----------------------------
INSERT INTO `base_dict` VALUES ('cardstate_loss', 'Loss', '挂失', 'card.cardstate', '', '\0', '2');
INSERT INTO `base_dict` VALUES ('cardstate_normal', 'Normal', '正常', 'card.cardstate', '', '', '1');
INSERT INTO `base_dict` VALUES ('cardstate_off', 'Off', '注销', 'card.cardstate', '', '\0', '3');
INSERT INTO `base_dict` VALUES ('fieldtype_boolean', 'Boolean', '布尔类型', 'query.fieldtype', '', '\0', '5');
INSERT INTO `base_dict` VALUES ('fieldtype_date', 'Date', '日期类型', 'query.fieldtype', '', '\0', '3');
INSERT INTO `base_dict` VALUES ('fieldtype_number', 'Number', '数值类型', 'query.fieldtype', '', '\0', '2');
INSERT INTO `base_dict` VALUES ('fieldtype_string', 'String', '字符类型', 'query.fieldtype', '', '', '1');
INSERT INTO `base_dict` VALUES ('fieldtype_time', 'Time', '时间类型', 'query.fieldtype', '', '\0', '4');
INSERT INTO `base_dict` VALUES ('partyorg_dw', 'DW', '党委', 'pb.org', '', '', '1');
INSERT INTO `base_dict` VALUES ('partyorg_dzb', 'DZB', '党支部', 'pb.org', '', '\0', '5');
INSERT INTO `base_dict` VALUES ('partyorg_dzzb', 'DZZB', '党总支部', 'pb.org', '', '\0', '4');
INSERT INTO `base_dict` VALUES ('partyorg_jcdw', 'JCDW', '基层党委', 'pb.org', '', '\0', '3');
INSERT INTO `base_dict` VALUES ('partyorg_jgdw', 'JGDW', '机关党委', 'pb.org', '', '\0', '2');
INSERT INTO `base_dict` VALUES ('permtype_func', 'Func', '功能权限', 'auth.permtype', '', '', '1');
INSERT INTO `base_dict` VALUES ('permtype_opt', 'Opt', '操作权限', 'auth.permtype', '', '\0', '2');

-- ----------------------------
-- Table structure for base_dict_type
-- ----------------------------
DROP TABLE IF EXISTS `base_dict_type`;
CREATE TABLE `base_dict_type` (
  `type_code` varchar(40) NOT NULL COMMENT '类型编号',
  `type_name` varchar(20) DEFAULT NULL COMMENT '类型名称',
  `type_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统预制',
  PRIMARY KEY (`type_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of base_dict_type
-- ----------------------------
INSERT INTO `base_dict_type` VALUES ('auth.permtype', '权限类型', '');
INSERT INTO `base_dict_type` VALUES ('card.cardstate', '一卡通状态', '');
INSERT INTO `base_dict_type` VALUES ('pb.org', '党组织类型', '');
INSERT INTO `base_dict_type` VALUES ('query.fieldtype', '查询字段类型', '');

-- ----------------------------
-- Table structure for party_area
-- ----------------------------
DROP TABLE IF EXISTS `party_area`;
CREATE TABLE `party_area` (
  `id` varchar(40) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `town` varchar(255) DEFAULT NULL,
  `village` varchar(255) DEFAULT NULL,
  `floor_area` varchar(255) DEFAULT NULL,
  `courtyard_area` varchar(255) DEFAULT NULL,
  `levels` varchar(255) DEFAULT NULL,
  `rooms` varchar(255) DEFAULT NULL,
  `location` varchar(255) DEFAULT NULL,
  `gps` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of party_area
-- ----------------------------
INSERT INTO `party_area` VALUES ('064B4E05B1BD8403235D5948F7274066', '曹城石庄', '曹城街道办事处', '石庄', '120', '140', '2', '2', '曹城街道办事处石庄', null);
INSERT INTO `party_area` VALUES ('146DF94996AC6BDDD4AF6DA11E99B43C', '曹城北街', '曹城街道办事处', '北街', '90', '91', '1', '1', '曹城街道办事处北街', null);
INSERT INTO `party_area` VALUES ('1545766bf6674114be87b14dc6ebc536', '徐楼村活动室', '王集镇', '徐楼村', '100', '120', '1', '1', '王集镇徐楼村', '');
INSERT INTO `party_area` VALUES ('35636ED005DC1F62C42EBDB4920C3E90\r\n\r\n', '邵庄活动室', '孙老家镇', '邵庄', '120', '130', '4', '10', '孙老家镇邵庄村', null);
INSERT INTO `party_area` VALUES ('469ec38305be4078a4e650ed9ad9741e', '谷庄村活动室', '王集镇', '谷庄村', '80', '110', '2', '1', '王集镇谷庄村', '');
INSERT INTO `party_area` VALUES ('61AA01B1202E943183E0F35861C98837\r\n\r\n', '曹城西城', '曹城街道办事处', '西城', '79', '88', '1', '1', '曹城街道办事处西城', null);
INSERT INTO `party_area` VALUES ('6f877692fca14aaa88ed7d2f52dadf8f', '张店活动室', '王集镇', '张店村', '60', '70', '3', '2', '王集镇张店村', '');
INSERT INTO `party_area` VALUES ('765c0319bbda40c6ba9d8541554cf989', '白楼活动室', '孙老家镇', '白楼村', '65', '85', '1', '1', '孙老家镇白楼村', '');
INSERT INTO `party_area` VALUES ('7FECFFDCA677568FAF21D983EC6EA82F\r\n\r\n', '焦庄活动室', '孙老家镇', '焦庄', '99', '102', '1', '1', '孙老家镇焦庄村', null);
INSERT INTO `party_area` VALUES ('A00E0AABF7638205EAD0B7A0396B78CF\r\n\r\n', '曹城南关', '曹城街道办事处', '南关', '65', '78', '1', '1', '曹城街道办事处南关', null);
INSERT INTO `party_area` VALUES ('BF04158F5E99150598891CAA6B1587FD\r\n\r\n', '曹城八里庙', '曹城街道办事处', '八里庙', '66', '78', '1', '1', '曹城街道办事处八里庙', null);
INSERT INTO `party_area` VALUES ('C3B8608D4A09C401EEEB07A98FAB392A\r\n\r\n', '曹城马山庄', '曹城街道办事处', '马山庄', '130', '150', '1', '2', '曹城街道办事处马山庄', null);

-- ----------------------------
-- Table structure for party_info
-- ----------------------------
DROP TABLE IF EXISTS `party_info`;
CREATE TABLE `party_info` (
  `id` varchar(32) NOT NULL,
  `title` varchar(255) DEFAULT NULL,
  `content` varchar(1000) DEFAULT NULL,
  `issue_time` varchar(40) DEFAULT NULL,
  `party` varchar(255) DEFAULT NULL,
  `state` varchar(255) DEFAULT NULL,
  `read_state` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of party_info
-- ----------------------------
INSERT INTO `party_info` VALUES ('55066b57be704b54b12ad063846e46f2', '推动非公党建工作取得新突破', '前不久，中共中央办公厅下发的《关于加强和改进非公有制企业党的建设工作的意见（试行）》，明确了非公企业党建工作许多重大问题，集中体现了改革开放以来非公企业党建工作实践成果和重大理论创新。我们要进一步加强学习和研究，在实践中不断深化认识、加深理解、贯彻落实。', '2016-11-24 08:52:37', '曹县县委组织部', '已发布', '已读');
INSERT INTO `party_info` VALUES ('6dcf5421bdea4409942b89c522c8ca24', '曹县“党建+电商”带动群众脱贫致富', '曹县大集镇常庙村村民胡影，之前，他由于没有技术，在外打工收入甚微，自从前年在家开网店以来，收入一年比一年高，去年，他算算帐，20多万元稳拿手中。2013年7月，曹县探索建立了大集镇淘宝行业商会党支部，在发展壮大电子商务产业的同时，帮助更多的像胡影这样的贫困村民在家就能找到致富门路，全镇电子商务产值从2013年的2亿元，在2015年突破到12亿元。', '2016-11-24 08:52:52', '曹县县委组织部', '已发布', '已读');
INSERT INTO `party_info` VALUES ('99dfcd09bfd242e9a80694b9eac765ac', '充分发挥非公企业党组织实质作用', '近年来，我省各级党组织将非公企业党建工作摆在更加突出的位置来抓，坚持围绕打造基层服务型党组织，以“双强争先”活动为总载体，积极探索发挥党组织实质作用的有效方法和途径，取得了显著成效。面对新的形势和任务，我们要以推动实质作用发挥为着力点，进一步提升非公企业党建工作整体水平。', '2016-11-24 08:53:28', '曹县县委组织部', '已发布', '已读');
INSERT INTO `party_info` VALUES ('d54a2d57092a425bbc2f8b7244af97f9', '曹县一乡镇狠抓就业扶贫，解决就业人数8000余人', '凝心聚力抓好全民创业，精准施策助力就业扶贫。据悉，仅去年一年，曹县梁堤头镇新增规模以上企业6家，个体工商户839家，农民专业合作社60余家，发展电商网店400余家。今年第一季度，该镇新培植“一村一品”重点村6个，新增电商网店120余家，吸纳就业8000余人。', null, '曹县县委组织部', '编辑', '已读');
INSERT INTO `party_info` VALUES ('e4d8b9d8fe444233b19623043221dfec', 'asdf', 'asdf', '2016-11-24 15:45:25', '曹县县委组织部', '已发布', '已读');

-- ----------------------------
-- Table structure for party_org
-- ----------------------------
DROP TABLE IF EXISTS `party_org`;
CREATE TABLE `party_org` (
  `po_id` varchar(32) NOT NULL COMMENT '组织id，对应auth_group的gp_id',
  `po_type` varchar(20) DEFAULT NULL COMMENT '党组织类型',
  `po_chg_num` varchar(100) DEFAULT NULL COMMENT '换届文号',
  `po_chg_date` date DEFAULT NULL COMMENT '换届日期',
  `po_expire_date` date DEFAULT NULL COMMENT '任届期满日期',
  `po_chg_remind` bit(1) DEFAULT b'0' COMMENT '是否换届提醒',
  `po_mem_normal` int(255) DEFAULT '0' COMMENT '正式党员人数',
  `po_mem_potential` int(255) DEFAULT '0' COMMENT '发展党员人数',
  `po_mem_activists` int(255) DEFAULT '0' COMMENT '入党积极分子人数',
  `po_remark` varchar(255) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`po_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='党建——党组织详细信息';

-- ----------------------------
-- Records of party_org
-- ----------------------------
INSERT INTO `party_org` VALUES ('6953306b35ab47f8a040dbd1d8893b13', 'PartyOrgDZB', '曹县【20140101】', '2014-01-01', '2019-01-01', '', '0', '0', '0', 'asdfasdf');
INSERT INTO `party_org` VALUES ('admin', 'PartyOrgJGDW', 'ABDCDE', '2015-12-21', '2019-01-21', '', '10', '2', '2', '阿斯蒂芬');

-- ----------------------------
-- Table structure for party_task
-- ----------------------------
DROP TABLE IF EXISTS `party_task`;
CREATE TABLE `party_task` (
  `id` varchar(40) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `content` varchar(1000) DEFAULT NULL,
  `priority` varchar(40) DEFAULT NULL,
  `receiver` varchar(255) DEFAULT NULL,
  `issue_time` varchar(40) DEFAULT NULL,
  `expire_time` varchar(40) DEFAULT NULL,
  `progress` varchar(1000) DEFAULT NULL,
  `state` varchar(255) DEFAULT NULL,
  `complete_state` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of party_task
-- ----------------------------
INSERT INTO `party_task` VALUES ('441031032cbf4866b5abaf7dc55e5450', '尽快组织党员思想建设活', '请各级党组织尽快落实党员思想建设工作，并总结汇报成果，截止日期2016年12月10日', '高', '全部', '2016-11-24 08:57:45', '2016/12/10', '已完成任务，共开展党员思想建设课程3次，总计参与28人次', '已完成', '已完成');
INSERT INTO `party_task` VALUES ('7e2411cad9294728ab691f55b916b8e2', 'sdf', 'asdf', '中', '曹县县委组织部,曹城办事处党组织,曹县王集镇党组织,曹县孙老家镇党组织', '', '2016/12/3', null, '编辑', null);
INSERT INTO `party_task` VALUES ('7f31f885b47f4cac894cb4a4224ab7d9', 'zpf', '张鹏飞', '高', '曹县县委组织部,曹城办事处党组织,曹县王集镇党组织,曹县孙老家镇党组织', '2016-11-29 09:30:38', '2016/12/10', 'sdfgsdfg', '已完成', '已完成');
INSERT INTO `party_task` VALUES ('83b6f08386e3493dabce7876f480abc9', '活动场所信息采集', '请各级党组织尽快采集活动场所信息，并录入系统！', '高', '全部', '2016-11-24 11:06:07', '2016/12/10', '王集镇徐楼村已采集并上传', '已完成', '已完成');
INSERT INTO `party_task` VALUES ('8bbdeb51397c4383ae4110cdeb126123', 'aaa', 'asdfs', '中', 'aaa', '2016-11-24 15:46:08', '2016/11/24', null, '已发布', '未领');
INSERT INTO `party_task` VALUES ('98c80605b0a24373abe5b07c1172b24c', 'bbb', 'bbb', '低', 'bb', '2016-11-24 17:02:18', '', null, '已发布', '未领');

-- ----------------------------
-- Table structure for query_fields
-- ----------------------------
DROP TABLE IF EXISTS `query_fields`;
CREATE TABLE `query_fields` (
  `id` char(32) NOT NULL,
  `tbid` char(32) NOT NULL,
  `fieldname` varchar(20) NOT NULL,
  `displayname` varchar(40) DEFAULT NULL,
  `fieldtype` varchar(20) NOT NULL,
  `remark` varchar(255) DEFAULT NULL,
  `visible` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否可见',
  PRIMARY KEY (`id`),
  KEY `FK_Reference_fields_tables` (`tbid`),
  CONSTRAINT `FK_Reference_fields_tables` FOREIGN KEY (`tbid`) REFERENCES `query_tables` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='查询字段定义';

-- ----------------------------
-- Records of query_fields
-- ----------------------------
INSERT INTO `query_fields` VALUES ('200b37d9689f4ed1b6ccab510b845697', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_creator', '创建人', 'String', null, '');
INSERT INTO `query_fields` VALUES ('24f31c3ff25141e092acf2a858290808', 'eeaccd3ca12d400ab50f8ca6750aecc8', 'ag.gp_id', '组织id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('2a8ed6dcf644453faae4fc251c2da4f4', 'eeaccd3ca12d400ab50f8ca6750aecc8', 'ag.gp_parent', '上级组织', 'String', '编号', '');
INSERT INTO `query_fields` VALUES ('2bfc88cc968f4f188928ccd10373b755', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_method', '操作命令名称', 'String', null, '');
INSERT INTO `query_fields` VALUES ('31877b63167445cea4effe3d5331d924', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_order', '排序号', 'Number', null, '');
INSERT INTO `query_fields` VALUES ('31bee5d2a51f46118994f754e18586e0', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_parent', '上级权限', 'String', '编号', '');
INSERT INTO `query_fields` VALUES ('367f65a36b724e39b0bcddf35dba0318', 'eeaccd3ca12d400ab50f8ca6750aecc8', 'ag.gp_code', '组织编号', 'String', '唯一', '');
INSERT INTO `query_fields` VALUES ('45295a04969f4f3885702d9433efa004', '95669276b97f47b0891c8830e07c243a', 'aup.rel_userid', '用户id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('465a89a4076a4e7e8d18f77bd75e8f4e', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_id', '字典id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('5e6a3c79c0044ec3b488ecfa776b243e', '95669276b97f47b0891c8830e07c243a', 'aup.rel_permissionid', '权限id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('65b3209b9060441faef190cd762bc6a6', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_sort', '排序', 'String', null, '');
INSERT INTO `query_fields` VALUES ('670540e7b96f4b53ab79436127eb4fa5', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_idcard', '身份证号', 'String', null, '');
INSERT INTO `query_fields` VALUES ('6a1f19706aa24f4886db8570eaa42c65', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_code', '字典编号', 'String', '相同类型下，编号唯一', '');
INSERT INTO `query_fields` VALUES ('8094173d569e4dfdbad1aebd98225838', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_icon', '菜单图标', 'String', null, '');
INSERT INTO `query_fields` VALUES ('8ab9924bab52471a83c767fd1fb21ab5', 'eeaccd3ca12d400ab50f8ca6750aecc8', 'ag.gp_system', '是否系统', 'Boolean', null, '');
INSERT INTO `query_fields` VALUES ('8cf89927757d439ebe49d95016c03a23', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_type', '字典类型', 'String', null, '');
INSERT INTO `query_fields` VALUES ('955537dc677d4157b94876bd3c37df23', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_code', '权限编号', 'String', '唯一', '');
INSERT INTO `query_fields` VALUES ('995390f4a8b64dd6a61ba9c8562cba87', '95669276b97f47b0891c8830e07c243a', 'aup.rel_id', '权限关联id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('99caf922bbec4f71a90fee736c2d6d20', '002ecde479994729918edbd40cb1e459', 'bdt.type_code', '类型编号', 'String', null, '');
INSERT INTO `query_fields` VALUES ('99fdd23ad6e54df88557e8b57e7e6746', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_truename', '真实姓名', 'String', null, '');
INSERT INTO `query_fields` VALUES ('ab960445f8254cfd90ab3f959d997873', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_id', '权限id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('ae7323d993bc4104837d6a116bb5d2c8', '002ecde479994729918edbd40cb1e459', 'bdt.type_name', '类型名称', 'String', null, '');
INSERT INTO `query_fields` VALUES ('b4b98c4a870d4b12b91148d4d1530f55', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_halign', '菜单水平对齐', 'String', null, '');
INSERT INTO `query_fields` VALUES ('b80e2b34984d4e9db2fac38044f2b1e9', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_name', '用户名', 'String', null, '');
INSERT INTO `query_fields` VALUES ('bb934df9f635474db373ee2b8acff409', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_system', '是否系统', 'Boolean', null, '');
INSERT INTO `query_fields` VALUES ('c003c3be38ad497384d4722b436a9e57', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_uri', '功能菜单uri', 'String', null, '');
INSERT INTO `query_fields` VALUES ('c478f73157844ae39281bd86a0bbf036', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_remark', '备注', 'String', null, '');
INSERT INTO `query_fields` VALUES ('cdd32772b7d044cf9aa296f0e64f9e19', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_type', '权限类别', 'String', 'Func、Opt', '');
INSERT INTO `query_fields` VALUES ('cf77bdfcc31d4634917d395791dae50d', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_name', '权限名称', 'String', null, '');
INSERT INTO `query_fields` VALUES ('d8fa79ef8cdf4902841f71c7d6a07502', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_regioncode', '区域编码', 'String', null, '');
INSERT INTO `query_fields` VALUES ('d9114204f6464d7795c8c582e0e85f67', '8ff23fbcc00f4bd8b07020106a8bf0f2', 'ap.per_system', '是否系统', 'Boolean', null, '');
INSERT INTO `query_fields` VALUES ('d9fe63aadfa24ea39b10154f9dd5d3cb', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_remark', '备注', 'String', null, '');
INSERT INTO `query_fields` VALUES ('e80787b659bb48878e99983bfa3b0aee', 'eeaccd3ca12d400ab50f8ca6750aecc8', 'ag.gp_sort', '排序号', 'String', null, '');
INSERT INTO `query_fields` VALUES ('eb87fed800914884b944c2d863e9e61e', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_default', '是否默认', 'Boolean', null, '');
INSERT INTO `query_fields` VALUES ('f01cc2062b7d4b59842a882dd1968297', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_group', '所属组织', 'String', 'id', '');
INSERT INTO `query_fields` VALUES ('f235eb6b7e244ab48f60a9ff91fb96af', 'b9fcddb564d04658a9dd9f6b090c4477', 'au.user_id', '用户id', 'String', null, '\0');
INSERT INTO `query_fields` VALUES ('f4f6d8422f034b4a8f7dde3ad4fbf2e3', '002ecde479994729918edbd40cb1e459', 'bdt.type_system', '是否系统', 'Boolean', null, '');
INSERT INTO `query_fields` VALUES ('f9da91799457414b8613326ad6c73416', 'fa59224d9e0946fa9e1500516bfd4184', 'bd.dict_name', '字典名称', 'String', null, '');
INSERT INTO `query_fields` VALUES ('fbaf61f7e35e4a3a8ebcc0bc71142e80', 'eeaccd3ca12d400ab50f8ca6750aecc8', 'ag.gp_name', '组织名称', 'String', null, '');

-- ----------------------------
-- Table structure for query_tables
-- ----------------------------
DROP TABLE IF EXISTS `query_tables`;
CREATE TABLE `query_tables` (
  `id` char(32) NOT NULL,
  `tbname` varchar(100) NOT NULL,
  `alias` varchar(20) NOT NULL,
  `comment` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='查询表定义';

-- ----------------------------
-- Records of query_tables
-- ----------------------------
INSERT INTO `query_tables` VALUES ('002ecde479994729918edbd40cb1e459', 'base_dict_type', 'bdt', '字典类型');
INSERT INTO `query_tables` VALUES ('8ff23fbcc00f4bd8b07020106a8bf0f2', 'auth_permission', 'ap', '权限信息');
INSERT INTO `query_tables` VALUES ('95669276b97f47b0891c8830e07c243a', 'auth_user_permission', 'aup', '用户权限');
INSERT INTO `query_tables` VALUES ('b9fcddb564d04658a9dd9f6b090c4477', 'auth_user', 'au', '用户信息');
INSERT INTO `query_tables` VALUES ('eeaccd3ca12d400ab50f8ca6750aecc8', 'auth_group', 'ag', '组织信息');
INSERT INTO `query_tables` VALUES ('fa59224d9e0946fa9e1500516bfd4184', 'base_dict', 'bd', '字典信息');

-- ----------------------------
-- Procedure structure for sp_page_query
-- ----------------------------
DROP PROCEDURE IF EXISTS `sp_page_query`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_page_query`(  
    #输入参数  
    _fields VARCHAR(2000), #要查询的字段，用逗号(,)分隔  
    _tables TEXT,  #要查询的表  
    _where VARCHAR(2000),   #查询条件  
    _orderby VARCHAR(200),  #排序规则  
    _pageindex INT,  #查询页码  
    _pageSize INT,   #每页记录数  
    #输出参数  
    OUT _totalcount INT,  #总记录数  
    OUT _pagecount INT    #总页数  
)
BEGIN  
    #140529-xxj-分页存储过程  
    #计算起始行号  
    SET @startRow = _pageSize * (_pageIndex - 1);  
    SET @pageSize = _pageSize;  
    SET @rowindex = 0; #行号  
  
    #合并字符串  
    SET @strsql = CONCAT(  
        #'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号  
        'select sql_calc_found_rows '  
        ,_fields  
        ,' from '  
        ,_tables  
        ,CASE IFNULL(_where, '') WHEN '' THEN '' ELSE CONCAT(' where ', _where) END  
        ,CASE IFNULL(_orderby, '') WHEN '' THEN '' ELSE CONCAT(' order by ', _orderby) END  
      ,' limit '   
        ,@startRow  
        ,','   
        ,@pageSize  
    );  
  
    PREPARE strsql FROM @strsql;#定义预处理语句   
    EXECUTE strsql;                         #执行预处理语句   
    DEALLOCATE PREPARE strsql;  #删除定义   
    #通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数  
    SET _totalcount = FOUND_ROWS();  
  
    #计算总页数  
    IF (_totalcount <= _pageSize) THEN  
        SET _pagecount = 1;  
    ELSE IF (_totalcount % _pageSize > 0) THEN  
        SET _pagecount = _totalcount / _pageSize + 1;  
    ELSE  
        SET _pagecount = _totalcount / _pageSize;  
    END IF;  
    END IF;    
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for sp_test
-- ----------------------------
DROP PROCEDURE IF EXISTS `sp_test`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_test`(  
    Foo int, 
	  out Bar int
)
BEGIN
set Bar=Foo;
select 1 as A;
END
;;
DELIMITER ;
