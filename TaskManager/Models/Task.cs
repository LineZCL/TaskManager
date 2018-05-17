using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum TaskStatus
    {
        [Description("Pendente")]
        PENDING,
        [Description("Em Produção")]
        PRODUCTION,
        [Description("Suspensa")]
        SUSPENSION,
        [Description("Finalizada")]
        FINISHED
    }

    [TableName("Task")]
    [PrimaryKey("id", AutoIncrement = true)]
    public class Task : BaseModel
    {
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Description { get; set; }

        public TaskStatus? Status { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime Date { get; set; }

        public long SponsorId { get; set; }

        [Ignore]
        public Profile Sponsor { get; set; }

        public List<Task> PendingTasks { get; set; }
    }
}