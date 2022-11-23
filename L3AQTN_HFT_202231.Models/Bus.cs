using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Models
{

	[Table("buses")]
	public class Bus : IDbEntity
	{


		public Bus() { }

		public Bus(Bus bus)
		{
			
		}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int  Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Model { get; set; }

        [NotMapped]
        public virtual  Brand Brand { get; set; }
        public int BrandId { get; set; }
        public int? Price { get; set; }

        public int OwnerId { get; set; }
        [NotMapped]
        public virtual Owner Owner { get; set; }

        public void CopyFrom(Bus bus)
		{
			this.Model = bus.Model;
			this.BrandId = bus.BrandId;
			this.Price = bus.Price;
            this.OwnerId = bus.OwnerId;
		}

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Model)}: {Model}, {nameof(Price)}: {Price}, {nameof(BrandId)}: {BrandId}, ({nameof(Brand)}: {Brand})";
        }

    }
}

