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

        public Relationship(User requesterUserId, User requestedUserId, int status)
        {
            RequesterUserId = requesterUserId;
            RequestedUserId = requestedUserId;
            Status = status;
        }

        public Relationship(User requesterUserId, User requestedUserId)
        {
            RequesterUserId = requesterUserId;
            RequestedUserId = requestedUserId;
        }

        public Relationship(User requesterUserId)
        {
            RequesterUserId = requesterUserId;
        }

        public User RequesterUserId { get; private set; }

        public User RequestedUserId { get; private set; }

        public int Status { get; private set; }
    }
}
