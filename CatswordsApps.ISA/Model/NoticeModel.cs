using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatswordsApps.ISA.Model
{
    public class NoticeModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Content { get; set; }
        public int? GroupID { get; set; }
    }
}
