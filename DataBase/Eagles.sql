/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/6/13 20:35:18                           */
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
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   ActivityId           INT(8) not null auto_increment comment '活动id',
   ActivityName         NVARCHAR(50) comment '活动名称',
   HtmlContent          NVARCHAR(4000) comment '活动内容',
   BeginTime            DATETIME comment '开始日期',
   EndTime              DATETIME comment '截止日期',
   FromUser             INT(10) comment '发起人',
   ActivityType         CHAR(2) comment '活动类型;0:报名;1:投票;2:问卷调查',
   MaxCount             INT(5) comment '每人最大参与次数',
   CanComment           INT(2) comment '是否允许评论;0:允许;1:禁止',
   TestId               INT(8) comment '问卷调查试卷id',
   MaxUser              INT(5) comment '最大参与人数',
   Attach1              NVARCHAR(255) comment '附件1',
   Attach2              NVARCHAR(255) comment '附件2',
   Attach3              NVARCHAR(255) comment '附件3',
   Attach4              NVARCHAR(255) comment '附件4',
   AttachType1          char(2) comment '附件1类型;0:图片;1:其他',
   AttachType2          char(2) comment '附件1类型;0:图片;1:其他',
   AttachType3          char(2) comment '附件1类型;0:图片;1:其他',
   AttachType4          char(2) comment '附件1类型;0:图片;1:其他',
   ImageUrl             NVARCHAR(255) comment '图片',
   IsPublic             CHAR(2) comment '是否公开;
            0:公开;
            1:不公开',
   OrgReview            CHAR(2) comment '机构审核状态;
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   BranchReview         CHAR(2) comment '支部审核状态
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   ToUserId             INT(8) comment '负责人Id',
   primary key (ActivityId)
)
auto_increment = 10000000;

alter table TB_ACTIVITY comment '活动表';

/*==============================================================*/
/* Table: TB_APP_MENU                                           */
/*==============================================================*/
create table TB_APP_MENU
(
   OrgId                int(8) not null comment '组织id',
   MenuId               int(8) not null auto_increment comment '菜单id
            菜单id
            菜单id',
   MenuName             NVARCHAR(20) comment '菜单名称',
   Level                CHAR(2) comment '等级;',
   ParentMenuId         INT(8) comment '上级菜单id',
   TragetUrl            NVARCHAR(255) comment '跳转链接',
   primary key (MenuId)
);

alter table TB_APP_MENU comment '菜单表';

/*==============================================================*/
/* Table: TB_APP_MODULE                                         */
/*==============================================================*/
create table TB_APP_MODULE
(
   OrgId                int(8) not null comment '组织id',
   ModuleId             int(8) not null auto_increment comment '菜单id
            菜单id
            菜单id',
   ModuleName           NVARCHAR(20) comment '菜单名称',
   TragetUrl            NVARCHAR(255) comment '跳转链接',
   ModuleType           INT(2) comment '类型;
            0:首页;
            1:党建门户;
            2:党务工作;
            3:党建学习',
   SmallImageUrl        NVARCHAR(255) comment '小图',
   ImageUrl             NVARCHAR(255) comment '大图',
   Priority             INT(8) comment '优先级',
   IndexPageCount       INT(2) comment '首页显示数量',
   IndexDisplay         INT(2) comment '是否在首页显示',
   primary key (ModuleId)
);

alter table TB_APP_MODULE comment '栏目表';

/*==============================================================*/
/* Table: TB_AUTHORITY                                          */
/*==============================================================*/
create table TB_AUTHORITY
(
   OrgId                int(8) not null comment '组织id',
   GroupId              INT(8) not null comment '权限组编号',
   FunCode              NVARCHAR(8) not null comment '功能id',
   OperId               int(8) comment '修改用户',
   CreateTime           DATETIME comment '创建时间',
   EditTime             datetime comment '修改时间',
   primary key (GroupId, FunCode)
);

alter table TB_AUTHORITY comment '权限管理表';

/*==============================================================*/
/* Table: TB_BRANCH                                             */
/*==============================================================*/
create table TB_BRANCH
(
   OrgId                int(8) not null comment '组织id',
   BranchId             int(8) not null auto_increment comment '支部id',
   BranchName           NVARCHAR(50) not null comment '支部名称',
   BranchDesc           NVARCHAR(50) comment '支部描述',
   CreateTime           DATETIME comment '创建时间',
   primary key (BranchId)
)
auto_increment = 10000000;

alter table TB_BRANCH comment '支部表';

/*==============================================================*/
/* Table: TB_IMPORT_LOG                                         */
/*==============================================================*/
create table TB_IMPORT_LOG
(
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   TraceId              INT(10) not null auto_increment comment '流水号',
   OperId               INT(8) comment '操作员',
   CreateTime           DATETIME comment '创建时间',
   Description          CHAR(100) comment '描述',
   OperType             CHAR(2) comment '导入类型',
   Status               INT(2) comment '导入结果;0:成功;1:失败',
   primary key (TraceId)
);

alter table TB_IMPORT_LOG comment '试卷问题关联表';

/*==============================================================*/
/* Table: TB_MEETING_USER                                       */
/*==============================================================*/
create table TB_MEETING_USER
(
   NewsId               int(8) not null comment '会议id',
   OrgId                int(8) not null comment '组织id',
   BranchId             int(10) comment '支部id',
   UserId               INT(10) not null comment '参与用户id',
   primary key (NewsId, UserId)
);

alter table TB_MEETING_USER comment '会议参与人员表';

/*==============================================================*/
/* Table: TB_NEWS                                               */
/*==============================================================*/
create table TB_NEWS
(
   OrgId                int(8) not null comment '组织id',
   NewsId               int(10) not null auto_increment comment '新闻id
            ',
   ShortDesc            NVARCHAR(100) comment '简单描述',
   Title                NVARCHAR(100) comment '新闻名称',
   HtmlContent          NVARCHAR(4000) comment '新闻内容',
   Author               NVARCHAR(20) comment '作者',
   Source               NVARCHAR(20) comment '来源',
   Module               INT(8) comment '所属栏目',
   Status               CHAR(2) comment '状态;
            0:正常 
            -1:待审核;',
   BeginTime            DATETIME comment '开始日期',
   EndTime              DATETIME comment '结束日期',
   TestId               INT(8) comment '试卷id',
   Attach1              NVARCHAR(255) comment '附件1',
   Attach2              NVARCHAR(255) comment '附件2',
   Attach3              NVARCHAR(255) comment '附件3',
   Attach4              NVARCHAR(255) comment '附件4',
   Attach5              NVARCHAR(255) comment '附件5',
   OperId               INT(8) comment '操作员id',
   CreateTime           DATETIME comment '创建日期',
   IsImage              INT(2) comment '有图片',
   IsVideo              INT(2) comment '有视频',
   IsAttach             INT(2) comment '有附件',
   IsClass              INT(2) comment '有课件',
   IsLearning           INT(2) comment '是学习心得',
   IsText               INT(2) comment '是文章',
   ViewCount            INT(8) comment '阅读数量',
   ReviewId             INT(10) comment '审核id',
   CanStudy             INT(2) comment '是否允许学习',
   ImageUrl             char(255) comment '缩略图',
   primary key (NewsId)
);

alter table TB_NEWS comment '新闻表';

/*==============================================================*/
/* Table: TB_OPER                                               */
/*==============================================================*/
create table TB_OPER
(
   OrgId                int(8) not null comment '组织id',
   OperId               int(8) not null auto_increment comment '操作员编号',
   OperName             NVARCHAR(20) comment '操作员名称',
   CreateTime           DATETIME comment '创建时间',
   GroupId              INT(8) comment '权限组编号',
   Status               INT(2) comment '状态;0:正常;1:禁用',
   Password             CHAR(100) comment '密码',
   primary key (OperId)
)
auto_increment = 10000000;

alter table TB_OPER comment '操作员表';

/*==============================================================*/
/* Table: TB_OPER_GROUP                                         */
/*==============================================================*/
create table TB_OPER_GROUP
(
   OrgId                int(8) not null comment '组织id',
   GroupId              INT(8) not null auto_increment comment '权限组编号',
   GroupName            NVARCHAR(8) comment '管理组名称',
   CreateTime           DATETIME comment '创建时间',
   EditTime             datetime comment '修改时间',
   primary key (GroupId)
);

alter table TB_OPER_GROUP comment '操作员管理组表';

/*==============================================================*/
/* Table: TB_ORDER                                              */
/*==============================================================*/
create table TB_ORDER
(
   OrgId                int(8) not null comment '组织id',
   OrderId              int(12) not null auto_increment comment '订单编号',
   ProdId               INT(8) not null comment '产品编号',
   ProdName             NVARCHAR(200) comment '产品名称',
   OrderStatus          INT(2) comment '订单状态;
            0:成功
            1:失败',
   Score                INT(8) comment '支付积分',
   Count                INT(5) comment '数量',
   UserId               INT(10) comment '用户id',
   ExpressId            NVARCHAR(500) comment '快递id',
   Address              NVARCHAR(200) comment '订单寄送地址',
   Province             NVARCHAR(15) comment '省',
   City                 CHAR(20) comment '市',
   District             CHAR(20) comment '区',
   CreateTime           DATETIME comment '创建时间',
   UpdateTime           DATETIME comment '修改时间',
   OperId               INT(8) comment '操作员id',
   primary key (OrderId)
);

alter table TB_ORDER comment '订单表';

/*==============================================================*/
/* Table: TB_ORG_INFO                                           */
/*==============================================================*/
create table TB_ORG_INFO
(
   OrgId                int(8) not null auto_increment comment '组织id',
   OrgName              INT(8) comment '机构名称',
   Province             NVARCHAR(8) comment '省',
   City                 NVARCHAR(8) comment '市',
   District             NVARCHAR(8) comment '区',
   Address              NVARCHAR(8) comment '地址',
   CreateTime           DATETIME comment '创建时间',
   EditTime             datetime comment '修改时间',
   OperId               INT(8) comment '操作员',
   Logo                 NVARCHAR(255) comment '组织logo',
   primary key (OrgId)
)
auto_increment = 10000000;

alter table TB_ORG_INFO comment '党组织关系表';

/*==============================================================*/
/* Table: TB_ORG_RELATIONSHIP                                   */
/*==============================================================*/
create table TB_ORG_RELATIONSHIP
(
   OrgId                int(8) not null comment '组织id',
   SubOrgId             int(8) not null comment '所属机构id',
   OperId               INT(8) comment '操作员id',
   CreateTime           DATETIME comment '创建时间',
   EditTime             DATETIME comment '修改时间',
   primary key (OrgId, SubOrgId)
);

alter table TB_ORG_RELATIONSHIP comment '组织机构关系表';

/*==============================================================*/
/* Table: TB_ORG_SMS_CONFIG                                     */
/*==============================================================*/
create table TB_ORG_SMS_CONFIG
(
   OrgId                INT(8) not null comment '机构id',
   VendorId             INT(8) not null comment '短信提供方id',
   MaxCount             INT(8) comment '最大可发送数量',
   SendCount            INT(8) comment '已发送数量',
   CreateTime           DATETIME comment '最后修改时间',
   OperId               INT(8) comment '最后修改人',
   Priority             INT(2) comment '优先级',
   Status               INT(2) comment '状态;0:正常;1:禁用',
   primary key (OrgId, VendorId)
);

alter table TB_ORG_SMS_CONFIG comment '机构短信配置表';

/*==============================================================*/
/* Table: TB_PRODUCT                                            */
/*==============================================================*/
create table TB_PRODUCT
(
   ProdId               int(8) not null auto_increment comment '产品id',
   OrgId                int(8) not null comment '组织id',
   ProdName             NVARCHAR(200) comment '产品名称',
   CreateTime           DATETIME comment '创建时间',
   EditTime             DATETIME comment '修改时间',
   Price                decimal(8,2) comment '产品价值',
   Score                INT(8) comment '购买积分',
   Stock                INT(8) comment '库存',
   SmallImageUrl        NVARCHAR(255) comment '小图',
   ImageUrl             NVARCHAR(255) comment '大图',
   MaxBuyCount          INT(5) comment '每人最大购买限制;0:不限',
   SaleCount            INT(5) comment '销售数量',
   BeginTime            DATETIME comment '产品上架时间',
   EndTime              DATETIME comment '产品下家时间',
   HtmlDescription      TEXT comment '产品描述',
   Status               INT(2) comment '产品状态;0:正常:1:禁用',
   primary key (ProdId)
)
auto_increment = 10000000;

alter table TB_PRODUCT comment '产品表';

/*==============================================================*/
/* Table: TB_QUESTION                                           */
/*==============================================================*/
create table TB_QUESTION
(
   OrgId                INT(8) comment '机构ID',
   QuestionId           INT(10) not null auto_increment comment '问题id',
   Question             NVARCHAR(500) comment '问题',
   Answer               INT(2) comment '答案类型;0:默认:1:自定义',
   Multiple             INT(2) comment '是否允许多选;0:否;1:是',
   MultipleCount        INT(2) comment '多选数量',
   primary key (QuestionId)
);

alter table TB_QUESTION comment '习题库';

/*==============================================================*/
/* Table: TB_QUEST_ANWSER                                       */
/*==============================================================*/
create table TB_QUEST_ANWSER
(
   OrgId                INT(8) comment '机构id',
   QuestionId           INT(10) not null comment '问题编号',
   AnswerId             INT(10) not null auto_increment comment '选项编号',
   Answer               NVARCHAR(100) comment '选项',
   AnswerType           INT(2) comment '答案类型;
            0:默认
            1:自定义',
   IsRight              INT(2) comment '答案是否是正确答案;
            0;是;
            1:否',
   ImageUrl             NVARCHAR(255) comment '选项图片',
   primary key (AnswerId)
);

alter table TB_QUEST_ANWSER comment '问题答案表';

/*==============================================================*/
/* Table: TB_REVIEW                                             */
/*==============================================================*/
create table TB_REVIEW
(
   ReviewId             int(10) not null auto_increment comment '审核流水id',
   OrgId                int(8) not null comment '组织id',
   BranchId             INT(10) comment '支部id',
   NewsId               INT(10) not null comment '新闻id',
   NewsType             CHAR(2) comment '审核类型
            00:文章
            10:任务
            20:活动
            ',
   OperId               INT(8) comment '审核用户',
   Result               NVARCHAR(200) comment '审核结果',
   CreateTime           DATETIME comment '审核时间',
   ReviewStatus         INT(2) comment '审核状态',
   primary key (ReviewId)
);

alter table TB_REVIEW comment '审核表';

/*==============================================================*/
/* Table: TB_REWARD_SCORE                                       */
/*==============================================================*/
create table TB_REWARD_SCORE
(
   RewardId             INT(8) not null auto_increment comment '奖励id',
   OrgId                int(8) not null comment '组织id',
   BranchId             INT(8) comment '支部id',
   RewardType           INT(2) not null comment '奖励类型;
            0:任务奖励
            1:活动奖励;
            2:字数奖励
            3:关键字奖励
            4:学习时间奖励',
   Score                INT(5) comment '奖励积分',
   keyWord              NVARCHAR(200) comment '关键字',
   LearnTime            INT(4) comment '学习时间;单位:分钟',
   WordCount            int(6) comment '字数',
   primary key (RewardId)
)
auto_increment = 10000000;

alter table TB_REWARD_SCORE comment '积分奖励配置表';

/*==============================================================*/
/* Table: TB_SCROLL_IMAGE                                       */
/*==============================================================*/
create table TB_SCROLL_IMAGE
(
   OrgId                int(8) not null comment '组织id',
   PageType             CHAR(2) not null comment '页面类型;
            0:首页;
            1:党建门户;
            2:党务工作;
            3:党建学习',
   ImageUrl             INT(8) comment '操作员id',
   primary key (OrgId, PageType)
);

alter table TB_SCROLL_IMAGE comment '滚动图片表';

/*==============================================================*/
/* Table: TB_SMS_CONFIG                                         */
/*==============================================================*/
create table TB_SMS_CONFIG
(
   VendorId             INT(8) not null auto_increment comment '短信提供方ID',
   VendorName           CHAR(100) comment '短信提供方名称',
   SendCount            INT(8) comment '已发送数量',
   CreateTime           DATETIME comment '创建时间',
   AppId                CHAR(255) comment '短信方appId',
   AppKey               CHAR(255) comment '短信方appKey',
   SginKey              CHAR(255) comment '签名key',
   ServiceUrl           CHAR(255) comment '接口地址',
   MaxCount             INT(8) comment '最大发送数量',
   Priority             INT(2) comment '优先级',
   Status               INT(2) comment '状态;0:正常:1:禁用',
   primary key (VendorId)
)
auto_increment = 10000000;

alter table TB_SMS_CONFIG comment '短信配置表';

/*==============================================================*/
/* Table: TB_SMS_SEND_LOG                                       */
/*==============================================================*/
create table TB_SMS_SEND_LOG
(
   OrgId                int(8) not null comment '组织id',
   TraceId              int(10) not null auto_increment comment '流水号',
   VendorId             INT(8) comment '短信提供方id',
   SmsContent           NVARCHAR(200) comment '短信内容',
   CreateTime           DATETIME comment '发送时间',
   Phone                CHAR(11) comment '发送手机',
   Status               INT(2) comment '发送状态;0:成功;1:失败',
   RequestMsg           NVARCHAR(1000) comment '请求内容',
   ResponseMsg          NVARCHAR(1000) comment '返回内容',
   primary key (TraceId)
);

alter table TB_SMS_SEND_LOG comment '短信发送日志表';

/*==============================================================*/
/* Table: TB_SYSTEM_NEWS                                        */
/*==============================================================*/
create table TB_SYSTEM_NEWS
(
   NewsId               INT(8) not null auto_increment comment '消息id',
   NewsName             NVARCHAR(100) comment '消息名称',
   NewsContent          NVARCHAR(100) comment '消息内容',
   NoticeTime           CHAR(8) comment '提示日期',
   Status               INT(2) comment '状态',
   OperId               INT(8) comment '操作员',
   RepeatTime           int(2) comment '重复类型;
            0:每年;
            1:仅1次',
   HtmlDesc             NVARCHAR(4000) comment 'HTML描述',
   NewsType             CHAR(2) comment '系统通知类型
            00:领袖诞辰
            10:系统通知',
   primary key (NewsId)
);

alter table TB_SYSTEM_NEWS comment '系统消息表';

/*==============================================================*/
/* Table: TB_TASK                                               */
/*==============================================================*/
create table TB_TASK
(
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   TaskId               INT(8) not null auto_increment comment '任务id',
   TaskName             NVARCHAR(100) comment '任务名称',
   FromUser             INT(10) comment '发起人',
   TaskContent          NVARCHAR(4000) comment '任务内容',
   BeginTime            DATETIME comment '开始时间',
   endTime              DATETIME comment '结束时间',
   Attch1               NVARCHAR(255) comment '附件1',
   Attch2               NVARCHAR(255) comment '附件2',
   Attch3               NVARCHAR(255) comment '附件3',
   Attch4               NVARCHAR(255) comment '附件4',
   CreateTime           DATETIME comment '发起时间',
   CanComment           INT(2) comment '是否允许评论;0:允许;1:禁止',
   Status               INT(2) comment '任务状态;
            0:发起状态;
            1:执行中
            2:完成任务待审核
            3:审核通过',
   IsPublic             INT(2) comment '是否公开
            0:公开;
            1:不公开',
   OrgReview            CHAR(2) comment '机构审核状态;
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   BranchReview         CHAR(2) comment '支部审核状态
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   primary key (TaskId)
);

alter table TB_TASK comment '任务表';

/*==============================================================*/
/* Table: TB_TEST_PAPER                                         */
/*==============================================================*/
create table TB_TEST_PAPER
(
   OrgId                int(8) not null comment '组织id',
   BranchId             INT(8) comment '支部id',
   TestId               INT(8) not null auto_increment comment '试卷id',
   TestName             NVARCHAR(20) comment '试卷名称',
   HasReward            INT(2) comment '是否奖励积分; 
            0:奖励;
            1:不奖励',
   QuestionSocre        INT(3) comment '每一题分值',
   PassScore            INT(5) comment '及格分值',
   HasLimitedTime       INT(2) comment '是否限制时间',
   LimitedTime          INT(5) comment '时间(分钟)',
   HtmlDescription      text comment 'html描述',
   OperId               int(8) comment '操作员id',
   CreateTime           DATETIME comment '创建时间',
   EditTime             DATETIME comment '修改时间',
   Status               INT(2) comment '状态;0;正常;1:禁用',
   primary key (TestId)
);

alter table TB_TEST_PAPER comment '试卷表';

/*==============================================================*/
/* Table: TB_TEST_QUESTION                                      */
/*==============================================================*/
create table TB_TEST_QUESTION
(
   OrgId                INT(8) comment '机构id',
   TestId               INT(8) not null comment '试卷id',
   QuestionId           INT(10) not null comment '试卷问题id',
   primary key (TestId, QuestionId)
);

alter table TB_TEST_QUESTION comment '试卷问题关联表';

/*==============================================================*/
/* Table: TB_USER_ACTIVITY                                      */
/*==============================================================*/
create table TB_USER_ACTIVITY
(
   orgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   ActivityId           INT(8) not null auto_increment comment '活动id',
   UserId               INT(10) not null comment '用户id',
   UserFeedBack         NVARCHAR(4000) comment '用户反馈',
   CreateTime           DATETIME comment '接受时间',
   Status               CHAR(2) comment '活动状态',
   CompleteTime         INT(8) comment '完成时间',
   RewardsScore         INT(5) comment '奖励积分',
   primary key (ActivityId, UserId)
);

alter table TB_USER_ACTIVITY comment '用户参与活动表';

/*==============================================================*/
/* Table: TB_USER_COMMENT                                       */
/*==============================================================*/
create table TB_USER_COMMENT
(
   OrgId                int(8) not null comment '组织id',
   MessageId            int(12) not null auto_increment comment '评论id',
   Content              NVARCHAR(200) not null comment '评论内容',
   CreateTime           DATETIME comment '评论时间',
   UserId               INT(10) comment '评论用户',
   ReviewStatus         INT(2) comment '审核状态;
            0:通过;
            1:未通过',
   ReviewUser           INT(8) comment '审核用户',
   ReviewTime           DATETIME comment '审核时间',
   primary key (MessageId)
);

alter table TB_USER_COMMENT comment '评论表';

/*==============================================================*/
/* Table: TB_USER_INFO                                          */
/*==============================================================*/
create table TB_USER_INFO
(
   OrgId                int(8) not null comment '组织id',
   BranchId             INT(8) comment '支部编号',
   UserId               int(10) not null auto_increment comment '用户id',
   Password             CHAR(255) comment '密码',
   Name                 NVARCHAR(20) comment '姓名',
   Sex                  INT(2) comment '性别;0:男;1:女',
   Ethinc               NVARCHAR(20) comment '民族',
   Birthday             DATETIME comment '出生日期',
   Origin               NVARCHAR(20) comment '籍贯',
   OriginAddress        NVARCHAR(200) comment '户籍地址',
   Phone                CHAR(15) comment '联系电话',
   IdNumber             CHAR(18) comment '身份证号',
   Eduction             NVARCHAR(20) comment '学历',
   School               NVARCHAR(50) comment '毕业学校',
   Provice              NVARCHAR(8) comment '省',
   City                 NVARCHAR(8) comment '市',
   District             NVARCHAR(8) comment '区',
   Address              NVARCHAR(200) comment '地址',
   Company              NVARCHAR(100) comment '工作单位',
   Dept                 NVARCHAR(20) comment '部门',
   Title                NVARCHAR(20) comment '职位',
   PreMemberTime        DATETIME comment '预备党员日期',
   MemberTime           DATETIME comment '正式党员日期',
   MemberType           INT(2) comment '党员类型;
            0:党员;
            1:预备党员',
   Status               INT(2) comment '用户状态;0:正常;1:禁用',
   MemberStatus         INT(2) comment '党员状态;0:正常;1:禁用',
   PhotoUrl             NVARCHAR(255) comment '头像',
   NickPhotoUrl         NVARCHAR(255) comment '昵称头像',
   CreateTime           DATETIME comment '创建时间',
   EditTime             datetime comment '修改时间',
   OperId               int(8) comment '操作员id',
   IsCustomer           int(2) comment '游客;
            0:是 ;
            1:否',
   primary key (UserId)
)
auto_increment = 10000000;

alter table TB_USER_INFO comment '用户表';

/*==============================================================*/
/* Table: TB_USER_NEWS                                          */
/*==============================================================*/
create table TB_USER_NEWS
(
   OrgId                int(8) not null comment '组织id',
   BranchId             INT(8) comment '支部id',
   NewsId               int(10) not null auto_increment comment '新闻id
            ',
   UserId               int(10) comment '用户id',
   Title                NVARCHAR(100) comment '新闻名称',
   HtmlContent          NVARCHAR(4000) comment '文章内容',
   NewsType             CHAR(2) comment '文章类型
            00:文章
            01:心得体会
            02:会议
            ',
   Status               CHAR(2) comment '状态;0:正常 -1:待审核;',
   CreateTime           DATETIME comment '创建日期',
   ViewsCount           INT(8) comment '阅读数量',
   RewardsScore         INT(8) comment '奖励积分',
   ReviewId             INT(10) comment '审核流水id',
   OrgReview            CHAR(2) comment '机构审核状态;
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   BranchReview         CHAR(2) comment '支部审核状态
            0:审核通过;
            -1:待审核
            1:审核不通过
            ',
   primary key (NewsId)
);

alter table TB_USER_NEWS comment '用户文章表';

/*==============================================================*/
/* Table: TB_USER_NOTICE                                        */
/*==============================================================*/
create table TB_USER_NOTICE
(
   OrgId                INT(8) comment '机构id',
   NewsId               INT(8) not null auto_increment comment '通知id',
   NewsType             INT(2) comment '通知类型',
   Title                CHAR(100) comment '标题',
   UserId               INT(10) comment '接收用户',
   Content              NVARCHAR(250) comment '通知内容',
   IsRead               INT(1) comment '是否已阅读;
            0:已阅读
            1:未阅读',
   FromUser             INT(10) comment '发送人',
   CreateTime           DATETIME comment '创建时间',
   ReadTime             DATETIME comment '阅读时间',
   primary key (NewsId)
);

alter table TB_USER_NOTICE comment '用户通知信息表
';

/*==============================================================*/
/* Table: TB_USER_RELATIONSHIP                                  */
/*==============================================================*/
create table TB_USER_RELATIONSHIP
(
   OrgId                int(8) not null comment '组织id',
   UserId               int(10) not null comment '领导id',
   SubUserId            int(10) comment '下级id',
   primary key (UserId)
);

alter table TB_USER_RELATIONSHIP comment '用户关系表';

/*==============================================================*/
/* Table: TB_USER_SCORE_TRACE                                   */
/*==============================================================*/
create table TB_USER_SCORE_TRACE
(
   OrgId                int(8) not null comment '组织id',
   UserId               INT(10) not null comment '参与用户id',
   TraceId              int not null auto_increment,
   CreateTime           DATETIME comment '创建时间',
   Score                INT(8) comment '获得积分',
   RewardsType          CHAR(2) comment '积分奖励类型;
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
   Comment              NVARCHAR(200) comment '描述',
   OriScore             INT(8) comment '原积分',
   primary key (TraceId)
);

alter table TB_USER_SCORE_TRACE comment '用户积分流水表';

/*==============================================================*/
/* Table: TB_USER_STUDY_LOG                                     */
/*==============================================================*/
create table TB_USER_STUDY_LOG
(
   OrgId                INT(8) comment '机构ID',
   BranchId             INT(8) comment '支部id',
   TraceId              INT(12) not null auto_increment comment '流水号',
   UserId               INT(8) comment '用户id',
   NewsId               INT(8) comment '新闻id',
   ModuleId             INT(2) comment '新闻栏目id',
   StudyId              INT(5) comment '学习时间;单位:分钟',
   CreateTime           DATETIME comment '创建时间',
   Score                INT(5) comment '奖励积分',
   primary key (TraceId)
);

alter table TB_USER_STUDY_LOG comment '用户文章学习流水表';

/*==============================================================*/
/* Table: TB_USER_TASK                                          */
/*==============================================================*/
create table TB_USER_TASK
(
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   TaskId               INT(8) not null comment '任务id',
   UserId               INT(10) not null comment '用户id',
   AcceptId             DATETIME comment '接受时间',
   Status               INT(2) comment '状态',
   RewardsScore         INT(5) comment '奖励积分',
   Comment              NVARCHAR(200) comment '评语',
   Score                INT(2) comment '打分;1~5分',
   primary key (TaskId, UserId)
);

alter table TB_USER_TASK comment '我的任务表';

/*==============================================================*/
/* Table: TB_USER_TASK_STEP                                     */
/*==============================================================*/
create table TB_USER_TASK_STEP
(
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   TaskId               INT(8) not null comment '任务id',
   UserId               INT(10) not null comment '用户id',
   StepId               INT(2) not null comment '步骤id',
   StepName             NVARCHAR(100) comment '步骤名称',
   CreateTime           DATETIME comment '步骤创建时间',
   Content              NVARCHAR(4000) comment '反馈',
   UpdateTime           DATETIME comment '反馈时间',
   primary key (TaskId, UserId, StepId)
);

alter table TB_USER_TASK_STEP comment '任务表步骤表';

/*==============================================================*/
/* Table: TB_USER_TEST                                          */
/*==============================================================*/
create table TB_USER_TEST
(
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   UserId               int(8) not null comment '用户id',
   TestId               INT(10) not null comment '试卷id',
   Score                INT(3) comment '试卷得分',
   TotalScore           INT(3) comment '试卷总分',
   CreateTime           DATETIME comment '答卷时间',
   UseTime              INT(5) comment '答卷用时',
   primary key (UserId, TestId)
);

alter table TB_USER_TEST comment '用户答题表';

/*==============================================================*/
/* Table: TB_USER_TEST_QUESTION                                 */
/*==============================================================*/
create table TB_USER_TEST_QUESTION
(
   OrgId                INT(8) comment '机构id',
   TestId               INT(8) not null comment '试卷id',
   UserId               int(8) not null comment '用户id',
   QuestionId           INT(10) not null comment '问题id',
   QuestionName         NVARCHAR(500) comment '问题名称',
   Answer1              CHAR(100) comment '答案1',
   Answer2              CHAR(100) comment '答案2',
   Answer3              CHAR(100) comment '答案3',
   Answer4              CHAR(100) comment '答案4',
   IsRight              INT(2) comment '是否正确;0:正确;1:错误',
   primary key (TestId, UserId, QuestionId)
);

alter table TB_USER_TEST_QUESTION comment '用户试卷答案表';

/*==============================================================*/
/* Table: TB_USER_TOKEN                                         */
/*==============================================================*/
create table TB_USER_TOKEN
(
   OrgId                INT(8) comment '机构id',
   BranchId             INT(8) comment '支部id',
   UserId               INT(8) comment '操作员id',
   Token                CHAR(255) not null comment '操作员token',
   CreateTime           DATETIME comment '创建时间',
   ExpireTime           DATETIME comment '过期时间',
   TokenType            INT(2) comment 'token类型;0:前端用户;1:后端操作员',
   primary key (Token)
);

alter table TB_USER_TOKEN comment '用户登录信息会话表';

/*==============================================================*/
/* Table: TB_VALIDCODE                                          */
/*==============================================================*/
create table TB_VALIDCODE
(
   OrgId                int(8) not null comment '组织id',
   TraceId              int(10) not null auto_increment comment '流水号',
   Phone                CHAR(15) not null comment '手机号码',
   ValidCode            INT(6) not null comment '验证码',
   Seq                  INT(2) comment '验证码序号',
   CreateTime           DATETIME comment '发送时间',
   ExpireTime           DATETIME comment '过期时间',
   primary key (TraceId)
);

alter table TB_VALIDCODE comment '验证码表';

