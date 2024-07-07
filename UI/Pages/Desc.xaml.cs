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
using System.ComponentModel;
using KPT1.Model.Data;

namespace KPT1
{
    /// <summary>
    /// Логика взаимодействия для Desc.xaml
    /// </summary>
    public partial class Desc : Page
    {
        private Characteristics _currentChar = new Characteristics();
        private readonly SMEntities _context = SMEntities.GetContext();
        public Desc()
        {
            InitializeComponent();
            DataContext = _currentChar;
        }

           

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentChar.Description))
                errors.AppendLine("Укажите описание товара");
            if (_currentChar.EnergyConsumption_KWT_H_ == 0)
                errors.AppendLine("Укажите энергопотребление товара");
            if (_currentChar.Power_KWT_ == 0)
                errors.AppendLine("Укажите мощность товара");
            if (_currentChar.Guaranty_Years_ == 0)
                errors.AppendLine("Укажите гарантию на товар");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (_currentChar.CharacteristicsID == 0)
                    _context.Characteristics.Add(_currentChar);

                _context.SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
