--Ϊÿ��ѧ��������У�ڼ�ÿ�ſγ̵ĳɼ�����Ҫ��ÿ��ѧ���μ�ÿ�ſγ̵����һ�ο��Գɼ���Ϊ�������γ̵����ճɼ�
select Student.StudentName ѧ������,(select Grade.GradeName from Grade where Subject.GradeId=grade.GradeId) �༶����,
Subject.SubjectName �γ�����,Result.ExamDate ��������,Result.StudentResult ���Գɼ� from Result
inner join Student on Result.StudentNo=Student.StudentNo
inner join Subject on Subject.SubjectId=Result.SubjectId 
where result.ExamDate=
(
  select MAX(result.ExamDate) from result where result.StudentNo=Student.StudentNo and Result.SubjectId=Subject.SubjectId
)
order by Result.StudentNo,Result.SubjectId