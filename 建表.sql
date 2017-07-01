--�����ݿ�School
create DataBase School
on
(
   Name='School_data',
   FileName='F:\��������ղ��ʦU2\School\School.mdf',
   size=5MB,
   maxSize=100MB,
   FileGrowth=15%
)
log on
(
   Name='School_log',
   FileName='F:\��������ղ��ʦU2\School\School.ldf',
   size=5MB,
   FileGrowth=15%
)
use School
go
--��Subject��
if Exists(select * from  sysobjects  where name='Subject') 
  Drop table Subject
go
create table Subject
(
  SubjectNo int identity(1,1) not null,
  SubjectName nvarchar(50),
  ClassHour int,
  GradeID int
)
if Exists(select * from  sysobjects  where name='Result') 
  Drop table Result
go
--��Result��
create table Result
(
  StudentNo int not null,
  SubjectNo int not null,
  ExamDate datetime not null,
  StudentResult int not null
)
if Exists(select * from  sysobjects  where name='Student') 
  Drop table Student
go
--��Student��
create table Student
(
  StudentNo int not null,
  LoginPwd nvarchar(50) not null,
  StudentName nvarchar(50) not null,
  Sex bit not null,
  GradeID int not null,
  Phone varchar(50),
  Address nvarchar(255),
  BornDate datetime not null,
  Email varchar(50),
  IdentityCard varchar(18) not null
)
if Exists(select * from  sysobjects  where name='Grade') 
  Drop table Grade
go
--��Grade��
create table Grade
(
  GradeID int identity(1,1) not null,
  GradeName nvarchar(50) not null
)
--ΪGrade����ӵ�Լ��
Alter Table Grade
Add Constraint PK_GradeID Primary key (GradeID)
--ΪStudent����ӵ�Լ��
Alter Table Student
Add Constraint PK_StudentNo Primary Key (StudentNo)
Alter Table Student
Add Constraint UQ_IdentityCard unique (IdentityCard)
Alter Table Student
Add Constraint DF_Address Default('��ַ����') for address
Alter Table Student
Add Constraint CK_BornDate check(BornDate>='1980-01-01')
Alter Table Student
Add Constraint FK_GradeID
  Foreign key (GradeID) references Grade(GradeID)
go
--ΪSubject�����Լ��
Alter Table Subject
Add Constraint PK_SubjectNo Primary Key (SubjectNo)
Alter Table Subject
Add Constraint CK_SubjectName check(SubjectName is not null)
Alter Table Subject
Add Constraint CK_Classhour check(classhour>=0)
Alter Table Subject
Add Constraint FK_Grade_Subject_GradeID
  Foreign key (GradeID) references Grade(GradeID)
go
--ΪResult��ӵ�Լ��
Alter Table Result
Add Constraint PK_SubjectNo_StudentNo_ExamDate Primary Key (SubjectNo,StudentNo,ExamDate)
Alter Table Result
--Add Constraint CK_ExamDate check(examdate=GetDate())
Add Constraint DF_ExamDate Default(GetDate()) for Examdate
Alter Table Result
Add Constraint CK_StudentResult check(studentresult>=0 and studentresult<=100)
Alter Table Result
Add Constraint FK_Result_Student_StudentNo
  Foreign key (Studentno) references Student(studentno)
go
Alter Table Result
Add Constraint FK_Result_Subject_SubjectNo
  Foreign key (Subjectno) references Subject(Subjectno)
go
--Grade����������
select * from Grade
insert into grade values('S1')
insert into grade values('S2')
insert into grade values('S3')
--Student����������
select * from Student
insert into student values(1001,'123456','�Ų�»','1',1,'13856739983',default,'1997-09-09','8839@qq.com','3408291')
insert into student values(1002,'126456','�����','2',1,'13856739983',default,'1997-09-09','8839@qq.com','3608291')
insert into student values(1003,'121456','���˲�','2',1,'13856739983',default,'1996-09-09','8839@qq.com','34099291')
insert into student values(2001,'123456','�Ų�','10',2,'13856739983',default,'1997-09-09','8839@qq.com','34028291')
insert into student values(2002,'123456','��»','1',1,'13856739983',default,'1997-09-09','8839@qq.com','340822291')
insert into student values(2003,'1234356','��»','1',3,'13856739983',default,'1997-09-09','8839@qq.com','3433308291')
insert into student values(3001,'123456','�Ų�»','1',3,'13856739983',default,'1997-09-09','8839@qq.com','340823391')
--Subject����������
select * from Subject
insert into Subject values('Java','99',1)
insert into Subject values('C#','89',2)
insert into Subject values('Java11','99',3)
insert into Subject values('Java2','79',1)
insert into Subject values('C#1','89',2)
insert into Subject values('Java112','99',3)
--Result����������
select * from Result
insert into Result values(1001,5,default,'89')
insert into Result values(1002,6,default,'89')
insert into Result values(2001,7,default,'99')
insert into Result values(2003,7,default,'89')
insert into Result values(3001,8,default,'89')
insert into Result values(2002,9,default,'99')