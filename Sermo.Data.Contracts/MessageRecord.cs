namespace Sermo.Data.Contracts
{
    public class MessageRecord
    {
        public MessageRecord(int roomID, string authorName, string text)
        {
            RoomID = roomID;
            Text = text;
            AuthorName = authorName;
        }

        public int RoomID { get; private set; }

        public string Text { get; private set; }

        public string AuthorName { get; private set; }
    }
}