   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   -    'HBBio.Communication.ConfAlarmWarningWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.Communication     5   	   -   5           Com_ConfAlarmWarningWin  � Title#  �   5   	    � SizeToContent$ Height=�6;   $	��750��6Y   $	��309��6e   $	��500��6u    � WindowStartupLocation$ CenterScreen=�6�   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgvAlarmWarning��5      $��0��6*      CanUserReorderColumns .     CanUserSortColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5        Com_SignalName,  &System.Windows.Controls.DataGridColumn   Header# �  6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6m   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6O   �� *	MNameUnit+6w     5      -   5        	colLL�� 	 labLL# �  5   4   $
 0.5* 6p   
 6U   �� *MValLL+6p     5      -   5        colL��  labL# �  5   3   $
 0.5* 6m   
 6S   �� *MValL+6m     5      -   5        colH��  labH# �  5   3   $
 0.5* 6m   
 6S   �� *MValH+6m     5      -   5        	colHH�� 	 labHH# �  5   4   $
 0.5* 6p   
 6U   �� *MValHH+6p     5       
 labMin# �  6%   $
 0.5* 6c   	 . 
 6G   �� *	MValMin+6p     5       
 labMax# �  6%   $
 0.5* 6c   	 . 
 6G   �� *	MValMax+6p   5      � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5       $
��Auto �6#   .+�5"      �� -   5         ��Name$ 	btnCommun��5"      $��0��6(     	btnCommun#���  68   6^   �� 5#      -   5        $ btnOK��5#      $��2��6$    		 btnOK#��� 	 64   6V   �� 5$      -	   5        $ 	btnCancel��5$      $��4��6(    
 	btnCancel#��� 
 68   6^   5%      5&      5'      