   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HBBio.TubeStandHBBio.TubeStand  5   	   &     HBBio.TubeStand.TubeStandItemWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   )localclr-namespace:HBBio.TubeStand  5   	   -   5           TS_TubeStandItemWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�67   $	��250��6]    � WindowStartupLocation$ CenterOwner=�6l   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � -   5         �Name$
 grid��5      $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5       $��10 �6    �� 5!      $
��Auto �6    �� 5"      $��10 �6    �� 5#      $
��Auto �6    �� 5$      $��10 �6    �� 5%      $
��Auto �6    .+�5'      �� -   5         ��Name$ labName��5'      $��0��6)   $��0��66     TS_Name1#���  6F   �� 5(      -   5         ��Name$ txtName��5(      $��0��6'   $��2��64   ��6D   �� *MName+�� 5)      -   5        $ labCollVolume��5)      $��2��6/   $��0��6<     TS_CollVolume1#���  6L   $  Xceed.Wpf.Toolkit.DoubleUpDown  5*      -   5          Name$ doubleCollVolume��5*       $��2��6:   $��2��6G   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Value	 6W   �� *MCollVolume+�� 5+      -   5        $ 	labVolume��5+      $��4��6+   $��0��68     
TS_Volume1#���  6H     5,      -   5        $ doubleVolume��5,       $��4��66   $��2��6C   	 6S   �� *	MVolume+�� 5-      -	   5        $ labCount��5-      $��6��6*   $��0��67     	TS_Count1#���  6G   %  Xceed.Wpf.Toolkit.IntegerUpDown  5.      -
   5        
  Name$
 intCollCount��5.   !   $��6��67   $��2��6D   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6T   �� *MCount+�� 5/      -   5        $ labDiameter��5/      $��8��6-   $��0��6:     TS_Diameter1#���  6J     50      -   5        $ doubleDiameter��50       $��8��68   $��2��6E   	 6U   �� *	MDiameter+�� 51      -   5        $ 	labHeight��51      $��10��6+   $��0��69     
TS_Height1#���  6I     52      -   5        $ doubleHeight��52       $��10��66   $��2��6D   	 6T   �� *	MHeight+�� 53      -   5        $ labRow��53      $��12��6(   $��0��66     TS_Row1#���  6F     54      -   5        $
 intRow��54   !   $��12��61   $��2��6?    6O   �� *MRow+�� 55      -   5        $ labCol��55      $��14��6(   $��0��66     TS_Col1#���  6F     56      -   5        $
 intCol��56   !   $��14��61   $��2��6?    6O   �� *MCol+57      � 58   
   $��2��6    59      �� 5:      $��* �6#   �� 5;      $
��Auto �6#   �� 5<      $��10 �6#   �� 5=      $
��Auto �6#   .+�5?      �� -   5         ��Name$ btnOK��5?      $��1��6$    		 btnOK#��� 	 64   6V   �� 5@      -   5        $ 	btnCancel��5@      $��3��6(    
 	btnCancel#��� 
 68   6^   5A      5B      5C      