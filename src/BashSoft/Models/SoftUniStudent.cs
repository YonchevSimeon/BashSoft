﻿namespace BashSoft.Models
{
    using System.Linq;
    using System.Collections.Generic;

    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    public class SoftUniStudent : IStudent
    {
        private string userName;
        private Dictionary<string, ICourse> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public SoftUniStudent(string userName)
        {
            this.userName = userName;
            this.enrolledCourses = new Dictionary<string, ICourse>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses
        {
            get
            {
                return this.enrolledCourses;
            }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get
            {
                return this.marksByCourseName;
            }
        }

        public void EnrollInCourse(ICourse course)
        {
            if (this.enrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.UserName, course.Name);
            }
            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                throw new CourseNotFoundException();
            }

            if(scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new InvalidNumberOfScoresException();
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        public int CompareTo(IStudent other) => this.UserName.CompareTo(other.UserName);

        public override string ToString() => this.UserName;

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum() /
                (double)(SoftUniCourse.NumberOfTasksOnExam * SoftUniCourse.MaxScoresOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}
