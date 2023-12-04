using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Contexts.Configurations
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.ToTable("tbl_Auction");

            builder.HasKey(e => e.Id).HasName("PK__tbl_Auct__3214EC27F52F6AD8");

            builder.HasIndex(e => e.IdAuction, "UQ_Auction_ID_Auction").IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.Property(e => e.IdAuction)
                .IsRequired()
                .HasColumnName("ID_Auction");

            builder.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");

            builder.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");

            builder.Property(e => e.HasBid).HasColumnName("Has_Bid");
            builder.Property(e => e.IdProduct).HasColumnName("ID_Product");
            builder.Property(e => e.IdWinner).HasColumnName("ID_Winner");
            builder.Property(e => e.IsEnd).HasColumnName("Is_End");
            builder.Property(e => e.IsStart).HasColumnName("Is_Start");
            builder.Property(e => e.PriceCurrentMax).HasColumnName("Price_Current_Max");

            builder.HasOne(d => d.IdProductNavigation).WithMany(p => p.Auctions)
                .HasPrincipalKey(p => p.IdProduct)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_Auction_Product");

            builder.HasOne(d => d.IdWinnerNavigation).WithMany(p => p.Auctions)
                .HasPrincipalKey(p => p.IdUser)
                .HasForeignKey(d => d.IdWinner)
                .HasConstraintName("FK_Auction_User");
        }
    }
}