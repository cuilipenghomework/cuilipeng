--为每个学生制作在校期间每门课程的成绩单，要求每个学生参加每门课程的最后一次考试成绩作为该生本课程的最终成绩
select Student.StudentName 学生姓名,(select Grade.GradeName from Grade where Subject.GradeId=grade.GradeId) 班级名称,
Subject.SubjectName 课程名称,Result.ExamDate 考试日期,Result.StudentResult 考试成绩 from Result
inner join Student on Result.StudentNo=Student.StudentNo
inner join Subject on Subject.SubjectId=Result.SubjectId 
where result.ExamDate=
(
  select MAX(result.ExamDate) from result where result.StudentNo=Student.StudentNo and Result.SubjectId=Subject.SubjectId
)
order by Result.StudentNo,Result.SubjectId