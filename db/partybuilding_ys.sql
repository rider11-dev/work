/*
Navicat MySQL Data Transfer

Source Server         : mysql_local
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : partybuilding_ys

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2016-12-01 11:47:23
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
INSERT INTO `auth_group` VALUES ('4d93696b61cf4ea5acbda0a6e7122c89', 'cxccbsc', '曹城办事处党组织', '\0', 'cxxwzzb', '0101');
INSERT INTO `auth_group` VALUES ('6953306b35ab47f8a040dbd1d8893b13', 'cxxwzzb', '曹县县委组织部', '\0', null, '01');
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
INSERT INTO `auth_permission` VALUES ('061f1473a00411e6a0a014dda9275f65', 'func_party_org_actplace', '组织活动场所管理', 'PermTypeFunc', 'func_party_org', '1009', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/ActivityPlacePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('15077f0d1fa340819f041905516a9c57', 'func_party_org_orgstruct', '组织架构', 'PermTypeFunc', 'func_party_org', '1001', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/OrgStructPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('1d9195a7a00411e6a0a014dda9275f65', 'func_party_org_query', '查询统计', 'PermTypeFunc', 'func_party_org', '1010', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/QueryPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('1d999f9625ec403b84bbe9eefa2be065', 'func_party_learn', '党建学习', 'PermTypeFunc', null, '13', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_learn.png', 'Center');
INSERT INTO `auth_permission` VALUES ('3828bf62a00311e6a0a014dda9275f65', 'func_party_org_2new', '两新组织', 'PermTypeFunc', 'func_party_org', '1002', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/Org2NewPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('468f84c5a00511e6a0a014dda9275f65', 'func_party_daily_taskrec', '我的任务', 'PermTypeFunc', 'func_party_daily', '1102', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/TaskReceivePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('50c8ccbf9853454080aa80c6feab88fc', 'func_party_sys_eval_proj', '考核项目设置', 'PermTypeFunc', 'func_party_eval', '1205', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Evaluation/EvaluateProjectPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('54bd6fd248d345eeab8cb51ed844aa3a', 'func_party_sys_eval_projassign', '考核项目分配', 'PermTypeFunc', 'func_party_eval', '1206', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Evaluation/EvalProjAssignPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('5a6203d3a00511e6a0a014dda9275f65', 'func_party_daily_notice', '通知管理', 'PermTypeFunc', 'func_party_daily', '1103', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/NoticePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('5c3f2bd9a00b11e6a0a014dda9275f65', 'func_party_sys_learn_channel', '栏目设置', 'PermTypeFunc', 'func_party_learn', '1305', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Learn/ChannelSetPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('63d9d648a00311e6a0a014dda9275f65', 'func_party_org_mem', '党员管理', 'PermTypeFunc', 'func_party_org', '1003', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartyMemberPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('69872d59a00511e6a0a014dda9275f65', 'func_party_daily_inforelease', '信息发布', 'PermTypeFunc', 'func_party_daily', '1104', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/InfoReleasePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('7392abf3a00b11e6a0a014dda9275f65', 'func_party_sys_learn_article', '文章发布', 'PermTypeFunc', 'func_party_learn', '1306', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Learn/ArticlesPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('760de1580c9d48608e94da6324f35933', 'func_party_daily_taskdisp', '任务派遣', 'PermTypeFunc', 'func_party_daily', '1101', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/TaskDispatchPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('7668a42fa00311e6a0a014dda9275f65', 'func_party_org_memaddbook', '党员通讯录', 'PermTypeFunc', 'func_party_org', '1004', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartymemAddrBookPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('809ef779a9ff43bf902a9f2d3ef6b207', 'func_party_daily', '日常管理', 'PermTypeFunc', null, '11', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_daily.png', 'Center');
INSERT INTO `auth_permission` VALUES ('81944bfca00511e6a0a014dda9275f65', 'func_party_daily_partyactrecord', '党内组织生活', 'PermTypeFunc', 'func_party_daily', '1105', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/PartyActRecordPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('91e3bcd4a00311e6a0a014dda9275f65', 'func_party_org_memdues', '党费管理', 'PermTypeFunc', 'func_party_org', '1005', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartyMemDuesPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ac72bb5ba00311e6a0a014dda9275f65', 'func_party_org_villagecadres', '村干部管理', 'PermTypeFunc', 'func_party_org', '1006', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/VillageCadresPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('b393d5078e9e451dace2d369769d46b9', 'func_party_eval', '考核管理', 'PermTypeFunc', null, '12', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_evaluation.png', 'Center');
INSERT INTO `auth_permission` VALUES ('b3da249ba00911e6a0a014dda9275f65', 'func_party_learn_cleangov', '廉政建设', 'PermTypeFunc', 'func_party_learn', '1301', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('c3a2fc90a00811e6a0a014dda9275f65', 'func_party_eval_evaluate', '考核评价', 'PermTypeFunc', 'func_party_eval', '1202', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluatePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('c6df3d18a00911e6a0a014dda9275f65', 'func_party_learn_theory', '理论制度', 'PermTypeFunc', 'func_party_learn', '1302', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('cdabebee2c0f4dcc9520d7045e0e161f', 'func_party_org', '党组织管理', 'PermTypeFunc', null, '10', '', null, null, null, '/Biz.PartyBuilding.YS.Client;component/Resources/img/party_base.png', 'Center');
INSERT INTO `auth_permission` VALUES ('d2afbb0da00311e6a0a014dda9275f65', 'func_party_org_collstuofficer', '大学生村官管理', 'PermTypeFunc', 'func_party_org', '1007', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/CollegeStuOfficerPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('d581b580a00911e6a0a014dda9275f65', 'func_party_learn_school', '网上党校', 'PermTypeFunc', 'func_party_learn', '1303', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('df2d7f48a00811e6a0a014dda9275f65', 'func_party_eval_evaldetail', '考核情况查询', 'PermTypeFunc', 'func_party_eval', '1203', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluateDetailPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ebadfada1d2c49408132f732b6d96b1e', 'func_party_eval_upload', '资料上传', 'PermTypeFunc', 'func_party_eval', '1201', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/FileuploadPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ecd4b83aa00311e6a0a014dda9275f65', 'func_party_org_firstsecretary', '第一书记管理', 'PermTypeFunc', 'func_party_org', '1008', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/FirstSecretaryPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('ef3a914ca00811e6a0a014dda9275f65', 'func_party_eval_evalscore', '考核分数统计', 'PermTypeFunc', 'func_party_eval', '1204', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluateScorePage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('f044577ea00911e6a0a014dda9275f65', 'func_party_learn_pubedu', '宣传教育', 'PermTypeFunc', 'func_party_learn', '1304', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_auth', 'func_auth', '权限管理', 'PermTypeFunc', '', '20', '', '', '', null, '/MyNet.Client;component/Resources/img/auth.png', 'Center');
INSERT INTO `auth_permission` VALUES ('func_auth_group', 'func_auth_group', '组织管理', 'PermTypeFunc', 'func_auth', '200', '', null, '/MyNet.Client;component/Pages/Auth/GroupMngPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_auth_per', 'func_auth_per', '权限管理', 'PermTypeFunc', 'func_auth', '202', '', '', '/MyNet.Client;component/Pages/Auth/PermissionMngPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_auth_usr', 'func_auth_usr', '用户管理', 'PermTypeFunc', 'func_auth', '201', '', '', '/MyNet.Client;component/Pages/Auth/UserMngPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_changepwd', 'func_changepwd', '密码修改', 'PermTypeFunc', 'func_myaccount', '302', '', 'asdfasdf', '/MyNet.Client;component/Pages/Account/ChangePwdPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('func_myaccount', 'func_myaccount', '我的账户', 'PermTypeFunc', '', '30', '', null, '', null, '/MyNet.Client;component/Resources/img/account.png', 'Center');
INSERT INTO `auth_permission` VALUES ('func_mydetail', 'func_mydetail', '我的信息', 'PermTypeFunc', 'func_myaccount', '301', '', null, '/MyNet.Client;component/Pages/Account/DetailPage.xaml', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_changepwd_sav', 'opt_changepwd_sav', '保存密码', 'PermTypeOpt', 'func_changepwd', '30201', '', '', '', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_group_add', 'opt_group_add', '新增组织', 'PermTypeOpt', 'func_auth_group', '20001', '', null, null, 'Add', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_group_del', 'opt_group_del', '删除组织', 'PermTypeOpt', 'func_auth_group', '20003', '', null, null, 'Delete', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_group_edit', 'opt_group_edit', '修改组织', 'PermTypeOpt', 'func_auth_group', '20002', '', null, null, 'Edit', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_myinfo_save', 'opt_myinfo_save', '保存我的信息', 'PermTypeOpt', 'func_myinfo', '30101', '', '', '', null, null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_per_add', 'opt_per_add', '新增权限', 'PermTypeOpt', 'func_auth_per', '20201', '', '', '', 'Add', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_per_del', 'opt_per_del', '删除权限', 'PermTypeOpt', 'func_auth_per', '20203', '', '', '', 'Delete', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_per_edit', 'opt_per_edit', '修改权限', 'PermTypeOpt', 'func_auth_per', '20202', '', '', '', 'Edit', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_add', 'opt_usr_add', '新增用户', 'PermTypeOpt', 'func_auth_usr', '20101', '', '', '', 'Add', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_assign_per', 'opt_usr_assign_per', '分配权限', 'PermTypeOpt', 'func_auth_usr', '20104', '', '', '', 'Assign', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_del', 'opt_usr_del', '删除用户', 'PermTypeOpt', 'func_auth_usr', '20103', '', '', '', 'Delete', null, 'Left');
INSERT INTO `auth_permission` VALUES ('opt_usr_edit', 'opt_usr_edit', '修改用户', 'PermTypeOpt', 'func_auth_usr', '20102', '', '', '', 'Edit', null, 'Left');

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
INSERT INTO `auth_user` VALUES ('admin', 'admin', '0b4e7a0e5fe84ad35fb5f95b9ceeac79', '372924198708265138', '管理员', '372924', '', null, '管理员\r\n继续加油！！！');
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
INSERT INTO `auth_user_permission` VALUES ('0a943e6d9bdc40ff853508bbbfa5e431', 'e4831f52908b4c3e90d50032d4ffb043', 'func_changepwd');
INSERT INTO `auth_user_permission` VALUES ('0b15492d624f4b4c8b9c6f60008699aa', 'test', 'func_mydetail');
INSERT INTO `auth_user_permission` VALUES ('0bbb62d304d84d24a876c7b0ec5a94e1', 'test', 'opt_changepwd_sav');
INSERT INTO `auth_user_permission` VALUES ('202735c0bb7f47c789b6316bc48ad5f3', 'admin', '*');
INSERT INTO `auth_user_permission` VALUES ('217ff9497fb14c728160bf883a3f81f6', 'test', 'func_myaccount');
INSERT INTO `auth_user_permission` VALUES ('2ea7e6baf42a414eaf80d76ab1a0433e', 'e4831f52908b4c3e90d50032d4ffb043', 'func_myaccount');
INSERT INTO `auth_user_permission` VALUES ('4990a9bc9a1346ae85d0f84dcef17b8d', 'e4831f52908b4c3e90d50032d4ffb043', 'opt_changepwd_sav');
INSERT INTO `auth_user_permission` VALUES ('8c0f2547c6b8414fbf83b8894c225d4c', 'test', 'func_changepwd');
INSERT INTO `auth_user_permission` VALUES ('99478692c30e4d16b48daedf420c1bfb', 'e4831f52908b4c3e90d50032d4ffb043', 'func_mydetail');

-- ----------------------------
-- Table structure for base_dict
-- ----------------------------
DROP TABLE IF EXISTS `base_dict`;
CREATE TABLE `base_dict` (
  `dict_id` varchar(32) NOT NULL,
  `dict_code` varchar(40) NOT NULL COMMENT '字典编号，相同类型下，编号不能为空',
  `dict_name` varchar(20) NOT NULL COMMENT '字典名称',
  `dict_type` varchar(10) NOT NULL COMMENT '字典类型',
  `dict_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统预制(系统预制的不允许删除）',
  `dict_default` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否默认值',
  `dict_order` int(10) NOT NULL DEFAULT '0' COMMENT '排序号',
  PRIMARY KEY (`dict_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of base_dict
-- ----------------------------
INSERT INTO `base_dict` VALUES ('cardstate_loss', 'CardStateLoss', '挂失', 'cardstate', '', '\0', '2');
INSERT INTO `base_dict` VALUES ('cardstate_normal', 'CardStateNormal', '正常', 'cardstate', '', '', '1');
INSERT INTO `base_dict` VALUES ('cardstate_off', 'CardStateOff', '注销', 'cardstate', '', '\0', '3');
INSERT INTO `base_dict` VALUES ('partyorg_dw', 'PartyOrgDW', '党委', 'partyorg', '', '', '1');
INSERT INTO `base_dict` VALUES ('partyorg_dzb', 'PartyOrgDZB', '党支部', 'partyorg', '', '\0', '5');
INSERT INTO `base_dict` VALUES ('partyorg_dzzb', 'PartyOrgDZZB', '党总支部', 'partyorg', '', '\0', '4');
INSERT INTO `base_dict` VALUES ('partyorg_jcdw', 'PartyOrgJCDW', '基层党委', 'partyorg', '', '\0', '3');
INSERT INTO `base_dict` VALUES ('partyorg_jgdw', 'PartyOrgJGDW', '机关党委', 'partyorg', '', '\0', '2');
INSERT INTO `base_dict` VALUES ('permtype_func', 'PermTypeFunc', '功能权限', 'permtype', '', '', '1');
INSERT INTO `base_dict` VALUES ('permtype_opt', 'PermTypeOpt', '操作权限', 'permtype', '', '\0', '2');

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
INSERT INTO `base_dict_type` VALUES ('cardstate', '一卡通状态', '');
INSERT INTO `base_dict_type` VALUES ('partyorg', '党组织类型', '');
INSERT INTO `base_dict_type` VALUES ('permtype', '权限类型', '');

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
