using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum TaskStatus
    {
        PENDING,
        PRODUCTION,
        SUSPENSION,
        FINISHED
    }

    [TableName("Task")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Task
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime Date { get; set; }

        public Boolean IsActive { get; set; }

        public Profile Sponsor { get; set; }

        public List<Task> PendingTasks { get; set; }
    }
}