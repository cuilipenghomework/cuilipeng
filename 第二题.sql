--ͨ����ѯ����߽�java�������Ŀγ̱�ź����һ�εĿ���ʱ��
declare @date datetime
declare @subjectno int
select @subjectno=Subjectid from Subject where SubjectName='�߽�java�������'
select @date=max(ExamDate) from Result where SubjectId=@subjectno
select @subjectno,@date
--ͨ����ѯ�õ�ȱ������
declare @num1 int
declare @num2 int
select @num1=count(*) from student where gradeid=(select Gradeid from Subject where SubjectId=@subjectno)  
select @num2=COUNT(*) from Result where SubjectId=@subjectno and ExamDate=@date
select @num1,@num2,ȱ������=@num1-@num2
--ͨ����ѯ�õ��ɼ���Ϣ��ѧ��������ѧ�š��ɼ����Ƿ�ͨ����
select StudentName ѧ������,Student.StudentNo ѧ��,StudentResult �ɼ�,isPass=
case
when studentresult>=80 then 'ͨ��'
else 'δͨ��'
end
into TempResult from Student left join(select * from Result where SubjectId=@subjectno and ExamDate=@date)Result on Student.StudentNo=Result.StudentNo
where GradeId=1
select * from TempResult
--��ȡƽ���֣���������ֲ�����������ispass��
declare @avgscore int
select @avgscore=avg(�ɼ�) from TempResult where �ɼ� is not null
select @avgscore ƽ����
if (@avgscore<80)
   set @avgscore=80
select @avgscore ƽ����
while 1=1
if not exists(select * from TempResult where �ɼ�<@avgscore)
   break
else
   update TempResult set �ɼ�=�ɼ�+1 where �ɼ� is not null and �ɼ�<@avgscore and �ɼ�<=95

update TempResult set isPass=
case
when  �ɼ�>=80 then 'ͨ��'
else 'δͨ��'
end
--���ĳ��ѧ���ĳɼ�ΪNULL(��)�����滻Ϊ��ȱ����������ԭ����ʾ    isPass���е�1�滻Ϊ�ǣ�0�滻Ϊ��
update TempResult set �ɼ�=
case
when  �ɼ� is null then 'ȱ��'
else  CONVERT(varchar,�ɼ�)
end

update TempResult set isPass=
case
when �ɼ�!='ȱ��' then '1'
else '0'
end
select * from TempResult
select ������=COUNT(*),ͨ������=sum(isPass),ͨ����=Convert(varchar,((SUM(isPass)/COUNT(*))*100))+'%' from TempResult