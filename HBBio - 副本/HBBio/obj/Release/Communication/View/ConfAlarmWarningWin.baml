   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  -    'HBBio.Communication.ConfAlarmWarningWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:HBBio.Communication     -      Com_ConfAlarmWarningWin  � Title#  �    � SizeToContent$ Height=�$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $��* ��� $��10 ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -   $��dgvAlarmWarning��$��0��   CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    Com_SignalName,  &System.Windows.Controls.DataGridColumn   Header# �     Width5  /System.Windows.Controls.DataGridLengthConverter$ * 	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *	MNameUnit+  -   	colLL�� 	 labLL# �  $
 0.5* 
 �� *MValLL+  -   colL��  labL# �  $
 0.5* 
 �� *MValL+  -   colH��  labH# �  $
 0.5* 
 �� *MValH+  -   	colHH�� 	 labHH# �  $
 0.5* 
 �� *MValHH+   
 labMin# �  $
 0.5* 	 . 
 �� *	MValMin+   
 labMax# �  $
 0.5* 	 . 
 �� *	MValMax+� $��2�� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ 	btnCommun��$��0��  	btnCommun#���  �� -   $ btnOK��$��2�� 		 btnOK#��� 	 �� -	   $ 	btnCancel��$��4�� 
 	btnCancel#��� 
 