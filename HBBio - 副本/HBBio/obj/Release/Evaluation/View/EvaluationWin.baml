   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.EvaluationHBBio.Evaluation  )clr-namespace:HBBio.ShareHBBio.Share  5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  $    HBBio.Evaluation.EvaluationWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.Evaluation     +shareclr-namespace:HBBio.Share     .ATclr-namespace:HBBio.AuditTrails     9chromatogram clr-namespace:HBBio.Chromatogram     7auditTrailsclr-namespace:HBBio.AuditTrails     -      E_EvaluationWin  � Title#  �   $	��500��$	��750�� � WindowState$ 	Maximized=�.��� -    �Name$
 grid�� � RowDefinitions �� $
��Auto ��� $��1 ��� $
��Auto ��� $��1 ��� $��* ��� $��1 ��� $
��Auto ��� $��1 ��� $
��Auto �.+�c� -    c�Name$ toolBar��$��2��.@�   HBBio.Share.IconButton  -     Name$ btnOutputSelect��   HBBio.Share.IconButton   Click$ btnOutputSelect_Click��  E_OutputSelect � ToolTip# �  $��5q�   Image$/ )/Bio-LabChrom;component/image/message.png��  -   $ btnPrint��$ btnPrint_Click��  btnPrint# �  $��5q�$+ %/Bio-LabChrom;component/Image/pdf.png��  -   $ btnPrintExcel��$ btnPrintExcel_Click��# �  $��5q�$- '/Bio-LabChrom;component/Image/excel.png��  -   $ btnIntegration��$ btnIntegration_Click��  E_Integration# �  $��5q�$0 */Bio-LabChrom;component/image/ouputSet.png��"   HBBio.Share.IconToggleButton  -   	  Name$	 btnPeakWidth��"   HBBio.Share.IconToggleButton
   Click$
 btnPeakWidth_Click��  E_PeakWidth# �  $��5q�   Image$6 0/Bio-LabChrom;component/Image/peak/PeakWidth.png��  -	   $	 btnPeakBegin��$
 btnPeakBegin_Click��  E_PeakBegin# �  $��5q�$6 0/Bio-LabChrom;component/Image/peak/PeakStart.png��  -
   $	 
btnPeakEnd��$
 btnPeakEnd_Click��  	E_PeakEnd# �  $��5q�$4 ./Bio-LabChrom;component/Image/peak/PeakEnd.png��  -   $	 
btnPeakAdd��$
 btnPeakAdd_Click��  	E_PeakAdd# �  $��5q�$5 //Bio-LabChrom;component/Image/peak/PlusPeak.png��  -   $	 
btnPeakDel��$
 btnPeakDel_Click��  	E_PeakDel# �  $��5q�$4 ./Bio-LabChrom;component/Image/peak/DelPeak.png��  -   $	 btnPeakFrontCut��$
 btnPeakFrontCut_Click�� 	 E_PeakFrontCut# � 	 $��5q�$9 3/Bio-LabChrom;component/Image/peak/PeakFrontCut.png��  -   $	 btnPeakBackCut��$
 btnPeakBackCut_Click�� 
 E_PeakBackCut# � 
 $��5q�$8 2/Bio-LabChrom;component/Image/peak/PeakBackCut.png��  -   $ btnPeakSeververtical��$  btnPeakSeververtical_Click��  E_PeakSeververtical# �  $��5q�$: 4/Bio-LabChrom;component/Image/peak/Seververtical.png��  -   $ btnPeakSeverPeakValley��$" btnPeakSeverPeakValley_Click��  E_PeakSeverPeakValley# �  $��5q�$< 6/Bio-LabChrom;component/Image/peak/SeverPeakValley.png��'   !HBBio.Chromatogram.ChromatogramUC  -     Name$ chromatogramUC��$��4��   HBBio.Share.LoadingWaitUC  -     Name$ loadingWaitUC��$��4��$y�	Collapsed=��� $��6��$	��300��.@���   E_AuditTrails#���  .��+   %HBBio.AuditTrails.AuditTrailsSearchUC  -     Name$ auditTrailsSearchUC����   E_IntegralResult#���  .��&   System.Windows.Controls.DataGrid  -     Name$ dgvIntegration��   
IsReadOnly .    CanUserReorderColumns .     CanUserSortColumns .     Columns 0	  *System.Windows.Controls.DataGridTextColumn	  -   colName��1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *MName+,  &System.Windows.Controls.DataGridColumn   Header ��   	E_PI_Name#���  	  -   colRetentionTime�� �� *MRetentionTime+ �� StringFormat$ {0:F2}�� ��  �� Orientation$ 
Horizontal=�.+���   E_PI_RetentionTime#���  �� $��(����    HBBio.Share.DlyBase   SC_TUNIT#��Z �� $��)��	  -   
colStartPt�� �� *
MStartValX+$ {0:F2}�� �� $ 
Horizontal=�.+���   E_PI_StartPt#���  �� $��(���� #��Z �� $��)��	  -   colEndPt�� �� *
MEndValX+$ {0:F2}�� �� $ 
Horizontal=�.+���   
E_PI_EndPt#���  �� $��(���� #��Z �� $��)��	  -   	colTopVal�� �� *	MTopVal+$ {0:F2}�� �� $ 
Horizontal=�.+���   E_PI_TopVal#���  �� $��(���� -    ��Name$ colTopValUnit���� $��)��	  -   colStartVal�� �� *
MStartValY+$ {0:F2}�� �� $ 
Horizontal=�.+���   E_PI_StartVal#���  �� $��(���� -   $ colStartValUnit���� $��)��	  -   	colEndVal�� �� *
MEndValY+$ {0:F2}�� �� $ 
Horizontal=�.+���   E_PI_EndVal#���  �� $��(���� -   $ colEndValUnit���� $��)��	  -   	colHeight�� �� *	MHeight+$ {0:F2}�� �� $ 
Horizontal=�.+���   E_PI_Height#���  �� $��(���� -    $ colHeightUnit���� $��)��	  -!   colArea�� �� *MArea+$ {0:F3}�� �� $ 
Horizontal=�.+���   	E_PI_Area#���  �� $��(���� -"   $ colAreaUnit���� $��)��	  -#   
colAreaPer�� �� *
MAreaPer+$ {0:F3}�� �� $ 
Horizontal=�.+���   E_PI_AreaPer#���  �� $��(����    
SC_FERUNIT#��Z �� $��)��	  -$   colHalfWidth�� �� *
MHalfWidth+$ {0:F3}�� �� $ 
Horizontal=�.+���   E_PI_HalfWidth#���  �� $��(���� #��Z �� $��)��	  -%   
colTpn�� �� *MTpn+$ {0:F3}�� ��   E_PI_Tpn#���  	  -&   colTailingFactor�� �� *MTailingFactor+$ {0:F3}�� ��   E_PI_TailingFactor#���  	  -'   colSymmetryFactor�� �� *MSymmetryFactor+$ {0:F3}�� ��   E_PI_SymmetryFactor#���  	  -(   colResolution�� �� *MResolution+$ {0:F3}�� ��   E_PI_Resolution#���  �� -)    ��Name$ tabItemMethod��  E_Method#���  .��-� -*    -�Name$ 	docReader�� -� ViewingMode$ Scroll=��� -+   $ tabItemAttachment��  E_Attachment#���  .���� -,   $ txtAttachment��$
��Wrap=�$	��Top=�