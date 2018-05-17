using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class BaseModel
    {
        public long Id { get; set; }

        public Boolean IsActive { get; set; }
    }
}