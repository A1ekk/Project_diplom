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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Threading;

namespace Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageEditProject.xaml
    /// </summary>
    public partial class PageEditProject : Page, INotifyPropertyChanged
    {

        int ProjectGrades = 0;

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageEditProject(Projects project)
        {
            try
            {
                InitializeComponent();
                ProjectObj.id_project = project.id_project;
                txtblRoleUser.Text = UserObj.role.ToString();

                //Проверка принадлежности пользователя к данному проекту, вообще его уровня доступа в системе, и предоставления ему функциональностей исходя из этого
                Teams team = ConnectObj.conObj.Teams.Where(x => x.id_project == ProjectObj.id_project && x.id_user == UserObj.id_user && x.application_status == 1).FirstOrDefault();
                if (team != null)
                {
                    txtblRoleTeam.Text = "1";
                }
                else
                {
                    if (txtblRoleUser.Text == "1") 
                    { 
                        txtblRoleTeam.Text = "1";
                        btnSubmitApply.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        txtblRoleTeam.Text = "0";
                        btnDeleteProject.Visibility = Visibility.Hidden;
                    }
                }

                //Заполнение полей данными о проекте
                UpdateDataTeams();
                DataContext = this;
                cmbbxProjectType.ItemsSource = ConnectObj.conObj.Project_types.Select(x => x.type_name).ToList();
                cmbbxProductType.ItemsSource = ConnectObj.conObj.Product_types.Select(x => x.type_name).ToList();
                cmbbxAchievedLevel.ItemsSource = ConnectObj.conObj.Achieved_levels.Select(x => x.level_name).ToList();
                txtbxProjectName.Text = project.project_name;
                txtbxCustomer.Text = project.customer;
                txtbxProjectObjective.Text = project.project_objective;
                txtbxProjectTasks.Text = project.project_tasks;
                txtbxProjectProblem.Text = project.project_problem;
                txtbxProjectTimeline.Text = project.project_timeline;
                txtbxProjectRelevance.Text = project.project_relevance;
                txtbxRequiredResources.Text = project.required_resources;
                txtbxTargetAudience.Text = project.target_audience;
                txtbxAnaloguesCompetitors.Text = project.analogues_competitors;
                txtbxProjectNovelty.Text = project.project_novelty;
                txtbxBusinessPlan.Text = project.business_plan;
                txtbxFinalResult.Text = project.final_result;
                txtbxProjectVacancies.Text = project.project_vacancies; 
                ProjectGrades = (int)project.project_grades;
                if (project.project_type.ToString() != "")
                {
                    cmbbxProjectType.SelectedIndex = (int)project.project_type - 1;
                }
                if (project.prodoct_type.ToString() != "")
                {
                    cmbbxProductType.SelectedIndex = (int)project.prodoct_type - 1;
                }
                if (project.achieved_level.ToString() != "")
                {
                    cmbbxAchievedLevel.SelectedIndex = (int)project.achieved_level - 1;
                }

                //Заполнение полей о оценках проекта
                if (project.project_grades != null)
                {
                    MessageBox.Show("Переход к редактированию проекта!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    IEnumerable<Grades> grades = ConnectObj.conObj.Grades.Where(x => x.id_grade == ProjectGrades).AsEnumerable().
                    Select(x =>
                    {
                        return x;
                    });
                    foreach (Grades grade in grades)
                    {
                        txtbxPointOne.Text = grade.idea_grade.ToString();
                        txtbxPointTwo.Text = grade.team_grade.ToString();
                        txtbxPointThree.Text = grade.innovation_grade.ToString();
                        txtbxPointFour.Text = grade.marketing_grade.ToString();
                        txtbxPointFive.Text = grade.economy_grade.ToString();
                        txtbxPointSix.Text = grade.management_grade.ToString();
                        txtbxPointSeven.Text = grade.business_plan_grade.ToString();
                        txtbxPointEight.Text = grade.presentation_grade.ToString();
                        txtblResult.Text = "Итоговая оценка - " + grade.final_grade.ToString();
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Событие для валидации
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод обновления данных о команде проекта
        /// </summary>
        public void UpdateDataTeams()
        {
            try
            {
                //Запись в новый объект всех записей из таблицы "Teams" из БД
                var team = ConnectObj.conObj.Teams.Where(x => x.id_project == ProjectObj.id_project);

                //Поиск пользователей, которые связаны с проектом, путём объединения нескольких таблиц с помощью команды "JOIN"
                ListTeams.ItemsSource = ConnectObj.conObj.Team_roles.Join(team,
                    r => r.id_team_role,
                    t => t.team_role,
                    (r, t) => new { r.name_team_role, t.id_user, t.application_status }
                    ).Join(ConnectObj.conObj.Users,
                    t => t.id_user,
                    u => u.id_user,
                    (t, u) => new { u.id_user, t.application_status, t.name_team_role, u.avatar, u.name, u.surname }
                    ).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод изменения данных о проекте по нажатию кнопки "Сохранить изменения"
        /// </summary>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<Projects> projects = ConnectObj.conObj.Projects.Where(x => x.id_project == ProjectObj.id_project).AsEnumerable().
                Select(x =>
                    {
                        x.project_name = txtbxProjectName.Text;
                        x.customer = txtbxCustomer.Text;
                        x.project_objective = txtbxProjectObjective.Text;
                        x.project_tasks = txtbxProjectTasks.Text;
                        x.project_problem = txtbxProjectProblem.Text;
                        x.project_timeline = txtbxProjectTimeline.Text;
                        x.project_relevance = txtbxProjectRelevance.Text;
                        x.required_resources = txtbxRequiredResources.Text;
                        x.target_audience = txtbxTargetAudience.Text;
                        x.analogues_competitors = txtbxAnaloguesCompetitors.Text;
                        x.project_novelty = txtbxProjectNovelty.Text;
                        x.business_plan = txtbxBusinessPlan.Text;
                        x.final_result = txtbxFinalResult.Text;
                        x.project_vacancies = txtbxProjectVacancies.Text;
                        if (cmbbxProjectType.SelectedIndex != -1)
                        {
                            x.project_type = cmbbxProjectType.SelectedIndex + 1;
                        }
                        if (cmbbxProductType.SelectedIndex != -1)
                        {
                            x.prodoct_type = cmbbxProductType.SelectedIndex + 1;
                        }
                        if (cmbbxAchievedLevel.SelectedIndex != -1)
                        {
                            x.achieved_level = cmbbxAchievedLevel.SelectedIndex + 1;
                        }
                        return x;
                    });
                foreach (Projects project in projects)
                {
                    ConnectObj.conObj.Entry(project).State = System.Data.Entity.EntityState.Modified;
                }
                ConnectObj.conObj.SaveChanges();
                MessageBox.Show("Проект успешно изменён!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameObj.frameObj.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Данные для привязки в валидации
        /// </summary>
        public string Double { get; set; }


        /// <summary>
        /// Метод для обновления данных полей
        /// </summary>
        public void UpdateTextBox()
        {
            try
            {
                txtbxPointOne.Text = txtbxPointOne.Text;
                txtbxPointTwo.Text = txtbxPointTwo.Text;
                txtbxPointThree.Text = txtbxPointThree.Text;
                txtbxPointFour.Text = txtbxPointFour.Text;
                txtbxPointFive.Text = txtbxPointFive.Text;
                txtbxPointSix.Text = txtbxPointSix.Text;
                txtbxPointSeven.Text = txtbxPointSeven.Text;
                txtbxPointEight.Text = txtbxPointEight.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод выставления оценки проекту по нажатию кнопки "Оценить"
        /// </summary>
        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Вызов метода для обновления данных полей
                UpdateTextBox();

                //Проверка валидации полей
                if (!Validation.GetHasError(txtbxPointOne) && !Validation.GetHasError(txtbxPointTwo) && !Validation.GetHasError(txtbxPointThree) && !Validation.GetHasError(txtbxPointFour) && !Validation.GetHasError(txtbxPointFive)
                    && !Validation.GetHasError(txtbxPointSix) && !Validation.GetHasError(txtbxPointSeven) && !Validation.GetHasError(txtbxPointEight))
                {
                    //Запись в перемнную результата выполнения метода вычисления итоговой оценки
                    double result = CalcFinalGrade.final_grade(Convert.ToDouble(txtbxPointOne.Text), Convert.ToDouble(txtbxPointTwo.Text), Convert.ToDouble(txtbxPointThree.Text),
                        Convert.ToDouble(txtbxPointFour.Text), Convert.ToDouble(txtbxPointFive.Text), Convert.ToDouble(txtbxPointSix.Text), Convert.ToDouble(txtbxPointSeven.Text), Convert.ToDouble(txtbxPointEight.Text));

                    //Вывод итоговой оценки на экран
                    txtblResult.Text = "Итоговая оценка - " + result.ToString();

                    //Изменения данных об оценке проекта
                    IEnumerable<Grades> grades = ConnectObj.conObj.Grades.Where(x => x.id_grade == ProjectGrades).AsEnumerable().
                    Select(x =>
                    {
                        x.final_grade = result;
                        x.idea_grade = Math.Round(Convert.ToDouble(txtbxPointOne.Text), 2);
                        x.team_grade = Math.Round(Convert.ToDouble(txtbxPointTwo.Text), 2);
                        x.innovation_grade = Math.Round(Convert.ToDouble(txtbxPointThree.Text), 2);
                        x.marketing_grade = Math.Round(Convert.ToDouble(txtbxPointFour.Text), 2);
                        x.economy_grade = Math.Round(Convert.ToDouble(txtbxPointFive.Text), 2);
                        x.management_grade = Math.Round(Convert.ToDouble(txtbxPointSix.Text), 2);
                        x.business_plan_grade = Math.Round(Convert.ToDouble(txtbxPointSeven.Text), 2);
                        x.presentation_grade = Math.Round(Convert.ToDouble(txtbxPointEight.Text), 2);
                        return x;
                    });
                    foreach (Grades grade in grades)
                    {
                        ConnectObj.conObj.Entry(grade).State = System.Data.Entity.EntityState.Modified;
                    }
                    ConnectObj.conObj.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод подачи заявки на вступление в проект по нажатию кнопки "Подать заявку на вступление в проект"
        /// </summary>
        private void btnSubmitApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Нахождение в БД всех связанных с проектом пользователей
                Teams teams = ConnectObj.conObj.Teams.Where(x => x.id_project == ProjectObj.id_project && x.id_user == UserObj.id_user).FirstOrDefault();

                //Принятие заявки, если пользователб ещё не состоит в данном проекте или не подавал заявку ранее
                if (teams != null)
                {
                    if (teams.application_status == 1)
                    {
                        MessageBox.Show("Вы уже состоите в этом проекте!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (teams.application_status == 2)
                    {
                        MessageBox.Show("Вы уже подали заявку в этот проект!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (MessageBox.Show("Вы хотите подать заявку на вступление в эту команду?",
                    "Сообщение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Teams team = new Teams()
                    {
                        id_project = ProjectObj.id_project,
                        id_user = UserObj.id_user,
                        team_role = 3,
                        application_status = 2
                    };
                    ConnectObj.conObj.Teams.Add(team);
                    ConnectObj.conObj.SaveChanges();
                    UpdateDataTeams();
                    MessageBox.Show("Вы успешно подали заявку на вступление в проект!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод принятия заявки пользователя на вступление в проект по нажатию на пользователя
        /// </summary>
        private void btnAddInTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Хотите добавить этого пользователя в свою команду?",
                    "Сообщение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    //Нахождение уникального идентификатора пользователя, для нахождения по нему записи о пользователе в БД
                    var myselfProject = (sender as Button).DataContext;
                    int ID_user = 0;
                    Type objType = myselfProject.GetType();
                    foreach (var prop in objType.GetProperties())
                    {
                        var value = prop.GetValue(myselfProject);
                        Type valueType = value.GetType();
                        if (value is string || (valueType.IsValueType && valueType.IsPrimitive))
                        {
                            if ($"{prop.Name}" == "id_user")
                            {
                                ID_user = Convert.ToInt32($"{value}");
                                break;
                            }
                        }
                    }

                    //Добавление пользователя в команду
                    IEnumerable<Teams> teams = ConnectObj.conObj.Teams.Where(x => x.id_user == ID_user && x.id_project == ProjectObj.id_project).AsEnumerable().
                    Select(x =>
                    {
                        x.application_status = 1;
                        return x;
                    });
                    foreach (Teams team in teams)
                    {
                        ConnectObj.conObj.Entry(team).State = System.Data.Entity.EntityState.Modified;
                    }
                    ConnectObj.conObj.SaveChanges();
                    UpdateDataTeams();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод удаления проекта из системы по нажатию кнопки "Удалить проект"
        /// </summary>
        private void btnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этот проект?",
                    "Сообщение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Projects project = ConnectObj.conObj.Projects.Where(x => x.id_project == ProjectObj.id_project).FirstOrDefault();
                    ConnectObj.conObj.Projects.Remove(project);

                    ConnectObj.conObj.SaveChanges();
                    MessageBox.Show("Вы успешно удалили проект!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameObj.frameObj.Navigate(new PageProject());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
