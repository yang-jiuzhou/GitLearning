   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.CollectionHBBio.Collection  )    #HBBio.Collection.CollectionObjectUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  *localclr-namespace:HBBio.Collection  .���   � RowDefinitions  �� ����  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)   #HBBio.Collection.CollectionObjectUC"   �� Path$ MHeader��� $
��Auto � � ColumnDefinitions �� $��* ��� $��5 ��� $��* ��� $��5 ��� $��* ��� $��5 ��� $��* ��� $��5 ��� $��* ��� $��5 ��� $��* ��� $��5 ��� $��* �.+��� -    ��Name$ labType��$��0��$��0��$	��0,5q�   
Coll_Type1#���   �� -   $ 	labLength��$��0��$��2��  Coll_Length1#���  y���  �� ElementName$ doubleLength��$ 
Visibility��� -   $ labThresholdSlope��$��0��$��4��  Coll_ThresholdSlope1#���  y��� $ cboxTS��$ 
Visibility��� -   $ labThresholdBegin��$��0��$��6��  Coll_ThresholdBegin1#���  y��� $ 	doubleTdB��$ 
Visibility��� -   $ labThresholdEnd��$��0��$��8��  Coll_ThresholdEnd1#���  y��� $ 	doubleTdE��$ 
Visibility��� -   $ labSlopeJudge��$��0��$��10��  Coll_SlopeJudge1#���  y��� $ cboxSlopeJudge��$ 
Visibility��� $��0��$��12��$	��0,5q� �� Orientation$ 
Horizontal=�y��� $ doubleSlope��$ 
Visibility�.+��� -   $ labSlope��  Coll_Slope1#���  �� -   $ labSlopeUnit���� -	   	 ��Name$	 cboxType��$��1��$��0��
 �� DisplayMemberPath$
 MName�� �� SelectedValuePath$ MIndex�� �� SelectedValue �� *MType+$  Xceed.Wpf.Toolkit.DoubleUpDown  -
     Name$ doubleLength��$��1��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *	MLength+y��� *MEnabledLength+�� -   $	 cboxTS��$��1��$��4��$
 MString��$ MEnum�� �� *MTS+y��� *
MEnabledTS+  -   $ 	doubleTdB��$��1��$��6�� �� *MTdB+y��� *MEnabledTdB+  -   $ 	doubleTdE��$��1��$��8�� �� *MTdE+y��� *MEnabledTdE+�� -   $	 cboxSlopeJudge��$��1��$��10��$
 MString��$ MEnum�� �� *MSJ+y��� *
MEnabledSJ+  -   $ doubleSlope��$��1��$��12�� �� *MSlope+y��� *
MEnabledSJ+