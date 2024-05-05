using Contract.Dto.ExamPlaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
 
namespace Contract.Dto.Exams
{

    public class TimeSpanToStringConverter : System.Text.Json.Serialization.JsonConverter<TimeSpan>
    {
        //public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //{
        //    var value = reader.GetString();
        //    return TimeSpan.Parse(value);
        //}

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Read properties from the object (assuming Hours, Minutes, Seconds)
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        if (reader.Read())
                        {
                            switch (propertyName)
                            {
                                case "hours":
                                    hours = reader.GetInt32();
                                    break;
                                case "minutes":
                                    minutes = reader.GetInt32();
                                    break;
                                case "seconds":
                                    seconds = reader.GetInt32();
                                    break;
                            }
                        }
                    }
                    else if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }
                }
                return new TimeSpan(hours, minutes, seconds);
            }
            else
            {
                throw new System.Text.Json.JsonException("Expected a JSON object for DeadLine property.");
            }
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
    public class ExamDto
    {
        public string Name { get; set; }
        public int FaullMarks { get; set; }
        public string Title { get; set; }
        public DateTime SartedAt { get; set; }
        //    [DataType(DataType.Duration)]

        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanToStringConverter))]
        public TimeSpan DeadLine { get; set; }
        public ExamType ExamType { get; set; }
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public int CourseCycleId { get; set; }
        public string ProfessorUserName { get; set; }
        public string InstructorUserName { get; set; }
        public string StuffUserName {  get; set; }  
        public ExamPlaceDto GetExamPlaceDto()
        {
            return new ExamPlaceDto
            {
                ExamType = ExamType,
                CourseCycleId = CourseCycleId,
                SectionId = SectionId,
                CourseId = CourseId
            };
        }

        public Exam GetExam( )
        {
            return new Exam
            {
                Name = Name,
                FullMark = FaullMarks,
                Title = Title,
                StratedAt = SartedAt,
                DeadLine = DeadLine

                //ExamPlaceId will be added in CQRS of add exam to get ExamPlaceId according to type of exam
            };
        }

    }
}
