   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  '    !HBBio.Communication.EditCommunWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:HBBio.Communication        Com_EditCommunWin  � Title#  �   $	��463��$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��5 ��� $
��Auto � � RowDefinitions �� $��* ��� $��5 ��� $
��Auto �.+��� -    ��Name$ scroll��$��0��$��0��$��5��$
��Auto=����� $
��Auto=�.+��� -    ��Name$ labSystemName��$	��Top=�$	��0,5q�  Com_SystemName1#���  �� -    ��Name$ 
systemName��$	��Top=�$	��0,5q��� -   $ labNote��$	��Top=�$	��0,5q�  Com_SystemNote1#���  �� -   $ 
systemNote��$	��Top=�$	��0,5q� �� TextWrapping$
 Wrap=� �� AcceptsReturn . "	 �� VerticalScrollBarVisibility$
	 Auto=�$��80��]� $	��Top=�$	��0,5q�.+��� -   $��btnConn��$��Right=�  	Com_Check#���  �� $
��Left=�  Com_CommunList1#���  &   System.Windows.Controls.DataGrid  -   $��
dgvComConf��$	��0,5q�
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 *    CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    Com_Name,  &System.Windows.Controls.DataGridColumn   Header# �     
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *MName+    	Com_Model# �   .  �� *MModel+4  .System.Windows.Controls.DataGridComboBoxColumn  -   dgvComConfPortName��  Com_PortName# �     SelectedItemBinding �� *	MPortName+  -	   dgvComConfAddress��  Com_Address# �   �� *
MAddress+  -
   dgvComConfPort�� 	 Com_Port# � 	  �� *MPort+   
 Com_Version# � 
  .  �� *
MVersion+    
Com_Serial# �   .  �� *	MSerial+    
Com_Result# �   .  �� *	MResult+�� $	��0,5q�  Com_ComconfList1#���    -   $��dgvInstrument��$	��0,5q�$
 *  .   .     # �   .  �� *MName+  # �   .  �� *MModel+  -   dgvInstrumentPortName��# �   .  �� *	MPortName+  -   dgvInstrumentAddress��# �   .  �� *
MAddress+  -   dgvInstrumentPort��# � 	  .  �� *MPort+    Com_ConstName# �   .  �� *
MConstName+    Com_DlyName# �   �� *
MDlyName+    Com_SetTime# �   �� *
MSetTime+    Com_RunTime# �   .  �� *
MRunTime+4  .System.Windows.Controls.DataGridCheckBoxColumn    
Com_Enable# �   �� *MEnableRunTime+4  .System.Windows.Controls.DataGridTemplateColumn    Com_Calibration# �     CellTemplate �� .R�(	  "System.Windows.Controls.DatePicker	   	  SelectedDate ��  �� Path$ MCalibration� �� Mode$ TwoWay=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError .  �� UpdateSourceTrigger$ PropertyChanged=�  # �   �� *MEnableCalibration+�� -    ��Name$ btnProcessPicture��  menuProcessPicture#���  $��0��$��2���� -   $ btnOK�� 	 btnOK#���  $��2��$��2���� -   $ 	btnCancel��  	btnCancel#���  $��4��$��2��