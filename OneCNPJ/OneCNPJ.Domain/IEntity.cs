using OneCNPJ.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Domain
{
    public interface IEntity
    {
        long Id { get; set; }
        StatusEnum Status { get; set; }
    }
}
