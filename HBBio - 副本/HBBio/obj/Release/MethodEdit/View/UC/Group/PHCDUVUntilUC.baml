   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  $    HBBio.MethodEdit.PHCDUVUntilUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  *localclr-namespace:HBBio.MethodEdit  %shareclr-namespace:HBBio.Share  -     � 	Resources  #   HBBio.Share.EnumBoolConverter   EnumBoolConverter��%   &	          .���� ����  �� Orientation$ 
Horizontal=�.+���    ME_EnumGroupType_PHCDUVUntil#���  �� $��(���� ���� *MHeaderText+�� ����  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$   HBBio.MethodEdit.PHCDUVUntilUC"   �� Path$ 
TextHeader�y���  �� $ FindAncestor=�"  $ VisibilityHeader��� $��)��.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* �.+��� $��0��$ 
Horizontal=�.+�	� -    	�Name$ 	rbtnTotal�� 	� 	GroupName$ 
wash_until��  ME_TheTotalIs1#���  	 d� 	IsChecked	 �� $ 
MUntilType�
 �� 	Converter#
 [  &    HBBio.MethodEdit.EnumPHCDUVUntil   Total �� ConverterParameter# Z $  Xceed.Wpf.Toolkit.DoubleUpDown  $��5q�{���  �� ElementName$ 	rbtnTotal��$ 	IsChecked��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *
MTotalTVCV+�� {��� $ 	rbtnTotal��$ 	IsChecked����� *MBaseUnitStr+	� -   $ rbtnCondition��$��2��$ 
wash_until��  	ME_TFCIM1#���  	 �� $ 
MUntilType�#
 [  
   Met# Z � $��4��{��� $ rbtnCondition��$ 	IsChecked� � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* �.+��� $��0��  ME_SignalSelection1#���  �� -    ��Name$ cboxMonitor��$��0��$��2��$��3�� �� SelectedIndex �� $ MMonitorIndex� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��2��  ME_JudgmentLogic1#���  �� -   $ 	cboxJudge��$��2��$��2��$��3�� �� $ MJudgeIndex�$ TwoWay=�$ PropertyChanged=��� $��4��  ME_PHMoreThan1#���  y��� *MVisibPHMore+�� $��4��  ME_CDMoreThan1#���  y��� *MVisibCDMore+�� $��4��  ME_UVMoreThan1#���  y��� *MVisibUVMore+  $��4��$��2�� �� *	MMoreThan+   Minimum �� *MMoreLessThanMin+   Maximum �� *MMoreLessThanMax+y��� *MVisibMoreThan+�� $��4��$��4������ *MMoreLessThanStr+y��� *MVisibMoreThan+�� $��6�� 	 ME_PHLessThan1#��� 	 y��� *MVisibPHLess+�� $��6�� 
 ME_CDLessThan1#��� 
 y��� *MVisibCDLess+�� $��6��  ME_UVLessThan1#���  y��� *MVisibUVLess+  $��6��$��2�� �� *	MLessThan+ �� *MMoreLessThanMin+ �� *MMoreLessThanMax+y��� *MVisibLessThan+�� $��6��$��4������ *MMoreLessThanStr+y��� *MVisibLessThan+�� $��8��  ME_StabilityTime1#���    $��8��$��2�� �� *MStabilityTime+�� $��8��$��4��   HBBio.Share.DlyBase   SC_TUNIT#��Z �� $��10��  ME_MaximumWash1#���    $��10��$��2�� �� *
MMaxTVCV+�� $��10��$��4������ *MBaseUnitStr+