   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HBBio.TubeStandHBBio.TubeStand  &     HBBio.TubeStand.TubeStandItemWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  )localclr-namespace:HBBio.TubeStand  -      TS_TubeStandItemWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��250�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� -    �Name$
 grid��$��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ labName��$��0��$��0��  TS_Name1#���  �� -    ��Name$ txtName��$��0��$��2������ *MName+�� -   $ labCollVolume��$��2��$��0��  TS_CollVolume1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ doubleCollVolume��$��2��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Value	 �� *MCollVolume+�� -   $ 	labVolume��$��4��$��0��  
TS_Volume1#���    -   $ doubleVolume��$��4��$��2��	 �� *	MVolume+�� -	   $ labCount��$��6��$��0��  	TS_Count1#���  %  Xceed.Wpf.Toolkit.IntegerUpDown  -
   
  Name$
 intCollCount��$��6��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *MCount+�� -   $ labDiameter��$��8��$��0��  TS_Diameter1#���    -   $ doubleDiameter��$��8��$��2��	 �� *	MDiameter+�� -   $ 	labHeight��$��10��$��0��  
TS_Height1#���    -   $ doubleHeight��$��10��$��2��	 �� *	MHeight+�� -   $ labRow��$��12��$��0��  TS_Row1#���    -   $
 intRow��$��12��$��2�� �� *MRow+�� -   $ labCol��$��14��$��0��  TS_Col1#���    -   $
 intCol��$��14��$��2�� �� *MCol+� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 		 btnOK#��� 	 �� -   $ 	btnCancel��$��3�� 
 	btnCancel#��� 
 