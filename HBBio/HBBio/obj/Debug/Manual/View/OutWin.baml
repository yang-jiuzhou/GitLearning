   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  5   	       HBBio.Manual.OutWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   &localclr-namespace:HBBio.Manual  5   	   -   5           M_OutWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�6,   ���#EEF5FD J� Opacity$	 0.8Q�6g    � AllowsTransparency .  � WindowStyle$
 None=�6�   .��5
      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� -   5         ��Name$ title��5      $��0��6'   $
��Bold"�67   �� 5      -   5         ��Name$ btnClose��5        closeBtnStyle#��[ 6'   $��2��6N   6^   5      �� 5   
   $��2��6     M_OutWin_SingleSelect#���  6    .��5      � $��5q�6    5      �� 5       $
��Auto �6'   �� 5!      $��10 �6'   �� 5"      $
��Auto �6'   �� 5#      $��15 �6'   �� 5$      $
��Auto �6'   .+�5&      	� -   5        	 	�Name$	 rbtnRealSingle��5&      $��0��66     labReal#���  6F   
 d� 	IsChecked$

 TrueY�6j   	� 5'      -   5        $	 rbtnDelaySingle��5'      $��2��67     labDelay#���  6G   � 5(      -   5         �Name$
 grid��5(      $��4��6%   5)      5*      �� 5+   
   -   5         ��Name$ groupMultipleSelect��5+      $��4��60     M_OutWin_MultipleSelect#���  6=   .��5,      � $��5q�6    5-      �� 5.      $
��Auto �6'   �� 5/      $��10 �6'   �� 50      $
��Auto �6'   �� 51      $��15 �6'   �� 52      $
��Auto �6'   �� 53      $��5 �6'   �� 54      $
��Auto �6'   �� 55      $��5 �6'   �� 56      $
��Auto �6'   �� 57      $��5 �6'   �� 58      $
��Auto �6'   �� 59      $��5 �6'   �� 5:      $
��Auto �6'   �� 5;      $��5 �6'   �� 5<      $
��Auto �6'   .+�5>      	� -   5        $	 rbtnRealMultiple��5>      $��0��68   #���  6H   	� 5?      -	   5        $	 rbtnDelayMultiple��5?      $��2��69   #���  6I   �� 5@      $��4��6     M_OutWin_Start1#���  6,   �� 5A      -
   5         ��Name$ 
cboxSelect��5A      $��6��6/   �� 5B      $��8��6     M_OutWin_Vol1#���  6,   $  Xceed.Wpf.Toolkit.DoubleUpDown  5C      -   5          Name$ 	doubleVol��5C   $   $��10��67   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$
 0.01X�6H   �� 5D      -   5        $ btnStart��5D   *   $��12��6    
 btnSet#���  6<   6_   �� 5E      -   5        $ btnStop��5E   *   $��14��6    	 	btnCancel#��� 	 6;   6a   5F      5G      �� 5H   
   $��6��6   .+�5I      �� -   5        $ btnIntervene��5I      $��5q�6+   $��Right=�66    
 M_CollInterveneSet#��� 
 6R   6�   5J      5K      5L      