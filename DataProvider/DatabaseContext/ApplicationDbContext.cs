using DataProvider.Models.Chat;
using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, UserClaim,
        UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }


        public DbSet<ChatHubConnection> ChatHubConnections { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");

            //Identity user section
            builder.Entity<User>(e => { e.ToTable(name: "Users"); });
            builder.Entity<Role>(e => { e.ToTable(name: "Roles"); });
            builder.Entity<UserRole>(e => { e.ToTable(name: "UserRoles"); });
            builder.Entity<RoleClaim>(e => { e.ToTable(name: "RoleClaims"); });
            builder.Entity<UserClaim>(e => { e.ToTable(name: "UserClaims"); });
            builder.Entity<UserLogin>(e => { e.ToTable(name: "UserLogins"); });
            builder.Entity<UserToken>(e => { e.ToTable(name: "UserTokens"); });

            // Other Custom model entity section
            builder.Entity<UserProfile>(e => { e.ToTable(name: "UserProfiles"); });
            builder.Entity<Message>(e => {
                e.ToTable(name: "Messages");
                e.HasOne(m => m.Sender)
                   .WithMany()
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(m => m.Receiver)
                    .WithMany() 
                    .HasForeignKey(m => m.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ChatHubConnection>(e => { e.ToTable(name: "ChatHubConnections"); });
            builder.Entity<ChatRoom>(e => {
                 e.ToTable(name: "ChatRooms");
                 e.HasMany(cr => cr.RoomUsers)
                    .WithOne()
                    .HasForeignKey("ChatRoomId")
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(cr => cr.Messages)
                    .WithOne()
                    .HasForeignKey("ChatRoomId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<RoomUser>(e =>
            {
                e.ToTable(name: "RoomUsers");
                // Configure as keyless
                //e.HasNoKey();

                e.HasOne(m => m.ChatRoom)
                  .WithMany()
                  .HasForeignKey(m => m.ChatRoomId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

        }

    }
}
