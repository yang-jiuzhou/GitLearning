   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   )    #HBBio.Communication.AlarmWarningWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.Communication     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           Com_AlarmWarningWin  � Title#  �   5	   	    � SizeToContent$ Height=�67   $	��750��6U   $	��309��6a   $	��500��6q    � WindowStartupLocation$ CenterScreen=�6�   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgvAlarmWarning��5      $��0��6*      CanUserReorderColumns .     CanUserSortColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5        Com_SignalName,  &System.Windows.Controls.DataGridColumn   Header# �  6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6m   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6O   �� *	MNameUnit+6w     5      -   5        	labLL�� 	 labLL# �  5   4   $
 0.5* 6p   
 6U   �� *MValLL+6p   4  .System.Windows.Controls.DataGridComboBoxColumn  5      -   5        	colLL��  	labStatus# �  5   8      DisplayMemberPath$ MString��6]      SelectedValuePath$ MEnum��6y   $
 0.5* 6�      SelectedValueBinding 6�   �� *
MCheckLL+6�     5      -   5        labL��  labL# �  5   3   $
 0.5* 6m   
 6S   �� *MValL+6m     5      -   5        colL��# �  5   7   $ MString��6\   $ MEnum��6x   $
 0.5* 6�    6�   �� *	MCheckL+6�     5      -   5        labH��  labH# �  5   3   $
 0.5* 6m   
 6S   �� *MValH+6m     5      -   5        colH��# �  5   7   $ MString��6\   $ MEnum��6x   $
 0.5* 6�    6�   �� *	MCheckH+6�     5      -	   5        	labHH�� 	 labHH# �  5   4   $
 0.5* 6p   
 6U   �� *MValHH+6p     5      -
   5        	colHH��# �  5   8   $ MString��6]   $ MEnum��6y   $
 0.5* 6�    6�   �� *
MCheckHH+6�     5       
 labMin# �  6%   $
 0.5* 6c   	 . 
 6G   �� *	MValMin+6p     5       
 labMax# �  6%   $
 0.5* 6c   	 . 
 6G   �� *	MValMax+6p   5      � 5   
   $��2��6    � ColumnDefinitions 5       �� 5!      $��* �6#   �� 5"      $
��Auto �6#   �� 5#      $��10 �6#   �� 5$      $
��Auto �6#   .+�5&      �� -   5         ��Name$ btnOK��5&      $��1��6$    		 btnOK#��� 	 64   6V   �� 5'      -   5        $ 	btnCancel��5'      $��3��6(    
 	btnCancel#��� 
 68   6^   5(      5)      5*      