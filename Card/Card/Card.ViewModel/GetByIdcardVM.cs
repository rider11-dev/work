using MyNet.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.ViewModel
{
    public class GetByIdcardVM
    {
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(ViewModelResource))]
        public string idcard { get; set; }
    }
}
