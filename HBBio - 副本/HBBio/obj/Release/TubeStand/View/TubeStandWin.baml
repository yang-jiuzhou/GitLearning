   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HBBio.TubeStandHBBio.TubeStand  "    HBBio.TubeStand.TubeStandWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:HBBio.TubeStand     -      TS_TubeStandWin  � Title#  �   $	��463��$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $��5 ��� $��* ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnAdd��$��0��$��0�� 
 btnAdd#���  �� -   $ btnEdit��$��0��$��2��  btnEdit#���  �� -   $ btnDel��$��0��$��4�� 
 btnDel#���  &   System.Windows.Controls.DataGrid  -     Name$	 dgv��$��2��$��0��$��7��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly .    CanUserReorderColumns .  	   CanUserSortColumns	 .  
   Columns
 0  *System.Windows.Controls.DataGridTextColumn    TS_Name,  &System.Windows.Controls.DataGridColumn   Header# �  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *MName+    TS_CollVolume# �   �� *MCollVolume+    	TS_Volume# �   �� *	MVolume+    TS_Count# �   �� *MCount+    TS_Diameter# �   �� *	MDiameter+   	 	TS_Height# � 	  �� *	MHeight+   

 TS_Row# � 
  �� *MRow+   
 TS_Col# �   �� *MCol+