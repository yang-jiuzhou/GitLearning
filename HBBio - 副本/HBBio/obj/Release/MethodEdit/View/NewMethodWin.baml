   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  #    HBBio.MethodEdit.NewMethodWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.MethodEdit        ME_NewMethodWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��369��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��  ME_Type1#���  	� -    	�Name$ rbtnPurification��$��2��$��20,0,0,0q�  ME_Purification1#���  �� -    ��Name$ cboxPurification��$��4��$��40,0,0,0q�{���  �� ElementName$ rbtnPurification�� �� Path$ 	IsChecked�	� -   $ rbtnMaintenance��$��6��$��20,0,0,0q�  ME_Maintenance1#���  �� -   $ cboxMaintenance��$��8��$��40,0,0,0q�{��� $ rbtnMaintenance��$ 	IsChecked�	� $��10��$��20,0,0,0q�  ME_Empty#���   d� 	IsChecked$
 TrueY��� $��12��  ME_Name1#���  �� -   	 ��Name$	 txtMethodName��$��14��$��40,0,0,0q�� $��2��
 � ColumnDefinitions
 �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3��  	btnCancel#���  