using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_main_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try // если папки нет, она создаётся. если есть, то ничего не делается
            {
                Directory.CreateDirectory(Constant.DATA_DIR_NAME); // нужна для хранения временных файлов
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            try
            {
                InitializeComponent();
                Offer[] offerArray = Files.ReadOfferBase();

                if (offerArray.Length == 0)
                {
                    MessageBox.Show(
                        "Сохранённые акции не найдены, загрузка будет начата автоматически после выбора города.",
                        "Акции не найдены");
                    ChooseCityWindow ccw = new ChooseCityWindow();
                    ccw.CancellButton.IsEnabled = false;
                    ccw.ShowDialog();
                    
                    LoadingWindow w = new LoadingWindow();
                    w.Show();
                    OfferListBox.ItemsSource = DownloadOffer.GetArray(w);
                    w.Close();
                }
                else
                    OfferListBox.ItemsSource = offerArray;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow w = new LoadingWindow();
            w.Show();
            //Files.RemoveNonUsingPics();
            OfferListBox.ItemsSource = DownloadOffer.GetArray(w);
            w.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
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

        private void ChooseCityButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseCityWindow chooseCity = new ChooseCityWindow();
            chooseCity.ShowDialog();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.ShowDialog();
        }

        private void CloseResultsButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
