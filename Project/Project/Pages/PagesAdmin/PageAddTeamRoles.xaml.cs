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
    /// Логика взаимодействия для PageAddTeamRoles.xaml
    /// </summary>
    public partial class PageAddTeamRoles : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageAddTeamRoles()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод добавления новой записи
        /// </summary>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbxName.Text.Length > 0)
                {
                    //Добавление записи в БД
                    Team_roles role = new Team_roles()
                    {
                        name_team_role = txtbxName.Text,
                    };
                    ConnectObj.conObj.Team_roles.Add(role);
                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Вы успешно создали новую запись!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameObj.frameObj.Navigate(new PageListTeamRoles());
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
