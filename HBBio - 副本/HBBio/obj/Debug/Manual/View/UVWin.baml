   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   9!clr-namespace:HBBio.CommunicationHBBio.Communication  5	   	       HBBio.Manual.UVWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   &localclr-namespace:HBBio.Manual  5   	   %shareclr-namespace:HBBio.Share  5   	   ,comm!clr-namespace:HBBio.Communication  5	   	   -   5           M_UVWin  � Title#  �   5   	    � SizeToContent$ WidthAndHeight=�6+   ���#EEF5FD J� Opacity$	 0.8Q�6f    � AllowsTransparency .  � WindowStyle$
 None=�6�   .��5      �� ���	#FF268DE4$��2q�6%    �� CornerRadius$ 5��69   ._�5   
   � $��2q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $
��Auto �6#   .+�5      � $��0��6   $��0��6    $��3��60   ���	#FF9DBBCD 5      �� 5      $
��Auto �6'   �� 5       $��5 �6'   �� 5!      $
��Auto �6'   �� 5"      $��* �6'   �� 5#      $
��Auto �6'   .+�5%      �� $��0��6   $��25��6(   $��25��63   $-��'/Bio-LabChrom;component/Image/hblog.ico��6?   �� 5&      $��2��6   #���   6,   $
��Bold"�6M   �� 5'      -   5         ��Name$ btnClose��5'      $��4��6+     closeBtnStyle#��[ 6;   6b   5(      � 5)      -   5        	 �Name$	 gridRead��5)      $��2��6%   $��0��62   $��3��6B    5*      �� 5+      $
��Auto �6$   �� 5,      $��10 �6$   �� 5-      $
��Auto �6$    5/      �� 50      $
��Auto �6'   �� 51      $��10 �6'   �� 52      $
��Auto �6'   �� 53      $��25 �6'   �� 54      $
��Auto �6'   �� 55      $��10 �6'   �� 56      $
��Auto �6'   .+�58      �� $��0��6   $��0��6)     
labUVLamp1#���  69       HBBio.Share.SwitchButtonUC  59      $��0��6'   $��2��64   {�.  
   	IsChecked
 6D   �� *MLamp+6`   �� 5:      $��0��6   $��4��6)     
labUVTime1#���  69   $  Xceed.Wpf.Toolkit.DoubleUpDown  5;      $��0��6$   $��6��61   {�.  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6A   �� *MTime+6Y   �� 5<      $��2��6   $��0��6)     	labUVRef1#���  69   %  Xceed.Wpf.Toolkit.IntegerUpDown  5=      $��2��6%   $��2��62   {�.  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6B   �� *MRef+6Y   �� 5>      $��2��6   $��4��6)     	labUVSig1#���  69     5?      $��2��6%   $��6��62   {�.   6B   �� *MSig+6Y   5@         HBBio.Communication.UVUC  5A      -   5          Name$
 ucUV��5A      $��4��6&   $��0��63   $��3��6C   $
��10,0q�6W   �� 5B      -   5        $ btnApply��5B      $��6��6'   $��0��64   $��3��6D   $
��10,0q�6Y     btnApply#���  6g   6�   5C      5D      5E      