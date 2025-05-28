using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesInformationSystem.Models;

namespace SalesInformationSystem.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<SalesInformationSystem.Models.Customer> Customer { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.Quotation> Quotation { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.Product> Product { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.Invoice> Invoice { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.Payment> Payment { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.QuotationItem> QuotationItem { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.SalesOrder> SalesOrder { get; set; } = default!;

public DbSet<SalesInformationSystem.Models.CartItems> CartItems { get; set; } = default!;

}
