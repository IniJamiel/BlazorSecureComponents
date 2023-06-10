using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModelsLib
{
    public class UserBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Email = "";
        public string Password = "";
        public string Username = "";
    }
}