   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  5   	   %    HBBio.Chromatogram.MarkerAddWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   ,local clr-namespace:HBBio.Chromatogram  5   	      Ch_MarkerAddWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�63   $	��300��6Y    � WindowStartupLocation$ CenterScreen=�6h   .��5
      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$     labName#���  61   �� 5   
   -   5         ��Name$ txtName��5      $��2��6#   $��4��63   $��0��6G   �� 5   
   $��0��6   $��2��6$     D_MA_Position#���  61   	� 5   
   -   5         	�Name$ rbtnNow��5       	� 	GroupName$ position��6'   $��2��6<   $��4��6L   $��2��6`    d� 	IsChecked$
 TrueY�6m     D_MA_Now#���  6~   �� 5    
   	 �� Orientation$	 
Horizontal=�6   $��2��6.   $��4��6>   $��4��6R   .+�5!      	� -   5        $ rbtnPast��5!      $ position��6,     	D_MA_Past#���  6A   $  Xceed.Wpf.Toolkit.DoubleUpDown  5"      -   5        
  Name$
 
doubleTVCV��5"       $	��5,0q�64   {�6A   ��  �� ElementName$ rbtnPast�� �� Path$ 	IsChecked��� 5#      -   5         ��Name$ labTVCV��5#      $	��5,0q�6)   5$      �� 5%   
   -   5         ��Name$ btnOK��5%      $��3��6    $��6��60    	 btnOK#���  6=   6_   �� 5&   
   -   5        $ 	btnCancel��5&      $��5��6$   $��6��64     	btnCancel#���  6A   6g   5'      5(      