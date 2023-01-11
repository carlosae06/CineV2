using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.DTOs
{
    public class UserInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int sedeId { get; set; }
        [Required]
        public int appId { get; set; }
    }

    public class UserAcademicoInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int sedeId { get; set; }
        [Required]
        public string Tipo { get; set; }



    }
}
