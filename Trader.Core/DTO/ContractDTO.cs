using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Trader.Core.DTO
{
    [Event("TransactionExecuted")]
    public class TransactionEventDTO : IEventDTO
    {
        [Parameter("address", "source_address", 1, true)]
        public string Address { get; set; }
        [Parameter("int", "source_asset_index", 2, false)]
        public BigInteger SourceIndex { get; set; }

        [Parameter("int", "target_asset_index", 3, false)]
        public BigInteger TargetIndex { get; set; }

        [Parameter("bytes6", "source", 4, false)]
        public byte[] Source { get; set; }

        [Parameter("bytes6", "target", 5, false)]
        public byte[] Target { get; set; }
        [Parameter("int", "quantity", 6, false)]
        public BigInteger Quantity { get; set; }

        [Parameter("int", "price", 8, false)]
        public BigInteger Price { get; set; }

        [Parameter("uint256", "timestamp", 9, false)]
        public BigInteger Timestamp { get; set; }

        [Parameter("int8", "state", 10, false)]
        public BigInteger State { get; set; }


    }
    [Event("AssetUpdated")]
    public class AssetUpdatedEventDTO : AssetJoinedEventDTO
    {


    }
    [Event("AssetJoined")]
    public class AssetJoinedEventDTO : IEventDTO
    {
        [Parameter("address", "asset_address", 1, true)]
        public string Address { get; set; }
        [Parameter("int", "index", 2, false)]
        public BigInteger Index { get; set; }

        [Parameter("bytes6", "id", 3, false)]
        public byte[] Id { get; set; }

        [Parameter("int", "quantity", 4, false)]
        public BigInteger Quantity { get; set; }

        [Parameter("int", "price", 5, false)]
        public BigInteger Price { get; set; }

        [Parameter("uint256", "timestamp", 6, false)]
        public BigInteger Timestamp { get; set; }

    }
    [FunctionOutput]
    public class AssetDTO
    {
        [Parameter("bytes6", "id", 1, false)]
        public byte[] Name { get; set; }

        [Parameter("int", "price", 2, false)]
        public BigInteger Price { get; set; }

        [Parameter("int", "quantity", 3, false)]
        public BigInteger Quantity { get; set; }


    }

    [FunctionOutput]
    public class TransactionDTO
    {
        [Parameter("bytes6", "source", 1, false)]
        public byte[] Source { get; set; }

        [Parameter("bytes6", "target", 2, false)]
        public byte[] Target { get; set; }
        [Parameter("int", "quantity", 3, false)]
        public BigInteger Quantity { get; set; }

        [Parameter("int", "price", 4, false)]
        public BigInteger Price { get; set; }

        [Parameter("uint256", "timestamp", 5, false)]
        public BigInteger Timestamp { get; set; }

        [Parameter("int8", "state", 6, false)]
        public BigInteger State { get; set; }


    }
}
