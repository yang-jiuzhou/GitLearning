   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  )clr-namespace:HBBio.ShareHBBio.Share      HBBio.Manual.CollectorWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  &localclr-namespace:HBBio.Manual  %shareclr-namespace:HBBio.Share  -      M_CollectorWin  � Title#  �    � SizeToContent$ WidthAndHeight=����#EEF5FD J� Opacity$	 0.8Q� � AllowsTransparency .  � WindowStyle$
 None=�.���� ���	#FF268DE4$��2q� �� CornerRadius$ 5��._�� $��2q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�����	#FF9DBBCD � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��25��$��25��$-��'/Bio-LabChrom;component/Image/hblog.ico���� $��2��#���   $
��Bold"��� -    ��Name$ btnClose��$��4��  closeBtnStyle#��[ �� $��2��  M_CollIndexStatusShow#���  .��� $��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��  M_CollIndex1#���  �� -   	 ��Name$	 cboxIndexShow��$��2��$	��100��
 �� DisplayMemberPath$
 MName�� �� SelectedValuePath$ MName��{�.   �� SelectedValue �� *	MIndexGet+�� $��4��  M_CollStatus1#���      HBBio.Share.SwitchButtonUC  -     Name$ sbtnStatusShow��$��6��{�.     	IsChecked �� *
MStatusGet+�� $��4��  M_CollIndexStatusSet#���  .��� $��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+�	� -    	�Name$ rbtnRealSingle��$��0��  labReal#���   d� 	IsChecked$
 TrueY�	� -   $ rbtnDelaySingle��$��2��  labDelay#���  �� -    ��Name$ labIndex��$��4��#���  �� -   $	 	cboxIndex��$��6��$	��100��$
 MName��$ MName���� -	   $ 	labStatus��$��8��#���    -
   $ 
sbtnStatus��    HBBio.Share.SwitchButtonUC   	MouseDown$ sbtnStatus_MouseDown��   
MouseEnter$ sbtnStatus_MouseEnter��   
MouseLeave$ sbtnStatus_MouseLeave��$��10��   HBBio.Share.IconButton  -     Name$ btnFront��   HBBio.Share.IconButton   Click$ btnFront_Click��$��12��  btnPrev � ToolTip# �     Image$, &/Bio-LabChrom;component/Image/prev.png��  -   $ btnBack��$ btnBack_Click��$��14�� 	 btnNext# � 	 $, &/Bio-LabChrom;component/Image/back.png���� -    ��Name$ groupMultipleSelect��$��6�� 
 M_OutWin_MultipleSelect#��� 
 .��� $��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+�	� -   $ rbtnRealMultiple��$��0��#���  	� -   $ rbtnDelayMultiple��$��2��#���  �� $��4��  M_OutWin_Start1#���  �� -   $	 
cboxSelect��$��6��$
 MName���� $��8��  M_OutWin_Vol1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ 	doubleVol��$��10���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$
 0.01X��� -   $ btnStart��$��12�� 
 btnSet#���  �� -   $ btnStop��$��14��  	btnCancel#���  �� $��8��.+��� -   $ btnIntervene��$��5q�$��Right=�  M_CollInterveneSet#���  