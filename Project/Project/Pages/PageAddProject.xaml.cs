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

namespace Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddProject.xaml
    /// </summary>
    public partial class PageAddProject : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageAddProject()
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
        /// Метод добавления нового проекта по нажитию кнопки "Добавить новый проект"
        /// </summary>
        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbxProjectName.Text.Length > 0)
                {
                    //Добавление записи с оценками в БД
                    Grades grade = new Grades()
                    {
                        final_grade = 0,
                        idea_grade = 0,
                        team_grade = 0,
                        innovation_grade = 0,
                        marketing_grade = 0,
                        economy_grade = 0,
                        management_grade = 0,
                        business_plan_grade = 0,
                        presentation_grade = 0
                    };
                    ConnectObj.conObj.Grades.Add(grade);

                    //Добавление проекта в БД
                    Projects project = new Projects()
                    {
                        project_name = txtbxProjectName.Text,
                        date_creation = DateTime.Today,
                        project_logo = "\\Resources\\picture.png",
                        project_grades = grade.id_grade
                    };
                        ConnectObj.conObj.Projects.Add(project);
                        ConnectObj.conObj.SaveChanges();
                        MessageBox.Show("Вы успешно создали новый проект!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameObj.frameObj.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Укажите название проекта!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
