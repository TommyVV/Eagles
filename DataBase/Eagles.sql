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
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   ACTIVITY_ID          INT(8) not null auto_increment comment '�id',
   ACTIVITY_NAME        NVARCHAR(50) comment '�����',
   HTML_CONTENT         NVARCHAR(4000) comment '�����',
   BEGIN_TIME           DATETIME comment '��ʼ����',
   END_TIME             DATETIME comment '��ֹ����',
   FROM_USER            INT(10) comment '������',
   ACTIVITY_TYPE        CHAR(2) comment '�����;0:����;1:ͶƱ;2:�ʾ����',
   MAX_COUNT            INT(5) comment 'ÿ�����������',
   CAN_COMMENT          INT(2) comment '�Ƿ���������;0:����;1:��ֹ',
   TEST_ID              INT(8) comment '�ʾ�����Ծ�id',
   MAX_USER             INT(5) comment '����������',
   ATTACH1              NVARCHAR(255) comment '����1',
   ATTACH2              NVARCHAR(255) comment '����2',
   ATTACH3              NVARCHAR(255) comment '����3',
   ATTACH4              NVARCHAR(255) comment '����4',
   IMAGE_URL            NVARCHAR(255) comment 'ͼƬ',
   IS_PUBLIC            CHAR(2) comment '�Ƿ񹫿�;
            0:����;
            1:������',
   ORG_REVIEW           CHAR(2) comment '�������״̬;
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   BRANCH_REVIEW        CHAR(2) comment '֧�����״̬
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   TO_USER_ID           INT(8) comment '������Id',
   primary key (ACTIVITY_ID)
)
auto_increment = 10000000;

alter table TB_ACTIVITY comment '���';

/*==============================================================*/
/* Table: TB_APP_MENU                                           */
/*==============================================================*/
create table TB_APP_MENU
(
   ORG_ID               int(8) not null comment '��֯id',
   MENU_ID              int(8) not null auto_increment comment '�˵�id
            �˵�id
            �˵�id',
   MENU_NAME            NVARCHAR(20) comment '�˵�����',
   LEVEL                CHAR(2) comment '�ȼ�;',
   PARENT_MENU_ID       INT(8) comment '�ϼ��˵�id',
   TARGET_URL           NVARCHAR(255) comment '��ת����',
   primary key (MENU_ID)
);

alter table TB_APP_MENU comment '�˵���';

/*==============================================================*/
/* Table: TB_APP_MODULE                                         */
/*==============================================================*/
create table TB_APP_MODULE
(
   ORG_ID               int(8) not null comment '��֯id',
   MODULE_ID            int(8) not null auto_increment comment '�˵�id
            �˵�id
            �˵�id',
   MODULE_NAME          NVARCHAR(20) comment '�˵�����',
   TARGET_URL           NVARCHAR(255) comment '��ת����',
   MODULE_TYPE          INT(2) comment '����;
            0:��ҳ;
            1:�����Ż�;
            2:������;
            3:����ѧϰ',
   SMALL_IMAGE_URL      NVARCHAR(255) comment 'Сͼ',
   IMAGE_URL            NVARCHAR(255) comment '��ͼ',
   PRIORITY             INT(8) comment '���ȼ�',
   INDEX_PAGE_NUMBER    INT(2) comment '��ҳ��ʾ����',
   INDEX_DISPLAY        INT(2) comment '�Ƿ�����ҳ��ʾ',
   primary key (MODULE_ID)
);

alter table TB_APP_MODULE comment '��Ŀ��';

/*==============================================================*/
/* Table: TB_AUTHORITY                                          */
/*==============================================================*/
create table TB_AUTHORITY
(
   ORG_ID               int(8) not null comment '��֯id',
   GROUP_ID             INT(8) not null comment 'Ȩ������',
   FUN_CODE             NVARCHAR(8) not null comment '����id',
   OPER_ID              int(8) comment '�޸��û�',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            datetime comment '�޸�ʱ��',
   primary key (GROUP_ID, FUN_CODE)
);

alter table TB_AUTHORITY comment 'Ȩ�޹����';

/*==============================================================*/
/* Table: TB_BRANCH                                             */
/*==============================================================*/
create table TB_BRANCH
(
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            int(8) not null auto_increment comment '֧��id',
   BRANCH_NAME          NVARCHAR(50) not null comment '֧������',
   BRANCH_DESC          NVARCHAR(50) comment '֧������',
   CREATE_TIME          DATETIME comment '����ʱ��',
   primary key (BRANCH_ID)
)
auto_increment = 10000000;

alter table TB_BRANCH comment '֧����';

/*==============================================================*/
/* Table: TB_IMPORT_LOG                                         */
/*==============================================================*/
create table TB_IMPORT_LOG
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   TRACE_ID             INT(10) not null auto_increment comment '��ˮ��',
   OPER_ID              INT(8) comment '����Ա',
   CREATE_TIME          DATETIME comment '����ʱ��',
   DESCRIPTION          CHAR(100) comment '����',
   OPER_TYPE            CHAR(2) comment '��������',
   STATUS               INT(2) comment '������;0:�ɹ�;1:ʧ��',
   primary key (TRACE_ID)
)
auto_increment = ?;

alter table TB_IMPORT_LOG comment '�Ծ����������';

/*==============================================================*/
/* Table: TB_MEETING_USER                                       */
/*==============================================================*/
create table TB_MEETING_USER
(
   NEWS_ID              int(8) not null comment '����id',
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            int(10) comment '֧��id',
   USER_ID              INT(10) not null comment '�����û�id',
   primary key (NEWS_ID, USER_ID)
);

alter table TB_MEETING_USER comment '���������Ա��';

/*==============================================================*/
/* Table: TB_NEWS                                               */
/*==============================================================*/
create table TB_NEWS
(
   ORG_ID               int(8) not null comment '��֯id',
   NEWS_ID              int(10) not null auto_increment comment '����id
            ',
   SHORT_DESC           NVARCHAR(100) comment '������',
   TITLE                NVARCHAR(100) comment '��������',
   HTML_CONTENT         NVARCHAR(4000) comment '��������',
   AUTHOR               NVARCHAR(20) comment '����',
   SOURCE               NVARCHAR(20) comment '��Դ',
   MODULE_ID            INT(8) comment '������Ŀ',
   STATUS               CHAR(2) comment '״̬;
            0:���� 
            -1:�����;',
   BEGIN_TIME           DATETIME comment '��ʼ����',
   END_TIME             DATETIME comment '��������',
   TEST_ID              INT(8) comment '�Ծ�id',
   ATTACH1              NVARCHAR(255) comment '����1',
   ATTACH2              NVARCHAR(255) comment '����2',
   ATTACH3              NVARCHAR(255) comment '����3',
   ATTACH4              NVARCHAR(255) comment '����4',
   ATTACH5              NVARCHAR(255) comment '����5',
   OPER_ID              INT(8) comment '����Աid',
   CREATE_TIME          DATETIME comment '��������',
   IS_IMAGE             INT(2) comment '��ͼƬ',
   IS_VIDEO             INT(2) comment '����Ƶ',
   IS_ATTACH            INT(2) comment '�и���',
   IS_CLASS             INT(2) comment '�пμ�',
   IS_LEARNING          INT(2) comment '��ѧϰ�ĵ�',
   IS_ARTICLE           INT(2) comment '������',
   VIEWS_COUNT          INT(8) comment '�Ķ�����',
   REVIEW_ID            INT(10) comment '���id',
   CAN_STUDY            INT(2) comment '�Ƿ�����ѧϰ',
   IMAGE_URL            char(255) comment '����ͼ',
   primary key (NEWS_ID)
);

alter table TB_NEWS comment '���ű�';

/*==============================================================*/
/* Table: TB_OPER                                               */
/*==============================================================*/
create table TB_OPER
(
   ORG_ID               int(8) not null comment '��֯id',
   OPER_ID              int(8) not null auto_increment comment '����Ա���',
   OPER_NAME            NVARCHAR(20) comment '����Ա����',
   CREATE_TIME          DATETIME comment '����ʱ��',
   GROUP_ID             INT(8) comment 'Ȩ������',
   STATUS               INT(2) comment '״̬;0:����;1:����',
   PASSWORD             CHAR(100) comment '����',
   primary key (OPER_ID)
)
auto_increment = 10000000;

alter table TB_OPER comment '����Ա��';

/*==============================================================*/
/* Table: TB_OPER_GROUP                                         */
/*==============================================================*/
create table TB_OPER_GROUP
(
   ORG_ID               int(8) not null comment '��֯id',
   GROUP_ID             INT(8) not null auto_increment comment 'Ȩ������',
   GROUP_NAME           NVARCHAR(8) comment '����������',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            datetime comment '�޸�ʱ��',
   primary key (GROUP_ID)
);

alter table TB_OPER_GROUP comment '����Ա�������';

/*==============================================================*/
/* Table: TB_ORDER                                              */
/*==============================================================*/
create table TB_ORDER
(
   ORG_ID               int(8) not null comment '��֯id',
   ORDER_ID             int(12) not null auto_increment comment '�������',
   PROD_ID              INT(8) not null comment '��Ʒ���',
   PROD_NAME            NVARCHAR(200) comment '��Ʒ����',
   ORDER_STATUS         INT(2) comment '����״̬;
            0:�ɹ�
            1:ʧ��',
   SOCRE                INT(8) comment '֧������',
   COUNT                INT(5) comment '����',
   USER_ID              INT(10) comment '�û�id',
   EXPRESS_ID           NVARCHAR(500) comment '���id',
   ADDRESS              NVARCHAR(200) comment '�������͵�ַ',
   PROVINCE             NVARCHAR(15) comment 'ʡ',
   CITY                 CHAR(20) comment '��',
   DISTRICT             CHAR(20) comment '��',
   CREATE_TIME          DATETIME comment '����ʱ��',
   UPDATE_TIME          DATETIME comment '�޸�ʱ��',
   OPER_ID              INT(8) comment '����Աid',
   primary key (ORDER_ID)
);

alter table TB_ORDER comment '������';

/*==============================================================*/
/* Table: TB_ORG_INFO                                           */
/*==============================================================*/
create table TB_ORG_INFO
(
   ORG_ID               int(8) not null auto_increment comment '��֯id',
   ORG_ANME             INT(8) comment '��������',
   PROVINCE             NVARCHAR(8) comment 'ʡ',
   CITY                 NVARCHAR(8) comment '��',
   DISTRCIT             NVARCHAR(8) comment '��',
   ADDRESS              NVARCHAR(8) comment '��ַ',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            datetime comment '�޸�ʱ��',
   OPER_ID              INT(8) comment '����Ա',
   LOGO                 NVARCHAR(255) comment '��֯logo',
   primary key (ORG_ID)
)
auto_increment = 10000000;

alter table TB_ORG_INFO comment '����֯��ϵ��';

/*==============================================================*/
/* Table: TB_ORG_RELATIONSHIP                                   */
/*==============================================================*/
create table TB_ORG_RELATIONSHIP
(
   ORG_ID               int(8) not null comment '��֯id',
   SUB_ORG_ID           int(8) not null comment '��������id',
   OPER_ID              INT(8) comment '����Աid',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            DATETIME comment '�޸�ʱ��',
   primary key (ORG_ID, SUB_ORG_ID)
);

alter table TB_ORG_RELATIONSHIP comment '��֯������ϵ��';

/*==============================================================*/
/* Table: TB_ORG_SMS_CONFIG                                     */
/*==============================================================*/
create table TB_ORG_SMS_CONFIG
(
   ORG_ID               INT(8) not null comment '����id',
   VENDOR_ID            INT(8) not null comment '�����ṩ��id',
   MAX_COUNT            INT(8) comment '���ɷ�������',
   SEND_COUNT           INT(8) comment '�ѷ�������',
   CREATE_TIME          DATETIME comment '����޸�ʱ��',
   OPER_ID              INT(8) comment '����޸���',
   PRIORITY             INT(2) comment '���ȼ�',
   STATUS               INT(2) comment '״̬;0:����;1:����',
   primary key (ORG_ID, VENDOR_ID)
);

alter table TB_ORG_SMS_CONFIG comment '�����������ñ�';

/*==============================================================*/
/* Table: TB_PRODUCT                                            */
/*==============================================================*/
create table TB_PRODUCT
(
   PROD_ID              int(8) not null auto_increment comment '��Ʒid',
   ORG_ID               int(8) not null comment '��֯id',
   PROD_NAME            NVARCHAR(200) comment '��Ʒ����',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            DATETIME comment '�޸�ʱ��',
   PRICE                decimal(8,2) comment '��Ʒ��ֵ',
   SOCRE                INT(8) comment '�������',
   STOCK                INT(8) comment '���',
   SMALL_IMAGE_URL      NVARCHAR(255) comment 'Сͼ',
   IMAGE_URL            NVARCHAR(255) comment '��ͼ',
   MAX_BUY_COUNT        INT(5) comment 'ÿ�����������;0:����',
   SALES_COUNT          INT(5) comment '��������',
   BEGIN_TIME           DATETIME comment '��Ʒ�ϼ�ʱ��',
   END_TIME             DATETIME comment '��Ʒ�¼�ʱ��',
   HTML_DESCRPTION      TEXT comment '��Ʒ����',
   STATUS               INT(2) comment '��Ʒ״̬;0:����:1:����',
   primary key (PROD_ID)
)
auto_increment = 10000000;

alter table TB_PRODUCT comment '��Ʒ��';

/*==============================================================*/
/* Table: TB_QUESTION                                           */
/*==============================================================*/
create table TB_QUESTION
(
   ORG_ID               INT(8) comment '����ID',
   QUESTION_ID          INT(10) not null auto_increment comment '����id',
   QUESTION             NVARCHAR(500) comment '����',
   ANWSER_TYPE          INT(2) comment '������;0:Ĭ��:1:�Զ���',
   MULTIPLE             INT(2) comment '�Ƿ������ѡ;0:��;1:��',
   MULTIPLE_COUNT       INT(2) comment '��ѡ����',
   primary key (QUESTION_ID)
);

alter table TB_QUESTION comment 'ϰ���';

/*==============================================================*/
/* Table: TB_QUEST_ANWSER                                       */
/*==============================================================*/
create table TB_QUEST_ANWSER
(
   ORG_ID               INT(8) comment '����id',
   QUEST_ID             INT(10) not null comment '������',
   ANWSER_ID            INT(10) not null auto_increment comment 'ѡ����',
   ANWSER               NVARCHAR(100) comment 'ѡ��',
   ANWSER_TYPE          INT(2) comment '������;
            0:Ĭ��
            1:�Զ���',
   IS_RIGHT             INT(2) comment '���Ƿ�����ȷ��;
            0;��;
            1:��',
   IMAGE_URL            NVARCHAR(255) comment 'ѡ��ͼƬ',
   primary key (ANWSER_ID)
);

alter table TB_QUEST_ANWSER comment '����𰸱�';

/*==============================================================*/
/* Table: TB_REVIEW                                             */
/*==============================================================*/
create table TB_REVIEW
(
   REVIEW_ID            int(10) not null auto_increment comment '�����ˮid',
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            INT(10) comment '֧��id',
   NEWS_ID              INT(10) not null comment '����id',
   NEWS_TYPE            CHAR(2) comment '�������
            00:����
            10:����
            20:�
            ',
   OPER_ID              INT(8) comment '����û�',
   RESULT               NVARCHAR(200) comment '��˽��',
   CREATE_TIME          DATETIME comment '���ʱ��',
   REVIEW_STATUS        INT(2) comment '���״̬',
   primary key (REVIEW_ID)
);

alter table TB_REVIEW comment '��˱�';

/*==============================================================*/
/* Table: TB_REWARD_SCORE                                       */
/*==============================================================*/
create table TB_REWARD_SCORE
(
   REWARD_ID            INT(8) not null auto_increment comment '����id',
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            INT(8) comment '֧��id',
   REWARD_TYPE          INT(2) not null comment '��������;
            0:������
            1:�����;
            2:��������
            3:�ؼ��ֽ���
            4:ѧϰʱ�佱��',
   SOCRE                INT(5) comment '��������',
   KEYWORDS             NVARCHAR(200) comment '�ؼ���',
   LEARN_TIME           INT(4) comment 'ѧϰʱ��;��λ:����',
   WORD_COUNT           int(6) comment '����',
   primary key (REWARD_ID)
)
auto_increment = 10000000;

alter table TB_REWARD_SCORE comment '���ֽ������ñ�';

/*==============================================================*/
/* Table: TB_SCROLL_IMAGE                                       */
/*==============================================================*/
create table TB_SCROLL_IMAGE
(
   ORG_ID               int(8) not null comment '��֯id',
   PAGE_TYPE            CHAR(2) not null comment 'ҳ������;
            0:��ҳ;
            1:�����Ż�;
            2:������;
            3:����ѧϰ',
   IMAGE_URL            INT(8) comment '����Աid',
   primary key (ORG_ID, PAGE_TYPE)
);

alter table TB_SCROLL_IMAGE comment '����ͼƬ��';

/*==============================================================*/
/* Table: TB_SMS_CONFIG                                         */
/*==============================================================*/
create table TB_SMS_CONFIG
(
   VENDOR_ID            INT(8) not null auto_increment comment '�����ṩ��ID',
   VENDOR_NAME          CHAR(100) comment '�����ṩ������',
   SEND_COUNT           INT(8) comment '�ѷ�������',
   CREATE_TIME          DATETIME comment '����ʱ��',
   APP_ID               CHAR(255) comment '���ŷ�appId',
   APP_KEY              CHAR(255) comment '���ŷ�appKey',
   SIGN_KEY             CHAR(255) comment 'ǩ��key',
   SERVICE_URL          CHAR(255) comment '�ӿڵ�ַ',
   MAX_COUNT            INT(8) comment '���������',
   PRIORITY             INT(2) comment '���ȼ�',
   STATUS               INT(2) comment '״̬;0:����:1:����',
   primary key (VENDOR_ID)
)
auto_increment = 10000000;

alter table TB_SMS_CONFIG comment '�������ñ�';

/*==============================================================*/
/* Table: TB_SMS_SEND_LOG                                       */
/*==============================================================*/
create table TB_SMS_SEND_LOG
(
   ORG_ID               int(8) not null comment '��֯id',
   TRACE_ID             int(10) not null auto_increment comment '��ˮ��',
   VENDOR_ID            INT(8) comment '�����ṩ��id',
   SMS_CONTENT          NVARCHAR(200) comment '��������',
   CREATE_TIME          DATETIME comment '����ʱ��',
   PHONE                CHAR(11) comment '�����ֻ�',
   STATUS               INT(2) comment '����״̬;0:�ɹ�;1:ʧ��',
   REQUEST_MSG          CHAR(1000) comment '��������',
   RESPONSE_MSG         CHAR(1000) comment '��������',
   primary key (TRACE_ID)
);

alter table TB_SMS_SEND_LOG comment '���ŷ�����־��';

/*==============================================================*/
/* Table: TB_SYSTEM_NEWS                                        */
/*==============================================================*/
create table TB_SYSTEM_NEWS
(
   NEWS_ID              INT(8) not null auto_increment comment '��Ϣid',
   NEWS_NAME            NVARCHAR(100) comment '��Ϣ����',
   NEWS_CONTENT         NVARCHAR(100) comment '��Ϣ����',
   NOTICE_DATE          CHAR(8) comment '��ʾ����',
   STATUS               INT(2) comment '״̬',
   OPER_ID              INT(8) comment '����Ա',
   REPEAT_TYPE          int(2) comment '�ظ�����;
            0:ÿ��;
            1:��1��',
   HTML_DESC            NVARCHAR(4000) comment 'HTML����',
   NEWS_TYPE            CHAR(2) comment 'ϵͳ֪ͨ����
            00:���䵮��
            10:ϵͳ֪ͨ',
   primary key (NEWS_ID)
);

alter table TB_SYSTEM_NEWS comment 'ϵͳ��Ϣ��';

/*==============================================================*/
/* Table: TB_TASK                                               */
/*==============================================================*/
create table TB_TASK
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   TASK_ID              INT(8) not null auto_increment comment '����id',
   TASK_NAME            NVARCHAR(100) comment '��������',
   FROM_USER            INT(10) comment '������',
   TASK_CONTENT         NVARCHAR(4000) comment '��������',
   BEGIN_TIME           DATETIME comment '��ʼʱ��',
   END_TIME             DATETIME comment '����ʱ��',
   ATTACH1              NVARCHAR(255) comment '����1',
   ATTACH2              NVARCHAR(255) comment '����2',
   ATTACH3              NVARCHAR(255) comment '����3',
   ATTACH4              NVARCHAR(255) comment '����4',
   CREATE_TIME          DATETIME comment '����ʱ��',
   CAN_COMMENT          INT(2) comment '�Ƿ���������;0:����;1:��ֹ',
   STATUS               INT(2) comment '����״̬;
            0:����״̬;
            1:ִ����
            2:�����������
            3:���ͨ��',
   IS_PUBLIC            INT(2) comment '�Ƿ񹫿�
            0:����;
            1:������',
   ORG_REVIEW           CHAR(2) comment '�������״̬;
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   BRANCH_REVIEW        CHAR(2) comment '֧�����״̬
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   primary key (TASK_ID)
);

alter table TB_TASK comment '�����';

/*==============================================================*/
/* Table: TB_TEST_PAPER                                         */
/*==============================================================*/
create table TB_TEST_PAPER
(
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            INT(8) comment '֧��id',
   TEST_ID              INT(8) not null auto_increment comment '�Ծ�id',
   TEST_NAME            NVARCHAR(20) comment '�Ծ�����',
   HAS_REWARD           INT(2) comment '�Ƿ�������; 
            0:����;
            1:������',
   EXCERCISE_SOCRE      INT(3) comment 'ÿһ���ֵ',
   PASS_SCORE           INT(5) comment '�����ֵ',
   HAS_LIMITED_TIME     INT(2) comment '�Ƿ�����ʱ��',
   LIMITED_TIME         INT(5) comment 'ʱ��(����)',
   HTML_DESCRIPT        text comment 'html����',
   OPER_ID              int(8) comment '����Աid',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            DATETIME comment '�޸�ʱ��',
   STATUS               INT(2) comment '״̬;0;����;1:����',
   primary key (TEST_ID)
);

alter table TB_TEST_PAPER comment '�Ծ��';

/*==============================================================*/
/* Table: TB_TEST_QUESTION                                      */
/*==============================================================*/
create table TB_TEST_QUESTION
(
   ORG_ID               INT(8) comment '����id',
   TEST_ID              INT(8) not null comment '�Ծ�id',
   QUESTION_ID          INT(10) not null comment '�Ծ�����id',
   primary key (TEST_ID, QUESTION_ID)
);

alter table TB_TEST_QUESTION comment '�Ծ����������';

/*==============================================================*/
/* Table: TB_USER_ACTIVITY                                      */
/*==============================================================*/
create table TB_USER_ACTIVITY
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   ACTIVITY_ID          INT(8) not null auto_increment comment '�id',
   USER_ID              INT(10) not null comment '�û�id',
   USER_FEEDBACK        NVARCHAR(4000) comment '�û�����',
   CREATE_TIME          DATETIME comment '����ʱ��',
   STATUS               CHAR(2) comment '�״̬',
   COMPLETE_TIME        INT(8) comment '���ʱ��',
   REWARDS_SOCRE        INT(5) comment '��������',
   primary key (ACTIVITY_ID, USER_ID)
);

alter table TB_USER_ACTIVITY comment '�û�������';

/*==============================================================*/
/* Table: TB_USER_COMMENT                                       */
/*==============================================================*/
create table TB_USER_COMMENT
(
   ORG_ID               int(8) not null comment '��֯id',
   MESSAGE_ID           int(12) not null auto_increment comment '����id',
   CONTENT              NVARCHAR(200) not null comment '��������',
   CREATE_TIME          DATETIME comment '����ʱ��',
   USER_ID              INT(10) comment '�����û�',
   REVIEW_STATUS        INT(2) comment '���״̬;
            0:ͨ��;
            1:δͨ��',
   REVIEW_USER          INT(8) comment '����û�',
   REVIEW_TIME          DATETIME comment '���ʱ��',
   primary key (MESSAGE_ID)
);

alter table TB_USER_COMMENT comment '���۱�';

/*==============================================================*/
/* Table: TB_USER_INFO                                          */
/*==============================================================*/
create table TB_USER_INFO
(
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            INT(8) comment '֧�����',
   USER_ID              int(10) not null auto_increment comment '�û�id',
   PASSWORD             CHAR(255) comment '����',
   NAME                 NVARCHAR(20) comment '����',
   SEX                  INT(2) comment '�Ա�;0:��;1:Ů',
   ETHNIC               NVARCHAR(20) comment '����',
   BIRTHDAY             DATETIME comment '��������',
   ORIGIN               NVARCHAR(20) comment '����',
   ORIGIN_ADDRESS       NVARCHAR(200) comment '������ַ',
   PHONE                CHAR(15) comment '��ϵ�绰',
   ID_NUMBER            CHAR(18) comment '���֤��',
   EDUCATION            NVARCHAR(20) comment 'ѧ��',
   SCHOOL               NVARCHAR(50) comment '��ҵѧУ',
   PROVINCE             NVARCHAR(8) comment 'ʡ',
   CITY                 NVARCHAR(8) comment '��',
   DISTRCIT             NVARCHAR(8) comment '��',
   ADDRESS              NVARCHAR(200) comment '��ַ',
   COMPANY              NVARCHAR(100) comment '������λ',
   DEPT                 NVARCHAR(20) comment '����',
   TITLE                NVARCHAR(20) comment 'ְλ',
   PRE_MEMBER_DATE      DATETIME comment 'Ԥ����Ա����',
   MEMBER_DATE          DATETIME comment '��ʽ��Ա����',
   MEMBER_TYPE          INT(2) comment '��Ա����;
            0:��Ա;
            1:Ԥ����Ա',
   STATUS               INT(2) comment '�û�״̬;0:����;1:����',
   MEMBER_STATUS        INT(2) comment '��Ա״̬;0:����;1:����',
   PHOTO_URL            NVARCHAR(255) comment 'ͷ��',
   NICK_PHOTO_URL       NVARCHAR(255) comment '�ǳ�ͷ��',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EDIT_TIME            datetime comment '�޸�ʱ��',
   OPER_ID              int(8) comment '����Աid',
   IS_CUSTOMER          int(2) comment '�ο�;
            0:�� ;
            1:��',
   primary key (USER_ID)
)
auto_increment = 10000000;

alter table TB_USER_INFO comment '�û���';

/*==============================================================*/
/* Table: TB_USER_NEWS                                          */
/*==============================================================*/
create table TB_USER_NEWS
(
   ORG_ID               int(8) not null comment '��֯id',
   BRANCH_ID            INT(8) comment '֧��id',
   NEWS_ID              int(10) not null auto_increment comment '����id
            ',
   USER_ID              int(10) comment '�û�id',
   TITLE                NVARCHAR(100) comment '��������',
   HTML_CONTENT         NVARCHAR(4000) comment '��������',
   NEWS_TYPE            CHAR(2) comment '��������
            00:����
            01:�ĵ����
            02:����
            ',
   STATUS               CHAR(2) comment '״̬;0:���� -1:�����;',
   CREATE_TIME          DATETIME comment '��������',
   VIEWS_COUNT          INT(8) comment '�Ķ�����',
   REWARD_SCORE         INT(8) comment '��������',
   REVIEW_ID            INT(10) comment '�����ˮid',
   ORG_REVIEW           CHAR(2) comment '�������״̬;
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   BRANCH_REVIEW        CHAR(2) comment '֧�����״̬
            0:���ͨ��;
            -1:�����
            1:��˲�ͨ��
            ',
   primary key (NEWS_ID)
);

alter table TB_USER_NEWS comment '�û����±�';

/*==============================================================*/
/* Table: TB_USER_NOTICE                                        */
/*==============================================================*/
create table TB_USER_NOTICE
(
   ORG_ID               INT(8) comment '����id',
   NEWS_ID              INT(8) not null auto_increment comment '֪ͨid',
   NEWS_TYPE            INT(2) comment '֪ͨ����',
   TITLE                CHAR(100) comment '����',
   USER_ID              INT(10) comment '�����û�',
   CONTENT              NVARCHAR(250) comment '֪ͨ����',
   IS_READ              INT(1) comment '�Ƿ����Ķ�;
            0:���Ķ�
            1:δ�Ķ�',
   FROM_USER            INT(10) comment '������',
   CREATE_TIME          DATETIME comment '����ʱ��',
   READ_TIME            DATETIME comment '�Ķ�ʱ��',
   primary key (NEWS_ID)
);

alter table TB_USER_NOTICE comment '�û�֪ͨ��Ϣ��
';

/*==============================================================*/
/* Table: TB_USER_RELATIONSHIP                                  */
/*==============================================================*/
create table TB_USER_RELATIONSHIP
(
   ORG_ID               int(8) not null comment '��֯id',
   USER_ID              int(10) not null comment '�쵼id',
   SUB_USER_ID          int(10) comment '�¼�id',
   primary key (USER_ID)
);

alter table TB_USER_RELATIONSHIP comment '�û���ϵ��';

/*==============================================================*/
/* Table: TB_USER_SCORE_TRACE                                   */
/*==============================================================*/
create table TB_USER_SCORE_TRACE
(
   ORG_ID               int(8) not null comment '��֯id',
   USER_ID              INT(10) not null comment '�����û�id',
   TRACE_ID             int not null auto_increment,
   CREATE_TIME          DATETIME comment '����ʱ��',
   SCORE                INT(8) comment '��û���',
   REWARD_TYPE          CHAR(2) comment '���ֽ�������;
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
   COMMENT              NVARCHAR(200) comment '����',
   ORI_SCORE            INT(8) comment 'ԭ����',
   primary key (TRACE_ID)
);

alter table TB_USER_SCORE_TRACE comment '�û�������ˮ��';

/*==============================================================*/
/* Table: TB_USER_STUDY_LOG                                     */
/*==============================================================*/
create table TB_USER_STUDY_LOG
(
   ORG_ID               INT(8) comment '����ID',
   BRANCH_ID            INT(8) comment '֧��id',
   TRACE_ID             INT(12) not null auto_increment comment '��ˮ��',
   USER_ID              INT(8) comment '�û�id',
   NEWS_ID              INT(8) comment '����id',
   MODULE_ID            INT(2) comment '������Ŀid',
   STUDY_TIME           INT(5) comment 'ѧϰʱ��;��λ:����',
   CREATE_TIME          DATETIME comment '����ʱ��',
   SCORE                INT(5) comment '��������',
   primary key (TRACE_ID)
);

alter table TB_USER_STUDY_LOG comment '�û�����ѧϰ��ˮ��';

/*==============================================================*/
/* Table: TB_USER_TASK                                          */
/*==============================================================*/
create table TB_USER_TASK
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   TASK_ID              INT(8) not null comment '����id',
   USER_ID              INT(10) not null comment '�û�id',
   ACCEPT_TIME          DATETIME comment '����ʱ��',
   STATUS               INT(2) comment '״̬',
   REWARD_SCORE         INT(5) comment '��������',
   COMMENT              NVARCHAR(200) comment '����',
   SOCRE                INT(2) comment '���;1~5��',
   primary key (TASK_ID, USER_ID)
);

alter table TB_USER_TASK comment '�ҵ������';

/*==============================================================*/
/* Table: TB_USER_TASK_STEP                                     */
/*==============================================================*/
create table TB_USER_TASK_STEP
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   TASK_ID              INT(8) not null comment '����id',
   USER_ID              INT(10) not null comment '�û�id',
   STEP_ID              INT(2) not null comment '����id',
   STEP_NAME            NVARCHAR(100) comment '��������',
   CREATE_TIME          DATETIME comment '���贴��ʱ��',
   CONTENT              NVARCHAR(4000) comment '����',
   UPDATE_TIME          DATETIME comment '����ʱ��',
   primary key (TASK_ID, USER_ID, STEP_ID)
);

alter table TB_USER_TASK_STEP comment '��������';

/*==============================================================*/
/* Table: TB_USER_TEST                                          */
/*==============================================================*/
create table TB_USER_TEST
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   USER_ID              int(8) not null comment '�û�id',
   TEST_ID              INT(10) not null comment '�Ծ�id',
   SCORE                INT(3) comment '�Ծ�÷�',
   TOTAL_SCORE          INT(3) comment '�Ծ��ܷ�',
   CREATE_TIME          DATETIME comment '���ʱ��',
   USE_TIME             INT(5) comment '�����ʱ',
   primary key (USER_ID, TEST_ID)
);

alter table TB_USER_TEST comment '�û������';

/*==============================================================*/
/* Table: TB_USER_TEST_QUESTION                                 */
/*==============================================================*/
create table TB_USER_TEST_QUESTION
(
   ORG_ID               INT(8) comment '����id',
   TEST_ID              INT(8) not null comment '�Ծ�id',
   USER_ID              int(8) not null comment '�û�id',
   QUESTION_ID          INT(10) not null comment '����id',
   QUESTION_NAME        NVARCHAR(500) comment '��������',
   ANSWER1              CHAR(100) comment '��1',
   ANSWER2              CHAR(100) comment '��2',
   ANSWER3              CHAR(100) comment '��3',
   ANSWER4              CHAR(100) comment '��4',
   IS_RIGHT             INT(2) comment '�Ƿ���ȷ;0:��ȷ;1:����',
   primary key (TEST_ID, USER_ID, QUESTION_ID)
);

alter table TB_USER_TEST_QUESTION comment '�û��Ծ�𰸱�';

/*==============================================================*/
/* Table: TB_USER_TOKEN                                         */
/*==============================================================*/
create table TB_USER_TOKEN
(
   ORG_ID               INT(8) comment '����id',
   BRANCH_ID            INT(8) comment '֧��id',
   USER_ID              INT(8) comment '����Աid',
   TOKEN                CHAR(255) not null comment '����Աtoken',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EXPIRE_TIME          DATETIME comment '����ʱ��',
   TOKEN_TYPE           INT(2) comment 'token����;0:ǰ���û�;1:��˲���Ա',
   primary key (TOKEN)
);

alter table TB_USER_TOKEN comment '�û���¼��Ϣ�Ự��';

/*==============================================================*/
/* Table: TB_VALIDCODE                                          */
/*==============================================================*/
create table TB_VALIDCODE
(
   ORG_ID               int(8) not null comment '��֯id',
   TRACE_ID             int(10) not null auto_increment comment '��ˮ��',
   PHONE                CHAR(15) not null comment '�ֻ�����',
   VALIDCODE            INT(6) not null comment '��֤��',
   SEQ                  INT(2) comment '��֤�����',
   CREATE_TIME          DATETIME comment '����ʱ��',
   EXPIRE_TIME          DATETIME comment '����ʱ��',
   primary key (TRACE_ID)
);

alter table TB_VALIDCODE comment '��֤���';

