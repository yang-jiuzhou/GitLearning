   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  )clr-namespace:HBBio.ShareHBBio.Share  /    )HBBio.Communication.EditProcessPictureWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -local!clr-namespace:HBBio.Communication  %shareclr-namespace:HBBio.Share     Com_EditProcessPictureWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��500��$	��750�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$	��100������  �� ElementName$ processPictureUC�� �� Path$ MName��� $��2��$��x:���� -    ��Name$ txtLeft��$��4�� �� 
IsReadOnly . $��0q�$��50������ $ processPictureUC��$ MLeft��� $��6��$��y:���� -   $ txtTop��$��8�� . $��0q�$��50������ $ processPictureUC��$
 MTop�� $��2�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��  Com_AdjustPt1#���  �� $��2��$	��X :��%  Xceed.Wpf.Toolkit.IntegerUpDown  -   	  Name$
	 intX��$��4���� $��6��$	��Y :��  -   $
	 intY��$��8���� -   
 ��Name$
 btnSet��$��10�� 
 btnSet#���  � $��4�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��25 ��� $
��Auto �.+��� $��0��  Com_AdjustPath1#���  �� -    ��Name$ cboxType��$��2���� -   $
 btnHVAdd��$��4��  	Com_HVAdd#���  �� -   $
 btnVHAdd��$��6��  	Com_VHAdd#���  �� -	   $
 btnDel��$��8�� 
 btnDel#���  �� -
   $
 btnClear��$��10��  btnClear#���  � $��6�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* �.+��� $��0��  Com_Circle1#���    -   $	 	intCircle��$��2���� $��4�� 	 Com_ColumnVisib#��� 	   -   $	 	intColumn��$��6��� $��8�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� .+��� $��0�� 
 Com_ProcessPictureSize1#��� 
 �� $��2��  	labWidth1#���    -   $	 doubleWidth��$��4���� $��6��  
labHeight1#���    -   $	 doubleHeight��$��8���� -   $
 
btnSetSize��$��10��#���  �� -    ��Name$ border��$��10��$��3q����#EEF5FD._�*   $HBBio.Communication.ProcessPictureUC  -     Name$ processPictureUC��� $��12�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -   $
 btnOK��$��1�� 	 btnOK#���  �� -   $
 	btnCancel��$��3��  	btnCancel#���  