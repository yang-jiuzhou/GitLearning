   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  )clr-namespace:HBBio.ShareHBBio.Share       HBBio.Manual.PumpSampleWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  &localclr-namespace:HBBio.Manual  %shareclr-namespace:HBBio.Share  -      M_PumpSampleWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��350�����#EEF5FD J� Opacity$	 0.9Q� � AllowsTransparency .  � WindowStyle$
 None=�.���� ���	#FF268DE4$��2q� �� CornerRadius$ 5��._�� -    �Name$
 grid��$��2q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* �.+�� $��0�����	#FF9DBBCD � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��25��$��25��$-��'/Bio-LabChrom;component/Image/hblog.ico���� $��2��#���   $
��Bold"��� -   	 ��Name$	 btnClose��$��4��  closeBtnStyle#��[ � $��2�� �� $
��Auto ��� $��5 ��� $
��Auto � �� $��* ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -   
 ��Name$
 labFlow��$��0��$��0��$��7��  M_SampleFlow#���  �� -    ��Name$ 
sliderFlow��$��2��$��0�� � Value �� *MFlow+$  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ 
doubleFlow��$��2��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value ��  �� ElementName$ 
sliderFlow�� �� Path$ Value� �� Mode$ TwoWay=��� $��2��$��4��$�� ���� -   $	 btnFlow��$��2��$��6��  M_SetFlowRate#���  � $��4�� �� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$��0��$��7��  labWash#���  �� -    ��Name$ cboxWash��$��2��$��0���� -	   $
 cbox��$��2��$��2���� -
   $	 btnStart��$��2��$��4��  M_StartWash#���  �� -   $	 btnStop��$��2��$��6��  
M_StopWash#���  