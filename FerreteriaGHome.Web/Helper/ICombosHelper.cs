using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Helper
{
    public interface ICombosHelper
    {
        public IEnumerable<SelectListItem> GetComboRoles();
        public IEnumerable<SelectListItem> GetComboPriorities();
        public IEnumerable<SelectListItem> GetComboStatuses();


    }
}
