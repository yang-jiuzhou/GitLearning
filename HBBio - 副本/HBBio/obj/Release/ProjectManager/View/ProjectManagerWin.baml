   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.ProjectManagerHBBio.ProjectManager  )clr-namespace:HBBio.ShareHBBio.Share  3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  ,    &HBBio.ProjectManager.ProjectManagerWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.ProjectManager     +shareclr-namespace:HBBio.Share     5
methodeditclr-namespace:HBBio.MethodEdit     -      PM_ProjectManagerWin  � Title#  �   $	��500��$	��800��$	��461��$	��750�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $��* ��� $��5 ��� $	��500 � � RowDefinitions �� $��* �.+�(   "HBBio.ProjectManager.ProjectTreeUC  -     Name$ projectTreeUC��$��0���� -    ��Name$ 
tabControl��$��2��.@��� -    ��Name$ 	tabMethod��  PM_MethodEditor#���  .��]� y���  �� ElementName$ 	tabMethod�� �� Path$ 
Visibility�.+��� $	��Top=�	 �� Orientation$	 
Horizontal=�.+�   HBBio.Share.IconButton  -   
  Name$
 btnNewMethod��   HBBio.Share.IconButton   Click$ btnNewMethod_Click��  PM_AddMethod � ToolTip# �  $��5q�   Image$+ %/Bio-LabChrom;component/Image/add.png��  -   $
 btnNewMethodQueue��$ btnNewMethodQueue_Click��  PM_AddMethodQueue# �  $��5q�$+ %/Bio-LabChrom;component/Image/add.png��  -   $
 btnRenameMethod��$ btnRenameMethod_Click��  	btnRename# �  $��5q�$. (/Bio-LabChrom;component/Image/rename.PNG��  -   $
 btnEditMethod��$ btnEditMethod_Click��  btnEdit# �  $��5q�$, &/Bio-LabChrom;component/Image/edit.PNG��  -	   $
 btnDelMethod��$ btnDelMethod_Click�� 
 btnDel# �  $��5q�$. (/Bio-LabChrom;component/Image/delete.png��  -
   $
 btnCopyMethod��$ btnCopyMethod_Click��  btnCopy# �  $��5q�$, &/Bio-LabChrom;component/Image/copy.PNG��  -   $
 btnPasteMethod��$ btnPasteMethod_Click��  btnPaste# �  $��5q�{�.  $- '/Bio-LabChrom;component/Image/paste.PNG��  -   $
 btnPrintMethod��$ btnPrintMethod_Click�� 	 btnPrint# � 	 $	��5,5q�$+ %/Bio-LabChrom;component/Image/pdf.png��  -   $
 btnExportMethod��$ btnExportMethod_Click�� 
 	btnExport# � 
 $	��5,5q�$. (/Bio-LabChrom;component/Image/export.png��  -   $
 btnImportMethod��$ btnImportMethod_Click��  	btnImport# �  $	��5,5q�$. (/Bio-LabChrom;component/Image/import.png��  -   $
 btnSendMethod��$ btnSendMethod_Click��  btnSend# �  $��5q�$, &/Bio-LabChrom;component/Image/send.PNG��� $��5q�$	��Top=� �� $
��Auto ��� $��5 ��� $��* �.+��� $��0��  	E_Filter1#���  �� -    ��Name$ txtFilterMethod��$��2���� -    ��Name$ 
listMethod�� �� SelectionMode$ Extended=����� .R��� $	 Vertical=�.+��� $	 
Horizontal=�$��30��.+��� $
��Fill=����� *MIcon+��  �� Padding$ 5q����� *MName+�� $ txtRenameMethod��-   $y�	Collapsed=����� *MName+�� -   $ tabEvaluation��  PM_Evaluation#���  .��]� y��� $ tabEvaluation��$ 
Visibility�.+�]� $	��Top=�.+�  -   $
 btnOpenResult��$ btnOpenResult_Click��  btnOpen# �  $��5q�$, &/Bio-LabChrom;component/Image/open.PNG��  -   $
 btnContrastResult��$ btnContrastResult_Click��  
btnCompare# �  $��5q�$0 */Bio-LabChrom;component/Image/contrast.PNG��  -   $
 btnBG��$ btnBG_Click��  D_BG# �  $��5q�$, &/Bio-LabChrom;component/Image/send.PNG��  -   $
 btnRenameResult��$ btnRenameResult_Click��# �  $��5q�$. (/Bio-LabChrom;component/Image/rename.PNG���� � $��5q�$	��Top=� �� $
��Auto ��� $��5 ��� $��* �.+��� $��0��#���  �� -   $ txtFilterResult��$��2���� -   $ 
listResult��$��Stretch=����� .R��� $	 Vertical=�.+��� $	 
Horizontal=�$��30��
������ .+��� $��30��$��30�� d� 	IsChecked �� *MCheck+�� $
��Fill=����� *MIcon+�� ���� *MName+ �  �� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��$��0��  labStartTime1#���  �� $��0��$��2������ *MBeginTimeStr+�� $��2��$��0��  labEndTime1#���  �� $��2��$��2������ *MEndTimeStr+�� $��4��$��0��  labUser1#���  �� $��4��$��2������ *	MUserName+�� $ txtRenameResult��-   $y�	Collapsed=����� *MName+