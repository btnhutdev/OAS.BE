namespace Core.Constant
{
    public static class PermissionNameConst
    {
        public const string Auctioneer = "Auctioneer";
        public const string Bidder = "Bidder";
        public const string Admin = "Admin";

        public const string AuctioneerOrBidder = Auctioneer + "," + Bidder;
        public const string AuctioneerOrAdmin = Auctioneer + "," + Admin;
        public const string AllRole = Auctioneer + "," + Bidder + "," + Admin;
    }

    public static class PermissionTypeConst
    {
        public const string Auctioneer = "C05BDDA5-10BE-4837-8120-CD3E92A1B6B6";
        public const string Bidder = "0B747DEC-F3C2-4884-BA2C-AB7A1A2E23BB";
        public const string Admin = "854C6BCD-6650-44A2-8ED7-343BFD1632D5";
    }
}
