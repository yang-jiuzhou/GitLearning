   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   '    !HBBio.Communication.CommunSetsWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.Communication     5   	   -   5           Com_CommunSetsWin  � Title#  �   5   	   $	��463��65   $	��750��6B   $	��309��6N   $	��500��6^    � WindowStartupLocation$ CenterScreen=�6m   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$     Com_CommunicationList1#���  61   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$	 dgv��5      $��0��6    $��2��60   $��8��6=      
IsReadOnly .    CanUserReorderColumns .     CanUserSortColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� *MName+,  &System.Windows.Controls.DataGridColumn
   Header
 5      �� 5         Com_SystemName#���  6$   5"        5#      	 6%   �� *MCommunMode+
 5$      �� 5%        Com_SystemMode#���  6$   5'        5(         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6?   	 6%   �� *MNote+
 5)      �� 5*        Com_SystemNote#���  6$   5,        5-      	 6%   �� *MDatetimeStr+
 5.      �� 5/        Com_CreationDate#���  6$   51      4  .System.Windows.Controls.DataGridTemplateColumn  52      
 53      �� 54        	labStatus#���  6$      CellTemplate 56      �� 57      .R�58      �� $
��None=�6$   ��63   �� *MIsEnabledIcon+59      5;      5=      �� 5>   
   -   5         ��Name$ btnAdd��5>       
 btnAdd#���  6!   $��1��6D   $��4��6T   6a   �� 5?   
   -   5        $ btnEdit��5?        btnEdit#���  6"   $��3��6F   $��4��6V   6c   �� 5@   
   -   5        $ btnDel��5@       
	 btnDel#��� 	 6!   $��5��6D   $��4��6T   6a   �� 5A   
   -   5        $ 
btnEnabled��5A       
 
btnEnabled#��� 
 6%   $��7��6L   $��4��6\   6i   5B      5C      