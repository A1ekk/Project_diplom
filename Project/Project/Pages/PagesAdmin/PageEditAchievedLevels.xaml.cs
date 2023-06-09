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
    /// Логика взаимодействия для PageEditAchievedLevels.xaml
    /// </summary>
    public partial class PageEditAchievedLevels : Page
    {
        int IdItem = 0;

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageEditAchievedLevels(Achieved_levels level)
        {
            try
            {
                InitializeComponent();
                IdItem = level.id_achieved_level;
                txtbxId.Text = level.id_achieved_level.ToString();
                txtbxName.Text = level.level_name.ToString();
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
                    IEnumerable<Achieved_levels> levels = ConnectObj.conObj.Achieved_levels.Where(x => x.id_achieved_level == IdItem).AsEnumerable().
                                Select(x =>
                                {
                                    x.level_name = txtbxName.Text;
                                    return x;
                                });
                foreach (Achieved_levels level in levels)
                {
                    ConnectObj.conObj.Entry(level).State = EntityState.Modified;
                }
                ConnectObj.conObj.SaveChanges();
                MessageBox.Show("Запись успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameObj.frameObj.Navigate(new PageListAchievedLevels());
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
                    Achieved_levels level = ConnectObj.conObj.Achieved_levels.Where(x => x.id_achieved_level == IdItem).FirstOrDefault();
                    ConnectObj.conObj.Achieved_levels.Remove(level);

                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Вы успешно удалили запись!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameObj.frameObj.Navigate(new PageListAchievedLevels());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
