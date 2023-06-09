using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.AppData
{
    internal class UserObj
    {
        public static int id_user { get; set; }

        public static string surname { get; set; }

        public static string name { get; set; }

        public static string patronymic { get; set; }

        public static string email { get; set; }

        public static int role { get; set; }

        public static string login { get; set; }

        public static string password { get; set; }

        public static string phone { get; set; }

        public static string avatar { get; set; }

    }
}
