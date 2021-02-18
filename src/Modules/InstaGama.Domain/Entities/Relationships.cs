
namespace InstaGama.Domain.Entities
{
    public class Relationships
    {
        public Relationships(int userId,
                             int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
        public Relationships(int id, 
                             int userId, 
                             int friendId)
        {
            Id = id;
            UserId = userId;
            FriendId = friendId;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int FriendId { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }

    }
}
