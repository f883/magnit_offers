using System.Windows;
using System.Windows.Controls;

namespace GUI_main_WPF
{
    /// <summary>
    /// Логика взаимодействия для ChooseCityWindow.xaml
    /// </summary>
    public partial class ChooseCityWindow : Window
    {
        public ChooseCityWindow()
        {
            InitializeComponent();
            Region[] regions = Files.ReadCityDataBase();
            //MessageBox.Show(regions[2].regionName, regions[3].regionName);
            RegionListBox.ItemsSource = regions;
            Settings settings = Files.ReadSettings();
            RegionListBox.SelectedIndex = settings.RegionInListIndex;
            CityListBox.ItemsSource = regions[settings.RegionInListIndex].city;
            CityListBox.SelectedIndex = settings.CityInListIndex;
        }

        private void CityChosenButton_Click(object sender, RoutedEventArgs e)
        {
            // сохранение установленных настроек города
            //MessageBox.Show(RegionListBox.SelectedIndex.ToString(), "region");
            //MessageBox.Show(CityListBox.SelectedIndex.ToString(), "city");
            if (CityListBox.SelectedIndex == -1 || RegionListBox.SelectedIndex == -1)
                MessageBox.Show("Вы не выбрали город.");
            else
            {
                Region[] regions = Files.ReadCityDataBase();
                int Rindex = RegionListBox.SelectedIndex;
                int Cindex = CityListBox.SelectedIndex;
                Settings stng = new Settings();
                stng.RegionID = regions[Rindex].regionID;
                stng.RegionName = regions[Rindex].regionName;
                stng.CityName = regions[Rindex].city[Cindex].cityName;
                stng.CityID = regions[Rindex].city[Cindex].cityID;
                stng.CityInListIndex = Cindex;
                stng.RegionInListIndex = Rindex;
                Files.SaveSettings(stng);
                this.Close();
            }
        }

        private void CancellButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegionListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = RegionListBox.SelectedIndex;
            Region[] regions = Files.ReadCityDataBase();
            CityListBox.ItemsSource = regions[index].city;
        }
    }
}
