   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  )clr-namespace:HBBio.ShareHBBio.Share      HBBio.Communication.UVUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -local!clr-namespace:HBBio.Communication  %shareclr-namespace:HBBio.Share  -     � 	Resources  $   HBBio.Share.BoolFalseConverter   BoolFalseConverter��%   &	          .����  �� Orientation$ 
Horizontal=�.+�� -    �Name$ gridUV1��$��5q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��25 ��� $
��Auto �.+��� $��0��  
labUVLamp1#���  	� -    	�Name$ rbtnUVOn��$��2��$	��5,0q�  labUVOn#���   d� 	IsChecked �� *MOnoff+	� -   $ 	rbtnUVOff��$��4��$	��5,0q�  labUVOff#���   �� *MOnoff+ �� 	Converter# [  �� -    ��Name$ chboxUVZero��$��6��  
labUVReset#���   �� *MClear+� -   $ gridUV2��$��5q� � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��$��0��  labUVWaveLength1#���  �� -   	 ��Name$	 labWaveLength��$��2��$��0���� -   $	 labUV1��$��0��$��2��  labUV11#���  %  Xceed.Wpf.Toolkit.IntegerUpDown  -	   
  Name$
 intUV1��$��2��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *MWave1+�� -
   $	 labUV2��$��0��$��4��  labUV21#���    -   $
 intUV2��$��2��$��4��   Minimum ��  �� ElementName$ intUV1�� �� Path$ Minimum�   Maximum �� $ intUV1��$ Maximum� �� *MWave2+{��� *MEnabledWave2+�� -   $ chboxEnabledWave2��$��4��$��4��$��0�� �� *MEnabledWave2+���� *ActualHeight+ �� RelativeSource �� *Self+�� -   $	 labUV3��$��0��$��6��  labUV31#���    -   $
 intUV3��$��2��$��6�� �� $ intUV1��$ Minimum� �� $ intUV1��$ Maximum� �� *MWave3+y��� $ labUV3��$ 
Visibility��� -   $	 labUV4��$��0��$��8�� 	 labUV41#��� 	   -   $
 intUV4��$��2��$��8�� �� $ intUV1��$ Minimum� �� $ intUV1��$ Maximum� �� *MWave4+y��� $ labUV4��$ 
Visibility�