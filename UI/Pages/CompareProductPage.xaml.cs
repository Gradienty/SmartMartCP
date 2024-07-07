using KPT1.Model.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace KPT1
{
    public partial class CompareProductsPage : Page
    {
        public CompareProductsPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var context = SMEntities.GetContext())
            {
                var products = context.Products.Include("Characteristics").ToList();
                ComboBoxProduct1.ItemsSource = products;
                ComboBoxProduct2.ItemsSource = products;
            }
        }

        private void ComboBoxProduct1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = ComboBoxProduct1.SelectedItem as Products;
            if (selectedProduct != null)
            {
                UpdateCharacteristicsDisplay(selectedProduct, StackPanelProduct1Characteristics);
            }
        }

        private void ComboBoxProduct2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = ComboBoxProduct2.SelectedItem as Products;
            if (selectedProduct != null)
            {
                UpdateCharacteristicsDisplay(selectedProduct, StackPanelProduct2Characteristics);
            }
        }

        private void UpdateCharacteristicsDisplay(Products product, StackPanel stackPanel)
        {
            stackPanel.Children.Clear();

            if (product.Characteristics != null)
            {
                stackPanel.Children.Add(new TextBlock { Text = $"Description: {product.Characteristics.Description}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Power: {product.Characteristics.Power_KWT_} KWT" });
                stackPanel.Children.Add(new TextBlock { Text = $"Energy Consumption: {product.Characteristics.EnergyConsumption_KWT_H_} KWT*H" });
                stackPanel.Children.Add(new TextBlock { Text = $"Guaranty: {product.Characteristics.Guaranty_Years_} Years" });
            }
        }
    }
}
