namespace GroceryStoreAPI.Responses
{
    public class MessageInfo
    {
        public string Field { get; set; }
        
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Field}: {Message}";
        }
    }
}