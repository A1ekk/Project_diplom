using Project.AppData;
using System;
using System.Collections;
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
    /// Логика взаимодействия для PagePersonalAccount.xaml
    /// </summary>
    public partial class PagePersonalAccount : Page
    {
        //Таймер
        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PagePersonalAccount()
        {
            try
            {
                InitializeComponent();
                //Таймер обновляет данные каждую секунду
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += UpdateDataMyProject;
                timer.Start();

                //Заполнение личных данных из класса UserObj
                txtbxSurname.Text = UserObj.surname;
                txtbxName.Text = UserObj.name;
                txtbxPatronymic.Text = UserObj.patronymic;
                Roles roles = ConnectObj.conObj.Roles.Where(x => x.access_level == UserObj.role).FirstOrDefault();
                txtbxRole.Text = roles.role_name;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод обновления проектов пользователя, который вызывает таймер
        /// </summary>
        public void UpdateDataMyProject(object sender, object e)
        {
            try
            {
                //Запись в новый объект всех записей из таблицы "Teams" из БД
                var myself = ConnectObj.conObj.Teams.Where(x => x.id_user == UserObj.id_user);

                //Поиск проектов пользователя путём объединения нескольких таблиц с помощью команды "JOIN"
                ListProject.ItemsSource = ConnectObj.conObj.Projects.Join(myself,
                    p => p.id_project,
                    m => m.id_project,
                    (p, m) => new { p.id_project, p.project_name, p.project_logo, p.project_type, p.project_objective, p.date_creation, p.project_vacancies }
                    ).Join(ConnectObj.conObj.Project_types,
                    p => p.project_type,
                    t => t.id_project_type,
                    (p, t) => new { p.id_project, p.project_name, p.project_logo, t.type_name, p.project_objective, p.date_creation, p.project_vacancies }
                    ).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод перехода на страницу редактирования профиля по нажатию кнопки "Редактировать профиль"
        /// </summary>
        private void btnPageEditProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users user = ConnectObj.conObj.Users.Where(x => x.id_user == UserObj.id_user).FirstOrDefault();
                FrameObj.frameObj.Navigate(new PageEditProfile(user, 0));
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
                //Нахождение уникального идентификатора, для нахождения по нему записи о проекте в БД
                var myselfProject = (sender as Button).DataContext;
                int ID_proj = 0;
                Type objType = myselfProject.GetType();
                foreach (var prop in objType.GetProperties())
                {
                    var value = prop.GetValue(myselfProject);
                    Type valueType = value.GetType();
                    if (value is string || (valueType.IsValueType && valueType.IsPrimitive))
                    {
                        if ($"{prop.Name}" == "id_project")
                        {
                            ID_proj = Convert.ToInt32($"{value}");
                            break;
                        }
                    }
                }

                //Нахождение записи о проекте в БД
                Projects projects = ConnectObj.conObj.Projects.Where(x => x.id_project == ID_proj).FirstOrDefault();
                
                //Переход на страницу редактирования проекта
                FrameObj.frameObj.Navigate(new PageEditProject(projects));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
