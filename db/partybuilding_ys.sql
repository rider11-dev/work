/*
Navicat MySQL Data Transfer

Source Server         : mysql-local
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : partybuilding_ys

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2016-11-08 17:49:33
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
INSERT INTO `auth_group` VALUES ('4d93696b61cf4ea5acbda0a6e7122c89', 'cxccbsc', '曹城办事处党组织', '\0', 'cxxwzzb', '0101');
INSERT INTO `auth_group` VALUES ('6953306b35ab47f8a040dbd1d8893b13', 'cxxwzzb', '曹县县委组织部', '\0', null, '01');
INSERT INTO `auth_group` VALUES ('7b82c887950c491fa0fd2122dfe623d6', 'cx_wagnjizhen', '曹县王集镇党组织', '\0', 'cxxwzzb', '0102');
INSERT INTO `auth_group` VALUES ('admin', 'admin', '超级管理员', '', null, '00');
INSERT INTO `auth_group` VALUES ('ec5871d098054dc7be2520d01ab84b54', 'cc_msz', '曹城街道马山庄党组织', '\0', 'cxccbsc', '010101');

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
  PRIMARY KEY (`per_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='权限控制——权限列表';

-- ----------------------------
-- Records of auth_permission
-- ----------------------------
INSERT INTO `auth_permission` VALUES ('061f1473a00411e6a0a014dda9275f65', 'func_party_org_actplace', '组织活动场所管理', 'PermTypeFunc', 'func_party_org', '1009', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/ActivityPlacePage.xaml', null);
INSERT INTO `auth_permission` VALUES ('15077f0d1fa340819f041905516a9c57', 'func_party_org_orgstruct', '组织架构', 'PermTypeFunc', 'func_party_org', '1001', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/OrgStructPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('1d70ef2c51a54a08adb6f2dac18d9db9', 'func_party_sys', '系统设置', 'PermTypeFunc', null, '14', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('1d9195a7a00411e6a0a014dda9275f65', 'func_party_org_query', '查询统计', 'PermTypeFunc', 'func_party_org', '1010', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/QueryPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('1d999f9625ec403b84bbe9eefa2be065', 'func_party_learn', '党建学习', 'PermTypeFunc', null, '13', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('3828bf62a00311e6a0a014dda9275f65', 'func_party_org_2new', '两新组织', 'PermTypeFunc', 'func_party_org', '1002', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/Org2NewPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('468f84c5a00511e6a0a014dda9275f65', 'func_party_daily_taskrec', '我的任务', 'PermTypeFunc', 'func_party_daily', '1102', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/TaskReceivePage.xaml', null);
INSERT INTO `auth_permission` VALUES ('50c8ccbf9853454080aa80c6feab88fc', 'func_party_sys_eval_proj', '考核项目设置', 'PermTypeFunc', 'func_party_sys_eval', '140201', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Evaluation/EvaluateProjectPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('54bd6fd248d345eeab8cb51ed844aa3a', 'func_party_sys_eval_projassign', '考核项目分配', 'PermTypeFunc', 'func_party_sys_eval', '140202', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Evaluation/EvalProjAssignPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('5a6203d3a00511e6a0a014dda9275f65', 'func_party_daily_notice', '通知管理', 'PermTypeFunc', 'func_party_daily', '1103', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/NoticePage.xaml', null);
INSERT INTO `auth_permission` VALUES ('5c3f2bd9a00b11e6a0a014dda9275f65', 'func_party_sys_learn_channel', '栏目设置', 'PermTypeFunc', 'func_party_sys_learn', '140101', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Learn/ChannelSetPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('63d9d648a00311e6a0a014dda9275f65', 'func_party_org_mem', '党员管理', 'PermTypeFunc', 'func_party_org', '1003', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartyMemberPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('69872d59a00511e6a0a014dda9275f65', 'func_party_daily_inforelease', '信息发布', 'PermTypeFunc', 'func_party_daily', '1104', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/InfoReleasePage.xaml', null);
INSERT INTO `auth_permission` VALUES ('7392abf3a00b11e6a0a014dda9275f65', 'func_party_sys_learn_article', '文章发布', 'PermTypeFunc', 'func_party_sys_learn', '140102', '', null, '/Biz.PartyBuilding.YS.Client;component/Sys/Learn/ArticlesPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('760de1580c9d48608e94da6324f35933', 'func_party_daily_taskdisp', '任务派遣', 'PermTypeFunc', 'func_party_daily', '1101', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/TaskDispatchPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('7668a42fa00311e6a0a014dda9275f65', 'func_party_org_memaddbook', '党员通讯录', 'PermTypeFunc', 'func_party_org', '1004', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartymemAddrBookPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('809ef779a9ff43bf902a9f2d3ef6b207', 'func_party_daily', '日常管理', 'PermTypeFunc', null, '11', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('81944bfca00511e6a0a014dda9275f65', 'func_party_daily_partyactrecord', '党内组织生活', 'PermTypeFunc', 'func_party_daily', '1105', '', null, '/Biz.PartyBuilding.YS.Client;component/Daily/PartyActRecordPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('91e3bcd4a00311e6a0a014dda9275f65', 'func_party_org_memdues', '党费管理', 'PermTypeFunc', 'func_party_org', '1005', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/PartyMemDuesPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('9533b43759b649b4be2625986ec35331', 'func_party_sys_learn', '党建学习', 'PermTypeFunc', 'func_party_sys', '1401', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('ac72bb5ba00311e6a0a014dda9275f65', 'func_party_org_villagecadres', '村干部管理', 'PermTypeFunc', 'func_party_org', '1006', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/VillageCadresPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('b393d5078e9e451dace2d369769d46b9', 'func_party_eval', '考核管理', 'PermTypeFunc', null, '12', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('b3da249ba00911e6a0a014dda9275f65', 'func_party_learn_cleangov', '廉政建设', 'PermTypeFunc', 'func_party_learn', '1301', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('c3a2fc90a00811e6a0a014dda9275f65', 'func_party_eval_evaluate', '考核评价', 'PermTypeFunc', 'func_party_eval', '1202', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluatePage.xaml', null);
INSERT INTO `auth_permission` VALUES ('c6df3d18a00911e6a0a014dda9275f65', 'func_party_learn_theory', '理论制度', 'PermTypeFunc', 'func_party_learn', '1302', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('cdabebee2c0f4dcc9520d7045e0e161f', 'func_party_org', '党组织管理', 'PermTypeFunc', null, '10', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('d2afbb0da00311e6a0a014dda9275f65', 'func_party_org_collstuofficer', '大学生村官管理', 'PermTypeFunc', 'func_party_org', '1007', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/CollegeStuOfficerPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('d581b580a00911e6a0a014dda9275f65', 'func_party_learn_school', '网上党校', 'PermTypeFunc', 'func_party_learn', '1303', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('df2d7f48a00811e6a0a014dda9275f65', 'func_party_eval_evaldetail', '考核情况查询', 'PermTypeFunc', 'func_party_eval', '1203', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluateDetailPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('eba3d856a53e446385fefd26773c6f52', 'func_party_sys_eval', '考核评价', 'PermTypeFunc', 'func_party_sys', '1402', '', null, null, null);
INSERT INTO `auth_permission` VALUES ('ebadfada1d2c49408132f732b6d96b1e', 'func_party_eval_upload', '资料上传', 'PermTypeFunc', 'func_party_eval', '1201', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/FileuploadPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('ecd4b83aa00311e6a0a014dda9275f65', 'func_party_org_firstsecretary', '第一书记管理', 'PermTypeFunc', 'func_party_org', '1008', '', null, '/Biz.PartyBuilding.YS.Client;component/PartyOrg/FirstSecretaryPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('ef3a914ca00811e6a0a014dda9275f65', 'func_party_eval_evalscore', '考核分数统计', 'PermTypeFunc', 'func_party_eval', '1204', '', null, '/Biz.PartyBuilding.YS.Client;component/Evaluation/EvaluateScorePage.xaml', null);
INSERT INTO `auth_permission` VALUES ('f044577ea00911e6a0a014dda9275f65', 'func_party_learn_pubedu', '宣传教育', 'PermTypeFunc', 'func_party_learn', '1304', '', null, '/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_auth', 'func_auth', '权限管理', 'PermTypeFunc', '', '20', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('func_auth_group', 'func_auth_group', '组织管理', 'PermTypeFunc', 'func_auth', '200', '', null, '/MyNet.Client;component/Pages/Auth/GroupMngPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_auth_per', 'func_auth_per', '权限管理', 'PermTypeFunc', 'func_auth', '202', '', '', '/MyNet.Client;component/Pages/Auth/PermissionMngPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_auth_usr', 'func_auth_usr', '用户管理', 'PermTypeFunc', 'func_auth', '201', '', '', '/MyNet.Client;component/Pages/Auth/UserMngPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_changepwd', 'func_changepwd', '密码修改', 'PermTypeFunc', 'func_myaccount', '302', '', 'asdfasdf', '/MyNet.Client;component/Pages/Account/ChangePwdPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_myaccount', 'func_myaccount', '我的账户', 'PermTypeFunc', '', '30', '', null, '', null);
INSERT INTO `auth_permission` VALUES ('func_mydetail', 'func_mydetail', '我的信息', 'PermTypeFunc', 'func_myaccount', '301', '', null, '/MyNet.Client;component/Pages/Account/DetailPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('opt_changepwd_sav', 'opt_changepwd_sav', '保存密码', 'PermTypeOpt', 'func_changepwd', '30201', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('opt_group_add', 'opt_group_add', '新增组织', 'PermTypeOpt', 'func_auth_group', '20001', '', null, null, 'Add');
INSERT INTO `auth_permission` VALUES ('opt_group_del', 'opt_group_del', '删除组织', 'PermTypeOpt', 'func_auth_group', '20003', '', null, null, 'Delete');
INSERT INTO `auth_permission` VALUES ('opt_group_edit', 'opt_group_edit', '修改组织', 'PermTypeOpt', 'func_auth_group', '20002', '', null, null, 'Edit');
INSERT INTO `auth_permission` VALUES ('opt_myinfo_save', 'opt_myinfo_save', '保存我的信息', 'PermTypeOpt', 'func_myinfo', '30101', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('opt_per_add', 'opt_per_add', '新增权限', 'PermTypeOpt', 'func_auth_per', '20201', '', '', '', 'Add');
INSERT INTO `auth_permission` VALUES ('opt_per_del', 'opt_per_del', '删除权限', 'PermTypeOpt', 'func_auth_per', '20203', '', '', '', 'Delete');
INSERT INTO `auth_permission` VALUES ('opt_per_edit', 'opt_per_edit', '修改权限', 'PermTypeOpt', 'func_auth_per', '20202', '', '', '', 'Edit');
INSERT INTO `auth_permission` VALUES ('opt_usr_add', 'opt_usr_add', '新增用户', 'PermTypeOpt', 'func_auth_usr', '20101', '', '', '', 'Add');
INSERT INTO `auth_permission` VALUES ('opt_usr_assign_per', 'opt_usr_assign_per', '分配权限', 'PermTypeOpt', 'func_auth_usr', '20104', '', '', '', 'Assign');
INSERT INTO `auth_permission` VALUES ('opt_usr_del', 'opt_usr_del', '删除用户', 'PermTypeOpt', 'func_auth_usr', '20103', '', '', '', 'Delete');
INSERT INTO `auth_permission` VALUES ('opt_usr_edit', 'opt_usr_edit', '修改用户', 'PermTypeOpt', 'func_auth_usr', '20102', '', '', '', 'Edit');

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
