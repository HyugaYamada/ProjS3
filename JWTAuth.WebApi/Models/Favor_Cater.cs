using System.ComponentModel.DataAnnotations;

namespace khiemnguyen.WebApi.Models
{
    public class Favor_Cater
    {
        [Key]
        public int id { get; set; }
        public int Custid { get; set; }
        public int Caterid { get; set; }
    }
}
