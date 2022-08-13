using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace API_IA_GOOGLE.Data
{
    public class API_IA_GOOGLEContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public API_IA_GOOGLEContext() : base("name=API_IA_GOOGLEContext")
        {
        }

        public System.Data.Entity.DbSet<API_IA_GOOGLE.Models.FACEBOOKJS> FACEBOOKJS { get; set; }
    }
}
