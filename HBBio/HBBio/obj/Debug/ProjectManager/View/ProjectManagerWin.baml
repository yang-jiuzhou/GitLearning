   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.ProjectManagerHBBio.ProjectManager  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   ,    &HBBio.ProjectManager.ProjectManagerWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.ProjectManager     5   	   +shareclr-namespace:HBBio.Share     5   	   5
methodeditclr-namespace:HBBio.MethodEdit     5   	   -   5           PM_ProjectManagerWin  � Title#  �   5
   	   $	��500��68   $	��800��6E   $	��461��6Q   $	��750��6a    � WindowStartupLocation$ CenterScreen=�6p   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $	��500 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   (   "HBBio.ProjectManager.ProjectTreeUC  -   5          Name$ projectTreeUC��5      $��0��65   �� 5   
   -   5         ��Name$ 
tabControl��5      $��2��6)   .@�5      �� -   5         ��Name$ 	tabMethod��5        PM_MethodEditor#���  6)   .��5      ]� y�6   ��  �� ElementName$ 	tabMethod�� �� Path$ 
Visibility�.+�5      �� $	��Top=�6!   	 �� Orientation$	 
Horizontal=�66   .+�5         HBBio.Share.IconButton  -   5        
  Name$
 btnNewMethod��5   +      HBBio.Share.IconButton   Click$ btnNewMethod_Click��6u     PM_AddMethod � ToolTip# �  6A   $��5q�6j      Image$+ %/Bio-LabChrom;component/Image/add.png��6�     5      -   5        $
 btnNewMethodQueue��5   +   $ btnNewMethodQueue_Click��6     PM_AddMethodQueue# �  6F   $��5q�6t   $+ %/Bio-LabChrom;component/Image/add.png��6�     5      -   5        $
 btnRenameMethod��5   +   $ btnRenameMethod_Click��6u     	btnRename# �  6D   $��5q�6j   $. (/Bio-LabChrom;component/Image/rename.PNG��6�     5      -   5        $
 btnEditMethod��5   +   $ btnEditMethod_Click��6q     btnEdit# �  6B   $��5q�6f   $, &/Bio-LabChrom;component/Image/edit.PNG��6�     5      -	   5        $
 btnDelMethod��5   +   $ btnDelMethod_Click��6o    
 btnDel# �  6A   $��5q�6d   $. (/Bio-LabChrom;component/Image/delete.png��6�     5      -
   5        $
 btnCopyMethod��5   +   $ btnCopyMethod_Click��6q     btnCopy# �  6B   $��5q�6f   $, &/Bio-LabChrom;component/Image/copy.PNG��6�     5      -   5        $
 btnPasteMethod��5   +   $ btnPasteMethod_Click��6�     btnPaste# �  6C   $��5q�6h   {�.  $- '/Bio-LabChrom;component/Image/paste.PNG��6�     5       -   5        $
 btnPrintMethod��5    +   $ btnPrintMethod_Click��6u    	 btnPrint# � 	 6C   $	��5,5q�6h   $+ %/Bio-LabChrom;component/Image/pdf.png��6�     5!      -   5        $
 btnExportMethod��5!   +   $ btnExportMethod_Click��6w    
 	btnExport# � 
 6D   $	��5,5q�6j   $. (/Bio-LabChrom;component/Image/export.png��6�     5"      -   5        $
 btnImportMethod��5"   +   $ btnImportMethod_Click��6w     	btnImport# �  6D   $	��5,5q�6j   $. (/Bio-LabChrom;component/Image/import.png��6�     5#      -   5        $
 btnSendMethod��5#   +   $ btnSendMethod_Click��6q     btnSend# �  6B   $��5q�6f   $, &/Bio-LabChrom;component/Image/send.PNG��6�   5$      � 5%      $��5q�6   $	��Top=�6&    5&      �� 5'      $
��Auto �6/   �� 5(      $��5 �6/   �� 5)      $��* �6/   .+�5+      �� $��0��6$     	E_Filter1#���  64   �� 5,      -   5         ��Name$ txtFilterMethod��5,   "   $��2��6;   6K   5-      �� 5.      -   5         ��Name$ 
listMethod��5.       �� SelectionMode$ Extended=�62   ��5/      �� 50      .R�51   "   �� $	 Vertical=�6-   .+�52   &   �� $	 
Horizontal=�61   $��30��6J   .+�53   *   �� $
��Fill=�60   ��6?   �� *MIcon+�� 54   *    �� Padding$ 5q�6K   ��64   �� *MName+6K   55   '   �� 56   &   $ txtRenameMethod��6.   -   5        $y�	Collapsed=�56   ^   ��6G   �� *MName+6u   57   #   58      5:      5;      5<      �� 5=      -   5        $ tabEvaluation��5=        PM_Evaluation#���  6-   .��5>      ]� y�6   �� $ tabEvaluation��$ 
Visibility�.+�5?      ]� $	��Top=�6    .+�5@        -   5        $
 btnOpenResult��5@   +   $ btnOpenResult_Click��6q     btnOpen# �  6B   $��5q�6f   $, &/Bio-LabChrom;component/Image/open.PNG��6�     5A      -   5        $
 btnContrastResult��5A   +   $ btnContrastResult_Click��6x     
btnCompare# �  6F   $��5q�6m   $0 */Bio-LabChrom;component/Image/contrast.PNG��6�     5B      -   5        $
 btnBG��5B   +   $ btnBG_Click��6f     D_BG# �  6:   $��5q�6[   $, &/Bio-LabChrom;component/Image/send.PNG��6z     5C      -   5        $
 btnRenameResult��5C   +   $ btnRenameResult_Click��6u   # �  6D   $��5q�6j   $. (/Bio-LabChrom;component/Image/rename.PNG��6�   �� 5D      5E      � 5F      $��5q�6   $	��Top=�6&    5G      �� 5H      $
��Auto �6/   �� 5I      $��5 �6/   �� 5J      $��* �6/   .+�5L      �� $��0��6$   #���  64   �� 5M      -   5        $ txtFilterResult��5M   "   $��2��6;   6K   5N      �� 5O      -   5        $ 
listResult��5O      $��Stretch=�62   ��5P      �� 5Q      .R�5R   "   �� $	 Vertical=�6-   .+�5S   &   �� $	 
Horizontal=�61   $��30��6J   
������ .+�5T   *   �� $��30��6P   $��30��6^    d� 	IsChecked 63   �� *MCheck+6^   �� 5U   *   $
��Fill=�60   ��6?   �� *MIcon+�� 5V   *   ��64   �� *MName+ 5W   *   � 5X   .    5Y   2   �� 5Z   6   $
��Auto �6G   �� 5[   6   $��5 �6G   �� 5\   6   $
��Auto �6G    5^   2   �� 5_   6   $
��Auto �6D   �� 5`   6   $��5 �6D   �� 5a   6   $
��Auto �6D   �� 5b   6   $��5 �6D   �� 5c   6   $
��Auto �6D   .+�5e   2   �� $��0��6<   $��0��6I     labStartTime1#���  6Y   �� 5f   2   $��0��6<   $��2��6I   ��6Y   �� *MBeginTimeStr+�� 5g   2   $��2��6<   $��0��6I     labEndTime1#���  6Y   �� 5h   2   $��2��6<   $��2��6I   ��6Y   �� *MEndTimeStr+�� 5i   2   $��4��6<   $��0��6I     labUser1#���  6Y   �� 5j   2   $��4��6<   $��2��6I   ��6Y   �� *	MUserName+5k   /   5m   '   �� 5n   &   $ txtRenameResult��6.   -   5        $y�	Collapsed=�5n   ^   ��6G   �� *MName+6u   5o   #   5p      5r      5s      5t      5u      5v      5w      