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
    /// Логика взаимодействия для PageListProjectTypes.xaml
    /// </summary>
    public partial class PageListProjectTypes : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageListProjectTypes()
        {
            InitializeComponent();
            List.ItemsSource = ConnectObj.conObj.Project_types.ToList();
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
                        if ($"{prop.Name}" == "id_project_type")
                        {
                            ID_item = Convert.ToInt32($"{value}");
                            break;
                        }
                    }
                }

                //Нахождение пользователя в БД
                Project_types types = ConnectObj.conObj.Project_types.Where(x => x.id_project_type == ID_item).FirstOrDefault();

                //Переход на страницу редактирования пользователя
                FrameObj.frameObj.Navigate(new PageEditProjectTypes(types));

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
                FrameObj.frameObj.Navigate(new PageAddProjectTypes());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
