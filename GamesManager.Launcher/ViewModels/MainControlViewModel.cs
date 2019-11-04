using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GamesManager.Launcher.Views;
using MVVM_Helper.Binding;

namespace GamesManager.Launcher.ViewModels
{
    public class MainControlViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<ProductItemView> productItemViews;

        public ObservableCollection<ProductItemView> ProductItemViews
        {
            get => productItemViews;
            private set
            {
                productItemViews = value;
                RaiseOnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public MainControlViewModel()
        {
            ProductItemViews = new ObservableCollection<ProductItemView>
            {
                new ProductItemView("Roll a Ball"),
                new ProductItemView("Roll a Ball Online", isActive: false),
            };
        }

        #endregion

        #region Methods

        #endregion
    }
}
