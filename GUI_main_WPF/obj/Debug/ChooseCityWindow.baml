   M S B A M L   `   `   `  ����G  CGUI_main_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GUI_main_WPFGUI_main_WPF  5   	   #    GUI_main_WPF.ChooseCityWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:GUI_main_WPF     5   	     � Icon$  
magnit.png��5   	    � Title$% Изменение города��5	   	   $	��322��6"   $	��542��6/    � ShowInTaskbar .  .��5
      � $��Stretch=�6   $��Stretch=�6'    J� RenderTransformOrigin$ 0.515,0.497&�6E    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��25 �6   6*   �� 5      $��* �6   6)   �� 5      $��30 �6   6*   .+�5   
   �� $#��Выберите регион��6   $��Stretch=�6+   $��Stretch=�6G   $��14(�6e   
������� �� TextAlignment$ Center=�6�   �� 5   
   $6��0Выберите населённый пункт��6   $��1��65   $��Stretch=�6E   $��Stretch=�6a   $��14(�6   
�������$ Center=�6�   6�   �� 5   
   -   5         ��Name$ RegionListBox��5   /   $��0��6   $��1��6"    %� 
ScrollUnit$ Pixel=�6F   $��Disabled=�6k   ��5      �� 5      .R�5      �  5      �� 5      61    5      �� 5       6.   .+�5"      �� $��0��6@   $��0��6P   $
��Wrap=�6]   $��14(�6q   $��Stretch=�6   $��Stretch=�6�   ��6$   �� *
regionName+6�   5#      5$      5&      �� 5'   
   -   5        $ CityListBox��5'   /   $��1��6   $��1��6"   $ Pixel=�6D   $��Disabled=�6i   ��5(      �� 5*      .R�5,      �� $
��Wrap=�6:   $��14(�6N   $��Stretch=�6\   $��Stretch=�6z   ��6    �� *
cityName+6�   5-      5/      �� 50   
   -   5        	 ��Name$	 CityChosenButton��50      $��1��6+   $��2��6;   $��Применить��6H   $��14(�6\   $
��10,0q�6j   $��Stretch=�6x   6�   �� 51   
   -   5        $	 CancellButton��51      $��0��6(   $��2��68   $��Отменить��6E   $��14(�6X   $
��10,0q�6f   $��Stretch=�6t   6�   52      53      