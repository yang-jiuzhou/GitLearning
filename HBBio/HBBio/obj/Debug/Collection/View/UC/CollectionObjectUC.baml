   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.CollectionHBBio.Collection  5      )    #HBBio.Collection.CollectionObjectUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      *localclr-namespace:HBBio.Collection  5      .��5	      �   � RowDefinitions  5
   
   �� 5      ��6   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)   #HBBio.Collection.CollectionObjectUC"   �� Path$ MHeader��� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ labType��5      $��0��6%   $��0��62   $	��0,5q�6B      
Coll_Type1#���   6O   �� 5   
   -   5        $ 	labLength��5      $��0��6'   $��2��64     Coll_Length1#���  6D   y�6j   ��  �� ElementName$ doubleLength��$ 
Visibility��� 5   
   -   5        $ labThresholdSlope��5      $��0��6/   $��4��6<     Coll_ThresholdSlope1#���  6L   y�6z   �� $ cboxTS��$ 
Visibility��� 5    
   -   5        $ labThresholdBegin��5       $��0��6/   $��6��6<     Coll_ThresholdBegin1#���  6L   y�6z   �� $ 	doubleTdB��$ 
Visibility��� 5!   
   -   5        $ labThresholdEnd��5!      $��0��6-   $��8��6:     Coll_ThresholdEnd1#���  6J   y�6v   �� $ 	doubleTdE��$ 
Visibility��� 5"   
   -   5        $ labSlopeJudge��5"      $��0��6+   $��10��68     Coll_SlopeJudge1#���  6I   y�6s   �� $ cboxSlopeJudge��$ 
Visibility��� 5#   
   $��0��6   $��12��6"   $	��0,5q�63    �� Orientation$ 
Horizontal=�6@   y�6Y   �� $ doubleSlope��$ 
Visibility�.+�5$      �� -   5        $ labSlope��5$        Coll_Slope1#���  6*   �� 5%      -   5        $ labSlopeUnit��5%      5&      �� 5'   
   -	   5        	 ��Name$	 cboxType��5'      $��1��6%   $��0��62   
 �� DisplayMemberPath$
 MName��6B    �� SelectedValuePath$ MIndex��6\    �� SelectedValue 6w   �� *MType+6�   $  Xceed.Wpf.Toolkit.DoubleUpDown  5(   
   -
   5          Name$ doubleLength��5(      $��1��62   $��2��6?   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6O   �� *	MLength+y�6i   �� *MEnabledLength+�� 5)   
   -   5        $	 cboxTS��5)      $��1��6#   $��4��60   $
 MString��6@   $ MEnum��6\    6v   �� *MTS+y�6�   �� *
MEnabledTS+  5*   
   -   5        $ 	doubleTdB��5*      $��1��6/   $��6��6<    6L   �� *MTdB+y�6c   �� *MEnabledTdB+  5+   
   -   5        $ 	doubleTdE��5+      $��1��6/   $��8��6<    6L   �� *MTdE+y�6c   �� *MEnabledTdE+�� 5,   
   -   5        $	 cboxSlopeJudge��5,      $��1��6+   $��10��68   $
 MString��6I   $ MEnum��6e    6   �� *MSJ+y�6�   �� *
MEnabledSJ+  5-   
   -   5        $ doubleSlope��5-      $��1��61   $��12��6>    6O   �� *MSlope+y�6h   �� *
MEnabledSJ+5.      5/      