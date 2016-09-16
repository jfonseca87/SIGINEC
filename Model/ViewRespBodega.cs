using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ViewRespBodega
    {
        [Required(ErrorMessage="Debe seleccionar un usuario")]
        public int RespBodega { get; set; }
    }
}
