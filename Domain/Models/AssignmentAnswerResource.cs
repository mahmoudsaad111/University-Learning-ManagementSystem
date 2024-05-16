using Domain.TmpFilesProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AssignmentAnswerResource : FileResource , IFileResourceModel
    {
        public int AssignmentAnswerId { get; set; }
        [JsonIgnore]
        public AssignmentAnswer AssignmentAnswer { get; set; }
    }
}
