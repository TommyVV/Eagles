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
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   ActivityId           INT(8) not null auto_increment comment '�id',
   ActivityName         NVARCHAR(50) comment '�����',
   HtmlContent          NVARCHAR(4000) comment '�����',
   BeginTime            DATETIME comment '��ʼ����',
   EndTime              DATETIME comment '��ֹ����',
   FromUser             INT(10) comment '������',
   ActivityType         CHAR(2) comment '�����;0:����;1:ͶƱ;2:�ʾ����',
   MaxCount             INT(5) comment 'ÿ�����������',
   CanComment           INT(2) comment '�Ƿ���������;0:����;1:��ֹ',
   TestId               INT(8) comment '�ʾ�����Ծ�id',
   MaxUser              INT(5) comment '����������',
   Attach1              NVARCHAR(255) comment '����1',
   Attach2              NVARCHAR(255) comment '����2',
   Attach3              NVARCHAR(255) comment '����3',
   Attach4              NVARCHAR(255) comment '����4',
   AttachType1          char(2) comment '����1����;0:ͼƬ;1:����',
   AttachType2          char(2) comment '����1����;0:ͼƬ;1:����',
   AttachType3          char(2) comment '����1����;0:ͼƬ;1:����',
   AttachType4          char(2) comment '����1����;0:ͼƬ;1:����',
   ImageUrl             NVARCHAR(255) comment 'ͼƬ',
   IsPublic             CHAR(2) comment '�Ƿ񹫿�;
            0:����;
            1:������',
   OrgReview            CHAR(2) comment '�������״̬;
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   BranchReview         CHAR(2) comment '֧�����״̬
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   ToUserId             INT(8) comment '������Id',
   primary key (ActivityId)
)
auto_increment = 10000000;

alter table TB_ACTIVITY comment '���';

/*==============================================================*/
/* Table: TB_APP_MENU                                           */
/*==============================================================*/
create table TB_APP_MENU
(
   OrgId                int(8) not null comment '��֯id',
   MenuId               int(8) not null auto_increment comment '�˵�id
            �˵�id
            �˵�id',
   MenuName             NVARCHAR(20) comment '�˵�����',
   Level                CHAR(2) comment '�ȼ�;',
   ParentMenuId         INT(8) comment '�ϼ��˵�id',
   TragetUrl            NVARCHAR(255) comment '��ת����',
   primary key (MenuId)
);

alter table TB_APP_MENU comment '�˵���';

/*==============================================================*/
/* Table: TB_APP_MODULE                                         */
/*==============================================================*/
create table TB_APP_MODULE
(
   OrgId                int(8) not null comment '��֯id',
   ModuleId             int(8) not null auto_increment comment '�˵�id
            �˵�id
            �˵�id',
   ModuleName           NVARCHAR(20) comment '�˵�����',
   TragetUrl            NVARCHAR(255) comment '��ת����',
   ModuleType           INT(2) comment '����;
            0:��ҳ;
            1:�����Ż�;
            2:������;
            3:����ѧϰ',
   SmallImageUrl        NVARCHAR(255) comment 'Сͼ',
   ImageUrl             NVARCHAR(255) comment '��ͼ',
   Priority             INT(8) comment '���ȼ�',
   IndexPageCount       INT(2) comment '��ҳ��ʾ����',
   IndexDisplay         INT(2) comment '�Ƿ�����ҳ��ʾ',
   primary key (ModuleId)
);

alter table TB_APP_MODULE comment '��Ŀ��';

/*==============================================================*/
/* Table: TB_AUTHORITY                                          */
/*==============================================================*/
create table TB_AUTHORITY
(
   OrgId                int(8) not null comment '��֯id',
   GroupId              INT(8) not null comment 'Ȩ������',
   FunCode              NVARCHAR(8) not null comment '����id',
   OperId               int(8) comment '�޸��û�',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             datetime comment '�޸�ʱ��',
   primary key (GroupId, FunCode)
);

alter table TB_AUTHORITY comment 'Ȩ�޹����';

/*==============================================================*/
/* Table: TB_BRANCH                                             */
/*==============================================================*/
create table TB_BRANCH
(
   OrgId                int(8) not null comment '��֯id',
   BranchId             int(8) not null auto_increment comment '֧��id',
   BranchName           NVARCHAR(50) not null comment '֧������',
   BranchDesc           NVARCHAR(50) comment '֧������',
   CreateTime           DATETIME comment '����ʱ��',
   primary key (BranchId)
)
auto_increment = 10000000;

alter table TB_BRANCH comment '֧����';

/*==============================================================*/
/* Table: TB_IMPORT_LOG                                         */
/*==============================================================*/
create table TB_IMPORT_LOG
(
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   TraceId              INT(10) not null auto_increment comment '��ˮ��',
   OperId               INT(8) comment '����Ա',
   CreateTime           DATETIME comment '����ʱ��',
   Description          CHAR(100) comment '����',
   OperType             CHAR(2) comment '��������',
   Status               INT(2) comment '������;0:�ɹ�;1:ʧ��',
   primary key (TraceId)
);

alter table TB_IMPORT_LOG comment '�Ծ����������';

/*==============================================================*/
/* Table: TB_MEETING_USER                                       */
/*==============================================================*/
create table TB_MEETING_USER
(
   NewsId               int(8) not null comment '����id',
   OrgId                int(8) not null comment '��֯id',
   BranchId             int(10) comment '֧��id',
   UserId               INT(10) not null comment '�����û�id',
   primary key (NewsId, UserId)
);

alter table TB_MEETING_USER comment '���������Ա��';

/*==============================================================*/
/* Table: TB_NEWS                                               */
/*==============================================================*/
create table TB_NEWS
(
   OrgId                int(8) not null comment '��֯id',
   NewsId               int(10) not null auto_increment comment '����id
            ',
   ShortDesc            NVARCHAR(100) comment '������',
   Title                NVARCHAR(100) comment '��������',
   HtmlContent          NVARCHAR(4000) comment '��������',
   Author               NVARCHAR(20) comment '����',
   Source               NVARCHAR(20) comment '��Դ',
   Module               INT(8) comment '������Ŀ',
   Status               CHAR(2) comment '״̬;
            0:���� 
            -1:�����;',
   BeginTime            DATETIME comment '��ʼ����',
   EndTime              DATETIME comment '��������',
   TestId               INT(8) comment '�Ծ�id',
   Attach1              NVARCHAR(255) comment '����1',
   Attach2              NVARCHAR(255) comment '����2',
   Attach3              NVARCHAR(255) comment '����3',
   Attach4              NVARCHAR(255) comment '����4',
   Attach5              NVARCHAR(255) comment '����5',
   OperId               INT(8) comment '����Աid',
   CreateTime           DATETIME comment '��������',
   IsImage              INT(2) comment '��ͼƬ',
   IsVideo              INT(2) comment '����Ƶ',
   IsAttach             INT(2) comment '�и���',
   IsClass              INT(2) comment '�пμ�',
   IsLearning           INT(2) comment '��ѧϰ�ĵ�',
   IsText               INT(2) comment '������',
   ViewCount            INT(8) comment '�Ķ�����',
   ReviewId             INT(10) comment '���id',
   CanStudy             INT(2) comment '�Ƿ�����ѧϰ',
   ImageUrl             char(255) comment '����ͼ',
   primary key (NewsId)
);

alter table TB_NEWS comment '���ű�';

/*==============================================================*/
/* Table: TB_OPER                                               */
/*==============================================================*/
create table TB_OPER
(
   OrgId                int(8) not null comment '��֯id',
   OperId               int(8) not null auto_increment comment '����Ա���',
   OperName             NVARCHAR(20) comment '����Ա����',
   CreateTime           DATETIME comment '����ʱ��',
   GroupId              INT(8) comment 'Ȩ������',
   Status               INT(2) comment '״̬;0:����;1:����',
   Password             CHAR(100) comment '����',
   primary key (OperId)
)
auto_increment = 10000000;

alter table TB_OPER comment '����Ա��';

/*==============================================================*/
/* Table: TB_OPER_GROUP                                         */
/*==============================================================*/
create table TB_OPER_GROUP
(
   OrgId                int(8) not null comment '��֯id',
   GroupId              INT(8) not null auto_increment comment 'Ȩ������',
   GroupName            NVARCHAR(8) comment '����������',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             datetime comment '�޸�ʱ��',
   primary key (GroupId)
);

alter table TB_OPER_GROUP comment '����Ա�������';

/*==============================================================*/
/* Table: TB_ORDER                                              */
/*==============================================================*/
create table TB_ORDER
(
   OrgId                int(8) not null comment '��֯id',
   OrderId              int(12) not null auto_increment comment '�������',
   ProdId               INT(8) not null comment '��Ʒ���',
   ProdName             NVARCHAR(200) comment '��Ʒ����',
   OrderStatus          INT(2) comment '����״̬;
            0:�ɹ�
            1:ʧ��',
   Score                INT(8) comment '֧������',
   Count                INT(5) comment '����',
   UserId               INT(10) comment '�û�id',
   ExpressId            NVARCHAR(500) comment '���id',
   Address              NVARCHAR(200) comment '�������͵�ַ',
   Province             NVARCHAR(15) comment 'ʡ',
   City                 CHAR(20) comment '��',
   District             CHAR(20) comment '��',
   CreateTime           DATETIME comment '����ʱ��',
   UpdateTime           DATETIME comment '�޸�ʱ��',
   OperId               INT(8) comment '����Աid',
   primary key (OrderId)
);

alter table TB_ORDER comment '������';

/*==============================================================*/
/* Table: TB_ORG_INFO                                           */
/*==============================================================*/
create table TB_ORG_INFO
(
   OrgId                int(8) not null auto_increment comment '��֯id',
   OrgName              INT(8) comment '��������',
   Province             NVARCHAR(8) comment 'ʡ',
   City                 NVARCHAR(8) comment '��',
   District             NVARCHAR(8) comment '��',
   Address              NVARCHAR(8) comment '��ַ',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             datetime comment '�޸�ʱ��',
   OperId               INT(8) comment '����Ա',
   Logo                 NVARCHAR(255) comment '��֯logo',
   primary key (OrgId)
)
auto_increment = 10000000;

alter table TB_ORG_INFO comment '����֯��ϵ��';

/*==============================================================*/
/* Table: TB_ORG_RELATIONSHIP                                   */
/*==============================================================*/
create table TB_ORG_RELATIONSHIP
(
   OrgId                int(8) not null comment '��֯id',
   SubOrgId             int(8) not null comment '��������id',
   OperId               INT(8) comment '����Աid',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             DATETIME comment '�޸�ʱ��',
   primary key (OrgId, SubOrgId)
);

alter table TB_ORG_RELATIONSHIP comment '��֯������ϵ��';

/*==============================================================*/
/* Table: TB_ORG_SMS_CONFIG                                     */
/*==============================================================*/
create table TB_ORG_SMS_CONFIG
(
   OrgId                INT(8) not null comment '����id',
   VendorId             INT(8) not null comment '�����ṩ��id',
   MaxCount             INT(8) comment '���ɷ�������',
   SendCount            INT(8) comment '�ѷ�������',
   CreateTime           DATETIME comment '����޸�ʱ��',
   OperId               INT(8) comment '����޸���',
   Priority             INT(2) comment '���ȼ�',
   Status               INT(2) comment '״̬;0:����;1:����',
   primary key (OrgId, VendorId)
);

alter table TB_ORG_SMS_CONFIG comment '�����������ñ�';

/*==============================================================*/
/* Table: TB_PRODUCT                                            */
/*==============================================================*/
create table TB_PRODUCT
(
   ProdId               int(8) not null auto_increment comment '��Ʒid',
   OrgId                int(8) not null comment '��֯id',
   ProdName             NVARCHAR(200) comment '��Ʒ����',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             DATETIME comment '�޸�ʱ��',
   Price                decimal(8,2) comment '��Ʒ��ֵ',
   Score                INT(8) comment '�������',
   Stock                INT(8) comment '���',
   SmallImageUrl        NVARCHAR(255) comment 'Сͼ',
   ImageUrl             NVARCHAR(255) comment '��ͼ',
   MaxBuyCount          INT(5) comment 'ÿ�����������;0:����',
   SaleCount            INT(5) comment '��������',
   BeginTime            DATETIME comment '��Ʒ�ϼ�ʱ��',
   EndTime              DATETIME comment '��Ʒ�¼�ʱ��',
   HtmlDescription      TEXT comment '��Ʒ����',
   Status               INT(2) comment '��Ʒ״̬;0:����:1:����',
   primary key (ProdId)
)
auto_increment = 10000000;

alter table TB_PRODUCT comment '��Ʒ��';

/*==============================================================*/
/* Table: TB_QUESTION                                           */
/*==============================================================*/
create table TB_QUESTION
(
   OrgId                INT(8) comment '����ID',
   QuestionId           INT(10) not null auto_increment comment '����id',
   Question             NVARCHAR(500) comment '����',
   Answer               INT(2) comment '������;0:Ĭ��:1:�Զ���',
   Multiple             INT(2) comment '�Ƿ������ѡ;0:��;1:��',
   MultipleCount        INT(2) comment '��ѡ����',
   primary key (QuestionId)
);

alter table TB_QUESTION comment 'ϰ���';

/*==============================================================*/
/* Table: TB_QUEST_ANWSER                                       */
/*==============================================================*/
create table TB_QUEST_ANWSER
(
   OrgId                INT(8) comment '����id',
   QuestionId           INT(10) not null comment '������',
   AnswerId             INT(10) not null auto_increment comment 'ѡ����',
   Answer               NVARCHAR(100) comment 'ѡ��',
   AnswerType           INT(2) comment '������;
            0:Ĭ��
            1:�Զ���',
   IsRight              INT(2) comment '���Ƿ�����ȷ��;
            0;��;
            1:��',
   ImageUrl             NVARCHAR(255) comment 'ѡ��ͼƬ',
   primary key (AnswerId)
);

alter table TB_QUEST_ANWSER comment '����𰸱�';

/*==============================================================*/
/* Table: TB_REVIEW                                             */
/*==============================================================*/
create table TB_REVIEW
(
   ReviewId             int(10) not null auto_increment comment '�����ˮid',
   OrgId                int(8) not null comment '��֯id',
   BranchId             INT(10) comment '֧��id',
   NewsId               INT(10) not null comment '����id',
   NewsType             CHAR(2) comment '�������
            00:����
            10:����
            20:�
            ',
   OperId               INT(8) comment '����û�',
   Result               NVARCHAR(200) comment '��˽��',
   CreateTime           DATETIME comment '���ʱ��',
   ReviewStatus         INT(2) comment '���״̬',
   primary key (ReviewId)
);

alter table TB_REVIEW comment '��˱�';

/*==============================================================*/
/* Table: TB_REWARD_SCORE                                       */
/*==============================================================*/
create table TB_REWARD_SCORE
(
   RewardId             INT(8) not null auto_increment comment '����id',
   OrgId                int(8) not null comment '��֯id',
   BranchId             INT(8) comment '֧��id',
   RewardType           INT(2) not null comment '��������;
            0:������
            1:�����;
            2:��������
            3:�ؼ��ֽ���
            4:ѧϰʱ�佱��',
   Score                INT(5) comment '��������',
   keyWord              NVARCHAR(200) comment '�ؼ���',
   LearnTime            INT(4) comment 'ѧϰʱ��;��λ:����',
   WordCount            int(6) comment '����',
   primary key (RewardId)
)
auto_increment = 10000000;

alter table TB_REWARD_SCORE comment '���ֽ������ñ�';

/*==============================================================*/
/* Table: TB_SCROLL_IMAGE                                       */
/*==============================================================*/
create table TB_SCROLL_IMAGE
(
   OrgId                int(8) not null comment '��֯id',
   PageType             CHAR(2) not null comment 'ҳ������;
            0:��ҳ;
            1:�����Ż�;
            2:������;
            3:����ѧϰ',
   ImageUrl             INT(8) comment '����Աid',
   primary key (OrgId, PageType)
);

alter table TB_SCROLL_IMAGE comment '����ͼƬ��';

/*==============================================================*/
/* Table: TB_SMS_CONFIG                                         */
/*==============================================================*/
create table TB_SMS_CONFIG
(
   VendorId             INT(8) not null auto_increment comment '�����ṩ��ID',
   VendorName           CHAR(100) comment '�����ṩ������',
   SendCount            INT(8) comment '�ѷ�������',
   CreateTime           DATETIME comment '����ʱ��',
   AppId                CHAR(255) comment '���ŷ�appId',
   AppKey               CHAR(255) comment '���ŷ�appKey',
   SginKey              CHAR(255) comment 'ǩ��key',
   ServiceUrl           CHAR(255) comment '�ӿڵ�ַ',
   MaxCount             INT(8) comment '���������',
   Priority             INT(2) comment '���ȼ�',
   Status               INT(2) comment '״̬;0:����:1:����',
   primary key (VendorId)
)
auto_increment = 10000000;

alter table TB_SMS_CONFIG comment '�������ñ�';

/*==============================================================*/
/* Table: TB_SMS_SEND_LOG                                       */
/*==============================================================*/
create table TB_SMS_SEND_LOG
(
   OrgId                int(8) not null comment '��֯id',
   TraceId              int(10) not null auto_increment comment '��ˮ��',
   VendorId             INT(8) comment '�����ṩ��id',
   SmsContent           NVARCHAR(200) comment '��������',
   CreateTime           DATETIME comment '����ʱ��',
   Phone                CHAR(11) comment '�����ֻ�',
   Status               INT(2) comment '����״̬;0:�ɹ�;1:ʧ��',
   RequestMsg           NVARCHAR(1000) comment '��������',
   ResponseMsg          NVARCHAR(1000) comment '��������',
   primary key (TraceId)
);

alter table TB_SMS_SEND_LOG comment '���ŷ�����־��';

/*==============================================================*/
/* Table: TB_SYSTEM_NEWS                                        */
/*==============================================================*/
create table TB_SYSTEM_NEWS
(
   NewsId               INT(8) not null auto_increment comment '��Ϣid',
   NewsName             NVARCHAR(100) comment '��Ϣ����',
   NewsContent          NVARCHAR(100) comment '��Ϣ����',
   NoticeTime           CHAR(8) comment '��ʾ����',
   Status               INT(2) comment '״̬',
   OperId               INT(8) comment '����Ա',
   RepeatTime           int(2) comment '�ظ�����;
            0:ÿ��;
            1:��1��',
   HtmlDesc             NVARCHAR(4000) comment 'HTML����',
   NewsType             CHAR(2) comment 'ϵͳ֪ͨ����
            00:���䵮��
            10:ϵͳ֪ͨ',
   primary key (NewsId)
);

alter table TB_SYSTEM_NEWS comment 'ϵͳ��Ϣ��';

/*==============================================================*/
/* Table: TB_TASK                                               */
/*==============================================================*/
create table TB_TASK
(
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   TaskId               INT(8) not null auto_increment comment '����id',
   TaskName             NVARCHAR(100) comment '��������',
   FromUser             INT(10) comment '������',
   TaskContent          NVARCHAR(4000) comment '��������',
   BeginTime            DATETIME comment '��ʼʱ��',
   endTime              DATETIME comment '����ʱ��',
   Attch1               NVARCHAR(255) comment '����1',
   Attch2               NVARCHAR(255) comment '����2',
   Attch3               NVARCHAR(255) comment '����3',
   Attch4               NVARCHAR(255) comment '����4',
   CreateTime           DATETIME comment '����ʱ��',
   CanComment           INT(2) comment '�Ƿ���������;0:����;1:��ֹ',
   Status               INT(2) comment '����״̬;
            0:����״̬;
            1:ִ����
            2:�����������
            3:���ͨ��',
   IsPublic             INT(2) comment '�Ƿ񹫿�
            0:����;
            1:������',
   OrgReview            CHAR(2) comment '�������״̬;
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   BranchReview         CHAR(2) comment '֧�����״̬
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   primary key (TaskId)
);

alter table TB_TASK comment '�����';

/*==============================================================*/
/* Table: TB_TEST_PAPER                                         */
/*==============================================================*/
create table TB_TEST_PAPER
(
   OrgId                int(8) not null comment '��֯id',
   BranchId             INT(8) comment '֧��id',
   TestId               INT(8) not null auto_increment comment '�Ծ�id',
   TestName             NVARCHAR(20) comment '�Ծ�����',
   HasReward            INT(2) comment '�Ƿ�������; 
            0:����;
            1:������',
   QuestionSocre        INT(3) comment 'ÿһ���ֵ',
   PassScore            INT(5) comment '�����ֵ',
   HasLimitedTime       INT(2) comment '�Ƿ�����ʱ��',
   LimitedTime          INT(5) comment 'ʱ��(����)',
   HtmlDescription      text comment 'html����',
   OperId               int(8) comment '����Աid',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             DATETIME comment '�޸�ʱ��',
   Status               INT(2) comment '״̬;0;����;1:����',
   primary key (TestId)
);

alter table TB_TEST_PAPER comment '�Ծ��';

/*==============================================================*/
/* Table: TB_TEST_QUESTION                                      */
/*==============================================================*/
create table TB_TEST_QUESTION
(
   OrgId                INT(8) comment '����id',
   TestId               INT(8) not null comment '�Ծ�id',
   QuestionId           INT(10) not null comment '�Ծ�����id',
   primary key (TestId, QuestionId)
);

alter table TB_TEST_QUESTION comment '�Ծ����������';

/*==============================================================*/
/* Table: TB_USER_ACTIVITY                                      */
/*==============================================================*/
create table TB_USER_ACTIVITY
(
   orgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   ActivityId           INT(8) not null auto_increment comment '�id',
   UserId               INT(10) not null comment '�û�id',
   UserFeedBack         NVARCHAR(4000) comment '�û�����',
   CreateTime           DATETIME comment '����ʱ��',
   Status               CHAR(2) comment '�״̬',
   CompleteTime         INT(8) comment '���ʱ��',
   RewardsScore         INT(5) comment '��������',
   primary key (ActivityId, UserId)
);

alter table TB_USER_ACTIVITY comment '�û�������';

/*==============================================================*/
/* Table: TB_USER_COMMENT                                       */
/*==============================================================*/
create table TB_USER_COMMENT
(
   OrgId                int(8) not null comment '��֯id',
   MessageId            int(12) not null auto_increment comment '����id',
   Content              NVARCHAR(200) not null comment '��������',
   CreateTime           DATETIME comment '����ʱ��',
   UserId               INT(10) comment '�����û�',
   ReviewStatus         INT(2) comment '���״̬;
            0:ͨ��;
            1:δͨ��',
   ReviewUser           INT(8) comment '����û�',
   ReviewTime           DATETIME comment '���ʱ��',
   primary key (MessageId)
);

alter table TB_USER_COMMENT comment '���۱�';

/*==============================================================*/
/* Table: TB_USER_INFO                                          */
/*==============================================================*/
create table TB_USER_INFO
(
   OrgId                int(8) not null comment '��֯id',
   BranchId             INT(8) comment '֧�����',
   UserId               int(10) not null auto_increment comment '�û�id',
   Password             CHAR(255) comment '����',
   Name                 NVARCHAR(20) comment '����',
   Sex                  INT(2) comment '�Ա�;0:��;1:Ů',
   Ethinc               NVARCHAR(20) comment '����',
   Birthday             DATETIME comment '��������',
   Origin               NVARCHAR(20) comment '����',
   OriginAddress        NVARCHAR(200) comment '������ַ',
   Phone                CHAR(15) comment '��ϵ�绰',
   IdNumber             CHAR(18) comment '���֤��',
   Eduction             NVARCHAR(20) comment 'ѧ��',
   School               NVARCHAR(50) comment '��ҵѧУ',
   Provice              NVARCHAR(8) comment 'ʡ',
   City                 NVARCHAR(8) comment '��',
   District             NVARCHAR(8) comment '��',
   Address              NVARCHAR(200) comment '��ַ',
   Company              NVARCHAR(100) comment '������λ',
   Dept                 NVARCHAR(20) comment '����',
   Title                NVARCHAR(20) comment 'ְλ',
   PreMemberTime        DATETIME comment 'Ԥ����Ա����',
   MemberTime           DATETIME comment '��ʽ��Ա����',
   MemberType           INT(2) comment '��Ա����;
            0:��Ա;
            1:Ԥ����Ա',
   Status               INT(2) comment '�û�״̬;0:����;1:����',
   MemberStatus         INT(2) comment '��Ա״̬;0:����;1:����',
   PhotoUrl             NVARCHAR(255) comment 'ͷ��',
   NickPhotoUrl         NVARCHAR(255) comment '�ǳ�ͷ��',
   CreateTime           DATETIME comment '����ʱ��',
   EditTime             datetime comment '�޸�ʱ��',
   OperId               int(8) comment '����Աid',
   IsCustomer           int(2) comment '�ο�;
            0:�� ;
            1:��',
   primary key (UserId)
)
auto_increment = 10000000;

alter table TB_USER_INFO comment '�û���';

/*==============================================================*/
/* Table: TB_USER_NEWS                                          */
/*==============================================================*/
create table TB_USER_NEWS
(
   OrgId                int(8) not null comment '��֯id',
   BranchId             INT(8) comment '֧��id',
   NewsId               int(10) not null auto_increment comment '����id
            ',
   UserId               int(10) comment '�û�id',
   Title                NVARCHAR(100) comment '��������',
   HtmlContent          NVARCHAR(4000) comment '��������',
   NewsType             CHAR(2) comment '��������
            00:����
            01:�ĵ����
            02:����
            ',
   Status               CHAR(2) comment '״̬;0:���� -1:�����;',
   CreateTime           DATETIME comment '��������',
   ViewsCount           INT(8) comment '�Ķ�����',
   RewardsScore         INT(8) comment '��������',
   ReviewId             INT(10) comment '�����ˮid',
   OrgReview            CHAR(2) comment '�������״̬;
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   BranchReview         CHAR(2) comment '֧�����״̬
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   primary key (NewsId)
);

alter table TB_USER_NEWS comment '�û����±�';

/*==============================================================*/
/* Table: TB_USER_NOTICE                                        */
/*==============================================================*/
create table TB_USER_NOTICE
(
   OrgId                INT(8) comment '����id',
   NewsId               INT(8) not null auto_increment comment '֪ͨid',
   NewsType             INT(2) comment '֪ͨ����',
   Title                CHAR(100) comment '����',
   UserId               INT(10) comment '�����û�',
   Content              NVARCHAR(250) comment '֪ͨ����',
   IsRead               INT(1) comment '�Ƿ����Ķ�;
            0:���Ķ�
            1:δ�Ķ�',
   FromUser             INT(10) comment '������',
   CreateTime           DATETIME comment '����ʱ��',
   ReadTime             DATETIME comment '�Ķ�ʱ��',
   primary key (NewsId)
);

alter table TB_USER_NOTICE comment '�û�֪ͨ��Ϣ��
';

/*==============================================================*/
/* Table: TB_USER_RELATIONSHIP                                  */
/*==============================================================*/
create table TB_USER_RELATIONSHIP
(
   OrgId                int(8) not null comment '��֯id',
   UserId               int(10) not null comment '�쵼id',
   SubUserId            int(10) comment '�¼�id',
   primary key (UserId)
);

alter table TB_USER_RELATIONSHIP comment '�û���ϵ��';

/*==============================================================*/
/* Table: TB_USER_SCORE_TRACE                                   */
/*==============================================================*/
create table TB_USER_SCORE_TRACE
(
   OrgId                int(8) not null comment '��֯id',
   UserId               INT(10) not null comment '�����û�id',
   TraceId              int not null auto_increment,
   CreateTime           DATETIME comment '����ʱ��',
   Score                INT(8) comment '��û���',
   RewardsType          CHAR(2) comment '���ֽ�������;
            00:�������½���
            01:������������
            02:���¹ؼ��ֽ���
            10:�μӻ����
            11:�����֧������
            12:�������֯����
            20:������ɽ���
            21:�������֧������
            22:���������֯����
            30:�������½���
            40:�ĵ�������ͽ���
            ',
   Comment              NVARCHAR(200) comment '����',
   OriScore             INT(8) comment 'ԭ����',
   primary key (TraceId)
);

alter table TB_USER_SCORE_TRACE comment '�û�������ˮ��';

/*==============================================================*/
/* Table: TB_USER_STUDY_LOG                                     */
/*==============================================================*/
create table TB_USER_STUDY_LOG
(
   OrgId                INT(8) comment '����ID',
   BranchId             INT(8) comment '֧��id',
   TraceId              INT(12) not null auto_increment comment '��ˮ��',
   UserId               INT(8) comment '�û�id',
   NewsId               INT(8) comment '����id',
   ModuleId             INT(2) comment '������Ŀid',
   StudyId              INT(5) comment 'ѧϰʱ��;��λ:����',
   CreateTime           DATETIME comment '����ʱ��',
   Score                INT(5) comment '��������',
   primary key (TraceId)
);

alter table TB_USER_STUDY_LOG comment '�û�����ѧϰ��ˮ��';

/*==============================================================*/
/* Table: TB_USER_TASK                                          */
/*==============================================================*/
create table TB_USER_TASK
(
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   TaskId               INT(8) not null comment '����id',
   UserId               INT(10) not null comment '�û�id',
   AcceptId             DATETIME comment '����ʱ��',
   Status               INT(2) comment '״̬',
   RewardsScore         INT(5) comment '��������',
   Comment              NVARCHAR(200) comment '����',
   Score                INT(2) comment '���;1~5��',
   primary key (TaskId, UserId)
);

alter table TB_USER_TASK comment '�ҵ������';

/*==============================================================*/
/* Table: TB_USER_TASK_STEP                                     */
/*==============================================================*/
create table TB_USER_TASK_STEP
(
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   TaskId               INT(8) not null comment '����id',
   UserId               INT(10) not null comment '�û�id',
   StepId               INT(2) not null comment '����id',
   StepName             NVARCHAR(100) comment '��������',
   CreateTime           DATETIME comment '���贴��ʱ��',
   Content              NVARCHAR(4000) comment '����',
   UpdateTime           DATETIME comment '����ʱ��',
   primary key (TaskId, UserId, StepId)
);

alter table TB_USER_TASK_STEP comment '��������';

/*==============================================================*/
/* Table: TB_USER_TEST                                          */
/*==============================================================*/
create table TB_USER_TEST
(
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   UserId               int(8) not null comment '�û�id',
   TestId               INT(10) not null comment '�Ծ�id',
   Score                INT(3) comment '�Ծ�÷�',
   TotalScore           INT(3) comment '�Ծ��ܷ�',
   CreateTime           DATETIME comment '���ʱ��',
   UseTime              INT(5) comment '�����ʱ',
   primary key (UserId, TestId)
);

alter table TB_USER_TEST comment '�û������';

/*==============================================================*/
/* Table: TB_USER_TEST_QUESTION                                 */
/*==============================================================*/
create table TB_USER_TEST_QUESTION
(
   OrgId                INT(8) comment '����id',
   TestId               INT(8) not null comment '�Ծ�id',
   UserId               int(8) not null comment '�û�id',
   QuestionId           INT(10) not null comment '����id',
   QuestionName         NVARCHAR(500) comment '��������',
   Answer1              CHAR(100) comment '��1',
   Answer2              CHAR(100) comment '��2',
   Answer3              CHAR(100) comment '��3',
   Answer4              CHAR(100) comment '��4',
   IsRight              INT(2) comment '�Ƿ���ȷ;0:��ȷ;1:����',
   primary key (TestId, UserId, QuestionId)
);

alter table TB_USER_TEST_QUESTION comment '�û��Ծ�𰸱�';

/*==============================================================*/
/* Table: TB_USER_TOKEN                                         */
/*==============================================================*/
create table TB_USER_TOKEN
(
   OrgId                INT(8) comment '����id',
   BranchId             INT(8) comment '֧��id',
   UserId               INT(8) comment '����Աid',
   Token                CHAR(255) not null comment '����Աtoken',
   CreateTime           DATETIME comment '����ʱ��',
   ExpireTime           DATETIME comment '����ʱ��',
   TokenType            INT(2) comment 'token����;0:ǰ���û�;1:��˲���Ա',
   primary key (Token)
);

alter table TB_USER_TOKEN comment '�û���¼��Ϣ�Ự��';

/*==============================================================*/
/* Table: TB_VALIDCODE                                          */
/*==============================================================*/
create table TB_VALIDCODE
(
   OrgId                int(8) not null comment '��֯id',
   TraceId              int(10) not null auto_increment comment '��ˮ��',
   Phone                CHAR(15) not null comment '�ֻ�����',
   ValidCode            INT(6) not null comment '��֤��',
   Seq                  INT(2) comment '��֤�����',
   CreateTime           DATETIME comment '����ʱ��',
   ExpireTime           DATETIME comment '����ʱ��',
   primary key (TraceId)
);

alter table TB_VALIDCODE comment '��֤���';

