   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  %    HBBio.Chromatogram.MarkerAddWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  ,local clr-namespace:HBBio.Chromatogram     Ch_MarkerAddWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��300�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* ��� $
��Auto ��� $��10 ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$��0��  labName#���  �� -    ��Name$ txtName��$��2��$��4��$��0���� $��0��$��2��  D_MA_Position#���  	� -    	�Name$ rbtnNow�� 	� 	GroupName$ position��$��2��$��4��$��2�� d� 	IsChecked$
 TrueY�  D_MA_Now#���  �� 	 �� Orientation$	 
Horizontal=�$��2��$��4��$��4��.+�	� -   $ rbtnPast��$ position��  	D_MA_Past#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -   
  Name$
 
doubleTVCV��$	��5,0q�{���  �� ElementName$ rbtnPast�� �� Path$ 	IsChecked��� -    ��Name$ labTVCV��$	��5,0q��� -    ��Name$ btnOK��$��3��$��6�� 	 btnOK#���  �� -   $ 	btnCancel��$��5��$��6��  	btnCancel#���  