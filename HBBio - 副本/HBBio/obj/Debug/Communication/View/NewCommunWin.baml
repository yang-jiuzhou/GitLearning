   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   &     HBBio.Communication.NewCommunWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.Communication     5   	      Com_NewCommunWin  � Title#  �   5   	   $	��500��64   $	��750��6A   $	��309��6M   $	��500��6]    � WindowStartupLocation$ CenterOwner=�6l    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�   '��       '�� J     �� 5
   
   " ��6   .�5      ��  � ��
 ���� 6+   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��$ 5q�6(   �� 5       � ��
 ���� 6+   5      5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5       $��10 �6   �� 5!      $
��Auto �6   .+�5#   
   �� -   5        $	��tab��5#      $��0��6    $��0��60   $��8��6=   .@�5$      �� $��0��6   .��5%      �  5&      �� 5'      $��* �6+   �� 5(      $��5 �6+   �� 5)      $
��Auto �6+   �� 5*      $��5 �6+   �� 5+      $��* �6+   .+�5-      �� $��0��6     Com_ListAccess#���  6/   .��5.      �� -   5         ��Name$ scroll��5.   '   $
��Auto=�6d   $
��Auto=�6�   ��5/      �� 50   "   -   5        $��
stackpanel��50   -   $��5q�6?   $
��Auto=�6J   52      53      � 54      $��2��6    55      �� 56      $��* �6,   �� 57      $
��Auto �6,   �� 58      $��10 �6,   �� 59      $
��Auto �6,   �� 5:      $��10 �6,   �� 5;      $
��Auto �6,   �� 5<      $
��Auto �6,   �� 5=      $��10 �6,   �� 5>      $
��Auto �6,   �� 5?      $��* �6,   �� 5@      $
��Auto �6,   �� 5A      $��10 �6,   �� 5B      $
��Auto �6,   �� 5C      $��* �6,   .+�5E      �� -   5        	 ��Name$	 cboxCommunMode��5E   #   $��1��6;   6H   �� 5F      -   5        
 ��Name$
 btnAddressPort��5F   !   $��3��69     
Com_IPPort#���  6F   6m   �� 5G      -   5        $��btnAutoSearch��5G   !   $��5��66     Com_AutoSearch#���  6C   6n   �� 5H      -   5        $��btnAutoStop��5H   !   $��6��64     Com_StopSearch#���  6A   $y�Hidden=�6�   �� 5I      -   5        $��labAuto��5I   $   $��8��63     Com_WaitSearch#���  6@    �� TextAlignment$ Center=�6h   $y�Hidden=�6   �� 5J      -	   5        $��btnAdd��5J   !   $��10��6/   $��>>��6=   6P   �� 5K      -
   5        $��btnDel��5K   !   $��12��6/   $��<<��6=   6P   5L      �� 5M      $��4��6     Com_ListSelect#���  6/   .��5N      �� -   5        $ scroll2��5N   '   $
��Auto=�6e   $
��Auto=�6�   ��5O      �� 5P   "   -   5        $��stackpanel2��5P   -   $��5q�6@   $
��Auto=�6K   5R      5S      5T      5U      �� 5V      $��0��6   .��5W      ]� $��5q�6   .+�5X      �� -   5         ��Name$ labSystemName��5X       $	��Top=�67   $	��0,5q�6L     Com_SystemName1#���  6Y   �� 5Y      -   5         ��Name$ 
systemName��5Y      $	��Top=�62   $	��0,5q�6G   �� 5Z      -   5        $ labNote��5Z       $	��Top=�61   $	��0,5q�6F     Com_SystemNote1#���  6S   �� 5[      -   5        $ 
systemNote��5[      $	��Top=�62   $	��0,5q�6G    �� TextWrapping$
 Wrap=�6T    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6}   $��80��6�   ]� 5\      $	��Top=�6    $	��0,5q�65   .+�5]      �� -   5        $��btnConn��5]   8   $��Right=�6!    	 	Com_Check#��� 	 6G   6m   �� 5^      $
��Left=�6$    
 Com_CommunList1#��� 
 6:   5_      &   System.Windows.Controls.DataGrid  5`      -   5        $��
dgvComConf��5`      $	��0,5q�61      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6>      CanUserReorderColumns .     CanUserSortColumns .     Columns 5a      0  *System.Windows.Controls.DataGridTextColumn  5b        Com_Name,  &System.Windows.Controls.DataGridColumn   Header# �  6K      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *MName+6o     5c        	Com_Model# �  6L    .  61   �� *MModel+6q   4  .System.Windows.Controls.DataGridComboBoxColumn  5d      -   5        dgvComConfPortName��  Com_PortName# �  5d   Q      SelectedItemBinding 6y   �� *	MPortName+  5e      -   5        dgvComConfAddress��  Com_Address# �  5e   P    6w   �� *
MAddress+  5f      -   5        dgvComConfPort��  Com_Port# �  5f   M    6q   �� *MPort+  5g        Com_Version# �  6N    .  61   �� *
MVersion+6u     5h        
Com_Serial# �  6M    .  61   �� *	MSerial+6s     5i        
Com_Result# �  6M    .  61   �� *	MResult+6s   5k      5l      5m      �� 5n      $��0��6   .��5o      ]� $��5q�6   .+�5p      �� $	��Top=�6    $	��0,5q�65     Com_ComconfList1#���  6B     5q      -   5        $��dgvInstrument��5q      $ * 64    .   .   5r        5s      # �  6K    .  61   �� *MName+6o     5t      # �  6L    .  61   �� *MModel+6q     5u      -   5        dgvInstrumentPortName��# �  5u   n    .  6P   �� *	MPortName+6�     5v      -   5        dgvInstrumentAddress��# �  5v   l    .  6O   �� *
MAddress+6�     5w      -   5        dgvInstrumentPort��# �  5w   f    .  6L   �� *MPort+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5x      -   5        colConstName��  Com_ConstName# �  5x   K      CellTemplate 5y   "   �� 5z   &   .R�5{   *   �� ��63   �� *MConstNameList+ �� Text 6Z   ��  �� Path$ 
MConstName� �� Mode$ TwoWay=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError .  �� UpdateSourceTrigger$ PropertyChanged=�5|   '   5~        5        Com_DlyName# �  6N    61   �� *
MDlyName+6N     5�        Com_SetTime# �  6N    61   �� *
MSetTime+6N     5�        Com_RunTime# �  6N    .  61   �� *
MRunTime+6u   4  .System.Windows.Controls.DataGridCheckBoxColumn  5�        
Com_Enable# �  6X    65   �� *MEnableRunTime+6X     5�        Com_Calibration# �  65    5�   "   �� 5�   &   .R�5�   *   (	  "System.Windows.Controls.DatePicker	    	  SelectedDate  65   �� $ MCalibration�$ TwoWay=� .  . $ PropertyChanged=�5�   '   5�        5�      # �  6\    65   �� *MEnableCalibration+6\     5�        Com_Del# �  65    5�   "   �� 5�   &   .R�5�   *   �� -   5        $
��None=�5�   0   $.��(/Bio-LabChrom;component/Image/delete.png��6?   6q   5�   '   5�      5�      5�      5�      5�      �� 5�   
   -   5        $
 btnPrev��5�        btnPrev#���  6"   $��1��6F   $��2��6V   6c   �� 5�   
   -   5        $
 btnNext��5�        btnNext#���  6"   $��3��6F   $��2��6V   6c   �� 5�   
   -   5        $
 btnOK��5�       	 btnOK#���  6    $��5��6B   $��2��6R   6_   �� 5�   
   -   5        $
 	btnCancel��5�        	btnCancel#���  6$   $��7��6J   $��2��6Z   6g   5�      5�      