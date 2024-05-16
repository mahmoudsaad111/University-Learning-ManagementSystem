using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.AssignementAnswers
{
    public class StudentHasAnswerOfAssignemet
    {
        public int StudentId { get; set; }
        public string FirstName { get; set;}
        public string SecondName { get; set;}
        public string ThirdName { get; set;}    
        public string FourthName {  get; set;}  
        public string Email { get; set;}    
        public string UserName { get; set; }
        public string ImageUrl { get; set; }    
        public int StudentMarks { get;  set; }
        public IEnumerable<AssignmentAnswerResource> StudentAnswerFiles { get; set; }   
    }
}
