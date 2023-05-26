using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FYP2.Models
{
    public class ShowAgentVariables
    {
        [Display(Name = "Agent Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string PicPath { get; set; }


        public int Id { get; set; }
        public string ContactNo{get;set;}
        public string MapUrl { get; set; }
        public string area { get; set; }
        public string block { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        //For images

        public string imageName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

        
    }
}