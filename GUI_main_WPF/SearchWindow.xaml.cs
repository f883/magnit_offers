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
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text.Length != 0)
            {
                Offer[] offers = Files.ReadOfferBase();
                Offer[] foundOffers = Search.FuzzySearch(offers, SearchTextBox.Text);
                FoundResultsWindow frw = new FoundResultsWindow(foundOffers);
                frw.ShowDialog();
                this.Close();
            }
        else
            MessageBox.Show("Ничего не введено. Повторите попытку или нажмите кнопку 'Отмена' для закрытия окна поиска");
        }

        private void SearchTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (SearchTextBox.Text.Length != 0)
                {
                    Offer[] offers = Files.ReadOfferBase();
                    Offer[] foundOffers = Search.FuzzySearch(offers, SearchTextBox.Text);
                    FoundResultsWindow frw = new FoundResultsWindow(foundOffers);
                    frw.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("Ничего не введено. Повторите попытку или нажмите кнопку 'Отмена' для закрытия окна поиска");
            }
        }
    }
}
