using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Models
{
    public class CourseSelect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType MyProperty { get; set; }
    }
    public enum CourseType
    {
        PartTime,
        FullTime
    }
}