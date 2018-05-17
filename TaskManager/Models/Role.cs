using NPoco;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TaskManager.Models
{
    [TableName("Role")]
    [PrimaryKey("id", AutoIncrement = true)]
    public class Role : BaseModel
    {
        [Column("Description")]
        [Required(ErrorMessage = "Descrição é um campo obrigatório")]
        public string Description { get; set; }

    }
}