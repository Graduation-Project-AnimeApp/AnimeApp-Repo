using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeFlixBackend.Domain.Entities
{
    public class AuthToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenId { get; set; }
        public string TokenName { get; set; }
        public DateTime TokenExpirationDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
