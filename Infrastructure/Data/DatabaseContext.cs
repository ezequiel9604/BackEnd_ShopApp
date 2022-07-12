
//using System.Data.Entity;
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

        builder.Entity<Administrator>()
            .Property(x => x.PhoneNumber)
            .IsRequired(false);

        builder.Entity<Administrator>()
            .HasIndex(x => x.Email)
            .IsUnique();

        builder.Entity<Order>()
            .Property(x => x.Note)
            .IsRequired(false);

        builder.Entity<Appearance>()
            .HasData(
                new Appearance() { Id = 1, Name = "light" },
                new Appearance() { Id = 2, Name = "dark" }
            );

        builder.Entity<Currancy>()
            .HasData(
                new Currancy() { Id= 1, Name = "usa / dollars" },
                new Currancy() { Id= 2, Name = "dom / pesos" }
            );

        builder.Entity<Language>()
            .HasData(
                new Language() { Id = 1, Name = "english" },
                new Language() { Id = 2, Name = "spanish" }
            );

        builder.Entity<Brand>()
            .HasData(
                new Brand() { Id = 1, Name = "samsung" },
                new Brand() { Id = 2, Name = "panasonic" }
            );
        builder.Entity<ClientType>()
            .HasData(
                new ClientType() { Id = 1,  Name = "normal" },
                new ClientType() { Id = 2, Name = "express" }
            );
        builder.Entity<State>()
            .HasData(
                new State() { Id = 1, Name = "connected" },
                new State() { Id = 2, Name = "suspended" },
                new State() { Id = 3, Name = "offline" },
                new State() { Id = 4, Name = "retired" }
            );
        builder.Entity<Category>()
            .HasData(
                new Category() { Id = 1, Name = "clothing" },
                new Category() { Id = 2, Name = "office" },
                new Category() { Id = 3, Name = "technology" },
                new Category() { Id = 4, Name = "home" }
            );

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
