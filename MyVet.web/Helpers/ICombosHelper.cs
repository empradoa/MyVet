using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyVet.web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPetTypes();

        IEnumerable<SelectListItem> GetComboServiceTypes();
    }
}