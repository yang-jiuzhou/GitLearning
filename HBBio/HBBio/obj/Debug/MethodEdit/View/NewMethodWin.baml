   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   #    HBBio.MethodEdit.NewMethodWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.MethodEdit     5   	      ME_NewMethodWin  � Title#  �   5   	    � SizeToContent$ WidthAndHeight=�63   $	��369��6R   $	��500��6b    � WindowStartupLocation$ CenterScreen=�6r   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��15 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    .+�5!      �� $��0��6     ME_Type1#���  6%   	� 5"      -   5         	�Name$ rbtnPurification��5"      $��2��64   $��20,0,0,0q�6A     ME_Purification1#���  6S   �� 5#      -   5         ��Name$ cboxPurification��5#      $��4��61   $��40,0,0,0q�6>   {�6P   ��  �� ElementName$ rbtnPurification�� �� Path$ 	IsChecked�	� 5$      -   5        $ rbtnMaintenance��5$      $��6��63   $��20,0,0,0q�6@     ME_Maintenance1#���  6R   �� 5%      -   5        $ cboxMaintenance��5%      $��8��60   $��40,0,0,0q�6=   {�6O   �� $ rbtnMaintenance��$ 	IsChecked�	� 5&      $��10��6   $��20,0,0,0q�6(     ME_Empty#���  6:    d� 	IsChecked$
 TrueY�6_   �� 5'      $��12��6     ME_Name1#���  6&   �� 5(      -   5        	 ��Name$	 txtMethodName��5(      $��14��6-   $��40,0,0,0q�6;   5)      � 5*   
   $��2��6   
 � ColumnDefinitions
 5+      �� 5,      $��* �6#   �� 5-      $
��Auto �6#   �� 5.      $��5 �6#   �� 5/      $
��Auto �6#   .+�51      �� -   5         ��Name$ btnOK��51      $��1��6$    	 btnOK#���  64   6V   �� 52      -   5        $ 	btnCancel��52      $��3��6(     	btnCancel#���  68   6^   53      54      55      