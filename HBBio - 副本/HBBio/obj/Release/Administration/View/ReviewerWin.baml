   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  &     HBBio.Administration.ReviewerWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration        A_ReviewerWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��350�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ 	labSigner��$��0��$��0��  A_SignerName1#���  �� -    ��Name$ 	txtSigner��$��0��$��2�� �� 
IsReadOnly . �� -   $ labSignerPwd��$��2��$��0��  
A_PwdSign1#���  G� -    G�Name$ pwdSignerPwd��$��2��$��2���� -   $ labReviewer��$��4��$��0��  A_ReviewerName1#���  �� -   	 ��Name$	 cboxReviewer��$��4��$��2��
 �� SelectedValuePath$	
 MID�� �� DisplayMemberPath$ 	MUserName���� -   $ labReviewerPwd��$��6��$��0��#���  G� -   $ pwdReviewerPwd��$��6��$��2���� -	   $ labNote��$��8��$��0��  labNote1#���  �� -
   $ txtNote��$��8��$��2�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�$��80��� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3��  	btnCancel#���  