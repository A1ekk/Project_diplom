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
    /// Логика взаимодействия для PageEditProjectTypes.xaml
    /// </summary>
    public partial class PageEditProjectTypes : Page
    {
        int IdItem = 0;

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageEditProjectTypes(Project_types types)
        {
            try
            {
                InitializeComponent();
                IdItem = types.id_project_type;
                txtbxId.Text = types.id_project_type.ToString();
                txtbxName.Text = types.type_name.ToString();
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
                    IEnumerable<Project_types> types = ConnectObj.conObj.Project_types.Where(x => x.id_project_type == IdItem).AsEnumerable().
                                Select(x =>
                                {
                                    x.type_name = txtbxName.Text;
                                    return x;
                                });
                foreach (Project_types type in types)
                {
                    ConnectObj.conObj.Entry(type).State = EntityState.Modified;
                }
                ConnectObj.conObj.SaveChanges();
                MessageBox.Show("Запись успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameObj.frameObj.Navigate(new PageListProjectTypes());
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
                    Project_types type = ConnectObj.conObj.Project_types.Where(x => x.id_project_type == IdItem).FirstOrDefault();
                    ConnectObj.conObj.Project_types.Remove(type);

                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Вы успешно удалили запись!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameObj.frameObj.Navigate(new PageListProjectTypes());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
