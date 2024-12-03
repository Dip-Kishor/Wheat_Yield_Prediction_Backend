using BackendCodeForML.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCodeForML.Data
{
    public class WYPredictionContext :DbContext
    {
        public WYPredictionContext(DbContextOptions<WYPredictionContext> options)
       : base(options)
        {
        }
         public DbSet<RegisterModel> Users { get; set; }
         public DbSet<UserRoleModel> UserRoles { get; set; }
         public DbSet<DistrictData> EDistrict { get; set; }
    }
}
