   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HBBio.TubeStandHBBio.TubeStand  5   	   "    HBBio.TubeStand.TubeStandWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:HBBio.TubeStand     5   	   -   5           TS_TubeStandWin  � Title#  �   5   	   $	��463��63   $	��750��6@   $	��309��6L   $	��500��6\    � WindowStartupLocation$ CenterScreen=�6k   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ btnAdd��5      $��0��6!   $��0��6.    
 btnAdd#���  6>   6a   �� 5   
   -   5        $ btnEdit��5      $��0��6"   $��2��6/     btnEdit#���  6?   6c   �� 5   
   -   5        $ btnDel��5      $��0��6!   $��4��6.    
 btnDel#���  6>   6a   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$	 dgv��5      $��2��6    $��0��6-   $��7��6=      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6Q      
IsReadOnly .    CanUserReorderColumns .  	   CanUserSortColumns	 .  
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5        TS_Name,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *MName+  5         TS_CollVolume# �  6%    6N   �� *MCollVolume+  5!        	TS_Volume# �  6%    6J   �� *	MVolume+  5"        TS_Count# �  6%    6I   �� *MCount+  5#        TS_Diameter# �  6%    6L   �� *	MDiameter+  5$       	 	TS_Height# � 	 6%    6J   �� *	MHeight+  5%       

 TS_Row# � 
 6%    6G   �� *MRow+  5&       
 TS_Col# �  6%    6G   �� *MCol+5(      5)      5*      