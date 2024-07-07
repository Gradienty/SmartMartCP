using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using KPT1.Model.Data;

namespace KPT1
{


    public class ProductsViewModel : INotifyPropertyChanged
    {
        private readonly SMEntities _context = SMEntities.GetContext();
        private Products _currentProduct;

        public Products CurrentProduct
        {
            get { return _currentProduct; }
            set
            {
                _currentProduct = value;
                OnPropertyChanged(nameof(CurrentProduct));
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Characteristics> Characteristics { get; set; }

        public ProductsViewModel(Products selectedProduct)
        {
            _currentProduct = selectedProduct ?? new Products();
            Categories = new ObservableCollection<Category>(_context.Category.ToList());
            Characteristics = new ObservableCollection<Characteristics>(_context.Characteristics.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
