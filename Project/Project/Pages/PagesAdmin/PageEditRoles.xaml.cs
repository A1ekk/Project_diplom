using Project.AppData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для PageEditRoles.xaml
    /// </summary>
    public partial class PageEditRoles : Page
    {
        int IdItem = 0;

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageEditRoles(Roles role)
        {
            try
            {
                InitializeComponent();
                IdItem = role.id_role;
                txtbxId.Text = role.id_role.ToString();
                txtbxName.Text = role.role_name.ToString();
                txtbxAccessLevels.Text = role.access_level.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод изменения записи в системе
        /// </summary>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbxName.Text.Length > 0 && txtbxAccessLevels.Text.Length > 0)
                {
                    IEnumerable<Roles> roles = ConnectObj.conObj.Roles.Where(x => x.id_role == IdItem).AsEnumerable().
                                Select(x =>
                                {
                                    x.role_name = txtbxName.Text;
                                    x.access_level = Convert.ToInt32(txtbxAccessLevels.Text);
                                    return x;
                                });
                    foreach (Roles role in roles)
                    {
                        ConnectObj.conObj.Entry(role).State = EntityState.Modified;
                    }
                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Запись успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameObj.frameObj.Navigate(new PageListRoles());
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

        /// <summary>
        /// Метод удаления записи из системы
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                        "Сообщение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Roles role = ConnectObj.conObj.Roles.Where(x => x.id_role == IdItem).FirstOrDefault();
                        ConnectObj.conObj.Roles.Remove(role);

                        ConnectObj.conObj.SaveChanges();
                        MessageBox.Show("Вы успешно удалили запись!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameObj.frameObj.Navigate(new PageListRoles());
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
