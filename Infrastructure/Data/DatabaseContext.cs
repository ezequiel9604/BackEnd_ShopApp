
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Client>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }

    public DbSet<Administrator>? Administrators { get; set; }
    public DbSet<Chat>? Chats { get; set; }
    public DbSet<Client>? Clients { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Phone>? Phones { get; set; }
    public DbSet<Appearance>? Appearances { get; set; }
    public DbSet<Language>? Languages { get; set; }
    public DbSet<State>? States { get; set; }
    public DbSet<Currancy>? Currancies { get; set; }
    public DbSet<ClientType>? Types { get; set; }
    public DbSet<ShoppingCart>? ShoppingCarts { get; set; }
    public DbSet<WishList>? WishLists { get; set; }
    public DbSet<Wallet>? Wallets { get; set; }
    public DbSet<Comment>? Comments { get; set; }
    public DbSet<Item>? Items { get; set; }
    public DbSet<Image>? Images { get; set; }
    public DbSet<SubItem>? SubItems { get; set; }
    public DbSet<Brand>? Brands { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<SubCategory>? SubCategories { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Purchase>? Purchases { get; set; }
    public DbSet<Status>? Statuses { get; set; }

}
