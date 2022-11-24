using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Models
{
	[Table("owners")]
	public class Owner
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        

        [Required]
        public int ZIPCode { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Bus> Buses { get; set; }

        public Owner()
        {
            Buses = new HashSet<Bus>();
        }

        public bool? HasMustache { get; set; }

        public void CopyFrom(Owner owner)
        {
            this.Id = owner.Id;
            this.Name = owner.Name;
            this.ZIPCode = owner.ZIPCode;

            
        }
    }
}

