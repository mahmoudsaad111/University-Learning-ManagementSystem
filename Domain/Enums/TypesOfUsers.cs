using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Enums
{
//    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypesOfUsers
    {
      //  [EnumMember(Value = "Student")]
        Student,
     //   [EnumMember(Value = "Professor")]
        Professor,
     //   [EnumMember(Value = "Instructor")]
        Instructor,
        Staff,
        Admin
    }
}
