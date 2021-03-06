using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Prog_Дз_7_4
{
    class Candidate
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public string City { get; set; }
        public float SalaryRequirements { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, Years Of Experience: {YearsOfExperience}, City: {City}, Salary Requirements: {SalaryRequirements}";
        }
    }
}
