   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.EvaluationHBBio.Evaluation  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  5   	   7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  5	   	   5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  5
   	   $    HBBio.Evaluation.EvaluationWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.Evaluation     5   	   +shareclr-namespace:HBBio.Share     5   	   .ATclr-namespace:HBBio.AuditTrails     5   	   9chromatogram clr-namespace:HBBio.Chromatogram     5	   	   7auditTrailsclr-namespace:HBBio.AuditTrails     5
   	   -   5           E_EvaluationWin  � Title#  �   5   	   $	��500��63   $	��750��6@    � WindowState$ 	Maximized=�6L   .��5      � -   5         �Name$
 grid��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��1 �6   �� 5      $
��Auto �6   �� 5      $��1 �6   �� 5      $��* �6   �� 5      $��1 �6   �� 5      $
��Auto �6   �� 5      $��1 �6   �� 5      $
��Auto �6   .+�5   
   c� -   5         c�Name$ toolBar��5      $��2��6#   .@�5         HBBio.Share.IconButton  -   5          Name$ btnOutputSelect��5         HBBio.Share.IconButton   Click$ btnOutputSelect_Click��6c     E_OutputSelect � ToolTip# �  68   $��5q�6�      Image$/ )/Bio-LabChrom;component/image/message.png��6�     5      -   5        $ btnPrint��5      $ btnPrint_Click��6V     btnPrint# �  61   $��5q�6m   $+ %/Bio-LabChrom;component/Image/pdf.png��6x     5      -   5        $ btnPrintExcel��5      $ btnPrintExcel_Click��6[   # �  66   $��5q�6w   $- '/Bio-LabChrom;component/Image/excel.png��6�     5      -   5        $ btnIntegration��5      $ btnIntegration_Click��6a     E_Integration# �  67   $��5q�6~   $0 */Bio-LabChrom;component/image/ouputSet.png��6�   "   HBBio.Share.IconToggleButton  5      -   5        	  Name$	 btnPeakWidth��5   %   "   HBBio.Share.IconToggleButton
   Click$
 btnPeakWidth_Click��6c     E_PeakWidth# �  6;   $��5q�6~      Image$6 0/Bio-LabChrom;component/Image/peak/PeakWidth.png��6�     5      -	   5        $	 btnPeakBegin��5   %   $
 btnPeakBegin_Click��6c     E_PeakBegin# �  6;   $��5q�6~   $6 0/Bio-LabChrom;component/Image/peak/PeakStart.png��6�     5       -
   5        $	 
btnPeakEnd��5    %   $
 btnPeakEnd_Click��6_     	E_PeakEnd# �  69   $��5q�6x   $4 ./Bio-LabChrom;component/Image/peak/PeakEnd.png��6�     5!      -   5        $	 
btnPeakAdd��5!   %   $
 btnPeakAdd_Click��6_     	E_PeakAdd# �  69   $��5q�6x   $5 //Bio-LabChrom;component/Image/peak/PlusPeak.png��6�     5"      -   5        $	 
btnPeakDel��5"   %   $
 btnPeakDel_Click��6_     	E_PeakDel# �  69   $��5q�6x   $4 ./Bio-LabChrom;component/Image/peak/DelPeak.png��6�     5#      -   5        $	 btnPeakFrontCut��5#   %   $
 btnPeakFrontCut_Click��6i    	 E_PeakFrontCut# � 	 6>   $��5q�6�   $9 3/Bio-LabChrom;component/Image/peak/PeakFrontCut.png��6�     5$      -   5        $	 btnPeakBackCut��5$   %   $
 btnPeakBackCut_Click��6g    
 E_PeakBackCut# � 
 6=   $��5q�6�   $8 2/Bio-LabChrom;component/Image/peak/PeakBackCut.png��6�     5%      -   5        $ btnPeakSeververtical��5%      $  btnPeakSeververtical_Click��6m     E_PeakSeververtical# �  6=   $��5q�6�   $: 4/Bio-LabChrom;component/Image/peak/Seververtical.png��6�     5&      -   5        $ btnPeakSeverPeakValley��5&      $" btnPeakSeverPeakValley_Click��6q     E_PeakSeverPeakValley# �  6?   $��5q�6�   $< 6/Bio-LabChrom;component/Image/peak/SeverPeakValley.png��6�   5'      '   !HBBio.Chromatogram.ChromatogramUC  5(   
   -   5          Name$ chromatogramUC��5(   &   $��4��6>      HBBio.Share.LoadingWaitUC  5)   
   -   5          Name$ loadingWaitUC��5)      $��4��65   $y�	Collapsed=�6B   �� 5*   
   $��6��6   $	��300��6"   .@�5+      ��   E_AuditTrails#���  6   .��5,      +   %HBBio.AuditTrails.AuditTrailsSearchUC  -   5          Name$ auditTrailsSearchUC��5,   2   5-      �� 5.        E_IntegralResult#���  6   .��5/      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvIntegration��5/         
IsReadOnly .    CanUserReorderColumns .     CanUserSortColumns .     Columns 50      0	  *System.Windows.Controls.DataGridTextColumn	  51      -   5        colName��1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 51   >   �� *MName+,  &System.Windows.Controls.DataGridColumn   Header 52      �� 53   "     	E_PI_Name#���  6,   55      	  56      -   5        colRetentionTime�� 56   G   �� *MRetentionTime+ �� StringFormat$ {0:F2}�� 57      �� 58   "    �� Orientation$ 
Horizontal=�6-   .+�59   &   ��   E_PI_RetentionTime#���  60   �� 5:   &   $��(��60   �� 5;   &      HBBio.Share.DlyBase   SC_TUNIT#��Z 60   �� 5<   &   $��)��60   5=   #   5?      	  5@      -   5        
colStartPt�� 5@   A   �� *
MStartValX+$ {0:F2}�� 5A      �� 5B   "   $ 
Horizontal=�6-   .+�5C   &   ��   E_PI_StartPt#���  60   �� 5D   &   $��(��60   �� 5E   &   #��Z 60   �� 5F   &   $��)��60   5G   #   5I      	  5J      -   5        colEndPt�� 5J   ?   �� *
MEndValX+$ {0:F2}�� 5K      �� 5L   "   $ 
Horizontal=�6-   .+�5M   &   ��   
E_PI_EndPt#���  60   �� 5N   &   $��(��60   �� 5O   &   #��Z 60   �� 5P   &   $��)��60   5Q   #   5S      	  5T      -   5        	colTopVal�� 5T   @   �� *	MTopVal+$ {0:F2}�� 5U      �� 5V   "   $ 
Horizontal=�6-   .+�5W   &   ��   E_PI_TopVal#���  60   �� 5X   &   $��(��60   �� 5Y   &   -   5         ��Name$ colTopValUnit��5Y   0   �� 5Z   &   $��)��60   5[   #   5]      	  5^      -   5        colStartVal�� 5^   B   �� *
MStartValY+$ {0:F2}�� 5_      �� 5`   "   $ 
Horizontal=�6-   .+�5a   &   ��   E_PI_StartVal#���  60   �� 5b   &   $��(��60   �� 5c   &   -   5        $ colStartValUnit��5c   0   �� 5d   &   $��)��60   5e   #   5g      	  5h      -   5        	colEndVal�� 5h   @   �� *
MEndValY+$ {0:F2}�� 5i      �� 5j   "   $ 
Horizontal=�6-   .+�5k   &   ��   E_PI_EndVal#���  60   �� 5l   &   $��(��60   �� 5m   &   -   5        $ colEndValUnit��5m   0   �� 5n   &   $��)��60   5o   #   5q      	  5r      -   5        	colHeight�� 5r   @   �� *	MHeight+$ {0:F2}�� 5s      �� 5t   "   $ 
Horizontal=�6-   .+�5u   &   ��   E_PI_Height#���  60   �� 5v   &   $��(��60   �� 5w   &   -    5        $ colHeightUnit��5w   0   �� 5x   &   $��)��60   5y   #   5{      	  5|      -!   5        colArea�� 5|   >   �� *MArea+$ {0:F3}�� 5}      �� 5~   "   $ 
Horizontal=�6-   .+�5   &   ��   	E_PI_Area#���  60   �� 5�   &   $��(��60   �� 5�   &   -"   5        $ colAreaUnit��5�   0   �� 5�   &   $��)��60   5�   #   5�      	  5�      -#   5        
colAreaPer�� 5�   A   �� *
MAreaPer+$ {0:F3}�� 5�      �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   ��   E_PI_AreaPer#���  60   �� 5�   &   $��(��60   �� 5�   &      
SC_FERUNIT#��Z 60   �� 5�   &   $��)��60   5�   #   5�      	  5�      -$   5        colHalfWidth�� 5�   C   �� *
MHalfWidth+$ {0:F3}�� 5�      �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   ��   E_PI_HalfWidth#���  60   �� 5�   &   $��(��60   �� 5�   &   #��Z 60   �� 5�   &   $��)��60   5�   #   5�      	  5�      -%   5        
colTpn�� 5�   =   �� *MTpn+$ {0:F3}�� 5�      �� 5�   "     E_PI_Tpn#���  6,   5�      	  5�      -&   5        colTailingFactor�� 5�   G   �� *MTailingFactor+$ {0:F3}�� 5�      �� 5�   "     E_PI_TailingFactor#���  6,   5�      	  5�      -'   5        colSymmetryFactor�� 5�   H   �� *MSymmetryFactor+$ {0:F3}�� 5�      �� 5�   "     E_PI_SymmetryFactor#���  6,   5�      	  5�      -(   5        colResolution�� 5�   D   �� *MResolution+$ {0:F3}�� 5�      �� 5�   "     E_PI_Resolution#���  6,   5�      5�      5�      �� 5�      -)   5         ��Name$ tabItemMethod��5�        E_Method#���  6-   .��5�      -� -*   5         -�Name$ 	docReader��5�   %    -� ViewingMode$ Scroll=�68   5�      �� 5�      -+   5        $ tabItemAttachment��5�        E_Attachment#���  61   .��5�      �� -,   5        $ txtAttachment��5�      $
��Wrap=�63   $	��Top=�6G   5�      5�      5�      5�      