using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace SWSU_Dating
{
    public class Ankets
    {
        [Column("id")]
        public int id { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("age")]
        public int age { get; set; }
        [Column("zodiac")]
        public string zodiac { get; set; }
        [Column("interests")]
        public string interests { get; set; }
        [Column("aboutme")]
        public string aboutMe { get; set; }
        [Column("photourl")]
        public string photoUrl { get; set; }
        [Column("liked")]
        public string liked { get; set; }
        [Column("disliked")]
        public string disliked { get; set; }
        [Column("banned")]
        public string banned { get; set; }
        [Column("chatwith")]
        public string chatWith { get; set; }
        [Column("subscribe")]
        public bool subscribe { get; set; }
        [Column("sex")]
        public string sex { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("tgusername")]
        public string tgusername { get; set; }
        [Column("latitude")]
        public string latitude { get; set; }
        [Column("longtitude")]
        public string longtitude { get; set; }
        [Column("badhabbits")]
        public string badhabbits { get; set; }
    }
}
