   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual      HBBio.Manual.OutWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  &localclr-namespace:HBBio.Manual  -      M_OutWin  � Title#  �    � SizeToContent$ WidthAndHeight=����#EEF5FD J� Opacity$	 0.8Q� � AllowsTransparency .  � WindowStyle$
 None=�.��� $��5q� � RowDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -    ��Name$ title��$��0��$
��Bold"��� -    ��Name$ btnClose��  closeBtnStyle#��[ $��2���� $��2��  M_OutWin_SingleSelect#���  .��� $��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto �.+�	� -   	 	�Name$	 rbtnRealSingle��$��0��  labReal#���  
 d� 	IsChecked$

 TrueY�	� -   $	 rbtnDelaySingle��$��2��  labDelay#���  � -    �Name$
 grid��$��4���� -    ��Name$ groupMultipleSelect��$��4��  M_OutWin_MultipleSelect#���  .��� $��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+�	� -   $	 rbtnRealMultiple��$��0��#���  	� -	   $	 rbtnDelayMultiple��$��2��#���  �� $��4��  M_OutWin_Start1#���  �� -
    ��Name$ 
cboxSelect��$��6���� $��8��  M_OutWin_Vol1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ 	doubleVol��$��10���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$
 0.01X��� -   $ btnStart��$��12�� 
 btnSet#���  �� -   $ btnStop��$��14�� 	 	btnCancel#��� 	 �� $��6��.+��� -   $ btnIntervene��$��5q�$��Right=� 
 M_CollInterveneSet#��� 
 