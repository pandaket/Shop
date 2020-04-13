using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace test.Models
{  
    [Table("users")]
    public class Users : IdentityUser<int>
    {
        [Column("uid"), Key]
        public int Uid { get; set; }

        [Column("login")]
        public override string UserName { get; set; }

        [Column("email")]
        public override string Email { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("patronymic")]
        public string Patronymic { get; set; }

        [Column("born")]
        public DateTime? Born { get; set; }

        [Column("phonenumber")]
        public string Phonenumber { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("password")]
        public override string PasswordHash { get; set; }

        //cлучайное значение, которое должно меняться при изменении учетных данных пользователя (пароль изменен, логин удален)
        [Column("securitystamp")]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }

        public Users() { }

        //For Create
        public Users(string login, string email, string sur, string name, string p, DateTime? b, string ph, string addr, string psw)
        {
            Email = email ?? string.Empty;
            UserName = login ?? string.Empty;
            Surname = sur ?? string.Empty;
            Name = name ?? string.Empty;
            Patronymic = p ?? string.Empty;
            PasswordHash = psw ?? string.Empty;
            SecurityStamp = Guid.NewGuid().ToString();
            Phonenumber = ph ?? string.Empty;
            Address = addr ?? string.Empty;
        }

        //For Edit
        public Users(int id, string login, string email, string sur, string name, string p, DateTime? b, string ph, string addr, string psw)
        {
            Email = email ?? string.Empty;
            UserName = login ?? string.Empty;
            Surname = sur ?? string.Empty;
            Name = name ?? string.Empty;
            Patronymic = p ?? string.Empty;
            PasswordHash = psw ?? string.Empty;
            SecurityStamp = Guid.NewGuid().ToString();
            Phonenumber = ph ?? string.Empty;
            Address = addr ?? string.Empty;
        }

        public Users(int id, string email, string login)
        {
            Uid = id;
            Email = email;
            UserName = login;
        }

        public string getFio()
        {
            return string.Join(" ", Surname, Name, Patronymic);
        }

        public string getFioWithPoint()
        {
            return Surname + " " + Name.Substring(0, 1).ToUpper() + ". " + Patronymic.Substring(0, 1).ToUpper() + ".";
        }
    }
}