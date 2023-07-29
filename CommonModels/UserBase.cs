using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Username { get; set; } = "";
        public DateTime Birthday { get; set; }
        [RegularExpression("^[0-9]*$")]
        public string PhoneNumber { get; set; } = "";

        public bool Locked { get; set; } = false;
        public Tuple<bool, string> Validate()
        {
            List<String> errorList = new List<String>();
            if (!Regex.Match(this.Password,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").Success)
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

            if (errorList.Any())
            {
                return new Tuple<bool, string>(false, string.Join(", ", errorList));
            }
            return new Tuple<bool, string>(true, "sukses");
        }

        public static bool validatePassword(string password)
        {
            return !Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").Success;
        }

    }
}