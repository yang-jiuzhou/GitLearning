   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  )clr-namespace:HBBio.ShareHBBio.Share  &     HBBio.Administration.UserEditWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration     +shareclr-namespace:HBBio.Share     -      A_UserEditWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��350�� � WindowStartupLocation$ CenterOwner=� � 	Resources $   HBBio.Share.BoolFalseConverter  BoolFalseConverter��%   &	         .��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� -    �Name$
 grid��$��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ labName��$��0��$��0��  A_UserName1#���  �� -    ��Name$ txtName��$��0��$��2��$��3��	 �� 
IsReadOnly	 . ���� *	MUserName+�� -   $ labPermission��$��2��$��0��  A_PermissionName1#���  �� -   
 ��Name$
 cboxPermission��$��2��$��2��$��3�� �� SelectedValuePath$	 MID�� �� DisplayMemberPath$ MName�� �� SelectedValue �� *MPermissionName+�� -   $ labNote��$��4��$��0��  labNote1#���  �� -   $ txtNote��$��4��$��2��$��3�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�$��80������ *MNote+�� -	   $ 	labStatic��$��6��$��0��  
labStatus1#���  	� -
    	�Name$ 
rbtnActive��$��6��$��2��  A_Active#���   d� 	IsChecked �� *
MEnabled+	� -   $ rbtnDisActive��$��6��$��4��  A_DisActive#���   �� *
MEnabled+ �� 	Converter# [ �� -    ��Name$ chboxUnlock��$��8��$��2��  A_Unlock#���  {��� *MLock+ �� *	MUnlock+�� -   $ labPwd��$��10��$��0�� 	 A_PwdLogin1#��� 	 �� -   $ chboxResetPwdLogin��$��10��$��2�� 
 labReset#��� 
  �� *	MResetPwd+�� -   $ txtPwdLogin��$��10��$��4��{���  �� ElementName$ chboxResetPwdLogin�� �� Path$ 	IsChecked����� *MResetPwdStr+�� -   $ 
labPwdSign��$��12��$��0��  
A_PwdSign1#���  �� -   $ chboxResetPwdSign��$��12��$��2��#��� 
  �� *MResetPwdSign+�� -   $ 
txtPwdSign��$��12��$��4��{��� $ chboxResetPwdSign��$ 	IsChecked����� *MResetPwdSignStr+� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3��  	btnCancel#���  