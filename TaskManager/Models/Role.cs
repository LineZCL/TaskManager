using NPoco;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TaskManager.Models
{
    [TableName("Role")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Role
    {
        [Column("Id")]
        public long Id { get; set; }

        [Column("Description")]
        [Required(ErrorMessage = "Descrição é um campo obrigatório")]
        public string Description { get; set; }

        public List<Profile> Users { get; set; }
    }
}