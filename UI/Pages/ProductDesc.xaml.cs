﻿using KPT1.Model.Data;
using System.Linq;
using System.Windows.Controls;

namespace KPT1
{
    public partial class ProductDetails : Page
    {
        public ProductDetails()
        {
            InitializeComponent();
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            using (var context = new SMEntities())
            {
                var productDetails = context.Products
                    .Join(context.Characteristics,
                          p => p.ProductID,
                          c => c.CharacteristicsID,
                          (p, c) => new ProductDetail
                          {
                              PName = p.PName,
                              Price = p.Price,
                              Description = c.Description,
                              Power_KWT = c.Power_KWT_,
                              EnergyConsumption_KWT_H = c.EnergyConsumption_KWT_H_,
                              Guaranty_Years = c.Guaranty_Years_,
                              Availability = p.Stock > 0 ? p.Stock.ToString() : "нет в наличии"
                          })
                    .ToList();

                DGridProductDetails.ItemsSource = productDetails;
            }
        }
    }
}
