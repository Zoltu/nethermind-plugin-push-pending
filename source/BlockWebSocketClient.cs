using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Nethermind.Core;
using Nethermind.Logging;
using Nethermind.Serialization.Json;

namespace Zoltu.Nethermind.Plugin.WebSocketPush
{
	public sealed class BlockWebSocketClient : WebSocketClient
	{
		private readonly IJsonSerializer _jsonSerializer;

		public BlockWebSocketClient(ILogger logger, IJsonSerializer jsonSerializer, IWebSocketPushConfig config, WebSocket webSocket, String id, String client) : base(logger, config, webSocket, id, client)
		{
			_jsonSerializer = jsonSerializer;
		}

		public async Task Send(Block block)
		{
			var simpleBlock = new
			{
				ParentHash = block.ParentHash!,
				Hash = block.Hash!,
				block.Number,
				block.Timestamp
			};
			var transactionAsString = _jsonSerializer.Serialize(simpleBlock);
			await SendRawAsync(transactionAsString);
		}
	}
}
