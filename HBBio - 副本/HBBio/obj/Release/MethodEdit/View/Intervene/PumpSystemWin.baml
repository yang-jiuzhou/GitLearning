   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  $    HBBio.MethodEdit.PumpSystemWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  *localclr-namespace:HBBio.MethodEdit  %shareclr-namespace:HBBio.Share  -      M_PumpSystemWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��500�����#EEF5FD J� Opacity$	 0.9Q� � AllowsTransparency .  � WindowStyle$
 None=� � 	Resources  �� 
TargetType��%   '��       �� " ��.�-   .���� ���	#FF268DE4$��2q� �� CornerRadius$ 5��._�� -    �Name$
 grid��$��2q�	 � RowDefinitions	 �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�����	#FF9DBBCD
 � ColumnDefinitions
 �� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��25��$��25��$-��'/Bio-LabChrom;component/Image/hblog.ico���� $��2��#���   $
��Bold"��� -    ��Name$ btnClose��$��4��  closeBtnStyle#��[ � $��2��	 �� $
��Auto ��� $��5 ��� $
��Auto �
 �� $��* ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ labFlow��$��0��$��0��$��7��  M_SystemFlow#���  �� -    ��Name$ 
sliderFlow��$��2��$��0��$  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ 
doubleFlow��$��2��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value ��  �� ElementName$ 
sliderFlow�� �� Path$ Value� �� Mode$ TwoWay=��� $��2��$��4��$�� ���� -   $ btnFlow��$��2��$��6��  M_SetFlowRate#���  � $��4��	 �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �
 �� $��* ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -	   $ labConc��$��0��$��0��$��7�� 
 M_Conc#���  �� -
   $ sliderB��$��2��$��0�� � Maximum$	 100Q�  -   $ doubleB��$��2��$��2��   Maximum$	 100X� �� $ sliderB��$ Value�$ TwoWay=�y��� $ sliderB��$ 
Visibility��� -   $ labBPer��$��2��$��4��$��%B��y��� $ sliderB��$ 
Visibility��� -   $ btnPer��$��2��$��6��  M_SetFlowPer#���  y��� $ sliderB��$ 
Visibility��� -   $ sliderC��$��4��$��0��$	 100Q�y��� $ 	btnStartC��$ 
Visibility�  -   $ doubleC��$��4��$��2��$	 100X� �� $ sliderC��$ Value�$ TwoWay=�y��� $ sliderC��$ 
Visibility��� -   $ labCPer��$��4��$��4��$��%C��y��� $ sliderC��$ 
Visibility��� -   $ sliderD��$��6��$��0��$	 100Q�y��� $ 	btnStartD��$ 
Visibility�  -   $ doubleD��$��6��$��2��$	 100X� �� $ sliderD��$ Value�$ TwoWay=�y��� $ sliderD��$ 
Visibility��� -   $ labDPer��$��26��$��4��$��%D��y��� $ sliderD��$ 
Visibility�