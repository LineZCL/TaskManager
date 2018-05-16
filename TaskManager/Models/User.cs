using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TaskManager.Models
{
    [TableName("Profile")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Profile
    {
        public long Id{ get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo precisa ser um e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [MaxLength(16, ErrorMessage = "A senha pode ter até 16 caracteres.")]
        public string Password { get; set; }

        public Boolean IsActive { get; set; }

        public List<Task> Tasks { get; set; }

        public Role Role { get; set; }
    }
}