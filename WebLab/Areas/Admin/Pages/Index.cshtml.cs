﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Entities;

namespace WebLab.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;

        public IndexModel(WebLab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Military> Military { get;set; }

        public async Task OnGetAsync()
        {
            Military = await _context.Militaries
                .Include(m => m.Group).ToListAsync();
        }
    }
}
