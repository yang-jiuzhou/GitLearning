   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5      )clr-namespace:HBBio.ShareHBBio.Share  5      $    HBBio.MethodEdit.PHCDUVUntilUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      *localclr-namespace:HBBio.MethodEdit  5      %shareclr-namespace:HBBio.Share  5      -   5          � 	Resources  5      #   HBBio.Share.EnumBoolConverter   EnumBoolConverter��%   &	          5   
   6>   5      .��5      �� ��5   
   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      ��    ME_EnumGroupType_PHCDUVUntil#���  6   �� 5      $��(��6   �� 5      ��6   �� *MHeaderText+�� 5      ��6   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$   HBBio.MethodEdit.PHCDUVUntilUC"   �� Path$ 
TextHeader�y�5      ��  �� $ FindAncestor=�"  $ VisibilityHeader��� 5      $��)��6   5      .��5   
   � $��10q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5       $��* �6    .+�5"      �� $��0��6   $ 
Horizontal=�6&   .+�5#      	� -   5         	�Name$ 	rbtnTotal��5#       	� 	GroupName$ 
wash_until��61     ME_TheTotalIs1#���  6H   	 d� 	IsChecked	 6s   �� $ 
MUntilType�
 �� 	Converter#
 [  &    HBBio.MethodEdit.EnumPHCDUVUntil   Total �� ConverterParameter# Z $  Xceed.Wpf.Toolkit.DoubleUpDown  5$      $��5q�6$   {�6/   ��  �� ElementName$ 	rbtnTotal��$ 	IsChecked��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6j   �� *
MTotalTVCV+�� 5%      {�6   �� $ 	rbtnTotal��$ 	IsChecked���6W   �� *MBaseUnitStr+5&      	� 5'      -   5        $ rbtnCondition��5'      $��2��61   $ 
wash_until��6>     	ME_TFCIM1#���  6U   	 6{   �� $ 
MUntilType�#
 [  
   Met# Z � 5(      $��4��6   {�6    �� $ rbtnCondition��$ 	IsChecked� � ColumnDefinitions 5)      �� 5*      $
��Auto �6'   �� 5+      $��5 �6'   �� 5,      $
��Auto �6'   �� 5-      $��5 �6'   �� 5.      $
��Auto �6'   �� 5/      $��* �6'    51      �� 52      $
��Auto �6$   �� 53      $��5 �6$   �� 54      $
��Auto �6$   �� 55      $��5 �6$   �� 56      $
��Auto �6$   �� 57      $��5 �6$   �� 58      $
��Auto �6$   �� 59      $��5 �6$   �� 5:      $
��Auto �6$   �� 5;      $��5 �6$   �� 5<      $
��Auto �6$   �� 5=      $��* �6$   .+�5?      �� $��0��6     ME_SignalSelection1#���  6)   �� 5@      -   5         ��Name$ cboxMonitor��5@      $��0��60   $��2��6=   $��3��6M    �� SelectedIndex 6a   �� $ MMonitorIndex� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5A      $��2��6     ME_JudgmentLogic1#���  6)   �� 5B      -   5        $ 	cboxJudge��5B      $��2��6.   $��2��6;   $��3��6K    6_   �� $ MJudgeIndex�$ TwoWay=�$ PropertyChanged=��� 5C      $��4��6     ME_PHMoreThan1#���  6)   y�6Q   �� *MVisibPHMore+�� 5D      $��4��6     ME_CDMoreThan1#���  6)   y�6Q   �� *MVisibCDMore+�� 5E      $��4��6     ME_UVMoreThan1#���  6)   y�6Q   �� *MVisibUVMore+  5F      $��4��6$   $��2��61    6A   �� *	MMoreThan+   Minimum 6]   �� *MMoreLessThanMin+   Maximum 6�   �� *MMoreLessThanMax+y�6�   �� *MVisibMoreThan+�� 5G      $��4��6   $��4��6)   ��69   �� *MMoreLessThanStr+y�6[   �� *MVisibMoreThan+�� 5H      $��6��6    	 ME_PHLessThan1#��� 	 6)   y�6Q   �� *MVisibPHLess+�� 5I      $��6��6    
 ME_CDLessThan1#��� 
 6)   y�6Q   �� *MVisibCDLess+�� 5J      $��6��6     ME_UVLessThan1#���  6)   y�6Q   �� *MVisibUVLess+  5K      $��6��6$   $��2��61    6A   �� *	MLessThan+ 6]   �� *MMoreLessThanMin+ 6�   �� *MMoreLessThanMax+y�6�   �� *MVisibLessThan+�� 5L      $��6��6   $��4��6)   ��69   �� *MMoreLessThanStr+y�6[   �� *MVisibLessThan+�� 5M      $��8��6     ME_StabilityTime1#���  6)     5N      $��8��6$   $��2��61    6A   �� *MStabilityTime+�� 5O      $��8��6   $��4��6)      HBBio.Share.DlyBase   SC_TUNIT#��Z 69   �� 5P      $��10��6     ME_MaximumWash1#���  6*     5Q      $��10��6$   $��2��62    6B   �� *
MMaxTVCV+�� 5R      $��10��6   $��4��6*   ��6:   �� *MBaseUnitStr+5S      5T      5U      5V      