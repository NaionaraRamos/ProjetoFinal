using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
    public class Relationship
    {
       /* public Relationship(int userId,
                             int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
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

        public void SetId(int id)
        {
            Id = id;
        }*/

        public Relationship(int requesterUserId, int requestedUserId, int status)
        {
            RequesterUserId = requesterUserId;
            RequestedUserId = requestedUserId;
            Status = status;
        }

        public Relationship(int requesterUserId, int requestedUserId)
        {
            RequesterUserId = requesterUserId;
            RequestedUserId = requestedUserId;
        }

        public int RequesterUserId { get; private set; }

        public int RequestedUserId { get; private set; }

        public int Status { get; private set; }
    }
}
