//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server.AppData.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ratings
    {
        public Ratings()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int idRating { get; set; }
        public string name { get; set; }
        public int limit { get; set; }
    
        public virtual ICollection<Users> Users { get; set; }
    }
}
