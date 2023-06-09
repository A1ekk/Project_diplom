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
using System.Windows.Threading;

namespace Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProject.xaml
    /// </summary>
    public partial class PageProject : Page
    {
        //Таймер
        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageProject()
        {
            try
            {
                InitializeComponent();
                //Таймер обновляет данные каждую 0,5 секунды
                timer.Interval = TimeSpan.FromSeconds(0.5);
                timer.Tick += UpdateDataProject;
                timer.Start();

                Sort.SelectedIndex = 0;
                Filtr.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод обновления проектов с поиском, фильтрацией и сортировкой, который вызывает таймер
        /// </summary>
        public void UpdateDataProject(object sender, object e)
        {
            try
            {
                if (Sort.SelectedIndex == 0)
                {
                    if (Filtr.SelectedIndex == 0)
                    {
                        ListProject.ItemsSource = ConnectObj.conObj.Projects.Where(x => x.project_name.StartsWith(Search.Text)).OrderByDescending(x => x.date_creation).ToList();
                    }
                    else if (Filtr.SelectedIndex > 0)
                    {
                        ListProject.ItemsSource = ConnectObj.conObj.Projects.Where(x => x.project_type == Filtr.SelectedIndex && x.project_name.StartsWith(Search.Text)).
                            OrderByDescending(x => x.date_creation).ToList();
                    }
                }
                else if (Sort.SelectedIndex == 1)
                {
                    if (Filtr.SelectedIndex == 0)
                    {
                        ListProject.ItemsSource = ConnectObj.conObj.Projects.Where(x => x.project_name.StartsWith(Search.Text)).OrderBy(x => x.date_creation).ToList();
                    }
                    else if (Filtr.SelectedIndex > 0)
                    {
                        ListProject.ItemsSource = ConnectObj.conObj.Projects.Where(x => x.project_type == Filtr.SelectedIndex && x.project_name.StartsWith(Search.Text)).
                            OrderBy(x => x.date_creation).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод перехода на страницу добавления проекта по нажатию кнопки "Добавить проект"
        /// </summary>
        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameObj.frameObj.Navigate(new PageAddProject());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод перехода на страницу редактирования проекта по нажатию на проект
        /// </summary>
        private void btnEditProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameObj.frameObj.Navigate(new PageEditProject((sender as Button).DataContext as Projects));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
