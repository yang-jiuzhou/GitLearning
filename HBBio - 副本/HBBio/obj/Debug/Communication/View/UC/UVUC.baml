   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5      )clr-namespace:HBBio.ShareHBBio.Share  5          HBBio.Communication.UVUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -local!clr-namespace:HBBio.Communication  5      %shareclr-namespace:HBBio.Share  5      -   5          � 	Resources  5      $   HBBio.Share.BoolFalseConverter   BoolFalseConverter��%   &	          5   
   6@   5      .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5   
   � -   5         �Name$ gridUV1��5      $��5q�6     � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��25 �6    �� 5      $
��Auto �6    .+�5      �� $��0��6     
labUVLamp1#���  6%   	� 5      -   5         	�Name$ rbtnUVOn��5      $��2��6,   $	��5,0q�69     labUVOn#���  6F    d� 	IsChecked 6j   �� *MOnoff+	� 5      -   5        $ 	rbtnUVOff��5      $��4��6-   $	��5,0q�6:     labUVOff#���  6G    6l   �� *MOnoff+ �� 	Converter# [  �� 5      -   5         ��Name$ chboxUVZero��5      $��6��6,     
labUVReset#���  69    6`   �� *MClear+5      � 5   
   -   5        $ gridUV2��5      $��5q�6     � ColumnDefinitions 5      �� 5       $
��Auto �6#   �� 5!      $��5 �6#   �� 5"      $
��Auto �6#   �� 5#      $��5 �6#   �� 5$      $
��Auto �6#    5&      �� 5'      $
��Auto �6    �� 5(      $��5 �6    �� 5)      $
��Auto �6    �� 5*      $��5 �6    �� 5+      $
��Auto �6    �� 5,      $��5 �6    �� 5-      $
��Auto �6    �� 5.      $��5 �6    �� 5/      $
��Auto �6    .+�51      �� $��0��6   $��0��6(     labUVWaveLength1#���  65   �� 52      -   5        	 ��Name$	 labWaveLength��52      $��2��6/   $��0��6?   �� 53      -   5        $	 labUV1��53      $��0��6(   $��2��68     labUV11#���  6E   %  Xceed.Wpf.Toolkit.IntegerUpDown  54      -	   5        
  Name$
 intUV1��54   !   $��2��61   $��2��6A   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6N   �� *MWave1+�� 55      -
   5        $	 labUV2��55      $��0��6(   $��4��68     labUV21#���  6E     56      -   5        $
 intUV2��56   !   $��2��61   $��4��6A      Minimum 6N   ��  �� ElementName$ intUV1�� �� Path$ Minimum�   Maximum 6�   �� $ intUV1��$ Maximum� 6�   �� *MWave2+{�6�   �� *MEnabledWave2+�� 57      -   5        $ chboxEnabledWave2��57      $��4��62   $��4��6B   $��0��6s    6O   �� *MEnabledWave2+��6�   �� *ActualHeight+ �� RelativeSource �� *Self+�� 58      -   5        $	 labUV3��58      $��0��6(   $��6��68     labUV31#���  6E     59      -   5        $
 intUV3��59   !   $��2��61   $��6��6A    6N   �� $ intUV1��$ Minimum� 6�   �� $ intUV1��$ Maximum� 6�   �� *MWave3+y�6�   �� $ labUV3��$ 
Visibility��� 5:      -   5        $	 labUV4��5:      $��0��6(   $��8��68    	 labUV41#��� 	 6E     5;      -   5        $
 intUV4��5;   !   $��2��61   $��8��6A    6N   �� $ intUV1��$ Minimum� 6�   �� $ intUV1��$ Maximum� 6�   �� *MWave4+y�6�   �� $ labUV4��$ 
Visibility�5<      5=      5>      