   M S B A M L   `   `   `  ���� I  EMagnit actions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GUI_main_WPFGUI_main_WPF  #    GUI_main_WPF.ChooseCityWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:GUI_main_WPF       � WindowStyle$
  None=� � Icon$ 
magnit.png�� � Title$% Изменение города��$	��322��$	��542�� � ShowInTaskbar .  .��� $��Stretch=�$��Stretch=� J� RenderTransformOrigin$ 0.515,0.497&� � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $��25 ��� $��* ��� $��30 �.+��� $#��Выберите регион��$��Stretch=�$��Stretch=�$��14(�
������� �� TextAlignment$ Center=��� $6��0Выберите населённый пункт��$��1��$��Stretch=�$��Stretch=�$��14(�
�������$ Center=��� -    ��Name$ RegionListBox��$��0��$��1��	 %� 
ScrollUnit$	 Pixel=�$��Disabled=����� .R��  ��  �� .+��� $��0��$��0��$
��Wrap=�$��14(�$��Stretch=�$��Stretch=����� *
regionName+�� -   $ CityListBox��$��1��$��1��$	 Pixel=�$��Disabled=����� .R��� $
��Wrap=�$��14(�$��Stretch=�$��Stretch=����� *
cityName+�� -   
 ��Name$
 CityChosenButton��$��1��$��2��$��Применить��$��14(�$
��10,0q�$��Stretch=��� -   $
 CancellButton��$��0��$��2��$��Отменить��$��14(�$
��10,0q�$��Stretch=�