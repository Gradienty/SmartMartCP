using KPT1.Model.Data;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace KPT1
{
    public partial class AddEditPG : Page
    {
        private Products _currentProduct = new Products();
        private readonly SMEntities _context = SMEntities.GetContext();

       
        private bool _isDataLoaded = false;

        public AddEditPG(Products selectedProduct)
        {
            InitializeComponent();

           
            if (!_isDataLoaded)
            {
              
                LoadCategoryData();
                LoadCharacteristicsData();
                _isDataLoaded = true;
            }

            if (selectedProduct != null)
            {
                _currentProduct = selectedProduct;
            }

            DataContext = _currentProduct;
        }

        private void LoadCategoryData()
        {
            ComboCategory.ItemsSource = _context.Category.ToList();
        }

        private void LoadCharacteristicsData()
        {
            ComboChar.ItemsSource = _context.Characteristics.ToList();
        }

        public void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

          
            if (string.IsNullOrWhiteSpace(_currentProduct.PName))
                errors.AppendLine("Укажите название товара");

           
            if (_currentProduct.CategoryID == 0)
                errors.AppendLine("Выберите категорию");

            
            if (_currentProduct.Price <= 0)
                errors.AppendLine("Укажите корректную цену");

           
            if (_currentProduct.Stock < 0)
                errors.AppendLine("Укажите корректное количество");

           
            if (_currentProduct.Archived != 0 && _currentProduct.Archived != 1)
                errors.AppendLine("Укажите корректное значение для статуса архивирования");

           
            if (_currentProduct.CharacteristicsID == 0)
                errors.AppendLine("Выберите характеристику");

           
            if (_currentProduct.ArrivalDate == default(DateTime))
                errors.AppendLine("Укажите корректную дату прибытия");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_currentProduct.ProductID == 0)
                    _context.Products.Add(_currentProduct);

                _context.SaveChanges();
                MessageBox.Show("Информация сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.Navigate(new ProductPG());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateDesc_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Desc());
        }
    }
}
