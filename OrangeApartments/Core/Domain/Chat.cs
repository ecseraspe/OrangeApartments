using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class Chat
    {
        public int ChatId { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime MessageDate { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}