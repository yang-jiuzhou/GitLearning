   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   %    HBBio.Administration.UserPwdWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	   -   5           A_UserPwdWin  � Title#  �   5   	    � SizeToContent$ WidthAndHeight=�60   $	��350��6V    � WindowStartupLocation$ CenterScreen=�6e   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��50 �6#   �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5       $��10 �6    �� 5!      $
��Auto �6    �� 5"      $��5 �6    �� 5#      $
��Auto �6    �� 5$      $��5 �6    �� 5%      $
��Auto �6    �� 5&      $��5 �6    �� 5'      $
��Auto �6    .+�5)      �� -   5         ��Name$ labName��5)      $��0��6)   $��0��66   $��2��6F     A_UserName1#���  6Z   �� 5*      -   5         ��Name$ txtName��5*      $��0��6'   $��3��64    �� 
IsReadOnly . 6D   �� 5+      -   5         ��Name$ 
chboxLogin��5+      $��2��6+   $��0��68   $��2��6H     
A_PwdLogin#���  6\   �� 5,      $��4��6   $��1��6%     	A_PwdOld1#���  65   G� 5-      -   5        	 G�Name$	 pwdOld��5-      $��4��6*   $��3��67   {�6G   �� 
 �� ElementName$
 
chboxLogin�� �� Path$ 	IsChecked��� 5.      $��6��6   $��1��6%     A_PwdLogin1#���  65   G� 5/      -   5        $		 pwd��5/      $��6��6'   $��3��64   {�6D   �� $
 
chboxLogin��$ 	IsChecked��� 50      $��8��6   $��1��6%     A_PwdConfirm1#���  65   G� 51      -   5        $	 
pwdConfirm��51      $��8��6.   $��3��6;   {�6K   �� $
 
chboxLogin��$ 	IsChecked��� 52      -   5        $ 	chboxSign��52      $��10��6*   $��0��68   $��2��6H     	A_PwdSign#���  6\   �� 53      $��12��6   $��1��6&   #���  66   G� 54      -	   5        $	 
pwdSignOld��54      $��12��6.   $��3��6<   {�6L   �� $
 	chboxSign��$ 	IsChecked��� 55      $��14��6   $��1��6'     
A_PwdSign1#���  67   G� 56      -
   5        $	 pwdSign��56      $��14��6+   $��3��69   {�6I   �� $
 	chboxSign��$ 	IsChecked��� 57      $��16��6   $��1��6&     A_PwdSignConfirm1#���  66   G� 58      -   5        $	 pwdSignConfirm��58      $��16��62   $��3��6@   {�6P   �� $
 	chboxSign��$ 	IsChecked�59      � 5:   
   $��2��6    5;      �� 5<      $��* �6#   �� 5=      $
��Auto �6#   �� 5>      $��10 �6#   �� 5?      $
��Auto �6#   .+�5A      �� -   5         ��Name$ btnOK��5A      $��1��6$    		 btnOK#��� 	 64   6V   �� 5B      -   5        $ 	btnCancel��5B      $��3��6(    
 	btnCancel#��� 
 68   6^   5C      5D      5E      