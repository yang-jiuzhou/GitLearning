   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   %    HBBio.Administration.UserAddWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	   -   5           A_UserAddWin  � Title#  �   5   	    � SizeToContent$ WidthAndHeight=�60   $	��350��6V    � WindowStartupLocation$ CenterOwner=�6e   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � -   5         �Name$
 grid��5      $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5       $
��Auto �6    �� 5!      $��10 �6    �� 5"      $
��Auto �6    .+�5$      �� -   5         ��Name$ labName��5$      $��0��6)   $��0��69     A_UserName1#���  6F   �� 5%      -   5         ��Name$ txtName��5%      $��2��6'   $��0��67   ��6D   �� *	MUserName+�� 5&      -   5        $ labPermission��5&      $��0��6/   $��2��6?     A_PermissionName1#���  6L   �� 5'      -   5         ��Name$ cboxPermission��5'      $��2��6/   $��2��6?   	 �� SelectedValuePath$		 MID��6L   
 �� DisplayMemberPath$
 MName��6d    �� SelectedValue 6~   �� *MPermissionName+�� 5(      -   5        $ labNote��5(      $��0��6)   $��4��69     labNote1#���  6F   �� 5)      -   5        $ txtNote��5)      $��2��6'   $��4��67    �� TextWrapping$
 Wrap=�6D    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6m   $��80��6�   ��6�   �� *MNote+�� 5*      $��0��6   $��6��6(     A_PwdLogin1#���  65   G� 5+      -	   5         G�Name$ pwdPwd��5+      $��2��6*   $��6��6:    J� CommandBindings 5,      )  #System.Windows.Input.CommandBinding  5-      -
   5           Command$ ApplicationCommands.Paste��5-   %   6l   5/      �� 50      $��0��6   $��8��6(     A_PwdConfirm1#���  65   G� 51      -   5        $ pwdPwdConfirm��51      $��2��61   $��8��6A    52        53      -   5        $ ApplicationCommands.Paste��53   %   6l   55      �� 56      $��0��6   $��10��6(     
A_PwdSign1#���  66   G� 57      -   5        $ 
pwdPwdSign��57      $��2��6.   $��10��6>    58        59      -   5        $ ApplicationCommands.Paste��59   %   6l   5;      �� 5<      $��0��6   $��12��6(     A_PwdSignConfirm1#���  66   G� 5=      -   5        $ pwdPwdSignConfirm��5=      $��2��65   $��12��6E    5>        5?      -   5        $ ApplicationCommands.Paste��5?   %   6l   5A      5B      � 5C   
   $��2��6    5D      �� 5E      $��* �6#   �� 5F      $
��Auto �6#   �� 5G      $��10 �6#   �� 5H      $
��Auto �6#   .+�5J      �� -   5         ��Name$ btnOK��5J      $��1��6$    	 btnOK#���  64   6V   �� 5K      -   5        $ 	btnCancel��5K      $��3��6(    	 	btnCancel#��� 	 68   6^   5L      5M      5N      