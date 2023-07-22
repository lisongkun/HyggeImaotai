namespace hygge_imaotai.Entity
{
    /// <summary>
    /// I茅台预约商品信息
    /// </summary>
    public class IMTItemInfo
    {
        #region Properties

        public string ShopId { get; set; }

        public int Count { get; set; }

        public string ItemId { get; set; }

        public int Inventory { get; set; }

        #endregion

        #region Construct

        public IMTItemInfo(string shopId, int count, string itemId, int inventory)
        {
            ShopId = shopId;
            Count = count;
            ItemId = itemId;
            Inventory = inventory;
        }

        public IMTItemInfo()
        {
        }

        #endregion
    }
}
