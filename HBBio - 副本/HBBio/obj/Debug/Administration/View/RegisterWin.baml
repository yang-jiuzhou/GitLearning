   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   &     HBBio.Administration.RegisterWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	      A_RegisteWin  � Title#  �   5   	    � SizeToContent$ WidthAndHeight=�60   $	��400��6V    � WindowStartupLocation$ CenterOwner=�6e   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $
��Auto �6#    5      �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5       $��5 �6    �� 5!      $
��Auto �6    �� 5"      $��5 �6    �� 5#      $
��Auto �6    �� 5$      $��5 �6    �� 5%      $
��Auto �6    �� 5&      $��5 �6    �� 5'      $
��Auto �6    .+�5)      �� -   5         ��Name$ labUserName��5)      $��0��6-   $��0��6=     A_UserName1#���  6J   �� 5*      -   5         ��Name$ txtName��5*      $��2��6'   $��0��67    �� PreferredImeState$	 Off=�6D   �� 5+      -   5        $ labUserNote��5+      $��0��6-   $��2��6=     labNote1#���  6J   �� 5,      -   5        $ txtUserNote��5,      $��2��6+   $��2��6;    �� TextWrapping$
 Wrap=�6H   	 �� AcceptsReturn	 . "
 �� VerticalScrollBarVisibility$

 Auto=�6q   $��80��6�   �� 5-      -   5        $ labPermissionName��5-      $��0��63   $��4��6C     A_PermissionName1#���  6P   �� 5.      -   5        $ txtPermission��5.      $��2��6-   $��4��6=    �� 
IsReadOnly .   A_PermissionAdmin#���  6\   �� 5/      -   5        $ labPermissionNote��5/      $��0��63   $��6��6C   #���  6P   �� 50      -   5        $ txtPermissionNote��50      $��2��61   $��6��6A     A_AllPermission#���  6N   $
 Wrap=�6w   	 . $

 Auto=�6�   $��80��6�   �� 51      $��0��6   $��8��6(     A_PwdLogin1#���  65   G� 52      -	   5         G�Name$ pwdPwd��52      $��2��6*   $��8��6:    J� CommandBindings 53      )  #System.Windows.Input.CommandBinding  54      -
   5           Command$ ApplicationCommands.Paste��54   %   6l   56      �� 57      $��0��6   $��10��6(     A_PwdConfirm1#���  66   G� 58      -   5        $ pwdPwdConfirm��58      $��2��61   $��10��6A    59        5:      -   5        $ ApplicationCommands.Paste��5:   %   6l   5<      �� 5=      $��0��6   $��12��6(     
A_PwdSign1#���  66   G� 5>      -   5        $ 
pwdPwdSign��5>      $��2��6.   $��12��6>    5?        5@      -   5        $ ApplicationCommands.Paste��5@   %   6l   5B      �� 5C      $��0��6   $��14��6(    	 A_PwdSignConfirm1#��� 	 66   G� 5D      -   5        $ pwdPwdSignConfirm��5D      $��2��65   $��14��6E    5E        5F      -   5        $ ApplicationCommands.Paste��5F   %   6l   5H      5I      � 5J   
   $��2��6    5K      �� 5L      $��* �6#   �� 5M      $
��Auto �6#   �� 5N      $��10 �6#   �� 5O      $
��Auto �6#   .+�5Q      �� -   5         ��Name$ btnOK��5Q      $��1��6$    	
 btnOK#��� 
 64   6V   �� 5R      -   5        $ 	btnCancel��5R      $��3��6(     	btnCancel#���  68   6^   5S      5T      5U      