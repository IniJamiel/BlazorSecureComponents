using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
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
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Username { get; set; } = "";
        public DateTime Birthday { get; set; }
        [RegularExpression("^[0-9]*$")]
        public string PhoneNumber { get; set; } = "";

        public Tuple<bool, string> Validate()
        {
            List<String> errorList = new List<String>();
            if (!Regex.Match(this.Password,
                    "/^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\\D*\\d)(?=[^!#%-]*[!#%])[A-Za-z0-9!#%]{8,32}$/\r\n").Success)
            {
                errorList.Add("Password not viable");
            }

            if (!Regex.Match(this.Email, "(@)(.+)$").Success)
            {
                errorList.Add("Email not in correct format");
            }

            if (!Regex.Match(this.PhoneNumber, "^[0-9]*$").Success)
            {
                errorList.Add("Phone Number must only contains Numbers");
            }

            return new Tuple<bool, string>(true, "sukses");
        }

    }
}