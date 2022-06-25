using System;

namespace botFunction.Models
{
    public class QueueMessage
    {
        public string SenderUser { get; set; }

        public string Text { get; set; }

        public DateTime datetime { get; set; }

        public string RoomName { get; set; }

        public string ConnectionId { get; set; }

    }
}
