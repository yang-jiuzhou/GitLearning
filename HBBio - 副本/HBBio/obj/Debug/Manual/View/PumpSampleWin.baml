   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	        HBBio.Manual.PumpSampleWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   &localclr-namespace:HBBio.Manual  5   	   %shareclr-namespace:HBBio.Share  5   	   -   5           M_PumpSampleWin  � Title#  �   5
   	    � SizeToContent$ WidthAndHeight=�63   $	��350��6Y   ���#EEF5FD J� Opacity$	 0.9Q�6}    � AllowsTransparency .  � WindowStyle$
 None=�6�   .��5      �� ���	#FF268DE4$��2q�6%    �� CornerRadius$ 5��69   ._�5   
   � -   5         �Name$
 grid��5      $��2q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      � $��0��6   ���	#FF9DBBCD � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��5 �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��25��6(   $��25��63   $-��'/Bio-LabChrom;component/Image/hblog.ico��6?   �� 5      $��2��6   #���   6,   $
��Bold"�6U   �� 5      -   5        	 ��Name$	 btnClose��5      $��4��6+     closeBtnStyle#��[ 6;   6b   5       � 5!      $��2��6    5"      �� 5#      $
��Auto �6$   �� 5$      $��5 �6$   �� 5%      $
��Auto �6$    5'      �� 5(      $��* �6'   �� 5)      $��10 �6'   �� 5*      $
��Auto �6'   �� 5+      $��10 �6'   �� 5,      $
��Auto �6'   �� 5-      $��10 �6'   �� 5.      $
��Auto �6'   .+�50      �� -   5        
 ��Name$
 labFlow��50      $��0��6-   $��0��6:   $��7��6J     M_SampleFlow#���  6^   �� 51      -   5         ��Name$ 
sliderFlow��51      $��2��6-   $��0��6:    � Value 6J   �� *MFlow+6b   $  Xceed.Wpf.Toolkit.DoubleUpDown  52      -   5          Name$ 
doubleFlow��52   $   $��2��68   $��2��6E   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6U   ��  �� ElementName$ 
sliderFlow�� �� Path$ Value� �� Mode$ TwoWay=��� 53      $��2��6   $��4��6)   $�� ��69   �� 54      -   5        $	 btnFlow��54      $��2��6*   $��6��67     M_SetFlowRate#���  6G   6q   55      � 56      $��4��6    57      �� 58      $
��Auto �6$   �� 59      $��5 �6$   �� 5:      $
��Auto �6$    5<      �� 5=      $
��Auto �6'   �� 5>      $��10 �6'   �� 5?      $
��Auto �6'   �� 5@      $��* �6'   �� 5A      $
��Auto �6'   �� 5B      $��10 �6'   �� 5C      $
��Auto �6'   .+�5E      �� $��0��6   $��0��6)   $��7��69     labWash#���  6M   �� 5F      -   5         ��Name$ cboxWash��5F      $��2��6-   $��0��6:   6J   �� 5G      -	   5        $
 cbox��5G      $��2��6)   $��2��66   �� 5H      -
   5        $	 btnStart��5H      $��2��6+   $��4��68     M_StartWash#���  6H   6p   �� 5I      -   5        $	 btnStop��5I      $��2��6*   $��6��67     
M_StopWash#���  6G   6n   5J      5K      5L      5M      