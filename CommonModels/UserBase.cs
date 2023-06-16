using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonModelsLib
{
    public class UserBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [RegularExpression("(@)(.+)$")]
        [Required]
        public string Email { get; set; } = "";
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Username { get; set; } = "";
        public DateTime Birthday { get; set; }
        [RegularExpression("^[0-9]*$")]
        public string PhoneNumber { get; set; } = "";

        public Tuple<bool, string> Validate()
        {


            return new Tuple<bool, string>(true, "sukses");
        }

    }
}