using KPT1.Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace KPT1
{
    /// <summary>
    /// Логика взаимодействия для ProductPGClient.xaml
    /// </summary>
    public partial class ProductPGClient : Page
    {
        public ProductPGClient()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var context = SMEntities.GetContext();
            Category.ItemsSource = context.Category.ToList(); 
            DGridProducts.ItemsSource = context.Products.Where(p => p.Archived == 0).Include(p => p.Category).ToList(); 
        }

        private void LoadUserInfo()
        {
            using (var context = new SMEntities())
            {
                int iduser = Info.Id_User;
                var user = context.Users.FirstOrDefault(u => u.UserID == iduser);

                if (user != null)
                {
                    string fio = $"{user.LName} {user.FName} {user.Patronymic}";

                    var userRole = context.UsersRoles.Include(ur => ur.Roles)
                                                      .FirstOrDefault(ur => ur.UserID == iduser);
                    if (userRole != null)
                    {
                        string rolename = userRole.Roles.Name;
                        
                    }
                }
            }
        }

        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = DGridProducts.SelectedItem as Products;

            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите один товар для просмотра.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Manager.MainFrame.Navigate(new ProductDetailPage(selectedProduct));
        }

        public void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var productsForRemoving = DGridProducts.SelectedItems.Cast<Products>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {productsForRemoving.Count} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var context = SMEntities.GetContext();
                    var reservedProducts = context.v_ReservedProducts.ToList();
                    foreach (var product in productsForRemoving)
                    {
                        if (reservedProducts.Any(r => r.ProductID == product.ProductID))
                        {
                            throw new Exception("Невозможно удалить данный товар, потому что он забронирован");
                        }
                    }

                    context.Products.RemoveRange(productsForRemoving);
                    context.SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                var context = SMEntities.GetContext();
                context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LoadData(); 
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TxtSearch.Text.ToLower();
            DGridProducts.ItemsSource = SMEntities.GetContext().Products.Include(p => p.Category)
                .Where(p => p.PName.ToLower().Contains(searchText) && p.Archived == 0).ToList();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string categoryFilter = Category.Text.ToLower();
            DGridProducts.ItemsSource = SMEntities.GetContext().Products.Include(p => p.Category)
                .Where(p => p.Category.CName.ToLower().Contains(categoryFilter) && p.Archived == 0).ToList();
        }

        private void Popular_Click(object sender, RoutedEventArgs e)
        {
             ProductPopularPage productPopularPage = new ProductPopularPage();
    Manager.MainFrame.Navigate(productPopularPage);
        }

        private void BtnSupport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("UI/Wind/SupportPage.xaml", UriKind.Relative));
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompareProductsPage());
        }
    }

}
