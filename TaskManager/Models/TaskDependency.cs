using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class TaskDependency : BaseModel
    {
        [Reference(ReferenceType.Foreign, ColumnName = "TaskId", ReferenceMemberName = "Id")]
        public Task Task { get; set; }
        [Reference(ReferenceType.Foreign, ColumnName = "PendingTaskId", ReferenceMemberName = "Id")]
        public Task PendingTask { get; set; }
    }
}