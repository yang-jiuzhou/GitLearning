   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.CollectionHBBio.Collection  )clr-namespace:HBBio.ShareHBBio.Share  (    "HBBio.Collection.CollectionItemWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.Collection     +shareclr-namespace:HBBio.Share     -      Coll_CollectionItemWin  � Title#  �    � SizeToContent$ Height=�$	��900�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� -    �Name$
 grid��$��0�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* �.+�.   (HBBio.Collection.CollectionObjectMultiUC  -     Name$ ucObjectMulti1��$��0��$��0��$��12���� -    ��Name$ labPositionType��$��2��$��0��  Coll_PositionType1#���  �� -    ��Name$ cboxPositionType��$��2��$��2��	 �� DisplayMemberPath$	 MString��
 �� SelectedValuePath$
 MEnum�� �� SelectedValue �� *MPositionType+�� -   $ labPositionStart��$��2��$��4��  Coll_PositionStart1#���  �� -   $ cboxPositionStart��$��2��$��6��$	 MName��$
 MName�� �� *MStart+  -   $ ucObjectMulti2��$��4��$��0��$��12��y��� *
MVisible+� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -	    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -
   $ 	btnCancel��$��3��  	btnCancel#���  