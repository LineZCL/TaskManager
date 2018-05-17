using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum TaskStatus
    {
        [Display(Name = "Pendente")]
        PENDING,
        [Display(Name = "Em Produção")]
        PRODUCTION,
        [Display(Name = "Suspensa")]
        SUSPENSION,
        [Display(Name = "Finalizada")]
        FINISHED
    }

    [TableName("Task")]
    [PrimaryKey("id", AutoIncrement = true)]
    public class Task : BaseModel
    {
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Description { get; set; }

        public TaskStatus? Status { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime Date { get; set; }

        [Display(Name = "Responsável")]
        [Reference(ReferenceType.Foreign, ColumnName = "SponsorId", ReferenceMemberName = "Id")]
        public Profile Sponsor { get; set; }

        public List<Task> PendingTasks { get; set; }
    }
}