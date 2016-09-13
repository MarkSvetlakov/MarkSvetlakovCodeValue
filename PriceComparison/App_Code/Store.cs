using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public struct Store
{
    public long ChainId { get; private set; }
    public long StoreId { get; private set; }
    public string StoreName { get; private set; }
    public string ReportDate { get; private set; }

    public Store(long chainId, long storeId, string storeName, string reportDate)
    {
        this.ChainId = chainId;
        this.StoreId = storeId;
        this.StoreName = storeName;
        this.ReportDate = reportDate;
    }
}