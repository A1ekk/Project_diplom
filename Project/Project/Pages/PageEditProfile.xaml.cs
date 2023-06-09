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
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using Project.Pages.PagesAdmin;

namespace Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageEditProfile.xaml
    /// </summary>
    public partial class PageEditProfile : Page, INotifyPropertyChanged
    {

        int IdUser = 0;
        string PassUser = "";
        int pageBack;

        /// <summary>
        /// Метод загрузки страницы
        /// </summary>
        public PageEditProfile(Users user, int PageBack)
        {
            try
            {
                InitializeComponent();
                DataContext = this;

                //Проверка перешёл ли пользователь на эту страницу из личного кабинета или из панели администратора
                pageBack = PageBack;
                if(pageBack == 0) 
                {
                    btnDeleteUser.Visibility = Visibility.Hidden;
                }

                //Заполнение полей данными пользователя
                cmbbxRole.ItemsSource = ConnectObj.conObj.Roles.Select(x => x.role_name).ToList();
                IdUser = user.id_user;
                PassUser = user.password;
                Surname = user.surname;
                FirstName = user.name;
                Patronymic = user.patronymic;
                Email = user.email;
                Phone = user.phone;
                Login = user.login;
                cmbbxRole.SelectedIndex = user.role - 1;
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
        /// Метод изменения данных пользователя по нажатию кнопки "Сохранить изменения"
        /// </summary>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Вызов метода для обновления данных полей
                UpdateTextBox();

                //Проверка валидации полей
                if (!Validation.GetHasError(txtbxSurname) && !Validation.GetHasError(txtbxName) && !Validation.GetHasError(txtbxPatronymic) && !Validation.GetHasError(txtbxEmail)
                    && !Validation.GetHasError(txtbxPhone) && !Validation.GetHasError(txtbxLogin))
                {
                    //Проверка корректно ли введён текущий пароль
                    if (MessageDigest5.hashing(txtbxOldPassText.Text)  == PassUser)
                    {
                        //Проверка корректно ли заполнены поля с новым паролем и подтверждением пароля или все поля с паролями пусты 
                        if ((txtbxNewPassText.Text == txtbxCheckPassText.Text && txtbxNewPassText.Text.Length > 0) ||
                            (txtbxCheckPassText.Text.Length == 0 && txtbxNewPassText.Text.Length == 0))
                        {
                            //Изменение данных о пользователе в БД
                            IEnumerable<Users> users = ConnectObj.conObj.Users.Where(x => x.id_user == IdUser).AsEnumerable().
                                Select(x =>
                                    {
                                        x.surname = txtbxSurname.Text;
                                        x.name = txtbxName.Text;
                                        x.patronymic = txtbxPatronymic.Text;
                                        x.email = txtbxEmail.Text;
                                        x.phone = txtbxPhone.Text;
                                        x.login = txtbxLogin.Text;
                                        x.role = cmbbxRole.SelectedIndex + 1;
                                        if (txtbxNewPassText.Text.Length > 0)
                                        {
                                            x.password = MessageDigest5.hashing(txtbxNewPassText.Text); 
                                        };
                                        return x;
                                    });
                            foreach (Users user in users)
                            {
                                ConnectObj.conObj.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            }
                            ConnectObj.conObj.SaveChanges();
                            MessageBox.Show("Профиль успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                            //Проверка пользователь сам ли изменял свои данные или это делал администратор системы
                            if (UserObj.id_user == IdUser)
                            {
                                UserObj.surname = txtbxSurname.Text;
                                UserObj.name = txtbxName.Text;
                                UserObj.patronymic = txtbxPatronymic.Text;
                                UserObj.role = cmbbxRole.SelectedIndex + 1;
                                UserObj.email = txtbxEmail.Text;
                                UserObj.phone = txtbxPhone.Text;
                                UserObj.login = txtbxLogin.Text;
                                if (txtbxOldPassText.Text.Length > 0)
                                {
                                    UserObj.password = MessageDigest5.hashing(txtbxNewPassText.Text);
                                }
                            }

                            //Переход на прошлую страницу в зависимости от того откуда перешли на данную страницу
                            if (UserObj.role == 1)
                            {
                                if (pageBack == 1)
                                {
                                    FrameObj.frameObj.Navigate(new PageListUsers());
                                }
                                else if (pageBack == 0)
                                {
                                    FrameObj.frameObj.Navigate(new PagePersonalAccount());
                                }
                            }
                            else
                            {
                                FrameObj.frameObj.Navigate(new PagePersonalAccount());
                            }
                        }
                        else
                        {
                            if (txtbxNewPass.Password != txtbxCheckPass.Password)
                            {
                                MessageBox.Show("Подтверждение пароля и новый пароль не совпадают!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Вы не ввели новый пароль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    else
                    {
                        if (txtbxOldPassText.Text.Length == 0)
                        {
                            MessageBox.Show("Вы не ввели текущий пароль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Вы ввели неверный пароль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
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
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }

        /// <summary>
        /// Метод просмотра пароля при состоянии чекбокса "включён"
        /// </summary>
        private void ShowPass_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtbxOldPassText.Text = txtbxOldPass.Password;
                txtbxOldPass.Visibility = Visibility.Collapsed;
                txtbxOldPassText.Visibility = Visibility.Visible;

                txtbxNewPassText.Text = txtbxNewPass.Password;
                txtbxNewPass.Visibility = Visibility.Collapsed;
                txtbxNewPassText.Visibility = Visibility.Visible;

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
                txtbxOldPass.Password = txtbxOldPassText.Text;
                txtbxOldPassText.Visibility = Visibility.Collapsed;
                txtbxOldPass.Visibility = Visibility.Visible;

                txtbxNewPass.Password = txtbxNewPassText.Text;
                txtbxNewPassText.Visibility = Visibility.Collapsed;
                txtbxNewPass.Visibility = Visibility.Visible;

                txtbxCheckPass.Password = txtbxCheckPassText.Text;
                txtbxCheckPassText.Visibility = Visibility.Collapsed;
                txtbxCheckPass.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод для обновления данных полей
        /// </summary>
        public void UpdateTextBox()
        {
            try
            {
                txtbxSurname.Text = txtbxSurname.Text;
                txtbxName.Text = txtbxName.Text;
                txtbxPatronymic.Text = txtbxPatronymic.Text;
                txtbxEmail.Text = txtbxEmail.Text;
                txtbxPhone.Text = txtbxPhone.Text;
                txtbxLogin.Text = txtbxLogin.Text;
                txtbxOldPassText.Text = txtbxOldPass.Password;
                txtbxNewPassText.Text = txtbxNewPass.Password;
                txtbxCheckPassText.Text = txtbxCheckPass.Password;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод удаления пользователя из системы по нажатию кнопки "Удалить пользователя"
        /// </summary>
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка не пытается ли пользователь удалить себя из системы
                if (UserObj.id_user == IdUser) 
                {
                    MessageBox.Show("Вы не можете удалить себя из системы!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else {
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?",
                        "Сообщение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Users user = ConnectObj.conObj.Users.Where(x => x.id_user == IdUser).FirstOrDefault();
                        ConnectObj.conObj.Users.Remove(user);

                        ConnectObj.conObj.SaveChanges();
                        MessageBox.Show("Вы успешно удалили пользователя!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameObj.frameObj.Navigate(new PageListUsers());
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    } 
}