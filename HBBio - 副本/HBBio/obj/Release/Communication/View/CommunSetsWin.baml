   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  '    !HBBio.Communication.CommunSetsWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:HBBio.Communication     -      Com_CommunSetsWin  � Title#  �   $	��463��$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $��5 ��� $��* ��� $��5 ��� $
��Auto �.+��� $��0��$��0��  Com_CommunicationList1#���  &   System.Windows.Controls.DataGrid  -     Name$	 dgv��$��0��$��2��$��8��   
IsReadOnly .    CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *MName+,  &System.Windows.Controls.DataGridColumn
   Header
 ��   Com_SystemName#���    	 �� *MCommunMode+
 ��   Com_SystemMode#���       Width5  /System.Windows.Controls.DataGridLengthConverter$ * 	 �� *MNote+
 ��   Com_SystemNote#���    	 �� *MDatetimeStr+
 ��   Com_CreationDate#���  4  .System.Windows.Controls.DataGridTemplateColumn  
 ��   	labStatus#���     CellTemplate �� .R��� $
��None=����� *MIsEnabledIcon+�� -    ��Name$ btnAdd�� 
 btnAdd#���  $��1��$��4���� -   $ btnEdit��  btnEdit#���  $��3��$��4���� -   $ btnDel�� 
	 btnDel#��� 	 $��5��$��4���� -   $ 
btnEnabled�� 
 
btnEnabled#��� 
 $��7��$��4��