/*
Navicat MySQL Data Transfer

Source Server         : mysql-local
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : partybuilding

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2016-10-26 17:30:37
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for auth_group
-- ----------------------------
DROP TABLE IF EXISTS `auth_group`;
CREATE TABLE `auth_group` (
  `gp_id` varchar(32) NOT NULL COMMENT '组织id',
  `gp_code` varchar(20) NOT NULL COMMENT '组织编号，唯一',
  `gp_name` varchar(40) NOT NULL COMMENT '组织名称',
  `gp_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统',
  `gp_parent` varchar(32) DEFAULT NULL COMMENT '上级组织编号',
  `gp_sort` varchar(10) NOT NULL DEFAULT '10' COMMENT '排序号',
  PRIMARY KEY (`gp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of auth_group
-- ----------------------------
INSERT INTO `auth_group` VALUES ('a203b7641ffd4159bf8365317e991ae0', 'admin', '超级管理员', '', '', '01');

-- ----------------------------
-- Table structure for auth_permission
-- ----------------------------
DROP TABLE IF EXISTS `auth_permission`;
CREATE TABLE `auth_permission` (
  `per_id` varchar(32) NOT NULL DEFAULT '',
  `per_code` varchar(20) NOT NULL COMMENT '权限编号',
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
INSERT INTO `auth_permission` VALUES ('func_changepwd', 'func_changepwd', '密码修改', 'PermTypeFunc', 'func_myaccount', '302', '', 'asdfasdf', 'Account/ChangePwdPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_myaccount', 'func_myaccount', '我的账户', 'PermTypeFunc', '', '30', '', null, '', null);
INSERT INTO `auth_permission` VALUES ('func_mydetail', 'func_mydetail', '我的信息', 'PermTypeFunc', 'func_myaccount', '301', '', null, 'Account/DetailPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_oneCard', 'func_oneCard', '一卡通管理', 'PermTypeFunc', '', '10', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('func_sys', 'func_sys', '系统管理', 'PermTypeFunc', '', '20', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('func_sys_group', 'func_sys_group', '组织管理', 'PermTypeFunc', 'func_sys', '200', '', null, 'Auth/GroupMngPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_sys_per', 'func_sys_per', '权限管理', 'PermTypeFunc', 'func_sys', '202', '', '', 'Auth/PermissionMngPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('func_sys_usr', 'func_sys_usr', '用户管理', 'PermTypeFunc', 'func_sys', '201', '', '', 'Auth/UserMngPage.xaml', null);
INSERT INTO `auth_permission` VALUES ('opt_changepwd_sav', 'opt_changepwd_sav', '保存密码', 'PermTypeOpt', 'func_changepwd', '30201', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('opt_group_add', 'opt_group_add', '新增组织', 'PermTypeOpt', 'func_sys_group', '20001', '', null, null, 'Add');
INSERT INTO `auth_permission` VALUES ('opt_group_del', 'opt_group_del', '删除组织', 'PermTypeOpt', 'func_sys_group', '20003', '', null, null, 'Delete');
INSERT INTO `auth_permission` VALUES ('opt_group_edit', 'opt_group_edit', '修改组织', 'PermTypeOpt', 'func_sys_group', '20002', '', null, null, 'Edit');
INSERT INTO `auth_permission` VALUES ('opt_myinfo_save', 'opt_myinfo_save', '保存我的信息', 'PermTypeOpt', 'func_myinfo', '30101', '', '', '', null);
INSERT INTO `auth_permission` VALUES ('opt_per_add', 'opt_per_add', '新增权限', 'PermTypeOpt', 'func_sys_per', '20201', '', '', '', 'Add');
INSERT INTO `auth_permission` VALUES ('opt_per_del', 'opt_per_del', '删除权限', 'PermTypeOpt', 'func_sys_per', '20203', '', '', '', 'Delete');
INSERT INTO `auth_permission` VALUES ('opt_per_edit', 'opt_per_edit', '修改权限', 'PermTypeOpt', 'func_sys_per', '20202', '', '', '', 'Edit');
INSERT INTO `auth_permission` VALUES ('opt_usr_add', 'opt_usr_add', '新增用户', 'PermTypeOpt', 'func_sys_usr', '20101', '', '', '', 'Add');
INSERT INTO `auth_permission` VALUES ('opt_usr_assign_per', 'opt_usr_assign_per', '分配权限', 'PermTypeOpt', 'func_sys_usr', '20104', '', '', '', 'Assign');
INSERT INTO `auth_permission` VALUES ('opt_usr_del', 'opt_usr_del', '删除用户', 'PermTypeOpt', 'func_sys_usr', '20103', '', '', '', 'Delete');
INSERT INTO `auth_permission` VALUES ('opt_usr_edit', 'opt_usr_edit', '修改用户', 'PermTypeOpt', 'func_sys_usr', '20102', '', '', '', 'Edit');

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
INSERT INTO `auth_user` VALUES ('admin', 'admin', '0b4e7a0e5fe84ad35fb5f95b9ceeac79', '372924198708265138', '管理员', '372924', 'a203b7641ffd4159bf8365317e991ae0', null, '管理员\r\n继续加油！！！');
INSERT INTO `auth_user` VALUES ('e4831f52908b4c3e90d50032d4ffb043', 'ww', '0b4e7a0e5fe84ad35fb5f95b9ceeac79', '333333333333333', '王五', '1234', null, 'admin', '啥都分开了');
INSERT INTO `auth_user` VALUES ('test', 'test', '0b4e7a0e5fe84ad35fb5f95b9ceeac79', '111111111111111111', '测试', '372924', '', 'admin', '阿斯蒂芬');

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
  `dict_code` varchar(20) NOT NULL COMMENT '字典编号，相同类型下，编号不能为空',
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
INSERT INTO `base_dict` VALUES ('permtype_func', 'PermTypeFunc', '功能权限', 'permtype', '', '', '1');
INSERT INTO `base_dict` VALUES ('permtype_opt', 'PermTypeOpt', '操作权限', 'permtype', '', '\0', '2');

-- ----------------------------
-- Table structure for base_dict_type
-- ----------------------------
DROP TABLE IF EXISTS `base_dict_type`;
CREATE TABLE `base_dict_type` (
  `type_code` varchar(10) NOT NULL COMMENT '类型编号',
  `type_name` varchar(20) DEFAULT NULL COMMENT '类型名称',
  `type_system` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统预制',
  PRIMARY KEY (`type_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of base_dict_type
-- ----------------------------
INSERT INTO `base_dict_type` VALUES ('cardstate', '一卡通状态', '');
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
INSERT INTO `party_org` VALUES ('a203b7641ffd4159bf8365317e991ae0', null, null, null, null, '\0', '0', '0', '0', null);
