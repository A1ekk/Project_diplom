using Project.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageListUsers.xaml
    /// </summary>
    public partial class PageListUsers : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageListUsers()
        {
            InitializeComponent();

            //Формирование списка всех пользователей системы
            ListUsers.ItemsSource = ConnectObj.conObj.Users.ToList();
        }

        /// <summary>
        /// Метод перехода на страницу редактирования пользователя по нажатию на пользователя
        /// </summary>
        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Нахождение уникального идентификатора пользователя
                var SelectUser = (sender as Button).DataContext;
                int ID_user = 0;
                Type objType = SelectUser.GetType();
                foreach (var prop in objType.GetProperties())
                {
                    var value = prop.GetValue(SelectUser);
                    Type valueType = value.GetType();
                    if (value is string || (valueType.IsValueType && valueType.IsPrimitive))
                    {
                        if ($"{prop.Name}" == "id_user")
                        {
                            ID_user = Convert.ToInt32($"{value}");
                            break;
                        }
                    }
                }

                //Нахождение пользователя в БД
                Users user = ConnectObj.conObj.Users.Where(x => x.id_user == ID_user).FirstOrDefault();

                //Переход на страницу редактирования пользователя
                FrameObj.frameObj.Navigate(new PageEditProfile(user, 1));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
