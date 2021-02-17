using System;

namespace InstaGama.Domain.Entities
{
    public class Postage
    {
        public Postage(string text,
                       string photo,
                       string video,
                       int userId)
        {
            Text = text;
            UserId = userId;
            Photo = photo;
            Video = video;
            Created = DateTime.Now;
        }

        public Postage(int id,
                        string text,
                        string photo,
                        string video,
                        int userId,
                        DateTime created)
        {
            Id = id;
            Text = text;
            Photo = photo;
            Video = video;
            UserId = userId;
            Created = created;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Text { get; private set; }
        public string Photo { get; private set; }
        public string Video { get; private set; }
        public DateTime Created { get; private set; }

        public bool IsValid()
        {
            bool valid = true;


            if (string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(Photo) && string.IsNullOrEmpty(Video))
            {
                valid = false;
            }

            if (!string.IsNullOrEmpty(Photo) && !string.IsNullOrEmpty(Video))
            {
                valid = false;
            }

            return valid;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
