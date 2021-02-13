using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
    
    public class Relationship
        
    {
        public Relationship(int id,
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
    }

}