   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  )clr-namespace:HBBio.ShareHBBio.Share  9!clr-namespace:HBBio.CommunicationHBBio.Communication      HBBio.Manual.UVWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  &localclr-namespace:HBBio.Manual  %shareclr-namespace:HBBio.Share  ,comm!clr-namespace:HBBio.Communication  -      M_UVWin  � Title#  �    � SizeToContent$ WidthAndHeight=����#EEF5FD J� Opacity$	 0.8Q� � AllowsTransparency .  � WindowStyle$
 None=�.���� ���	#FF268DE4$��2q� �� CornerRadius$ 5��._�� $��2q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 � � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0��$��0��$��3�����	#FF9DBBCD �� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��25��$��25��$-��'/Bio-LabChrom;component/Image/hblog.ico���� $��2��#���   $
��Bold"��� -    ��Name$ btnClose��$��4��  closeBtnStyle#��[ � -   	 �Name$	 gridRead��$��2��$��0��$��3�� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��25 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$��0��  
labUVLamp1#���      HBBio.Share.SwitchButtonUC  $��0��$��2��{�.  
   	IsChecked
 �� *MLamp+�� $��0��$��4��  
labUVTime1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  $��0��$��6��{�.  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *MTime+�� $��2��$��0��  	labUVRef1#���  %  Xceed.Wpf.Toolkit.IntegerUpDown  $��2��$��2��{�.  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *MRef+�� $��2��$��4��  	labUVSig1#���    $��2��$��6��{�.   �� *MSig+   HBBio.Communication.UVUC  -     Name$
 ucUV��$��4��$��0��$��3��$
��10,0q��� -   $ btnApply��$��6��$��0��$��3��$
��10,0q�  btnApply#���  