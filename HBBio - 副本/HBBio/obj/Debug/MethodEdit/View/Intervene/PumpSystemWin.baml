   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   $    HBBio.MethodEdit.PumpSystemWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   *localclr-namespace:HBBio.MethodEdit  5   	   %shareclr-namespace:HBBio.Share  5   	   -   5           M_PumpSystemWin  � Title#  �   5
   	    � SizeToContent$ WidthAndHeight=�63   $	��500��6Y   ���#EEF5FD J� Opacity$	 0.9Q�6}    � AllowsTransparency .  � WindowStyle$
 None=�6�    � 	Resources 5       �� 
TargetType��%E   '��       �� 5   
   " ��6   .�5      -   5        5      5      .��5      �� ���	#FF268DE4$��2q�6%    �� CornerRadius$ 5��69   ._�5   
   � -   5         �Name$
 grid��5      $��2q�6   	 � RowDefinitions	 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    .+�5      � $��0��6   ���	#FF9DBBCD
 � ColumnDefinitions
 5      �� 5      $
��Auto �6'   �� 5      $��5 �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5!      �� $��0��6   $��25��6(   $��25��63   $-��'/Bio-LabChrom;component/Image/hblog.ico��6?   �� 5"      $��2��6   #���   6,   $
��Bold"�6U   �� 5#      -   5         ��Name$ btnClose��5#      $��4��6+     closeBtnStyle#��[ 6;   6b   5$      � 5%      $��2��6   	 5&      �� 5'      $
��Auto �6$   �� 5(      $��5 �6$   �� 5)      $
��Auto �6$   
 5+      �� 5,      $��* �6'   �� 5-      $��10 �6'   �� 5.      $
��Auto �6'   �� 5/      $��10 �6'   �� 50      $
��Auto �6'   �� 51      $��10 �6'   �� 52      $
��Auto �6'   .+�54      �� -   5         ��Name$ labFlow��54      $��0��6-   $��0��6:   $��7��6J     M_SystemFlow#���  6^   �� 55      -   5         ��Name$ 
sliderFlow��55      $��2��6-   $��0��6:   $  Xceed.Wpf.Toolkit.DoubleUpDown  56      -   5          Name$ 
doubleFlow��56   $   $��2��68   $��2��6E   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6U   ��  �� ElementName$ 
sliderFlow�� �� Path$ Value� �� Mode$ TwoWay=��� 57      $��2��6   $��4��6)   $�� ��69   �� 58      -   5        $ btnFlow��58      $��2��6*   $��6��67     M_SetFlowRate#���  6G   6q   59      � 5:      $��4��6   	 5;      �� 5<      $
��Auto �6$   �� 5=      $��5 �6$   �� 5>      $
��Auto �6$   �� 5?      $��5 �6$   �� 5@      $
��Auto �6$   �� 5A      $��5 �6$   �� 5B      $
��Auto �6$   
 5D      �� 5E      $��* �6'   �� 5F      $��10 �6'   �� 5G      $
��Auto �6'   �� 5H      $��10 �6'   �� 5I      $
��Auto �6'   �� 5J      $��10 �6'   �� 5K      $
��Auto �6'   .+�5M      �� -	   5        $ labConc��5M      $��0��6-   $��0��6:   $��7��6J    
 M_Conc#���  6^   �� 5N      -
   5        $ sliderB��5N      $��2��6*   $��0��67    � Maximum$	 100Q�6G     5O      -   5        $ doubleB��5O   $   $��2��65   $��2��6B      Maximum$	 100X�6R    6`   �� $ sliderB��$ Value�$ TwoWay=�y�6�   �� $ sliderB��$ 
Visibility��� 5P      -   5        $ labBPer��5P      $��2��6-   $��4��6:   $��%B��6J   y�6T   �� $ sliderB��$ 
Visibility��� 5Q      -   5        $ btnPer��5Q      $��2��6)   $��6��66     M_SetFlowPer#���  6F   y�6o   �� $ sliderB��$ 
Visibility�6�   �� 5R      -   5        $ sliderC��5R      $��4��6*   $��0��67   $	 100Q�6�   y�6G   �� $ 	btnStartC��$ 
Visibility�6�     5S      -   5        $ doubleC��5S   $   $��4��65   $��2��6B   $	 100X�6R    6`   �� $ sliderC��$ Value�$ TwoWay=�y�6�   �� $ sliderC��$ 
Visibility��� 5T      -   5        $ labCPer��5T      $��4��6-   $��4��6:   $��%C��6J   y�6T   �� $ sliderC��$ 
Visibility��� 5U      -   5        $ sliderD��5U      $��6��6*   $��0��67   $	 100Q�6�   y�6G   �� $ 	btnStartD��$ 
Visibility�6�     5V      -   5        $ doubleD��5V   $   $��6��65   $��2��6B   $	 100X�6R    6`   �� $ sliderD��$ Value�$ TwoWay=�y�6�   �� $ sliderD��$ 
Visibility��� 5W      -   5        $ labDPer��5W      $��26��6-   $��4��6;   $��%D��6K   y�6U   �� $ sliderD��$ 
Visibility�5X      5Y      5Z      5[      