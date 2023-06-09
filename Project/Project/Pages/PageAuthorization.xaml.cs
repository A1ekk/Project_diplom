using Project.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageAuthorization()
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
        /// Метод авторизации в системе после нажатия кнопки "Авторизоваться" 
        /// </summary>
        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowPassCheckBox.IsChecked = true;
                ShowPassCheckBox.IsChecked = false;

                //Заполнен ли логин
                if (txtbxLogin.Text.Length > 0)
                {
                    //Заполнен ли пароль
                    if (txtbxPassword.Password.Length > 0)
                    {
                        //Шифрования пароля с помощью метода
                        string result = MessageDigest5.hashing(txtbxPassword.Password);

                        //Поиск пользователя в БД
                        Users user = ConnectObj.conObj.Users.Where(x => x.login == txtbxLogin.Text && x.password == result).FirstOrDefault();
                        if (user != null)
                        {
                            UserObj.id_user = user.id_user;
                            UserObj.surname = user.surname;
                            UserObj.name = user.name;
                            UserObj.patronymic = user.patronymic;
                            UserObj.role = user.role;
                            UserObj.email = user.email;
                            UserObj.phone = user.phone;
                            UserObj.login = user.login;
                            UserObj.password = user.password;
                            UserObj.avatar = user.avatar;

                            //Появление элементов в шапке
                            CheckAuthorization.CheckAuto = true;

                            //Переход на эран проектов или панели администратора
                            if (UserObj.role == 1)
                            {
                                FrameObj.frameObj.Navigate(new PageAdminPanel());
                            }
                            else
                            {
                                FrameObj.frameObj.Navigate(new PageProject());
                            }
                            MessageBox.Show("Успешный вход!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Введите логин!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод просмотра пароля при состоянии чекбокса "включён"
        /// </summary>
        private void ShowPass_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtbxPassText.Text = txtbxPassword.Password;
                txtbxPassword.Visibility = Visibility.Collapsed;
                txtbxPassText.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод скрытия пароля при состоянии чекбокса "отключён"
        /// </summary>
        private void ShowPass_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtbxPassword.Password = txtbxPassText.Text;
                txtbxPassText.Visibility = Visibility.Collapsed;
                txtbxPassword.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
