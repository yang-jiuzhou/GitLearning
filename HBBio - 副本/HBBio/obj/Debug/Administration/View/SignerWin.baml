   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   $    HBBio.Administration.SignerWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	      A_SignerWin  � Title#  �   5   	    � SizeToContent$ WidthAndHeight=�6/   $	��300��6U    � WindowStartupLocation$ CenterOwner=�6d   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    .+�5      �� -   5         ��Name$ 	labSigner��5      $��0��6+   $��0��68     A_SignerName1#���  6H   �� 5      -   5         ��Name$ 	txtSigner��5      $��0��6)   $��2��66    �� 
IsReadOnly . 6F   �� 5      -   5        $ labSignerPwd��5      $��2��6.   $��0��6;     
A_PwdSign1#���  6K   G� 5      -   5         G�Name$ pwdSignerPwd��5      $��2��60   $��2��6=   �� 5       -   5        $ labNote��5       $��4��6)   $��0��66     labNote1#���  6F   �� 5!      -   5        $ txtNote��5!      $��4��6'   $��2��64   	 �� TextWrapping$
	 Wrap=�6D   
 �� AcceptsReturn
 . " �� VerticalScrollBarVisibility$
 Auto=�6m   $��80��6�   5"      � 5#   
   $��2��6    5$      �� 5%      $��* �6#   �� 5&      $
��Auto �6#   �� 5'      $��10 �6#   �� 5(      $
��Auto �6#   .+�5*      �� -   5         ��Name$ btnOK��5*      $��1��6$    	 btnOK#���  64   6V   �� 5+      -   5        $ 	btnCancel��5+      $��3��6(     	btnCancel#���  68   6^   5,      5-      5.      