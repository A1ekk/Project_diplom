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
    /// Логика взаимодействия для PageAddAppStatus.xaml
    /// </summary>
    public partial class PageAddAppStatus : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageAddAppStatus()
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
                    int last = ConnectObj.conObj.Application_status.Max(x => x.id_application_status);
                    //Добавление записи в БД
                    Application_status status = new Application_status()
                    {
                        id_application_status = last + 1,
                        name_application_status = txtbxName.Text,
                    };
                    ConnectObj.conObj.Application_status.Add(status);
                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Вы успешно создали новую запись!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
