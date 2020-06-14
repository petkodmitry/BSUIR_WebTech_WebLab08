using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;
using WebLab.Extensions;
using WebLab.Models;

namespace WebLab.Controllers
{
    public class ProductController : Controller {
        ApplicationDbContext _context;
        int _pageSize;

        public ProductController(ApplicationDbContext context) {
            _pageSize = 3;
            _context = context;
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1) {
            var militariesFiltered = _context.Militaries.Where(d => !group.HasValue || d.MilitaryGroupId == group.Value);
            // Поместить список групп во ViewData 
            ViewData["Groups"] = _context.MilitaryGroups;

            // Получить id текущей группы и поместить в TempData 
            var currentGroup = group.HasValue ? group.Value : 0;

            ViewData["CurrentGroup"] = currentGroup;

            if (Request.IsAjaxRequest()) {
                return PartialView("_ListPartial", ListViewModel<Military>.GetModel(militariesFiltered, pageNo, _pageSize));
            }
            return View(ListViewModel<Military>.GetModel(militariesFiltered, pageNo, _pageSize));
        }
    }
}