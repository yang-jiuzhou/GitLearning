   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.CollectionHBBio.Collection  .    (HBBio.Collection.CollectionObjectMultiUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:HBBio.Collection     -   .��� -     �Name$
  grid�� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ 	labObjOne��$��0��$��0��   Coll_ObjOne1#���   $��0,0,0,5q�$��Bottom=�)   #HBBio.Collection.CollectionObjectUC  -     Name$ 	ucObject1��$��0��$��2���� -   $ 	labObjTwo��$��2��$��0��  Coll_ObjTwo1#���  $��0,0,0,5q�$��Bottom=�y��� *MVisibilityObj2+  -   $ 	ucObject2��$��2��$��2��y���  �� ElementName$ 	labObjTwo�� �� Path$ 
Visibility�� $��0��$��4�� �� $��5 ��� $
��Auto ��� $��5 ��� $��* �.+��� -   $ labRelation��$��1��  Coll_ObjSelect1#���  �� -    ��Name$ cboxRelation��$��3�� �� DisplayMemberPath$ MString��	 �� SelectedValuePath$	 MEnum��
 �� SelectedValue
 �� *	MRelation+