   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  &     HBBio.Communication.NewCommunWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:HBBio.Communication        Com_NewCommunWin  � Title#  �   $	��500��$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterOwner=� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� %     �� " ��.���  � ��
 ���� �� " ��.���  � ��$ 14(���  � ��$ 5q���  � ��
 ���� .��� $��10q� � ColumnDefinitions �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto � � RowDefinitions �� $��* ��� $��10 ��� $
��Auto �.+��� -   $	��tab��$��0��$��0��$��8��.@��� $��0��.���  �� $��* ��� $��5 ��� $
��Auto ��� $��5 ��� $��* �.+��� $��0��  Com_ListAccess#���  .���� -    ��Name$ scroll��$
��Auto=�$
��Auto=����� -   $��
stackpanel��$��5q�$
��Auto=�� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* �.+��� -   	 ��Name$	 cboxCommunMode��$��1���� -   
 ��Name$
 btnAddressPort��$��3��  
Com_IPPort#���  �� -   $��btnAutoSearch��$��5��  Com_AutoSearch#���  �� -   $��btnAutoStop��$��6��  Com_StopSearch#���  $y�Hidden=��� -   $��labAuto��$��8��  Com_WaitSearch#���   �� TextAlignment$ Center=�$y�Hidden=��� -	   $��btnAdd��$��10��$��>>���� -
   $��btnDel��$��12��$��<<���� $��4��  Com_ListSelect#���  .���� -   $ scroll2��$
��Auto=�$
��Auto=����� -   $��stackpanel2��$��5q�$
��Auto=��� $��0��.��]� $��5q�.+��� -    ��Name$ labSystemName��$	��Top=�$	��0,5q�  Com_SystemName1#���  �� -    ��Name$ 
systemName��$	��Top=�$	��0,5q��� -   $ labNote��$	��Top=�$	��0,5q�  Com_SystemNote1#���  �� -   $ 
systemNote��$	��Top=�$	��0,5q� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�$��80��]� $	��Top=�$	��0,5q�.+��� -   $��btnConn��$��Right=� 	 	Com_Check#��� 	 �� $
��Left=� 
 Com_CommunList1#��� 
 &   System.Windows.Controls.DataGrid  -   $��
dgvComConf��$	��0,5q�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    Com_Name,  &System.Windows.Controls.DataGridColumn   Header# �     
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *MName+    	Com_Model# �   .  �� *MModel+4  .System.Windows.Controls.DataGridComboBoxColumn  -   dgvComConfPortName��  Com_PortName# �     SelectedItemBinding �� *	MPortName+  -   dgvComConfAddress��  Com_Address# �   �� *
MAddress+  -   dgvComConfPort��  Com_Port# �   �� *MPort+    Com_Version# �   .  �� *
MVersion+    
Com_Serial# �   .  �� *	MSerial+    
Com_Result# �   .  �� *	MResult+�� $��0��.��]� $��5q�.+��� $	��Top=�$	��0,5q�  Com_ComconfList1#���    -   $��dgvInstrument��$ *  .   .     # �   .  �� *MName+  # �   .  �� *MModel+  -   dgvInstrumentPortName��# �   .  �� *	MPortName+  -   dgvInstrumentAddress��# �   .  �� *
MAddress+  -   dgvInstrumentPort��# �   .  �� *MPort+4  .System.Windows.Controls.DataGridTemplateColumn  -   colConstName��  Com_ConstName# �     CellTemplate �� .R��� ���� *MConstNameList+ �� Text ��  �� Path$ 
MConstName� �� Mode$ TwoWay=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError .  �� UpdateSourceTrigger$ PropertyChanged=�    Com_DlyName# �   �� *
MDlyName+    Com_SetTime# �   �� *
MSetTime+    Com_RunTime# �   .  �� *
MRunTime+4  .System.Windows.Controls.DataGridCheckBoxColumn    
Com_Enable# �   �� *MEnableRunTime+    Com_Calibration# �   �� .R�(	  "System.Windows.Controls.DatePicker	    	  SelectedDate  �� $ MCalibration�$ TwoWay=� .  . $ PropertyChanged=�  # �   �� *MEnableCalibration+    Com_Del# �   �� .R��� -   $
��None=�$.��(/Bio-LabChrom;component/Image/delete.png���� -   $
 btnPrev��  btnPrev#���  $��1��$��2���� -   $
 btnNext��  btnNext#���  $��3��$��2���� -   $
 btnOK�� 	 btnOK#���  $��5��$��2���� -   $
 	btnCancel��  	btnCancel#���  $��7��$��2��