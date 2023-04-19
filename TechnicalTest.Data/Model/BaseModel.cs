using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Data.Model
{
    public class BaseModel<IdType>
    {
        public IdType Id { get; set; }
        public int CreatedByUserID { get; set;  }
        public DateTime CreateDate { get; set; }
        public int UpdatedByUserID { get; set; }

        public DateTime UpdateDate { get; set; }

        public int? DeletedByUserID { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
