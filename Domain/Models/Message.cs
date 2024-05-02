using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
