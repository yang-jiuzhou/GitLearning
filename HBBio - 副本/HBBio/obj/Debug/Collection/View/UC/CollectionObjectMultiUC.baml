   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.CollectionHBBio.Collection  5      .    (HBBio.Collection.CollectionObjectMultiUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:HBBio.Collection     5      -   5        .��5      � -   5          �Name$
  grid��5       � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	labObjOne��5      $��0��6'   $��0��64      Coll_ObjOne1#���   6D   $��0,0,0,5q�6j   $��Bottom=�6{   )   #HBBio.Collection.CollectionObjectUC  5   
   -   5          Name$ 	ucObject1��5   #   $��0��66   $��2��6C   �� 5   
   -   5        $ 	labObjTwo��5      $��2��6'   $��0��64     Coll_ObjTwo1#���  6D   $��0,0,0,5q�6j   $��Bottom=�6{   y�6�   �� *MVisibilityObj2+  5   
   -   5        $ 	ucObject2��5   #   $��2��66   $��2��6C   y�6S   ��  �� ElementName$ 	labObjTwo�� �� Path$ 
Visibility�� 5   
   $��0��6   $��4��6    5      �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $��* �6    .+�5       �� -   5        $ labRelation��5       $��1��6-     Coll_ObjSelect1#���  6:   �� 5!      -   5         ��Name$ cboxRelation��5!      $��3��6-    �� DisplayMemberPath$ MString��6:   	 �� SelectedValuePath$	 MEnum��6V   
 �� SelectedValue
 6p   �� *	MRelation+5"      5#      5$      