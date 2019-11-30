using System.Collections.ObjectModel;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Helper.Binding;
using GamesManager.Launcher.Views;

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
                new ProductItemView(GameName.Roll_a_Ball),
                //new ProductItemView(GameName.Roll_a_Ball_Online),
            };
        }

        #endregion

        #region Methods

        #endregion
    }
}
