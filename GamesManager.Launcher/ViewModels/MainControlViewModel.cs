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

        private ObservableCollection<NewsItemView> newsItemViews;

        public ObservableCollection<ProductItemView> ProductItemViews
        {
            get => productItemViews;
            private set
            {
                productItemViews = value;
                RaiseOnPropertyChanged();
            }
        }

        public ObservableCollection<NewsItemView> NewsItemViews
        {
            get => newsItemViews;
            private set
            {
                newsItemViews = value;
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

            NewsItemViews = new ObservableCollection<NewsItemView>
            {
                new NewsItemView(),
            };
        }

        #endregion

        #region Methods

        #endregion
    }
}
