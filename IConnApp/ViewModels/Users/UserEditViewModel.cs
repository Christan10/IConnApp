using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IConnApp.ViewModels.Users
{
    public class UserEditViewModel
    {
        [Required, Range(1, 100)]
        public int Id { get; set; }

        [Required, DataType(DataType.EmailAddress), MinLength(6)]
        public string Email { get; set; }

        [Required, MinLength(2)]
        public string Name { get; set; }

        [Required, MinLength(2)]
        public string Surname { get; set; }

        [Required, Range(16, 99, ErrorMessage = "Age must be between 16 and 99 years")]
        public int Age { get; set; }
    }
}
