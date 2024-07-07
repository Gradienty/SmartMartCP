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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KPT1
{
    /// <summary>
    /// Логика взаимодействия для Analis.xaml
    /// </summary>
    public partial class ProductPopularPage : Page
    {
        public ProductPopularPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SMEntities context = new SMEntities())
                {
                    
                    var data = context.v_PopularProducts.ToList();
                    PopularGrid.ItemsSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}