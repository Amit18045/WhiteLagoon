﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaRepository(ApplicationDbContext context):base(db)
        {
            _context=context;
        }
  
     

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Villa villa)
        {
            _context.Villas.Update(villa);
        }
    }
}
