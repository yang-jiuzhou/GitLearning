   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  %    HBBio.Administration.UserPwdWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration     -      A_UserPwdWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��350�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $��50 ��� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ labName��$��0��$��0��$��2��  A_UserName1#���  �� -    ��Name$ txtName��$��0��$��3�� �� 
IsReadOnly . �� -    ��Name$ 
chboxLogin��$��2��$��0��$��2��  
A_PwdLogin#���  �� $��4��$��1��  	A_PwdOld1#���  G� -   	 G�Name$	 pwdOld��$��4��$��3��{��� 
 �� ElementName$
 
chboxLogin�� �� Path$ 	IsChecked��� $��6��$��1��  A_PwdLogin1#���  G� -   $		 pwd��$��6��$��3��{��� $
 
chboxLogin��$ 	IsChecked��� $��8��$��1��  A_PwdConfirm1#���  G� -   $	 
pwdConfirm��$��8��$��3��{��� $
 
chboxLogin��$ 	IsChecked��� -   $ 	chboxSign��$��10��$��0��$��2��  	A_PwdSign#���  �� $��12��$��1��#���  G� -	   $	 
pwdSignOld��$��12��$��3��{��� $
 	chboxSign��$ 	IsChecked��� $��14��$��1��  
A_PwdSign1#���  G� -
   $	 pwdSign��$��14��$��3��{��� $
 	chboxSign��$ 	IsChecked��� $��16��$��1��  A_PwdSignConfirm1#���  G� -   $	 pwdSignConfirm��$��16��$��3��{��� $
 	chboxSign��$ 	IsChecked�� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 		 btnOK#��� 	 �� -   $ 	btnCancel��$��3�� 
 	btnCancel#��� 
 