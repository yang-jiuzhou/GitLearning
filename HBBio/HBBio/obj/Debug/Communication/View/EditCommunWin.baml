   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   '    !HBBio.Communication.EditCommunWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.Communication     5   	      Com_EditCommunWin  � Title#  �   5   	   $	��463��65   $	��750��6B   $	��309��6N   $	��500��6^    � WindowStartupLocation$ CenterOwner=�6m   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ scroll��5      $��0��6'   $��0��67   $��5��6D   $
��Auto=�6X   ��5      �� 5      $
��Auto=�6   .+�5      �� -   5         ��Name$ labSystemName��5       $	��Top=�67   $	��0,5q�6L     Com_SystemName1#���  6Y   �� 5      -   5         ��Name$ 
systemName��5      $	��Top=�62   $	��0,5q�6G   �� 5      -   5        $ labNote��5       $	��Top=�61   $	��0,5q�6F     Com_SystemNote1#���  6S   �� 5      -   5        $ 
systemNote��5      $	��Top=�62   $	��0,5q�6G    �� TextWrapping$
 Wrap=�6T    �� AcceptsReturn . "	 �� VerticalScrollBarVisibility$
	 Auto=�6}   $��80��6�   ]� 5      $	��Top=�6    $	��0,5q�65   .+�5      �� -   5        $��btnConn��5   8   $��Right=�6!     	Com_Check#���  6G   6m   �� 5      $
��Left=�6$     Com_CommunList1#���  6:   5       &   System.Windows.Controls.DataGrid  5!      -   5        $��
dgvComConf��5!      $	��0,5q�61   
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 * 6>      CanUserReorderColumns .     CanUserSortColumns .     Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#        Com_Name,  &System.Windows.Controls.DataGridColumn   Header# �  6K      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *MName+6o     5$        	Com_Model# �  6L    .  61   �� *MModel+6q   4  .System.Windows.Controls.DataGridComboBoxColumn  5%      -   5        dgvComConfPortName��  Com_PortName# �  5%   Q      SelectedItemBinding 6y   �� *	MPortName+  5&      -	   5        dgvComConfAddress��  Com_Address# �  5&   P    6w   �� *
MAddress+  5'      -
   5        dgvComConfPort�� 	 Com_Port# � 	 5'   M    6q   �� *MPort+  5(       
 Com_Version# � 
 6N    .  61   �� *
MVersion+6u     5)        
Com_Serial# �  6M    .  61   �� *	MSerial+6s     5*        
Com_Result# �  6M    .  61   �� *	MResult+6s   5,      �� 5-      $	��0,5q�6      Com_ComconfList1#���  6-     5.      -   5        $��dgvInstrument��5.      $	��0,5q�64   $
 * 6A    .   .   5/        50      # �  6K    .  61   �� *MName+6o     51      # �  6L    .  61   �� *MModel+6q     52      -   5        dgvInstrumentPortName��# �  52   n    .  6P   �� *	MPortName+6�     53      -   5        dgvInstrumentAddress��# �  53   l    .  6O   �� *
MAddress+6�     54      -   5        dgvInstrumentPort��# � 	 54   f    .  6L   �� *MPort+6�     55        Com_ConstName# �  6P    .  61   �� *
MConstName+6y     56        Com_DlyName# �  6N    61   �� *
MDlyName+6N     57        Com_SetTime# �  6N    61   �� *
MSetTime+6N     58        Com_RunTime# �  6N    .  61   �� *
MRunTime+6u   4  .System.Windows.Controls.DataGridCheckBoxColumn  59        
Com_Enable# �  6X    65   �� *MEnableRunTime+6X   4  .System.Windows.Controls.DataGridTemplateColumn  5:        Com_Calibration# �  65      CellTemplate 5;   "   �� 5<   &   .R�5=   *   (	  "System.Windows.Controls.DatePicker	   	  SelectedDate 65   ��  �� Path$ MCalibration� �� Mode$ TwoWay=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError .  �� UpdateSourceTrigger$ PropertyChanged=�5>   '   5@        5A      # �  6\    65   �� *MEnableCalibration+6\   5C      5D      5F      �� 5G   
   -   5         ��Name$ btnProcessPicture��5G        menuProcessPicture#���  6,   $��0��6[   $��2��6k   6x   �� 5H   
   -   5        $ btnOK��5H       	 btnOK#���  6    $��2��6B   $��2��6R   6_   �� 5I   
   -   5        $ 	btnCancel��5I        	btnCancel#���  6$   $��4��6J   $��2��6Z   6g   5J      5K      