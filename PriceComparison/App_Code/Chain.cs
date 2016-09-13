using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public struct Chain
{
    public long ChainId { get; private set; }
    public string ChainName { get; private set; }

    public Chain(long id, string name)
    {
        this.ChainId = id;
        this.ChainName = name;
    }
}