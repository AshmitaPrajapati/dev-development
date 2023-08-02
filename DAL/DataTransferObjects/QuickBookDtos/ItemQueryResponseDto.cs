namespace DAL.DataTransferObjects.QuickBookDtos
{
    public class ItemQueryResponseDto
    {
        public QueryResponse QueryResponse { get; set; }
    }

    public class QueryResponse
    {
        public List<Item> Item { get; set; }
    }

    public class ItemResponse
    {
        public Item Item { get; set; }
        public DateTime time { get; set; }
    }

    public class AssetAccountRefResponse
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class ExpenseAccountRefResponse
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class IncomeAccountRefResponse
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string FullyQualifiedName { get; set; }
        public bool Taxable { get; set; }
        public int UnitPrice { get; set; }
        public string Type { get; set; }
        public bool TrackQtyOnHand { get; set; }
        public int QtyOnHand { get; set; }
        public string InvStartDate { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public MetaData MetaData { get; set; }
    }

    public class MetaData
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
    public class TaxClassificationRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }
}
