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
using System.Security.Cryptography;
using System.Windows.Threading;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Policy;

namespace Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page, INotifyPropertyChanged
    {
        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageRegistration()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
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
        /// Метод регистрации по нажатию кнопки "Зарегистрироваться"
        /// </summary>
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowPassCheckBox.IsChecked = true;
                ShowPassCheckBox.IsChecked = false;

                //Проверка валидации
                if (!Validation.GetHasError(txtbxSurname) && !Validation.GetHasError(txtbxName) && !Validation.GetHasError(txtbxPatronymic) && !Validation.GetHasError(txtbxEmail)
                    && !Validation.GetHasError(txtbxPhone) && !Validation.GetHasError(txtbxLogin) && !Validation.GetHasError(txtbxPassword) && !Validation.GetHasError(txtbxCheckPass))
                {
                    //Совпадают ли пароли
                    if (txtbxPassText.Text == txtbxCheckPassText.Text)
                    {
                        //Шифрования пароля с помощью метода
                        string result = MessageDigest5.hashing(txtbxPassText.Text);

                        //Добаление нового пользователя в БД
                        Users user = new Users()
                        {
                            surname = txtbxSurname.Text,
                            name = txtbxName.Text,
                            patronymic = txtbxPatronymic.Text,
                            email = txtbxEmail.Text,
                            phone = txtbxPhone.Text,
                            role = cmbbxRole.SelectedIndex + 2,
                            login = txtbxLogin.Text,
                            password = result,
                            avatar = "\\Resources\\AvatatDefault.png"
                        };
                        ConnectObj.conObj.Users.Add(user);
                        ConnectObj.conObj.SaveChanges();
                        MessageBox.Show("Вы успешно зарегистрировались в системе!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Переход на прошлую страницу
                        FrameObj.frameObj.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Данные для привязки в валидации
        /// </summary>
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }

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

                txtbxCheckPassText.Text = txtbxCheckPass.Password;
                txtbxCheckPass.Visibility = Visibility.Collapsed;
                txtbxCheckPassText.Visibility = Visibility.Visible;

                txtbxSurname.Text = txtbxSurname.Text;
                txtbxName.Text = txtbxName.Text;
                txtbxPatronymic.Text = txtbxPatronymic.Text;
                txtbxEmail.Text = txtbxEmail.Text;
                txtbxPhone.Text = txtbxPhone.Text;
                txtbxLogin.Text = txtbxLogin.Text;
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

                txtbxCheckPass.Password = txtbxCheckPassText.Text;
                txtbxCheckPassText.Visibility = Visibility.Collapsed;
                txtbxCheckPass.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
