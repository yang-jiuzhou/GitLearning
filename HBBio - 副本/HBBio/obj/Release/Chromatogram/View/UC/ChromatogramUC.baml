   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  '    !HBBio.Chromatogram.ChromatogramUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:HBBio.Chromatogram     -   .����    BorderBaseRead#��[  ._�]� -     ]�Name$  	dockPanel��.+�� -    �Name$ 
stackPanel��$	��Top=� � Orientation$ 
Horizontal=��� -    ��Name$ canvas��
�������.+��� -    ��Name$ image���� -    ��Name$ btnMenu��$��>>��$��28��$��28�����#BFEEFF��Z�  � ContextMenu �� -    ��Name$
 menu��.@�m� -    m�Name$ menuUndoZoom��  labD_UndoZoom#���  m� -	   $ menuResetZoom��  labD_ResetZoom#���  �� m� -
   $ menuDrag��  	labD_Drag#���  	 m� IsCheckable	 . 
 m� 	IsChecked
 .  �� m� -   $ menuGrid��  	labD_Grid#���  	 . 
 .  m� -   $ 
menuMultiY��  labD_MultiY#���  	 . 
 .  m�   
labD_Ruler#���  .@�m� -   $ menuRulerOddO��  labD_RulerOddO#���  	 . m� -   $ menuRulerOddM��  labD_RulerOddM#���  	 . m� -   $ menuRulerEven�� 	 labD_RulerEven#��� 	 	 . �� m� -   $ 
menuMarker�� 
 labD_Marker#��� 
 .@�m� -   $ menuMarkerVisibility��  labVisibility#���  	 . m� -   $ menuMarkerColor��  labColor#���  m�   labDirection#���  .@�m� -   $ menuMarkerDirectionH��  labDirectionH#���  	 . m� -   $ menuMarkerDirectionV��  labDirectionV#���  	 . m� -   $ menuMarkerAdd�� 
 btnAdd#���  m� -   $ menuMarkerDel�� 
 btnDel#���  m� -   $ menuColl��  	labD_Coll#���  .@�m� -   $ 	menuCollM��  
labD_CollM#���  .@�m� -   $ menuCollMVisibility��#���  	 . m� -   $ menuCollMColor��#���  m� #���  .@�m� -   $ menuCollMDirectionH��#���  	 . m� -   $ menuCollMDirectionV��#���  	 . m� -   $ 	menuCollA��  
labD_CollA#���  .@�m� -   $ menuCollAVisibility��#���  	 . m� -   $ menuCollAColor��#���  m� #���  .@�m� -    $ menuCollADirectionH��#���  	 . m� -!   $ menuCollADirectionV��#���  	 . m� -"   $ 	menuValve��  
labD_Valve#���  .@�m� -#   $ menuValveVisibility��#���  	 . m� -$   $ menuValveColor��#���  m� #���  .@�m� -%   $ menuValveDirectionH��#���  	 . m� -&   $ menuValveDirectionV��#���  	 . m� -'   $ 	menuPhase��  
labD_Phase#���  .@�m� -(   $ menuPhaseVisibility��#���  	 . m� -)   $ menuPhaseColor��#���  m� #���  .@�m� -*   $ menuPhaseDirectionH��#���  	 . m� -+   $ menuPhaseDirectionV��#���  	 . �� m� -,   $ 	menuCurve��  D_Curve#���  �� m� --   $ menuBG��  D_BG#���  .@�m� -.   $ 	menuBGAdd��#���  m� -/   $ 	menuBGDel��#���  m� -0   $ 
menuOutput�� 	 D_PNG#���  �� -1    ��Name$ rectZoom��$y�	Collapsed=�
���   ��� -2    ��Name$ lineRulerOdd��
���   �$y�	Collapsed=�$��1��	 �� Y1$ 40���� -3   $ lineRulerEven��
���   �$y�	Collapsed=�$��1��$ 40���� -4    ��Name$ txtRulerInfo��$y�	Collapsed=�$	��150��$
��Auto��$
��Wrap=��� -5   $ linePeakStart��
����  �$y�	Collapsed=� �� StrokeDashArray$	 5,5R�$��5��	 �� X1$ 70��	 �� X2$ 70���� -6   $ txtPeakStart��$
��Wrap=�
 �� Top$ 0��y���  �� ElementName$ linePeakStart�� �� Path$ 
Visibility��� -7   $ linePeakEnd��
����  �$y�	Collapsed=�$	 5,5R�$��5��$	 100��$	 100���� -8   $ 
txtPeakEnd��$
��Wrap=�$ 0��y��� $ linePeakEnd��$ 
Visibility�