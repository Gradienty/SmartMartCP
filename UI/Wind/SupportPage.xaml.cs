using System.Windows;
using System.Windows.Controls;

namespace KPT1
{
    public partial class SupportPage : Page
    {
        public SupportPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtsup.Clear();
            MessageBox.Show("Спасибо за обращение, мы займёмся этим в ближайшее время");
        }
    }
}
