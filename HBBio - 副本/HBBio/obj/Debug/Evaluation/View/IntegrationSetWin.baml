   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.EvaluationHBBio.Evaluation  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   (    "HBBio.Evaluation.IntegrationSetWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   *localclr-namespace:HBBio.Evaluation  5   	   %shareclr-namespace:HBBio.Share  5   	   -   5           E_IntegrationSetWin  � Title#  �   5
   	    � SizeToContent$ Height=�67   $	��450��6U    � WindowStartupLocation$ CenterOwner=�6a   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   .@�5      ��   E_IntegrationResult#���  6   .��5      � -   5         �Name$ 	wrapPanel��5      5      �� 5        E_IntegrationCondition#���  6   .��5      � $��5q�6    5      �� 5      $
��Auto �6(   �� 5      $��10 �6(   �� 5      $
��Auto �6(   �� 5      $��10 �6(   �� 5      $
��Auto �6(    � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5       $��10 �6+   �� 5!      $
��Auto �6+   .+�5#      �� -   5         ��Name$ 	labFilter��5#       $��0��63   $��0��6@     	E_Filter1#���  6P   � 5$      $��0��6   $��2��6(    5%      �� 5&      $
��Auto �6,   �� 5'      $��10 �6,   �� 5(      $
��Auto �6,   �� 5)      $��10 �6,   �� 5*      $
��Auto �6,   �� 5+      $��10 �6,   �� 5,      $
��Auto �6,    5.      �� 5/      $
��Auto �6/   �� 50      $��5 �6/   �� 51      $
��Auto �6/   �� 52      $��15 �6/   �� 53      $
��Auto �6/   �� 54      $��5 �6/   �� 55      $
��Auto �6/   .+�57      	� -   5         	�Name$ rbtnMix��57   &   $��0��67   $��0��6D   $��3��6T     E_FilterMin#���  6h    d� 	IsChecked 6�   �� *MIsMin+	� 58      -   5        $ 	rbtnCount��58   &   $��0��69   $��4��6F   $��3��6V     E_FilterCount#���  6j    6�   �� *
MIsCount+�� 59      -   5        $ labMinHeight��59   $   $��2��6:   $��0��6G     E_MinHeight1#���  6W   $  Xceed.Wpf.Toolkit.DoubleUpDown  5:      -   5        	  Name$	 doubleMinHeight��5:   ,   $��2��6E   $��2��6R   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Value
 6b   �� *
MMinHeight+�� 5;      -   5        $ labPeakCount��5;   $   $��2��6:   $��4��6G     E_PeakCount1#���  6W   %  Xceed.Wpf.Toolkit.IntegerUpDown  5<      -	   5          Name$ intPeakCount��5<   -   $��2��6C   $��6��6P   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6`   �� *
MPeakCount+�� 5=      -
   5        $ 
labMinArea��5=   $   $��4��68   $��0��6E     
E_MinArea1#���  6U     5>      -   5        $	 doubleMinArea��5>   ,   $��4��6C   $��2��6P   
 6`   �� *
MMinArea+�� 5?      -   5        $ labMinHalfWidth��5?   $   $��6��6=   $��0��6J    	 E_MinHalfWidth1#��� 	 6Z     5@      -   5        $	 doubleMinHalfWidth��5@   ,   $��6��6H   $��2��6U   
 6e   �� *	MMinWidth+5A      �� 5B      $��2��6    $��0��6-    
 E_Original1#��� 
 6=   �� 5C      -   5         ��Name$ cboxOriginal��5C      $��2��65   $��2��6B   �� 5D      $��4��6    $��0��6-     E_ColHeight1#���  6=   �� 5E      $��4��6!   $��2��6.    �� Orientation$ 
Horizontal=�6>   .+�5F        -   5        $	 doubleCH��5F   ,   
 6>   �� *MCH+�� 5G      $
��10,0q�6$      HBBio.Share.DlyBase   SC_LENGTHUNIT#��Z 62   5H      5I      5J      5K      � 5L   
   $��2��6    5M      �� 5N      $��* �6#   �� 5O      $
��Auto �6#   �� 5P      $��10 �6#   �� 5Q      $
��Auto �6#   .+�5S      �� -   5         ��Name$ btnOK��5S      $��1��6$    	 btnOK#���  64   6V   �� 5T      -   5        $ 	btnCancel��5T      $��3��6(     	btnCancel#���  68   6^   5U      5V      5W      