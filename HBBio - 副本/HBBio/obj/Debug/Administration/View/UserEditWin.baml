   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   &     HBBio.Administration.UserEditWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           A_UserEditWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�61   $	��350��6W    � WindowStartupLocation$ CenterOwner=�6f    � 	Resources 5
      $   HBBio.Share.BoolFalseConverter  BoolFalseConverter��%   &	         5   
   6@   5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � -   5         �Name$
 grid��5      $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5       $
��Auto �6    �� 5!      $��10 �6    �� 5"      $
��Auto �6    �� 5#      $��10 �6    �� 5$      $
��Auto �6    �� 5%      $��10 �6    �� 5&      $
��Auto �6    �� 5'      $��10 �6    �� 5(      $
��Auto �6    .+�5*      �� -   5         ��Name$ labName��5*      $��0��6)   $��0��66     A_UserName1#���  6F   �� 5+      -   5         ��Name$ txtName��5+      $��0��6'   $��2��64   $��3��6D   	 �� 
IsReadOnly	 . ��6j   �� *	MUserName+�� 5,      -   5        $ labPermission��5,      $��2��6/   $��0��6<     A_PermissionName1#���  6L   �� 5-      -   5        
 ��Name$
 cboxPermission��5-      $��2��6/   $��2��6<   $��3��6L    �� SelectedValuePath$	 MID��6`    �� DisplayMemberPath$ MName��6x    �� SelectedValue 6�   �� *MPermissionName+�� 5.      -   5        $ labNote��5.      $��4��6)   $��0��66     labNote1#���  6F   �� 5/      -   5        $ txtNote��5/      $��4��6'   $��2��64   $��3��6D    �� TextWrapping$
 Wrap=�6X    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6�   $��80��6�   ��6�   �� *MNote+�� 50      -	   5        $ 	labStatic��50      $��6��6+   $��0��68     
labStatus1#���  6H   	� 51      -
   5         	�Name$ 
rbtnActive��51      $��6��6.   $��2��6;     A_Active#���  6K    d� 	IsChecked 6p   �� *
MEnabled+	� 52      -   5        $ rbtnDisActive��52      $��6��61   $��4��6>     A_DisActive#���  6N    6v   �� *
MEnabled+ �� 	Converter# [ �� 53      -   5         ��Name$ chboxUnlock��53      $��8��6,   $��2��69     A_Unlock#���  6I   {�6n   �� *MLock+ 6�   �� *	MUnlock+�� 54      -   5        $ labPwd��54      $��10��6(   $��0��66    	 A_PwdLogin1#��� 	 6F   �� 55      -   5        $ chboxResetPwdLogin��55      $��10��63   $��2��6A    
 labReset#��� 
 6Q    6v   �� *	MResetPwd+�� 56      -   5        $ txtPwdLogin��56      $��10��6+   $��4��6:   {�6J   ��  �� ElementName$ chboxResetPwdLogin�� �� Path$ 	IsChecked���6�   �� *MResetPwdStr+�� 57      -   5        $ 
labPwdSign��57      $��12��6,   $��0��6:     
A_PwdSign1#���  6J   �� 58      -   5        $ chboxResetPwdSign��58      $��12��62   $��2��6@   #��� 
 6P    6u   �� *MResetPwdSign+�� 59      -   5        $ 
txtPwdSign��59      $��12��6+   $��4��69   {�6I   �� $ chboxResetPwdSign��$ 	IsChecked���6�   �� *MResetPwdSignStr+5:      � 5;   
   $��2��6    5<      �� 5=      $��* �6#   �� 5>      $
��Auto �6#   �� 5?      $��10 �6#   �� 5@      $
��Auto �6#   .+�5B      �� -   5         ��Name$ btnOK��5B      $��1��6$    	 btnOK#���  64   6V   �� 5C      -   5        $ 	btnCancel��5C      $��3��6(     	btnCancel#���  68   6^   5D      5E      5F      