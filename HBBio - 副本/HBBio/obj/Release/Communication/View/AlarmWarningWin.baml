   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  )clr-namespace:HBBio.ShareHBBio.Share  )    #HBBio.Communication.AlarmWarningWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:HBBio.Communication     +shareclr-namespace:HBBio.Share     -      Com_AlarmWarningWin  � Title#  �    � SizeToContent$ Height=�$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $��* ��� $��10 ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -   $��dgvAlarmWarning��$��0��   CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    Com_SignalName,  &System.Windows.Controls.DataGridColumn   Header# �     Width5  /System.Windows.Controls.DataGridLengthConverter$ * 	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *	MNameUnit+  -   	labLL�� 	 labLL# �  $
 0.5* 
 �� *MValLL+4  .System.Windows.Controls.DataGridComboBoxColumn  -   	colLL��  	labStatus# �     DisplayMemberPath$ MString��   SelectedValuePath$ MEnum��$
 0.5*    SelectedValueBinding �� *
MCheckLL+  -   labL��  labL# �  $
 0.5* 
 �� *MValL+  -   colL��# �  $ MString��$ MEnum��$
 0.5*  �� *	MCheckL+  -   labH��  labH# �  $
 0.5* 
 �� *MValH+  -   colH��# �  $ MString��$ MEnum��$
 0.5*  �� *	MCheckH+  -	   	labHH�� 	 labHH# �  $
 0.5* 
 �� *MValHH+  -
   	colHH��# �  $ MString��$ MEnum��$
 0.5*  �� *
MCheckHH+   
 labMin# �  $
 0.5* 	 . 
 �� *	MValMin+   
 labMax# �  $
 0.5* 	 . 
 �� *	MValMax+� $��2�� � ColumnDefinitions �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 		 btnOK#��� 	 �� -   $ 	btnCancel��$��3�� 
 	btnCancel#��� 
 