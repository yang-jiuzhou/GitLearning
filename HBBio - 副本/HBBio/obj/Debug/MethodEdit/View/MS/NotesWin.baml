   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	       HBBio.MethodEdit.NotesWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.MethodEdit     5   	      ME_NotesWin  � Title#  �   5   	   $	��369��6/   $	��500��6<   $	��369��6H   $	��500��6X    � WindowStartupLocation$ CenterOwner=�6g   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6!     	ME_Notes1#���  61   �� 5   
   -   5         ��Name$ txtNote��5      $��2��6#   $��0��60   $��5��6@    �� TextWrapping$
 Wrap=�6T    �� AcceptsReturn . " �� VerticalScrollBarVisibility$ Visible=�6}   $
��Auto��6�   $	��Top=�6�   �� 5   
   -   5         ��Name$ btnOK��5      $��4��6    $��2��6-    	 btnOK#���  6=   6_   �� 5   
   -   5        $ 	btnCancel��5      $��4��6$   $��4��61     	btnCancel#���  6A   6g   5      5      