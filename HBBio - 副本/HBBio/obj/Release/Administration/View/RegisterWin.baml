   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  &     HBBio.Administration.RegisterWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration        A_RegisteWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��400�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* ��� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ labUserName��$��0��$��0��  A_UserName1#���  �� -    ��Name$ txtName��$��2��$��0�� �� PreferredImeState$	 Off=��� -   $ labUserNote��$��0��$��2��  labNote1#���  �� -   $ txtUserNote��$��2��$��2�� �� TextWrapping$
 Wrap=�	 �� AcceptsReturn	 . "
 �� VerticalScrollBarVisibility$

 Auto=�$��80���� -   $ labPermissionName��$��0��$��4��  A_PermissionName1#���  �� -   $ txtPermission��$��2��$��4�� �� 
IsReadOnly .   A_PermissionAdmin#���  �� -   $ labPermissionNote��$��0��$��6��#���  �� -   $ txtPermissionNote��$��2��$��6��  A_AllPermission#���  $
 Wrap=�	 . $

 Auto=�$��80���� $��0��$��8��  A_PwdLogin1#���  G� -	    G�Name$ pwdPwd��$��2��$��8�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -
      Command$ ApplicationCommands.Paste���� $��0��$��10��  A_PwdConfirm1#���  G� -   $ pwdPwdConfirm��$��2��$��10��   -   $ ApplicationCommands.Paste���� $��0��$��12��  
A_PwdSign1#���  G� -   $ 
pwdPwdSign��$��2��$��12��   -   $ ApplicationCommands.Paste���� $��0��$��14�� 	 A_PwdSignConfirm1#��� 	 G� -   $ pwdPwdSignConfirm��$��2��$��14��   -   $ ApplicationCommands.Paste��� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	
 btnOK#��� 
 �� -   $ 	btnCancel��$��3��  	btnCancel#���  