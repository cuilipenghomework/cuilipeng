--通过查询获得走进java编程世界的课程编号和最近一次的考试时间
declare @date datetime
declare @subjectno int
select @subjectno=Subjectid from Subject where SubjectName='走进java编程世界'
select @date=max(ExamDate) from Result where SubjectId=@subjectno
select @subjectno,@date
--通过查询得到缺考人数
declare @num1 int
declare @num2 int
select @num1=count(*) from student where gradeid=(select Gradeid from Subject where SubjectId=@subjectno)  
select @num2=COUNT(*) from Result where SubjectId=@subjectno and ExamDate=@date
select @num1,@num2,缺考人数=@num1-@num2
--通过查询得到成绩信息（学生姓名、学号、成绩、是否通过）
select StudentName 学生姓名,Student.StudentNo 学号,StudentResult 成绩,isPass=
case
when studentresult>=80 then '通过'
else '未通过'
end
into TempResult from Student left join(select * from Result where SubjectId=@subjectno and ExamDate=@date)Result on Student.StudentNo=Result.StudentNo
where GradeId=1
select * from TempResult
--获取平均分，并进行提分操作，并更新ispass列
declare @avgscore int
select @avgscore=avg(成绩) from TempResult where 成绩 is not null
select @avgscore 平均分
if (@avgscore<80)
   set @avgscore=80
select @avgscore 平均分
while 1=1
if not exists(select * from TempResult where 成绩<@avgscore)
   break
else
   update TempResult set 成绩=成绩+1 where 成绩 is not null and 成绩<@avgscore and 成绩<=95

update TempResult set isPass=
case
when  成绩>=80 then '通过'
else '未通过'
end
--如果某个学生的成绩为NULL(空)，则替换为”缺考”，否则原样显示    isPass列中的1替换为是，0替换为否
update TempResult set 成绩=
case
when  成绩 is null then '缺考'
else  CONVERT(varchar,成绩)
end

update TempResult set isPass=
case
when 成绩!='缺考' then '1'
else '0'
end
select * from TempResult
select 总人数=COUNT(*),通过人数=sum(isPass),通过率=Convert(varchar,((SUM(isPass)/COUNT(*))*100))+'%' from TempResult