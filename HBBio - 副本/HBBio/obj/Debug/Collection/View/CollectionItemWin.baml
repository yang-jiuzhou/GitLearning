   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.CollectionHBBio.Collection  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   (    "HBBio.Collection.CollectionItemWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.Collection     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           Coll_CollectionItemWin  � Title#  �   5	   	    � SizeToContent$ Height=�6:   $	��900��6X    � WindowStartupLocation$ CenterScreen=�6d   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � -   5         �Name$
 grid��5      $��0��6    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $
��Auto �6#   �� 5       $��5 �6#   �� 5!      $
��Auto �6#   �� 5"      $��5 �6#   �� 5#      $
��Auto �6#   �� 5$      $��* �6#   .+�5&      .   (HBBio.Collection.CollectionObjectMultiUC  -   5          Name$ ucObjectMulti1��5&   ,   $��0��6D   $��0��6Q   $��12��6a   �� 5'      -   5         ��Name$ labPositionType��5'      $��2��61   $��0��6>     Coll_PositionType1#���  6N   �� 5(      -   5         ��Name$ cboxPositionType��5(      $��2��61   $��2��6>   	 �� DisplayMemberPath$	 MString��6N   
 �� SelectedValuePath$
 MEnum��6j    �� SelectedValue 6�   �� *MPositionType+�� 5)      -   5        $ labPositionStart��5)      $��2��62   $��4��6?     Coll_PositionStart1#���  6O   �� 5*      -   5        $ cboxPositionStart��5*      $��2��62   $��6��6?   $	 MName��6O   $
 MName��6i    6�   �� *MStart+  5+      -   5        $ ucObjectMulti2��5+   ,   $��4��6D   $��0��6Q   $��12��6a   y�6v   �� *
MVisible+5,      � 5-   
   $��2��6    5.      �� 5/      $��* �6#   �� 50      $
��Auto �6#   �� 51      $��10 �6#   �� 52      $
��Auto �6#   .+�54      �� -	   5         ��Name$ btnOK��54      $��1��6$    	 btnOK#���  64   6V   �� 55      -
   5        $ 	btnCancel��55      $��3��6(     	btnCancel#���  68   6^   56      57      58      