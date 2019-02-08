using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Model
{
    public class UsersRegisterNotCompleteListModel
    {

        public UsersRegisterNotCompleteListModel(int id, string username, string role, bool is_expir, DateTime expiry_time, string code)
        {
            this.ID = id;
            this.Username = username;
            this.Role = role;
            this.Is_Expiry = is_expir;
            this.Expiry_Time = expiry_time;
            this.Code = code;
        }

        public int ID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool Is_Expiry { get; set; }
        public DateTime Expiry_Time { get; set; }
        public string Code { get; set; }
    }
}