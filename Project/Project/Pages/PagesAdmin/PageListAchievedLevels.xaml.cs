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
    /// Логика взаимодействия для PageListAchievedLevels.xaml
    /// </summary>
    public partial class PageListAchievedLevels : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageListAchievedLevels()
        {
            InitializeComponent();
            List.ItemsSource = ConnectObj.conObj.Achieved_levels.ToList();
        }

        /// <summary>
        /// Метод перехода на страницу редактирования записи
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Нахождение уникального идентификатора
                var SelectUser = (sender as Button).DataContext;
                int ID_item = 0;
                Type objType = SelectUser.GetType();
                foreach (var prop in objType.GetProperties())
                {
                    var value = prop.GetValue(SelectUser);
                    Type valueType = value.GetType();
                    if (value is string || (valueType.IsValueType && valueType.IsPrimitive))
                    {
                        if ($"{prop.Name}" == "id_achieved_level")
                        {
                            ID_item = Convert.ToInt32($"{value}");
                            break;
                        }
                    }
                }

                //Нахождение пользователя в БД
                Achieved_levels levels = ConnectObj.conObj.Achieved_levels.Where(x => x.id_achieved_level == ID_item).FirstOrDefault();

                //Переход на страницу редактирования пользователя
                FrameObj.frameObj.Navigate(new PageEditAchievedLevels(levels));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameObj.frameObj.Navigate(new PageAddAchievedLevels());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
