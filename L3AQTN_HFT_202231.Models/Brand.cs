using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace L3AQTN_HFT_202231.Models
{

	[Table("brands")]
	public class Brand
	{
		

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Bus> Buses { get; set; }

        public Brand()
        {
            Buses = new HashSet<Bus>();
        }

        public void CopyFrom(Brand other)
        {
           
            this.Name = other.Name;
            this.Buses = other.Buses.Select(k => new Bus(k)).ToHashSet();
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}

