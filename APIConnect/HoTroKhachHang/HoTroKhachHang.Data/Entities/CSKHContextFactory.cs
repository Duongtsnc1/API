using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Data.Entities
{
    class CSKHContextFactory : IDesignTimeDbContextFactory<CSKHContext>
    {
        public CSKHContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CSKHContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-9IHGHBO;Initial Catalog=DemoToken;Integrated Security=True");
            return new CSKHContext(optionsBuilder.Options);
        }
    }
}
