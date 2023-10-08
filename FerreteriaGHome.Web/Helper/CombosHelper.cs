using FerreteriaGHome.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Helper
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext dataContext;
        

        public CombosHelper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = this.dataContext.Roles.Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = $"{b.Id}"
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Selecciona un rol",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPriorities()
        {
            var list = this.dataContext.Priorities.Select(b => new SelectListItem
            {
                Text = b.Description,
                Value = $"{b.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "Selecciona la Prioridad",
                Value = "0"
            });

            return list;
        }


      




    }
    
}
