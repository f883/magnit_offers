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
using System.Windows.Shapes;

namespace GUI_main_WPF
{
    /// <summary>
    /// Логика взаимодействия для FoundResultsWindow.xaml
    /// </summary>
    public partial class FoundResultsWindow : Window
    {
        public FoundResultsWindow(Offer[] foundOffers)
        {
            InitializeComponent();
            OfferListBox.ItemsSource = foundOffers;
        }

        private void OfferListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItemIndex = OfferListBox.SelectedIndex;
            Offer[] offerArray = (Offer[])OfferListBox.ItemsSource;

            try
            {
                LargeActionImage.Source = BitmapFrame.Create(new Uri(offerArray[selectedItemIndex].largePicDirectory));
                LargeActionTextBlock.Text = offerArray[selectedItemIndex].largeBody;
            }
            catch
            {
                LargeActionImage.Source = null;
                LargeActionTextBlock.Text = "У данной акции нет подробного описания";
            }
        }

        private void CloseResultsButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
