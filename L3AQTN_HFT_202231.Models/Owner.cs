using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Models
{
	[Table("owners")]
	public class Owner:IDbEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [MaxLength(4)]
        [Required]
        public int ZIPCode { get; set; }
        [NotMapped]
        public virtual ICollection<Bus> Buses { get; set; }

        public void CopyFrom(Owner owner)
        {
            this.Id = owner.Id;
            this.Name = owner.Name;
            this.ZIPCode = owner.ZIPCode;

            
        }
    }
}

