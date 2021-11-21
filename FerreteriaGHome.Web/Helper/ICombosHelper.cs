using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Helper
{
    public interface ICombosHelper
    {
        public IEnumerable<SelectListItem> GetComboBrands();
        public IEnumerable<SelectListItem> GetComboProducts();

        public IEnumerable<SelectListItem> GetComboItems();
    }
}
