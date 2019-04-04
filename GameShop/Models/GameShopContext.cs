namespace GameShop.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GameShopContext : DbContext
    {
        public GameShopContext()
            : base("name=GameShopContext")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(e => e.comment1)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.game_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasOptional(e => e.Discount)
                .WithRequired(e => e.Game);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.game_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.game_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Games)
                .Map(m => m.ToTable("GenreGame").MapLeftKey("game_id").MapRightKey("genre_id"));

            modelBuilder.Entity<Genre>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.balance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
