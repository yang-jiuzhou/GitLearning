   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  %    HBBio.Administration.UserAddWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration     -      A_UserAddWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��350�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� -    �Name$
 grid��$��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ labName��$��0��$��0��  A_UserName1#���  �� -    ��Name$ txtName��$��2��$��0������ *	MUserName+�� -   $ labPermission��$��0��$��2��  A_PermissionName1#���  �� -    ��Name$ cboxPermission��$��2��$��2��	 �� SelectedValuePath$		 MID��
 �� DisplayMemberPath$
 MName�� �� SelectedValue �� *MPermissionName+�� -   $ labNote��$��0��$��4��  labNote1#���  �� -   $ txtNote��$��2��$��4�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�$��80������ *MNote+�� $��0��$��6��  A_PwdLogin1#���  G� -	    G�Name$ pwdPwd��$��2��$��6�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -
      Command$ ApplicationCommands.Paste���� $��0��$��8��  A_PwdConfirm1#���  G� -   $ pwdPwdConfirm��$��2��$��8��   -   $ ApplicationCommands.Paste���� $��0��$��10��  
A_PwdSign1#���  G� -   $ 
pwdPwdSign��$��2��$��10��   -   $ ApplicationCommands.Paste���� $��0��$��12��  A_PwdSignConfirm1#���  G� -   $ pwdPwdSignConfirm��$��2��$��12��   -   $ ApplicationCommands.Paste��� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3�� 	 	btnCancel#��� 	 