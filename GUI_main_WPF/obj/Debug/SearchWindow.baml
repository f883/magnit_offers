   M S B A M L   `   `   `  ����G  CGUI_main_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GUI_main_WPFGUI_main_WPF  5   	       GUI_main_WPF.SearchWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:GUI_main_WPF     5   	     � Icon$  
magnit.png��5   	    � Title$ Поиск акций��5	   	   $	��346��6   $	��426��6*   .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��20 �6   6*   �� 5      $
��180* �6   6,   �� 5      $��30 �6   6*   �� 5      $��10 �6   6*   .+�5   
   �� $3��-Введите текст для поиска��6   $��14(�64    �� TextAlignment$ Center=�6B   $��0��6Y   $��0��6i   $��2��6v   6�   �� 5   
   -   5         ��Name$ SearchTextBox��5      $��0��6*   $��2��6:   $��1��6N   $��10q�6[   6g   �� 5   
   -   5         ��Name$ SearchButton��5   !   $��
Найти��6   $��1��67   $��2��6G   $
��10,0q�6T   6�   �� 5   
   -   5        $ CancelButton��5   "   $��Отмена��6   $��0��69   $��2��6I   $
��10,0q�6V   6�   5      5      