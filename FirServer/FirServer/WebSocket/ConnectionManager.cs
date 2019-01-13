using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketManager
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<long, WebSocket> _sockets = new ConcurrentDictionary<long, WebSocket>();
        private ConcurrentDictionary<string, List<long>> _groups = new ConcurrentDictionary<string, List<long>>();

        public WebSocket GetSocketById(long id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<long, WebSocket> GetAll()
        {
            return _sockets;
        }

        public List<long> GetAllFromGroup(string GroupID)
        {
            if (_groups.ContainsKey(GroupID))
            {
                return _groups[GroupID];
            }

            return default(List<long>);
        }

        public long GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(CreateConnectionId(), socket);
        }

        public void AddToGroup(long socketID, string groupID)
        {
            if (_groups.ContainsKey(groupID))
            {
                _groups[groupID].Add(socketID);

                return;
            }

            _groups.TryAdd(groupID, new List<long> { socketID });
        }

        public void RemoveFromGroup(long socketID, string groupID)
        {
            if (_groups.ContainsKey(groupID))
            {
                _groups[groupID].Remove(socketID);
            }
        }

        public async Task RemoveSocket(long id)
        {
            if (id == 0) return;

            WebSocket socket;
            _sockets.TryRemove(id, out socket);

            if (socket.State != WebSocketState.Open) return;

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None).ConfigureAwait(false);
        }

        private long CreateConnectionId()
        {
            return Utility.AppUtil.NewGuidId();
        }
    }
}
