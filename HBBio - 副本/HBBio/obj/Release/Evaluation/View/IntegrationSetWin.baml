   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.EvaluationHBBio.Evaluation  )clr-namespace:HBBio.ShareHBBio.Share  (    "HBBio.Evaluation.IntegrationSetWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  *localclr-namespace:HBBio.Evaluation  %shareclr-namespace:HBBio.Share  -      E_IntegrationSetWin  � Title#  �    � SizeToContent$ Height=�$	��450�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $��* ��� $��10 ��� $
��Auto �.+��� $��0��.@���   E_IntegrationResult#���  .��� -    �Name$ 	wrapPanel����   E_IntegrationCondition#���  .��� $��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ 	labFilter��$��0��$��0��  	E_Filter1#���  � $��0��$��2�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��5 ��� $
��Auto �.+�	� -    	�Name$ rbtnMix��$��0��$��0��$��3��  E_FilterMin#���   d� 	IsChecked �� *MIsMin+	� -   $ 	rbtnCount��$��0��$��4��$��3��  E_FilterCount#���   �� *
MIsCount+�� -   $ labMinHeight��$��2��$��0��  E_MinHeight1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -   	  Name$	 doubleMinHeight��$��2��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Value
 �� *
MMinHeight+�� -   $ labPeakCount��$��2��$��4��  E_PeakCount1#���  %  Xceed.Wpf.Toolkit.IntegerUpDown  -	     Name$ intPeakCount��$��2��$��6���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *
MPeakCount+�� -
   $ 
labMinArea��$��4��$��0��  
E_MinArea1#���    -   $	 doubleMinArea��$��4��$��2��
 �� *
MMinArea+�� -   $ labMinHalfWidth��$��6��$��0�� 	 E_MinHalfWidth1#��� 	   -   $	 doubleMinHalfWidth��$��6��$��2��
 �� *	MMinWidth+�� $��2��$��0�� 
 E_Original1#��� 
 �� -    ��Name$ cboxOriginal��$��2��$��2���� $��4��$��0��  E_ColHeight1#���  �� $��4��$��2�� �� Orientation$ 
Horizontal=�.+�  -   $	 doubleCH��
 �� *MCH+�� $
��10,0q�   HBBio.Share.DlyBase   SC_LENGTHUNIT#��Z � $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3��  	btnCancel#���  