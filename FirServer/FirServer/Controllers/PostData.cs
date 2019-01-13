using System.ComponentModel.DataAnnotations;

namespace FirServer.Controllers
{
    public class PostData
    {
        //[Required]
        //public int Id { get; set; }

        //[Display(Name = "姓名"), Required, MaxLength(20, ErrorMessage ="{0}的长度不可超过{1}")]
        //public string Name { get; set; }

        //[MinLength(3)]
        //public string AccountName { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }

        //[Url]
        //public string BlogUrl { get; set; }

        //[Range(100, 10000)]
        //public decimal Salary { get; set; }

        [Required, MinLength(3), MaxLength(10)]
        public string Action { get; set; }

        [Required, MinLength(3), MaxLength(10)]
        public string Method { get; set; }

        [Required, MaxLength(512, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Data { get; set; }
    }
}
