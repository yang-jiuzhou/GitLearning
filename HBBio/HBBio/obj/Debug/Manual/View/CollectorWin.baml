   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	       HBBio.Manual.CollectorWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   &localclr-namespace:HBBio.Manual  5   	   %shareclr-namespace:HBBio.Share  5   	   -   5           M_CollectorWin  � Title#  �   5
   	    � SizeToContent$ WidthAndHeight=�62   ���#EEF5FD J� Opacity$	 0.8Q�6m    � AllowsTransparency .  � WindowStyle$
 None=�6�   .��5      �� ���	#FF268DE4$��2q�6%    �� CornerRadius$ 5��69   ._�5   
   � $��2q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    .+�5      � $��0��6   ���	#FF9DBBCD � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��5 �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5       �� $��0��6   $��25��6(   $��25��63   $-��'/Bio-LabChrom;component/Image/hblog.ico��6?   �� 5!      $��2��6   #���   6,   $
��Bold"�6T   �� 5"      -   5         ��Name$ btnClose��5"      $��4��6+     closeBtnStyle#��[ 6;   6b   5#      �� 5$      $��2��6     M_CollIndexStatusShow#���  6$   .��5%      � $��5q�6    5&      �� 5'      $
��Auto �6+   �� 5(      $��10 �6+   �� 5)      $
��Auto �6+   �� 5*      $��15 �6+   �� 5+      $
��Auto �6+   �� 5,      $��10 �6+   �� 5-      $
��Auto �6+   .+�5/      �� $��0��6      M_CollIndex1#���  60   �� 50      -   5        	 ��Name$	 cboxIndexShow��50      $��2��66   $	��100��6F   
 �� DisplayMemberPath$
 MName��6U    �� SelectedValuePath$ MName��6o   {�.   �� SelectedValue 6�   �� *	MIndexGet+6�   �� 51      $��4��6      M_CollStatus1#���  60       HBBio.Share.SwitchButtonUC  52      -   5          Name$ sbtnStatusShow��52   +   $��6��6C   {�.     	IsChecked 6S   �� *
MStatusGet+6t   53      54      �� 55      $��4��6     M_CollIndexStatusSet#���  6$   .��56      � $��5q�6    57      �� 58      $
��Auto �6+   �� 59      $��10 �6+   �� 5:      $
��Auto �6+   �� 5;      $��10 �6+   �� 5<      $
��Auto �6+   �� 5=      $��10 �6+   �� 5>      $
��Auto �6+   �� 5?      $��10 �6+   �� 5@      $
��Auto �6+   �� 5A      $��10 �6+   �� 5B      $
��Auto �6+   �� 5C      $��10 �6+   �� 5D      $
��Auto �6+   �� 5E      $��10 �6+   �� 5F      $
��Auto �6+   .+�5H      	� -   5         	�Name$ rbtnRealSingle��5H   "   $��0��6:     labReal#���  6J    d� 	IsChecked$
 TrueY�6n   	� 5I      -   5        $ rbtnDelaySingle��5I   "   $��2��6;     labDelay#���  6K   �� 5J      -   5         ��Name$ labIndex��5J       $��4��62   #���  6B   �� 5K      -   5        $	 	cboxIndex��5K      $��6��62   $	��100��6B   $
 MName��6Q   $ MName��6k   6�   �� 5L      -	   5        $ 	labStatus��5L       $��8��63   #���  6C     5M      -
   5        $ 
sbtnStatus��5M   +       HBBio.Share.SwitchButtonUC   	MouseDown$ sbtnStatus_MouseDown��6P      
MouseEnter$ sbtnStatus_MouseEnter��6q      
MouseLeave$ sbtnStatus_MouseLeave��6�   $��10��6?   6�      HBBio.Share.IconButton  5N      -   5          Name$ btnFront��5N   '      HBBio.Share.IconButton   Click$ btnFront_Click��6�   $��12��69     btnPrev � ToolTip# �  6J      Image$, &/Bio-LabChrom;component/Image/prev.png��6n   6�     5O      -   5        $ btnBack��5O   '   $ btnBack_Click��6�   $��14��68    	 btnNext# � 	 6I   $, &/Bio-LabChrom;component/Image/back.png��6m   6�   5P      5Q      �� 5R      -   5         ��Name$ groupMultipleSelect��5R      $��6��64    
 M_OutWin_MultipleSelect#��� 
 6A   .��5S      � $��5q�6    5T      �� 5U      $
��Auto �6+   �� 5V      $��10 �6+   �� 5W      $
��Auto �6+   �� 5X      $��15 �6+   �� 5Y      $
��Auto �6+   �� 5Z      $��5 �6+   �� 5[      $
��Auto �6+   �� 5\      $��5 �6+   �� 5]      $
��Auto �6+   �� 5^      $��5 �6+   �� 5_      $
��Auto �6+   �� 5`      $��5 �6+   �� 5a      $
��Auto �6+   �� 5b      $��5 �6+   �� 5c      $
��Auto �6+   .+�5e      	� -   5        $ rbtnRealMultiple��5e   "   $��0��6<   #���  6L   	� 5f      -   5        $ rbtnDelayMultiple��5f   "   $��2��6=   #���  6M   �� 5g      $��4��6      M_OutWin_Start1#���  60   �� 5h      -   5        $	 
cboxSelect��5h      $��6��63   $
 MName��6C   �� 5i      $��8��6      M_OutWin_Vol1#���  60   $  Xceed.Wpf.Toolkit.DoubleUpDown  5j      -   5          Name$ 	doubleVol��5j   (   $��10��6;   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$
 0.01X�6L   �� 5k      -   5        $ btnStart��5k   .   $��12��6    
 btnSet#���  6@   6c   �� 5l      -   5        $ btnStop��5l   .   $��14��6     	btnCancel#���  6?   6e   5m      5n      �� 5o      $��8��6   .+�5p      �� -   5        $ btnIntervene��5p      $��5q�6/   $��Right=�6:     M_CollInterveneSet#���  6V   6�   5q      5r      5s      5t      