using KPT1.Model.Data;
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
using System.Windows.Shapes;
using KPT1;

namespace KPT1
{
    /// <summary>
    /// Логика взаимодействия для LogWin.xaml
    /// </summary>
    public partial class LogWin : Window
    {
        private int failureLoginCounter = 0;
        public LogWin()
        {
            InitializeComponent();
            Passwordbox.IsEnabled = false;
            LoginBtn.IsEnabled = false;
            CheckBox.IsEnabled = false;
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox.IsChecked == true)
            {
                PasswordTextBox.Text = Passwordbox.Password;
                PasswordTextBox.Visibility = Visibility.Visible;
                Passwordbox.Visibility = Visibility.Collapsed;
            }
            else
            {
                Passwordbox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                Passwordbox.Visibility = Visibility.Visible;
            }
        }
        private void LoginTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int characterCount = LoginTextbox.Text.Length;
            Passwordbox.IsEnabled = characterCount > 3;
            CheckBox.IsEnabled = characterCount > 3;
        }

        private void Passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            int characterCount = Passwordbox.Password.Length;
            LoginBtn.IsEnabled = characterCount > 3;
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            using (var tbe = new SMEntities())
            {
                string enteredUserName = LoginTextbox.Text;
                string enteredPassword = Passwordbox.Password;
                UsersRoles usersRoles = tbe.UsersRoles.FirstOrDefault(ur => ur.Login == enteredUserName && ur.Password == enteredPassword);

                if (usersRoles != null)
                {
                    string lname = usersRoles.Users.LName;
                    string fname = usersRoles.Users.FName;
                    string patronymic = usersRoles.Users.Patronymic;
                    Roles role = tbe.Roles.FirstOrDefault(f => f.RoleID == usersRoles.RoleID);
                    string rolename = role.Name;
                    Info.Id_User = usersRoles.Users.UserID;

                    MessageBox.Show($"Добро пожаловать!\nПользователь: {lname} {fname} {patronymic}\nРоль: {rolename}");

                    OwnProgWin mw = new OwnProgWin();
                    this.Hide();
                    mw.Show();
                }
                else
                {
                    this.InvalidPassword();
                }
            }



        }
        private async void BlockWindow()
        {
            MessageBox.Show("Вы потратили 3 попытки входа. Повторите попытку через 15 секунд");
            this.IsEnabled = false;
            await Task.Delay(15000);
            this.IsEnabled = true;
            failureLoginCounter = 0;
        }

        private void InvalidPassword()
        {
            MessageBox.Show("Неверный логин! Осталось попыток " + (2 - failureLoginCounter));
            failureLoginCounter++;
            LoginTextbox.Text = "";
            Passwordbox.Password = "";
            PasswordTextBox.Text = "";
            if (failureLoginCounter >= 3) this.BlockWindow();
        }   




        private void btnEx_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                LogWin rl = new LogWin();
                rl.Show();
            }

        }

    }
}

