using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using KPT1.Model.Data;

namespace KPT1
{
    public partial class OwnProgWin : Window
    {
        public OwnProgWin()
        {
            InitializeComponent();
            MainFrame.ContentRendered += MainFrame_ContentRendered;
            using (SMEntities tbe = new SMEntities())
            {
                int iduser = Info.Id_User;
                Users user = tbe.Users.FirstOrDefault(u => u.UserID == iduser);
                if (user != null)
                {
                    string fio = $"{user.LName} {user.FName} {user.Patronymic}";
                    UsersRoles userRole = tbe.UsersRoles.FirstOrDefault(ur => ur.UserID == iduser);
                    if (userRole != null)
                    {
                        Roles role = tbe.Roles.FirstOrDefault(f => f.RoleID == userRole.RoleID);
                        if (role != null)
                        {
                            string rolename = role.Name;
                            tb_fio.Text = fio;
                            tb_role.Text = rolename;

                            
                            if (rolename == "Консультант")
                            {
                                ProductPGClient productPGUser = new ProductPGClient();
                                MainFrame.Navigate(productPGUser);
                            }
                            else
                            {
                                ProductPG productPG = new ProductPG();
                                MainFrame.Navigate(productPG);
                            }
                            Manager.MainFrame = MainFrame;
                        }
                    }
                }
            }
        }

        private void BtnProductDetails_Click(object sender, RoutedEventArgs e)
        {
            ProductDetails productDetails = new ProductDetails();
            MainFrame.Navigate(productDetails);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }



        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    LogWin rl = new LogWin();
                    this.Hide();
                    rl.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void BtnSupport_Click(object sender, RoutedEventArgs e)
        {
            SupportPage supportPage = new SupportPage();
            MainFrame.Navigate(supportPage);
        }

        private void OwnProgWin_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
