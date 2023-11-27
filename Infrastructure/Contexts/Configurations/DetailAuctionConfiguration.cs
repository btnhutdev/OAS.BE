using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class DetailAuctionConfiguration : IEntityTypeConfiguration<DetailAuction>
    {
        public void Configure(EntityTypeBuilder<DetailAuction> builder)
        {
            builder.ToTable("tbl_Detail_Auction");
            builder.HasKey(e => e.Id).HasName("PK__tbl_Deta__3214EC27CAA99C62");
            builder.HasIndex(e => e.IdDetailAuction, "UQ_Detail_Auction_ID_Detail_Auction").IsUnique();
            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.Property(e => e.AuctionType).HasColumnName("Auction_Type");
            builder.Property(e => e.CurrentPrice).HasColumnName("Current_Price");
            builder.Property(e => e.IdAuction).HasColumnName("ID_Auction");
            builder.Property(e => e.IdBidder).HasColumnName("ID_Bidder");
            builder.Property(e => e.IdDetailAuction).HasColumnName("ID_Detail_Auction");
            builder.Property(e => e.MaxBidPrice).HasColumnName("Max_Bid_Price");

            builder.HasOne(d => d.IdAuctionNavigation).WithMany(p => p.DetailAuctions)
                .HasPrincipalKey(p => p.IdAuction)
                .HasForeignKey(d => d.IdAuction)
                .HasConstraintName("FK_Detail_Auction_Auction");

            builder.HasOne(d => d.IdBidderNavigation).WithMany(p => p.DetailAuctions)
                .HasPrincipalKey(p => p.IdUser)
                .HasForeignKey(d => d.IdBidder)
                .HasConstraintName("FK_Detail_Auction_Bidder");
        }
    }
}
