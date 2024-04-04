
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.TmpFilesProcessing
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
  //  [Flags]
    public enum FileType
    {
        [EnumMember(Value = "PDF")]
        PDF ,
        [EnumMember(Value = "DOCX")]
        DOCX,
        [EnumMember(Value = "JPG")]
        JPG,
        [EnumMember(Value = "PNG")]
        PNG

    }
}
 