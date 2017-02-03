using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.ViewModel.CustomQuery
{
    public class EditFieldViewModel : FieldVM
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string id { get; set; }
    }
}
