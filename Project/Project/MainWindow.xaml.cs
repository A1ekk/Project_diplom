using Project.AppData;
using Project.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Таймер
        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                //Таймер обновляет данные каждую секунду
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += UpdateData;
                timer.Start();

                //Создаём объект подключения к БД
                ConnectObj.conObj = new db_cefabEntities();

                //Создаём объект для навигации по страницам
                FrameObj.frameObj = FrmMain;

                //Открываем страницу авторизации
                FrmMain.Navigate(new PageAuthorization());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод обновления данных о пользователе и видимости элементов в шапке
        /// </summary>
        public void UpdateData(object sender, object e)
        {
            try
            {
                txtbxSurname.Text = UserObj.surname;
                txtbxName.Text = UserObj.name;
                ImageString.Text = UserObj.avatar;
                
                if (CheckAuthorization.CheckAuto == true)
                {
                    if (UserObj.role == 1)
                    {
                        btnProject.Content = "АДМИН-ПАНЕЛЬ";
                    }
                    else
                    {
                        btnProject.Content = "ПРОЕКТЫ";
                    }
                    btnPrsnlAcc.Visibility = Visibility;
                    btnProject.Visibility = Visibility;
                    btnExit.Visibility = Visibility;
                }
                else if (CheckAuthorization.CheckAuto == false)
                {
                    btnPrsnlAcc.Visibility = Visibility.Hidden;
                    btnProject.Visibility = Visibility.Hidden;
                    btnExit.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод перехода на страницу личного кабинета
        /// </summary>
        private void btnPrsnlAcc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameObj.frameObj.Navigate(new PagePersonalAccount());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод перехода на страницу на страницу авторизации
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                CheckAuthorization.CheckAuto = false;
                FrameObj.frameObj.Navigate(new PageAuthorization());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод перехода на страницу проектов или панели администратора
        /// </summary>
        private void btnProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserObj.role == 1)
                {
                    FrameObj.frameObj.Navigate(new PageAdminPanel());
                }
                else
                {
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
