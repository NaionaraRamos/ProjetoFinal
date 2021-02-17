using System;

namespace InstaGama.Domain.Entities
{
    public class Postage
    {
        public Postage(string text,
                       string image,
                       string video,
                       int userId)
        {
            Text = text;
            UserId = userId;
            Image = image;
            Video = video;
            Created = DateTime.Now;
        }

        public Postage(int id,
                        string text,
                        string image,
                        string video,
                        int userId,
                        DateTime created)
        {
            Id = id;
            Text = text;
            Image = image;
            Video = video;
            UserId = userId;
            Created = created;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Text { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }
        public DateTime Created { get; private set; }

        public bool IsValid()
        {
            bool valid = true;


            if (string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(Image) && string.IsNullOrEmpty(Video))
            {
                valid = false;
            }

            if (!string.IsNullOrEmpty(Image) && !string.IsNullOrEmpty(Video))
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
