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
            UserId = userId;
            Created = created;
            Photo = photo;
            Video = video;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Text { get; private set; }
        public string Photo { get; private set; }
        public string Video { get; private set; }
        public DateTime Created { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }


        public bool Isvalid()
        {            
            
           bool valid = true;

           if ((string.IsNullOrEmpty(Photo) &&
               string.IsNullOrEmpty(Video) &&
               string.IsNullOrEmpty(Text)) || (
               !string.IsNullOrEmpty(Photo)&& 
               !string.IsNullOrEmpty(Video)))
                

           {
              valid = false;
           }

                return valid;

            
        }
    }
}
