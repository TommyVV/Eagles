/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/6/9 21:18:34                            */
/*==============================================================*/


drop table if exists TB_ACTIVITY;

drop table if exists TB_APP_MENU;

drop table if exists TB_APP_MODULE;

drop table if exists TB_AUTHORITY;

drop table if exists TB_BRANCH;

drop table if exists TB_IMPORT_LOG;

drop table if exists TB_MEETING_USER;

drop table if exists TB_NEWS;

drop table if exists TB_OPER;

drop table if exists TB_OPER_GROUP;

drop table if exists TB_ORDER;

drop table if exists TB_ORG_INFO;

drop table if exists TB_ORG_RELATIONSHIP;

drop table if exists TB_ORG_SMS_CONFIG;

drop table if exists TB_PRODUCT;

drop table if exists TB_QUESTION;

drop table if exists TB_QUEST_ANWSER;

drop table if exists TB_REVIEW;

drop table if exists TB_REWARD_SCORE;

drop table if exists TB_SCROLL_IMAGE;

drop table if exists TB_SMS_CONFIG;

drop table if exists TB_SMS_SEND_LOG;

drop table if exists TB_SYSTEM_NEWS;

drop table if exists TB_TASK;

drop table if exists TB_TEST_PAPER;

drop table if exists TB_TEST_QUESTION;

drop table if exists TB_USER_ACTIVITY;

drop table if exists TB_USER_COMMENT;

drop table if exists TB_USER_INFO;

drop table if exists TB_USER_NEWS;

drop table if exists TB_USER_NOTICE;

drop table if exists TB_USER_RELATIONSHIP;

drop table if exists TB_USER_SCORE_TRACE;

drop table if exists TB_USER_STUDY_LOG;

drop table if exists TB_USER_TASK;

drop table if exists TB_USER_TASK_STEP;

drop table if exists TB_USER_TEST;

drop table if exists TB_USER_TEST_QUESTION;

drop table if exists TB_USER_TOKEN;

drop table if exists TB_VALIDCODE;

/*==============================================================*/
/* Table: TB_ACTIVITY                                           */
/*==============================================================*/
create table TB_ACTIVITY
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   ACTIVITY_ID          INT(8) not null auto_increment comment '活动id',
   ACTIVITY_NAME        NVARCHAR(50) comment '活动名称',
   HTML_CONTENT         NVARCHAR(4000) comment '活动内容',
   BEGIN_TIME           DATETIME comment '开始日期',
   END_TIME             DATETIME comment '截止日期',
   FROM_USER            INT(10) comment '发起人',
   ACTIVITY_TYPE        CHAR(2) comment '活动类型;0:报名;1:投票;2:问卷调查',
   MAX_COUNT            INT(5) comment '每人最大参与次数',
   CAN_COMMENT          INT(2) comment '是否允许评论;0:允许;1:禁止',
   TEST_ID              INT(8) comment '问卷调查试卷id',
   MAX_USER             INT(5) comment '最大参与人数',
   ATTACH1              NVARCHAR(255) comment '附件1',
   ATTACH2              NVARCHAR(255) comment '附件2',
   ATTACH3              NVARCHAR(255) comment '附件3',
   ATTACH4              NVARCHAR(255) comment '附件4',
   IMAGE_URL            NVARCHAR(255) comment '图片',
   IS_PUBLIC            CHAR(2) comment '是否公开;
            0:公开;
            1:不公开',
   ORG_REVIEW           CHAR(2) comment '机构审核状态;
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   BRANCH_REVIEW        CHAR(2) comment '支部审核状态
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   TO_USER_ID           INT(8) comment '负责人Id',
   primary key (ACTIVITY_ID)
)
auto_increment = 10000000;

alter table TB_ACTIVITY comment '活动表';

/*==============================================================*/
/* Table: TB_APP_MENU                                           */
/*==============================================================*/
create table TB_APP_MENU
(
   ORG_ID               int(8) not null comment '组织id',
   MENU_ID              int(8) not null auto_increment comment '菜单id
            菜单id
            菜单id',
   MENU_NAME            NVARCHAR(20) comment '菜单名称',
   LEVEL                CHAR(2) comment '等级;',
   PARENT_MENU_ID       INT(8) comment '上级菜单id',
   TARGET_URL           NVARCHAR(255) comment '跳转链接',
   primary key (MENU_ID)
);

alter table TB_APP_MENU comment '菜单表';

/*==============================================================*/
/* Table: TB_APP_MODULE                                         */
/*==============================================================*/
create table TB_APP_MODULE
(
   ORG_ID               int(8) not null comment '组织id',
   MODULE_ID            int(8) not null auto_increment comment '菜单id
            菜单id
            菜单id',
   MODULE_NAME          NVARCHAR(20) comment '菜单名称',
   TARGET_URL           NVARCHAR(255) comment '跳转链接',
   MODULE_TYPE          INT(2) comment '类型;
            0:首页;
            1:党建门户;
            2:党务工作;
            3:党建学习',
   SMALL_IMAGE_URL      NVARCHAR(255) comment '小图',
   IMAGE_URL            NVARCHAR(255) comment '大图',
   PRIORITY             INT(8) comment '优先级',
   INDEX_PAGE_NUMBER    INT(2) comment '首页显示数量',
   INDEX_DISPLAY        INT(2) comment '是否在首页显示',
   primary key (MODULE_ID)
);

alter table TB_APP_MODULE comment '栏目表';

/*==============================================================*/
/* Table: TB_AUTHORITY                                          */
/*==============================================================*/
create table TB_AUTHORITY
(
   ORG_ID               int(8) not null comment '组织id',
   GROUP_ID             INT(8) not null comment '权限组编号',
   FUN_CODE             NVARCHAR(8) not null comment '功能id',
   OPER_ID              int(8) comment '修改用户',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            datetime comment '修改时间',
   primary key (GROUP_ID, FUN_CODE)
);

alter table TB_AUTHORITY comment '权限管理表';

/*==============================================================*/
/* Table: TB_BRANCH                                             */
/*==============================================================*/
create table TB_BRANCH
(
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            int(8) not null auto_increment comment '支部id',
   BRANCH_NAME          NVARCHAR(50) not null comment '支部名称',
   BRANCH_DESC          NVARCHAR(50) comment '支部描述',
   CREATE_TIME          DATETIME comment '创建时间',
   primary key (BRANCH_ID)
)
auto_increment = 10000000;

alter table TB_BRANCH comment '支部表';

/*==============================================================*/
/* Table: TB_IMPORT_LOG                                         */
/*==============================================================*/
create table TB_IMPORT_LOG
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   TRACE_ID             INT(10) not null auto_increment comment '流水号',
   OPER_ID              INT(8) comment '操作员',
   CREATE_TIME          DATETIME comment '创建时间',
   DESCRIPTION          CHAR(100) comment '描述',
   OPER_TYPE            CHAR(2) comment '导入类型',
   STATUS               INT(2) comment '导入结果;0:成功;1:失败',
   primary key (TRACE_ID)
)
auto_increment = ?;

alter table TB_IMPORT_LOG comment '试卷问题关联表';

/*==============================================================*/
/* Table: TB_MEETING_USER                                       */
/*==============================================================*/
create table TB_MEETING_USER
(
   NEWS_ID              int(8) not null comment '会议id',
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            int(10) comment '支部id',
   USER_ID              INT(10) not null comment '参与用户id',
   primary key (NEWS_ID, USER_ID)
);

alter table TB_MEETING_USER comment '会议参与人员表';

/*==============================================================*/
/* Table: TB_NEWS                                               */
/*==============================================================*/
create table TB_NEWS
(
   ORG_ID               int(8) not null comment '组织id',
   NEWS_ID              int(10) not null auto_increment comment '新闻id
            ',
   SHORT_DESC           NVARCHAR(100) comment '简单描述',
   TITLE                NVARCHAR(100) comment '新闻名称',
   HTML_CONTENT         NVARCHAR(4000) comment '新闻内容',
   AUTHOR               NVARCHAR(20) comment '作者',
   SOURCE               NVARCHAR(20) comment '来源',
   MODULE_ID            INT(8) comment '所属栏目',
   STATUS               CHAR(2) comment '状态;
            0:正常 
            -1:待审核;',
   BEGIN_TIME           DATETIME comment '开始日期',
   END_TIME             DATETIME comment '结束日期',
   TEST_ID              INT(8) comment '试卷id',
   ATTACH1              NVARCHAR(255) comment '附件1',
   ATTACH2              NVARCHAR(255) comment '附件2',
   ATTACH3              NVARCHAR(255) comment '附件3',
   ATTACH4              NVARCHAR(255) comment '附件4',
   ATTACH5              NVARCHAR(255) comment '附件5',
   OPER_ID              INT(8) comment '操作员id',
   CREATE_TIME          DATETIME comment '创建日期',
   IS_IMAGE             INT(2) comment '有图片',
   IS_VIDEO             INT(2) comment '有视频',
   IS_ATTACH            INT(2) comment '有附件',
   IS_CLASS             INT(2) comment '有课件',
   IS_LEARNING          INT(2) comment '是学习心得',
   IS_ARTICLE           INT(2) comment '是文章',
   VIEWS_COUNT          INT(8) comment '阅读数量',
   REVIEW_ID            INT(10) comment '审核id',
   CAN_STUDY            INT(2) comment '是否允许学习',
   IMAGE_URL            char(255) comment '缩略图',
   primary key (NEWS_ID)
);

alter table TB_NEWS comment '新闻表';

/*==============================================================*/
/* Table: TB_OPER                                               */
/*==============================================================*/
create table TB_OPER
(
   ORG_ID               int(8) not null comment '组织id',
   OPER_ID              int(8) not null auto_increment comment '操作员编号',
   OPER_NAME            NVARCHAR(20) comment '操作员名称',
   CREATE_TIME          DATETIME comment '创建时间',
   GROUP_ID             INT(8) comment '权限组编号',
   STATUS               INT(2) comment '状态;0:正常;1:禁用',
   PASSWORD             CHAR(100) comment '密码',
   primary key (OPER_ID)
)
auto_increment = 10000000;

alter table TB_OPER comment '操作员表';

/*==============================================================*/
/* Table: TB_OPER_GROUP                                         */
/*==============================================================*/
create table TB_OPER_GROUP
(
   ORG_ID               int(8) not null comment '组织id',
   GROUP_ID             INT(8) not null auto_increment comment '权限组编号',
   GROUP_NAME           NVARCHAR(8) comment '管理组名称',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            datetime comment '修改时间',
   primary key (GROUP_ID)
);

alter table TB_OPER_GROUP comment '操作员管理组表';

/*==============================================================*/
/* Table: TB_ORDER                                              */
/*==============================================================*/
create table TB_ORDER
(
   ORG_ID               int(8) not null comment '组织id',
   ORDER_ID             int(12) not null auto_increment comment '订单编号',
   PROD_ID              INT(8) not null comment '产品编号',
   PROD_NAME            NVARCHAR(200) comment '产品名称',
   ORDER_STATUS         INT(2) comment '订单状态;
            0:成功
            1:失败',
   SOCRE                INT(8) comment '支付积分',
   COUNT                INT(5) comment '数量',
   USER_ID              INT(10) comment '用户id',
   EXPRESS_ID           NVARCHAR(500) comment '快递id',
   ADDRESS              NVARCHAR(200) comment '订单寄送地址',
   PROVINCE             NVARCHAR(15) comment '省',
   CITY                 CHAR(20) comment '市',
   DISTRICT             CHAR(20) comment '区',
   CREATE_TIME          DATETIME comment '创建时间',
   UPDATE_TIME          DATETIME comment '修改时间',
   OPER_ID              INT(8) comment '操作员id',
   primary key (ORDER_ID)
);

alter table TB_ORDER comment '订单表';

/*==============================================================*/
/* Table: TB_ORG_INFO                                           */
/*==============================================================*/
create table TB_ORG_INFO
(
   ORG_ID               int(8) not null auto_increment comment '组织id',
   ORG_ANME             INT(8) comment '机构名称',
   PROVINCE             NVARCHAR(8) comment '省',
   CITY                 NVARCHAR(8) comment '市',
   DISTRCIT             NVARCHAR(8) comment '区',
   ADDRESS              NVARCHAR(8) comment '地址',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            datetime comment '修改时间',
   OPER_ID              INT(8) comment '操作员',
   LOGO                 NVARCHAR(255) comment '组织logo',
   primary key (ORG_ID)
)
auto_increment = 10000000;

alter table TB_ORG_INFO comment '党组织关系表';

/*==============================================================*/
/* Table: TB_ORG_RELATIONSHIP                                   */
/*==============================================================*/
create table TB_ORG_RELATIONSHIP
(
   ORG_ID               int(8) not null comment '组织id',
   SUB_ORG_ID           int(8) not null comment '所属机构id',
   OPER_ID              INT(8) comment '操作员id',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            DATETIME comment '修改时间',
   primary key (ORG_ID, SUB_ORG_ID)
);

alter table TB_ORG_RELATIONSHIP comment '组织机构关系表';

/*==============================================================*/
/* Table: TB_ORG_SMS_CONFIG                                     */
/*==============================================================*/
create table TB_ORG_SMS_CONFIG
(
   ORG_ID               INT(8) not null comment '机构id',
   VENDOR_ID            INT(8) not null comment '短信提供方id',
   MAX_COUNT            INT(8) comment '最大可发送数量',
   SEND_COUNT           INT(8) comment '已发送数量',
   CREATE_TIME          DATETIME comment '最后修改时间',
   OPER_ID              INT(8) comment '最后修改人',
   PRIORITY             INT(2) comment '优先级',
   STATUS               INT(2) comment '状态;0:正常;1:禁用',
   primary key (ORG_ID, VENDOR_ID)
);

alter table TB_ORG_SMS_CONFIG comment '机构短信配置表';

/*==============================================================*/
/* Table: TB_PRODUCT                                            */
/*==============================================================*/
create table TB_PRODUCT
(
   PROD_ID              int(8) not null auto_increment comment '产品id',
   ORG_ID               int(8) not null comment '组织id',
   PROD_NAME            NVARCHAR(200) comment '产品名称',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            DATETIME comment '修改时间',
   PRICE                decimal(8,2) comment '产品价值',
   SOCRE                INT(8) comment '购买积分',
   STOCK                INT(8) comment '库存',
   SMALL_IMAGE_URL      NVARCHAR(255) comment '小图',
   IMAGE_URL            NVARCHAR(255) comment '大图',
   MAX_BUY_COUNT        INT(5) comment '每人最大购买限制;0:不限',
   SALES_COUNT          INT(5) comment '销售数量',
   BEGIN_TIME           DATETIME comment '产品上架时间',
   END_TIME             DATETIME comment '产品下家时间',
   HTML_DESCRPTION      TEXT comment '产品描述',
   STATUS               INT(2) comment '产品状态;0:正常:1:禁用',
   primary key (PROD_ID)
)
auto_increment = 10000000;

alter table TB_PRODUCT comment '产品表';

/*==============================================================*/
/* Table: TB_QUESTION                                           */
/*==============================================================*/
create table TB_QUESTION
(
   ORG_ID               INT(8) comment '机构ID',
   QUESTION_ID          INT(10) not null auto_increment comment '问题id',
   QUESTION             NVARCHAR(500) comment '问题',
   ANWSER_TYPE          INT(2) comment '答案类型;0:默认:1:自定义',
   MULTIPLE             INT(2) comment '是否允许多选;0:否;1:是',
   MULTIPLE_COUNT       INT(2) comment '多选数量',
   primary key (QUESTION_ID)
);

alter table TB_QUESTION comment '习题库';

/*==============================================================*/
/* Table: TB_QUEST_ANWSER                                       */
/*==============================================================*/
create table TB_QUEST_ANWSER
(
   ORG_ID               INT(8) comment '机构id',
   QUEST_ID             INT(10) not null comment '问题编号',
   ANWSER_ID            INT(10) not null auto_increment comment '选项编号',
   ANWSER               NVARCHAR(100) comment '选项',
   ANWSER_TYPE          INT(2) comment '答案类型;
            0:默认
            1:自定义',
   IS_RIGHT             INT(2) comment '答案是否是正确答案;
            0;是;
            1:否',
   IMAGE_URL            NVARCHAR(255) comment '选项图片',
   primary key (ANWSER_ID)
);

alter table TB_QUEST_ANWSER comment '问题答案表';

/*==============================================================*/
/* Table: TB_REVIEW                                             */
/*==============================================================*/
create table TB_REVIEW
(
   REVIEW_ID            int(10) not null auto_increment comment '审核流水id',
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            INT(10) comment '支部id',
   NEWS_ID              INT(10) not null comment '新闻id',
   NEWS_TYPE            CHAR(2) comment '审核类型
            00:文章
            10:任务
            20:活动
            ',
   OPER_ID              INT(8) comment '审核用户',
   RESULT               NVARCHAR(200) comment '审核结果',
   CREATE_TIME          DATETIME comment '审核时间',
   REVIEW_STATUS        INT(2) comment '审核状态',
   primary key (REVIEW_ID)
);

alter table TB_REVIEW comment '审核表';

/*==============================================================*/
/* Table: TB_REWARD_SCORE                                       */
/*==============================================================*/
create table TB_REWARD_SCORE
(
   REWARD_ID            INT(8) not null auto_increment comment '奖励id',
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            INT(8) comment '支部id',
   REWARD_TYPE          INT(2) not null comment '奖励类型;
            0:任务奖励
            1:活动奖励;
            2:字数奖励
            3:关键字奖励
            4:学习时间奖励',
   SOCRE                INT(5) comment '奖励积分',
   KEYWORDS             NVARCHAR(200) comment '关键字',
   LEARN_TIME           INT(4) comment '学习时间;单位:分钟',
   WORD_COUNT           int(6) comment '字数',
   primary key (REWARD_ID)
)
auto_increment = 10000000;

alter table TB_REWARD_SCORE comment '积分奖励配置表';

/*==============================================================*/
/* Table: TB_SCROLL_IMAGE                                       */
/*==============================================================*/
create table TB_SCROLL_IMAGE
(
   ORG_ID               int(8) not null comment '组织id',
   PAGE_TYPE            CHAR(2) not null comment '页面类型;
            0:首页;
            1:党建门户;
            2:党务工作;
            3:党建学习',
   IMAGE_URL            INT(8) comment '操作员id',
   primary key (ORG_ID, PAGE_TYPE)
);

alter table TB_SCROLL_IMAGE comment '滚动图片表';

/*==============================================================*/
/* Table: TB_SMS_CONFIG                                         */
/*==============================================================*/
create table TB_SMS_CONFIG
(
   VENDOR_ID            INT(8) not null auto_increment comment '短信提供方ID',
   VENDOR_NAME          CHAR(100) comment '短信提供方名称',
   SEND_COUNT           INT(8) comment '已发送数量',
   CREATE_TIME          DATETIME comment '创建时间',
   APP_ID               CHAR(255) comment '短信方appId',
   APP_KEY              CHAR(255) comment '短信方appKey',
   SIGN_KEY             CHAR(255) comment '签名key',
   SERVICE_URL          CHAR(255) comment '接口地址',
   MAX_COUNT            INT(8) comment '最大发送数量',
   PRIORITY             INT(2) comment '优先级',
   STATUS               INT(2) comment '状态;0:正常:1:禁用',
   primary key (VENDOR_ID)
)
auto_increment = 10000000;

alter table TB_SMS_CONFIG comment '短信配置表';

/*==============================================================*/
/* Table: TB_SMS_SEND_LOG                                       */
/*==============================================================*/
create table TB_SMS_SEND_LOG
(
   ORG_ID               int(8) not null comment '组织id',
   TRACE_ID             int(10) not null auto_increment comment '流水号',
   VENDOR_ID            INT(8) comment '短信提供方id',
   SMS_CONTENT          NVARCHAR(200) comment '短信内容',
   CREATE_TIME          DATETIME comment '发送时间',
   PHONE                CHAR(11) comment '发送手机',
   STATUS               INT(2) comment '发送状态;0:成功;1:失败',
   REQUEST_MSG          CHAR(1000) comment '请求内容',
   RESPONSE_MSG         CHAR(1000) comment '返回内容',
   primary key (TRACE_ID)
);

alter table TB_SMS_SEND_LOG comment '短信发送日志表';

/*==============================================================*/
/* Table: TB_SYSTEM_NEWS                                        */
/*==============================================================*/
create table TB_SYSTEM_NEWS
(
   NEWS_ID              INT(8) not null auto_increment comment '消息id',
   NEWS_NAME            NVARCHAR(100) comment '消息名称',
   NEWS_CONTENT         NVARCHAR(100) comment '消息内容',
   NOTICE_DATE          CHAR(8) comment '提示日期',
   STATUS               INT(2) comment '状态',
   OPER_ID              INT(8) comment '操作员',
   REPEAT_TYPE          int(2) comment '重复类型;
            0:每年;
            1:仅1次',
   HTML_DESC            NVARCHAR(4000) comment 'HTML描述',
   NEWS_TYPE            CHAR(2) comment '系统通知类型
            00:领袖诞辰
            10:系统通知',
   primary key (NEWS_ID)
);

alter table TB_SYSTEM_NEWS comment '系统消息表';

/*==============================================================*/
/* Table: TB_TASK                                               */
/*==============================================================*/
create table TB_TASK
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   TASK_ID              INT(8) not null auto_increment comment '任务id',
   TASK_NAME            NVARCHAR(100) comment '任务名称',
   FROM_USER            INT(10) comment '发起人',
   TASK_CONTENT         NVARCHAR(4000) comment '任务内容',
   BEGIN_TIME           DATETIME comment '开始时间',
   END_TIME             DATETIME comment '结束时间',
   ATTACH1              NVARCHAR(255) comment '附件1',
   ATTACH2              NVARCHAR(255) comment '附件2',
   ATTACH3              NVARCHAR(255) comment '附件3',
   ATTACH4              NVARCHAR(255) comment '附件4',
   CREATE_TIME          DATETIME comment '发起时间',
   CAN_COMMENT          INT(2) comment '是否允许评论;0:允许;1:禁止',
   STATUS               INT(2) comment '任务状态;
            0:发起状态;
            1:执行中
            2:完成任务待审核
            3:审核通过',
   IS_PUBLIC            INT(2) comment '是否公开
            0:公开;
            1:不公开',
   ORG_REVIEW           CHAR(2) comment '机构审核状态;
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   BRANCH_REVIEW        CHAR(2) comment '支部审核状态
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   primary key (TASK_ID)
);

alter table TB_TASK comment '任务表';

/*==============================================================*/
/* Table: TB_TEST_PAPER                                         */
/*==============================================================*/
create table TB_TEST_PAPER
(
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            INT(8) comment '支部id',
   TEST_ID              INT(8) not null auto_increment comment '试卷id',
   TEST_NAME            NVARCHAR(20) comment '试卷名称',
   HAS_REWARD           INT(2) comment '是否奖励积分; 
            0:奖励;
            1:不奖励',
   EXCERCISE_SOCRE      INT(3) comment '每一题分值',
   PASS_SCORE           INT(5) comment '及格分值',
   HAS_LIMITED_TIME     INT(2) comment '是否限制时间',
   LIMITED_TIME         INT(5) comment '时间(分钟)',
   HTML_DESCRIPT        text comment 'html描述',
   OPER_ID              int(8) comment '操作员id',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            DATETIME comment '修改时间',
   STATUS               INT(2) comment '状态;0;正常;1:禁用',
   primary key (TEST_ID)
);

alter table TB_TEST_PAPER comment '试卷表';

/*==============================================================*/
/* Table: TB_TEST_QUESTION                                      */
/*==============================================================*/
create table TB_TEST_QUESTION
(
   ORG_ID               INT(8) comment '机构id',
   TEST_ID              INT(8) not null comment '试卷id',
   QUESTION_ID          INT(10) not null comment '试卷问题id',
   primary key (TEST_ID, QUESTION_ID)
);

alter table TB_TEST_QUESTION comment '试卷问题关联表';

/*==============================================================*/
/* Table: TB_USER_ACTIVITY                                      */
/*==============================================================*/
create table TB_USER_ACTIVITY
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   ACTIVITY_ID          INT(8) not null auto_increment comment '活动id',
   USER_ID              INT(10) not null comment '用户id',
   USER_FEEDBACK        NVARCHAR(4000) comment '用户反馈',
   CREATE_TIME          DATETIME comment '接受时间',
   STATUS               CHAR(2) comment '活动状态',
   COMPLETE_TIME        INT(8) comment '完成时间',
   REWARDS_SOCRE        INT(5) comment '奖励积分',
   primary key (ACTIVITY_ID, USER_ID)
);

alter table TB_USER_ACTIVITY comment '用户参与活动表';

/*==============================================================*/
/* Table: TB_USER_COMMENT                                       */
/*==============================================================*/
create table TB_USER_COMMENT
(
   ORG_ID               int(8) not null comment '组织id',
   MESSAGE_ID           int(12) not null auto_increment comment '评论id',
   CONTENT              NVARCHAR(200) not null comment '评论内容',
   CREATE_TIME          DATETIME comment '评论时间',
   USER_ID              INT(10) comment '评论用户',
   REVIEW_STATUS        INT(2) comment '审核状态;
            0:通过;
            1:未通过',
   REVIEW_USER          INT(8) comment '审核用户',
   REVIEW_TIME          DATETIME comment '审核时间',
   primary key (MESSAGE_ID)
);

alter table TB_USER_COMMENT comment '评论表';

/*==============================================================*/
/* Table: TB_USER_INFO                                          */
/*==============================================================*/
create table TB_USER_INFO
(
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            INT(8) comment '支部编号',
   USER_ID              int(10) not null auto_increment comment '用户id',
   PASSWORD             CHAR(255) comment '密码',
   NAME                 NVARCHAR(20) comment '姓名',
   SEX                  INT(2) comment '性别;0:男;1:女',
   ETHNIC               NVARCHAR(20) comment '民族',
   BIRTHDAY             DATETIME comment '出生日期',
   ORIGIN               NVARCHAR(20) comment '籍贯',
   ORIGIN_ADDRESS       NVARCHAR(200) comment '户籍地址',
   PHONE                CHAR(15) comment '联系电话',
   ID_NUMBER            CHAR(18) comment '身份证号',
   EDUCATION            NVARCHAR(20) comment '学历',
   SCHOOL               NVARCHAR(50) comment '毕业学校',
   PROVINCE             NVARCHAR(8) comment '省',
   CITY                 NVARCHAR(8) comment '市',
   DISTRCIT             NVARCHAR(8) comment '区',
   ADDRESS              NVARCHAR(200) comment '地址',
   COMPANY              NVARCHAR(100) comment '工作单位',
   DEPT                 NVARCHAR(20) comment '部门',
   TITLE                NVARCHAR(20) comment '职位',
   PRE_MEMBER_DATE      DATETIME comment '预备党员日期',
   MEMBER_DATE          DATETIME comment '正式党员日期',
   MEMBER_TYPE          INT(2) comment '党员类型;
            0:党员;
            1:预备党员',
   STATUS               INT(2) comment '用户状态;0:正常;1:禁用',
   MEMBER_STATUS        INT(2) comment '党员状态;0:正常;1:禁用',
   PHOTO_URL            NVARCHAR(255) comment '头像',
   NICK_PHOTO_URL       NVARCHAR(255) comment '昵称头像',
   CREATE_TIME          DATETIME comment '创建时间',
   EDIT_TIME            datetime comment '修改时间',
   OPER_ID              int(8) comment '操作员id',
   IS_CUSTOMER          int(2) comment '游客;
            0:是 ;
            1:否',
   primary key (USER_ID)
)
auto_increment = 10000000;

alter table TB_USER_INFO comment '用户表';

/*==============================================================*/
/* Table: TB_USER_NEWS                                          */
/*==============================================================*/
create table TB_USER_NEWS
(
   ORG_ID               int(8) not null comment '组织id',
   BRANCH_ID            INT(8) comment '支部id',
   NEWS_ID              int(10) not null auto_increment comment '新闻id
            ',
   USER_ID              int(10) comment '用户id',
   TITLE                NVARCHAR(100) comment '新闻名称',
   HTML_CONTENT         NVARCHAR(4000) comment '文章内容',
   NEWS_TYPE            CHAR(2) comment '文章类型
            00:文章
            01:心得体会
            02:会议
            ',
   STATUS               CHAR(2) comment '状态;0:正常 -1:待审核;',
   CREATE_TIME          DATETIME comment '创建日期',
   VIEWS_COUNT          INT(8) comment '阅读数量',
   REWARD_SCORE         INT(8) comment '奖励积分',
   REVIEW_ID            INT(10) comment '审核流水id',
   ORG_REVIEW           CHAR(2) comment '机构审核状态;
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   BRANCH_REVIEW        CHAR(2) comment '支部审核状态
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   primary key (NEWS_ID)
);

alter table TB_USER_NEWS comment '用户文章表';

/*==============================================================*/
/* Table: TB_USER_NOTICE                                        */
/*==============================================================*/
create table TB_USER_NOTICE
(
   ORG_ID               INT(8) comment '机构id',
   NEWS_ID              INT(8) not null auto_increment comment '通知id',
   NEWS_TYPE            INT(2) comment '通知类型',
   TITLE                CHAR(100) comment '标题',
   USER_ID              INT(10) comment '接收用户',
   CONTENT              NVARCHAR(250) comment '通知内容',
   IS_READ              INT(1) comment '是否已阅读;
            0:已阅读
            1:未阅读',
   FROM_USER            INT(10) comment '发送人',
   CREATE_TIME          DATETIME comment '创建时间',
   READ_TIME            DATETIME comment '阅读时间',
   primary key (NEWS_ID)
);

alter table TB_USER_NOTICE comment '用户通知信息表
';

/*==============================================================*/
/* Table: TB_USER_RELATIONSHIP                                  */
/*==============================================================*/
create table TB_USER_RELATIONSHIP
(
   ORG_ID               int(8) not null comment '组织id',
   USER_ID              int(10) not null comment '领导id',
   SUB_USER_ID          int(10) comment '下级id',
   primary key (USER_ID)
);

alter table TB_USER_RELATIONSHIP comment '用户关系表';

/*==============================================================*/
/* Table: TB_USER_SCORE_TRACE                                   */
/*==============================================================*/
create table TB_USER_SCORE_TRACE
(
   ORG_ID               int(8) not null comment '组织id',
   USER_ID              INT(10) not null comment '参与用户id',
   TRACE_ID             int not null auto_increment,
   CREATE_TIME          DATETIME comment '创建时间',
   SCORE                INT(8) comment '获得积分',
   REWARD_TYPE          CHAR(2) comment '积分奖励类型;
            00:发表文章奖励
            01:文章字数奖励
            02:文章关键字奖励
            10:参加活动奖励
            11:活动分享到支部奖励
            12:活动分享到组织奖励
            20:任务完成奖励
            21:任务分享到支部奖励
            22:任务分享到组织奖励
            30:会议文章奖励
            40:心得体会类型奖励
            ',
   COMMENT              NVARCHAR(200) comment '描述',
   ORI_SCORE            INT(8) comment '原积分',
   primary key (TRACE_ID)
);

alter table TB_USER_SCORE_TRACE comment '用户积分流水表';

/*==============================================================*/
/* Table: TB_USER_STUDY_LOG                                     */
/*==============================================================*/
create table TB_USER_STUDY_LOG
(
   ORG_ID               INT(8) comment '机构ID',
   BRANCH_ID            INT(8) comment '支部id',
   TRACE_ID             INT(12) not null auto_increment comment '流水号',
   USER_ID              INT(8) comment '用户id',
   NEWS_ID              INT(8) comment '新闻id',
   MODULE_ID            INT(2) comment '新闻栏目id',
   STUDY_TIME           INT(5) comment '学习时间;单位:分钟',
   CREATE_TIME          DATETIME comment '创建时间',
   SCORE                INT(5) comment '奖励积分',
   primary key (TRACE_ID)
);

alter table TB_USER_STUDY_LOG comment '用户文章学习流水表';

/*==============================================================*/
/* Table: TB_USER_TASK                                          */
/*==============================================================*/
create table TB_USER_TASK
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   TASK_ID              INT(8) not null comment '任务id',
   USER_ID              INT(10) not null comment '用户id',
   ACCEPT_TIME          DATETIME comment '接受时间',
   STATUS               INT(2) comment '状态',
   REWARD_SCORE         INT(5) comment '奖励积分',
   COMMENT              NVARCHAR(200) comment '评语',
   SOCRE                INT(2) comment '打分;1~5分',
   primary key (TASK_ID, USER_ID)
);

alter table TB_USER_TASK comment '我的任务表';

/*==============================================================*/
/* Table: TB_USER_TASK_STEP                                     */
/*==============================================================*/
create table TB_USER_TASK_STEP
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   TASK_ID              INT(8) not null comment '任务id',
   USER_ID              INT(10) not null comment '用户id',
   STEP_ID              INT(2) not null comment '步骤id',
   STEP_NAME            NVARCHAR(100) comment '步骤名称',
   CREATE_TIME          DATETIME comment '步骤创建时间',
   CONTENT              NVARCHAR(4000) comment '反馈',
   UPDATE_TIME          DATETIME comment '反馈时间',
   primary key (TASK_ID, USER_ID, STEP_ID)
);

alter table TB_USER_TASK_STEP comment '任务表步骤表';

/*==============================================================*/
/* Table: TB_USER_TEST                                          */
/*==============================================================*/
create table TB_USER_TEST
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   USER_ID              int(8) not null comment '用户id',
   TEST_ID              INT(10) not null comment '试卷id',
   SCORE                INT(3) comment '试卷得分',
   TOTAL_SCORE          INT(3) comment '试卷总分',
   CREATE_TIME          DATETIME comment '答卷时间',
   USE_TIME             INT(5) comment '答卷用时',
   primary key (USER_ID, TEST_ID)
);

alter table TB_USER_TEST comment '用户答题表';

/*==============================================================*/
/* Table: TB_USER_TEST_QUESTION                                 */
/*==============================================================*/
create table TB_USER_TEST_QUESTION
(
   ORG_ID               INT(8) comment '机构id',
   TEST_ID              INT(8) not null comment '试卷id',
   USER_ID              int(8) not null comment '用户id',
   QUESTION_ID          INT(10) not null comment '问题id',
   QUESTION_NAME        NVARCHAR(500) comment '问题名称',
   ANSWER1              CHAR(100) comment '答案1',
   ANSWER2              CHAR(100) comment '答案2',
   ANSWER3              CHAR(100) comment '答案3',
   ANSWER4              CHAR(100) comment '答案4',
   IS_RIGHT             INT(2) comment '是否正确;0:正确;1:错误',
   primary key (TEST_ID, USER_ID, QUESTION_ID)
);

alter table TB_USER_TEST_QUESTION comment '用户试卷答案表';

/*==============================================================*/
/* Table: TB_USER_TOKEN                                         */
/*==============================================================*/
create table TB_USER_TOKEN
(
   ORG_ID               INT(8) comment '机构id',
   BRANCH_ID            INT(8) comment '支部id',
   USER_ID              INT(8) comment '操作员id',
   TOKEN                CHAR(255) not null comment '操作员token',
   CREATE_TIME          DATETIME comment '创建时间',
   EXPIRE_TIME          DATETIME comment '过期时间',
   TOKEN_TYPE           INT(2) comment 'token类型;0:前端用户;1:后端操作员',
   primary key (TOKEN)
);

alter table TB_USER_TOKEN comment '用户登录信息会话表';

/*==============================================================*/
/* Table: TB_VALIDCODE                                          */
/*==============================================================*/
create table TB_VALIDCODE
(
   ORG_ID               int(8) not null comment '组织id',
   TRACE_ID             int(10) not null auto_increment comment '流水号',
   PHONE                CHAR(15) not null comment '手机号码',
   VALIDCODE            INT(6) not null comment '验证码',
   SEQ                  INT(2) comment '验证码序号',
   CREATE_TIME          DATETIME comment '发送时间',
   EXPIRE_TIME          DATETIME comment '过期时间',
   primary key (TRACE_ID)
);

alter table TB_VALIDCODE comment '验证码表';

