using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marrwie.Models
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public RequestPage RequestPage { get; set; }

        public List<MyRoleViewModel> MyRoles { get; set; }

        public ChangeRoleViewModel()
        {
            MyRoles = new List<MyRoleViewModel>();
        }
    }

    public class MyRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasGot { get; set; }
    }

    public enum RequestPage
    {
        Kullanicilar, Yetkiler
    }
}