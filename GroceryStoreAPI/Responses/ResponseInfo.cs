using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Responses
{
    public class ResponseInfo<T>
    {
        private readonly List<MessageInfo> _messages;

        public T Data { get;  }
        public bool Success { get; }
        
        public ResponseInfo(bool status, T data = default(T))
        {
            _messages = new List<MessageInfo>();
            Success = status;
            Data = data;
        }

        public void AddMessage(string field, string message)
        {
            _messages.Add(new MessageInfo
            {
                Field = field,
                Message = message
            });
        }

        public IEnumerable<MessageInfo> Messages => _messages.AsEnumerable();

        public static implicit operator ResponseInfo<T>(T value)
        {
            return new ResponseInfo<T>(true, value);
        }
    }
}