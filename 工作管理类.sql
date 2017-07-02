--建数据库WorkManageSystem
create DataBase WorkManageSystem
on
(
   Name='WorkManageSystem_data',
   FileName='F:\北大青鸟詹老师U2\20170701上课资料\作业\工作管理数据库\WorkManageSystem.mdf',
   size=5MB,
   maxSize=100MB,
   FileGrowth=15%
)
log on
(
   Name='WorkManageSystem_log',
   FileName='F:\北大青鸟詹老师U2\20170701上课资料\作业\工作管理数据库\WorkManageSystem.ldf',
   size=5MB,
   FileGrowth=15%
)
use WorkManageSystem
go
--Joblist（职位列表）表
if Exists(select * from  sysobjects  where name='Joblist') 
  Drop table Joblist
go
create table Joblist
(
  users integer identity(1,1) not null Constraint PK_users Primary key(users),
  ApplyID integer not null,
  StartDate Date not null,
  EndDate Date not null,
  Company varchar not null,
  CompanyProperty varchar not null,
  Depaerment varchar not null,
  Jobtitle varchar not null,
  JobDescription varchar not null
)
--申请职位表(ApplyJob)
if Exists(select * from  sysobjects  where name='ApplyJob') 
  Drop table ApplyJob
go
create table ApplyJob
(
  RelId integer identity(1,1) not null Constraint PK_RelId Primary key(RelId),
  ApplyID integer not null,
  State varchar(50),
  InterviewTime Date,
  InterviewMan Date,
  InterviewResult varchar(50)
)
--职位类型表(JobType)
if Exists(select * from  sysobjects  where name='JobType') 
  Drop table JobType
go
create table JobType
(
  JobTypeID integer identity(1,1) not null Constraint PK_JobTypeID Primary key(JobTypeID),
  JobTypeName varchar(50) not null,
  JobTypeCName varchar(50) not null
)
--答案表(Answer)
if Exists(select * from  sysobjects  where name='Answer') 
  Drop table Answer
go
create table Answer
(
  AnsID integer identity(1,1) not null Constraint PK_AnsID Primary key(AnsID),
  QustID integer not null,
  Content varchar(50) not null,
  Mark float not null 
)
--技能表(Skill)
if Exists(select * from  sysobjects  where name='Skill') 
  Drop table Skill
go
create table Skill
(
  SkillID integer identity(1,1) not null Constraint PK_SkillID Primary key(SkillID),
  ApplyId integer not null,
  SkillName varchar(50) not null,
  SkillLevel varchar(50) not null,
  CertName varchar(50) not null,
  SkillDetail varchar(50) not null
)
--职位列表(JobList1)
if Exists(select * from  sysobjects  where name='JobList1') 
  Drop table JobList1
go
create table JobList1
(
  JobNAME varchar not null,
  JobID integer identity(1,1) not null Constraint PK_JobID Primary key(JobID),
  JobTypeID integer not null,
  PubDate Date,
  EndDate Date,
  JobNum integer not null,
  Description varchar(50),
  Requirements varchar(50),
  Others varchar(50)
)
--问题表(Question)
if Exists(select * from  sysobjects  where name='Question') 
  Drop table Question
go
create table Question
(
  questID integer identity(1,1) not null Constraint PK_questID Primary key(questID),
  QuestName varchar(50) not null,
  QuestContent varchar(50) not null,
  QuestType1 varchar(50) not null,
  QuestTyp2 varchar(50) not null,
  Others varchar(50) not null,
  Remark varchar(50) not null
)
--个人信息表(Personalinfo)
if Exists(select * from  sysobjects  where name='Personalinfo') 
  Drop table Personalinfo
go
create table Personalinfo
(
  ApplyId integer identity(1,1) not null Constraint PK_ApplyId Primary key(ApplyId),
  ApplyJobID integer not null,
  Name varchar(50) not null,
  EngName varchar(50) not null,
  Gender integer not null Constraint CK_Gender check(Gender=1 or Gender=2),
  BirthDate Date not null,
  Height float,
  Nation varchar(50),
  Hukou varchar(50) not null,
  IdentityType varchar(50) not null,
  IdentifyNO varchar(50) not null,
  MarriageType	varchar(50),
  WorkYear	Integer,
  AvailableDate	varchar(50) not null,
  CurrentSalary	varchar(50),
  ExpectedSalay	varchar(50),
  Residency	varchar(50),
  Homephone	varchar(50),
  CompanyPhone varchar(50),
  moblie	varchar(50),
  Email	varchar(50)
)
--考试详情表(Examdetail)
if Exists(select * from  sysobjects  where name='Examdetail') 
  Drop table Examdetail
go
create table Examdetail
(
   ExamDetailId	Integer identity(1,1) not null Constraint PK_ExamDetailId Primary key(ExamDetailId),
   ExamID	Integer not null,
   questID	Integer not null,
   QuestOrder varchar(50) not null
)
--教育情况表(Education)
if Exists(select * from  sysobjects  where name='Education') 
  Drop table Education
go
create table Education
(
   EducationID	Integer identity(1,1) not null Constraint PK_EducationID Primary key(EducationID),
   ApplyID	Integer not null,
   Startdate	Date not null,
   EndDate	Date not null,
   name	varchar(50) not null
)
--家庭情况表(Family)
if Exists(select * from  sysobjects  where name='Family') 
  Drop table Family
go
create table Family
(
   FamilyName	varchar(50) not null,
   FamilyID	integer identity(1,1) not null Constraint PK_FamilyID Primary key(FamilyID),
   ApplyID	integer not null,
   SchoolName	varchar(50) not null,
   Major	varchar(50) not null,
   Degree	integer not null,
   EduDetail	Varchar(50) not null
)
--考试表(Exam)
if Exists(select * from  sysobjects  where name='Exam') 
  Drop table Exam
go
create table Exam
(
    ExamID	Integer identity(1,1) not null Constraint PK_ExamId Primary key(ExamID),
    ExamName	varchar(50) not null,
    ExamType	Integer not null,
    Remark	varchar(50) not null,
    PassRemark	varchar(50) not null,
    IssueDate	Date not null,
    JobType	Integer not null
)
--用户表(Users)
if Exists(select * from  sysobjects  where name='Users') 
  Drop table Users
go
create table Users
(
   UserID	integer identity(1,1) not null Constraint PK_UserId Primary key(UserID),
   UserName	varchar(50) not null Constraint CK_UserName check(len(UserName)>=6),
   UserPwd	varchar(50) not null,
   Permisstion	Integer not null Constraint CK_Permisstion check(Permisstion=0 or Permisstion=1 or Permisstion=2 or Permisstion=3 or Permisstion=4)
)
insert users values('崔力鹏fanfeng','123456',2)
--考试成绩表(Examresult)
if Exists(select * from  sysobjects  where name='Examresult') 
  Drop table Examresult
go
create table Examresult
(
   ExamResutId	Integer  not null Constraint CK_ExamResutId check(len(ExamResutId)>=6),
   ApplyId	integer not null,
   Mark0 float not null,
   AnswerRecord0	varchar(50) not null,
   Mark1 float not null,
   AnswerRecord1	varchar(50) not null,
   Marks float not null
)
insert Examresult values(111111,1,78,'好',98,'很好',88)
--职位问题表(JobExam)
if Exists(select * from  sysobjects  where name='JobExam') 
  Drop table JobExam
go
create table JobExam
(
    RelID	Integer not null,
    JobID	Integer identity(1,1) not null Constraint PK_JobId1 Primary key(JobId),
    ExamID	Integer not null,
    ExamType varchar(50) not null,
    ExamName varchar(50) not null
)
insert JobExam values(1,1,'英语' ,'崔力鹏')
