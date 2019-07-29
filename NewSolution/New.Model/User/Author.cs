using Microsoft.AspNetCore.Mvc;
using New.Model.Binder;
using System;
using System.Collections.Generic;
using System.Text;

namespace New.Model.User
{
    //[ModelBinder(BinderType = typeof(AuthorEntityBinder))]
    public class AuthorBase
    {
        public string CompanyCode { get; set; }
        public int AdmDivCode { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string GitHub { get; set; }
        public string Twitter { get; set; }
        public string BlogUrl { get; set; }
        
    }
    public class Author<T>: AuthorBase
    {
        public List<T> Info { get; set; }
    }
    public class Book
    {
        public string Name { get; set; }
    }
}
