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
    /// Логика взаимодействия для PageEditAppStatus.xaml
    /// </summary>
    public partial class PageEditAppStatus : Page
    {
        int IdItem = 0;

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageEditAppStatus(Application_status status)
        {
            try
            {
                InitializeComponent();
                IdItem = status.id_application_status;
                txtbxId.Text = status.id_application_status.ToString();
                txtbxName.Text = status.name_application_status.ToString();
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
                if (txtbxName.Text.Length > 0)
                {
                    IEnumerable<Application_status> statuses = ConnectObj.conObj.Application_status.Where(x => x.id_application_status == IdItem).AsEnumerable().
                                Select(x =>
                                {
                                    x.name_application_status = txtbxName.Text;
                                    return x;
                                });
                foreach (Application_status status in statuses)
                {
                    ConnectObj.conObj.Entry(status).State = EntityState.Modified;
                }
                ConnectObj.conObj.SaveChanges();
                MessageBox.Show("Запись успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameObj.frameObj.Navigate(new PageListAppStatus());
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
                    Application_status status = ConnectObj.conObj.Application_status.Where(x => x.id_application_status == IdItem).FirstOrDefault();
                    ConnectObj.conObj.Application_status.Remove(status);

                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Вы успешно удалили запись!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameObj.frameObj.Navigate(new PageListAppStatus());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
