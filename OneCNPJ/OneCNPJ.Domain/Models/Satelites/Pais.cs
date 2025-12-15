using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Domain.Models.Satelites
{
    [Table("pais")]
    public class Pais : BaseModel, IEntity
    {

        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;
    }
}