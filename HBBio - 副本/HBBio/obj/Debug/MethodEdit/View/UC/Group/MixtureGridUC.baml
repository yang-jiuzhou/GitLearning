   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5      )clr-namespace:HBBio.ShareHBBio.Share  5      9!clr-namespace:HBBio.CommunicationHBBio.Communication  5      $    HBBio.MethodEdit.MixtureGridUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:HBBio.MethodEdit     5      +shareclr-namespace:HBBio.Share     5      2comm!clr-namespace:HBBio.Communication     5      -   5          � 	Resources  5         	ColumnInS%   HBBio.Communication.EnumInSInfo   NameList �� SelectedIndex �� Path �� UpdateSourceTrigger  	ColumnInA%   HBBio.Communication.EnumInAInfo   NameList  	ColumnInB%   HBBio.Communication.EnumInBInfo   NameList  	ColumnInC%   HBBio.Communication.EnumInCInfo   NameList  	ColumnInD%   HBBio.Communication.EnumInDInfo   NameList  	ColumnOut%   HBBio.Communication.EnumOutInfo	   NameList  	ColumnIJV%   HBBio.Communication.EnumIJVInfo
   NameList  	ColumnBPV%   HBBio.Communication.EnumBPVInfo   NameList  	ColumnCPV%	   HBBio.Communication.EnumCPVInfo 	  NameList 	 
ColumnWash&
    HBBio.Communication.EnumWashInfo 
  NameList��%  &	        &	 ]     &	 �     &	     &	 t    &	 �    &	 /    &	 �    &	 �    &		 F    �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInS�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInA�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInB�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInC�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInD�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z	 6    6J   �� $ MVOut�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z
 6    6J   �� $
 MIJV�$ 	LostFocus=�5       �� 5!   
   .R�5"      �� #��Z 6    6J   �� $
 MBPV�$ 	LostFocus=�5#      �� 5$   
   .R�5%      �� #��Z 6    6J   �� $
 MCPV�$ 	LostFocus=�5&      �� 5'   
   .R�5(      �� #��Z 6    6K   �� $ MFillSystem�$ 	LostFocus=�5)      5*      .��5+      ��   
 ME_EnumGroupType_MixtureGrid#��� 
 6   .��5,   
   � $��10q�6    � ColumnDefinitions 5-      �� 5.      $
��Auto �6#   �� 5/      $��5 �6#   �� 50      $��* �6#   .+�52      �� $��0��6   .+�53         HBBio.Share.IconButton  -   5          Name$ btnAdd��53   #      HBBio.Share.IconButton   Click$ btnAdd_Click��6a    
 btnAdd � ToolTip# �  63   $��5q�6V      Image$+ %/Bio-LabChrom;component/Image/add.png��6v     54      -   5        $ btnEdit��54   #   $ btnEdit_Click��6c     btnEdit# �  64   $��5q�6X   $, &/Bio-LabChrom;component/Image/edit.png��6y     55      -   5        $ btnDel��55   #   $ btnDel_Click��6a    
 btnDel# �  63   $��5q�6V   $. (/Bio-LabChrom;component/Image/delete.png��6v     56      -   5        $ btnUp��56   #   $ btnUp_Click��6_    	 btnUp# �  62   $��5q�6T   $5 //Bio-LabChrom;component/Image/upArrowCircle.png��6s     57      -   5        $ btnDown��57   #   $ btnDown_Click��6c     btnDown# �  64   $��5q�6X   $7 1/Bio-LabChrom;component/Image/downArrowCircle.png��6y     58      -   5        $ btnCopy��58   #   $ btnCopy_Click��6c     btnCopy# �  64   $��5q�6X   $, &/Bio-LabChrom;component/Image/copy.PNG��6y     59      -   5        $ btnPaste��59   #   $ btnPaste_Click��6e     btnPaste# �  65   $��5q�6Z   $- '/Bio-LabChrom;component/Image/paste.PNG��6|   5:      &   System.Windows.Controls.DataGrid  5;      -	   5          Name$	 dgv��5;      $��2��6$      FrozenColumnCount$ 1��64      CanUserReorderColumns .     CanUserSortColumns .     Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=        labNote,  &System.Windows.Controls.DataGridColumn   Header# [ 6C   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *MNote+6C   4  .System.Windows.Controls.DataGridTemplateColumn  5>      -
   5        colWash��   CellTemplate# [	 5>   >     labFillSystem# [ 6i     5?        ME_IncubationTime# [ 6g    6)   �� *MIncubation+$ 	LostFocus=�6g     5@      -   5        colTVCV�� 5@   :   �� $ 	MBaseTVCV�$ 	LostFocus=�   HeaderTemplate 5A      �� 5B      .R�5C   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5D   &   �� ��60   �� $ DataContext.MBaseStr� �� RelativeSource ��  �� AncestorType"  �� 5E   &   $��(��60   �� 5F   &   ��60   �� $ DataContext.MBaseUnitStr� �� "  �� 5G   &   $��)��60   5H   #   5I      5K        5L      -   5        colSampleFlowRate�� 5L   D   �� $ MFlowVolLenSample�$ 	LostFocus=� 5M      �� 5N      .R�5O   "   �� $ 
Horizontal=�6-   .+�5P   &   ��   labSampleFlowRate#��[ 60   �� 5Q   &   $��(��60   �� 5R   &   ��60   �� $" DataContext.MFlowRateUnitStr� �� "  �� 5S   &   $��)��60   5T   #   5U      5W        5X      -   5        colSystemFlowRate�� 5X   D   �� $ MFlowVolLenSystem�$ 	LostFocus=� 5Y      �� 5Z      .R�5[   "   �� $ 
Horizontal=�6-   .+�5\   &   ��   labSystemFlowRate#��[ 60   �� 5]   &   $��(��60   �� 5^   &   ��60   �� $" DataContext.MFlowRateUnitStr� �� "  �� 5_   &   $��)��60   5`   #   5a      5c        5d      -   5        	colBS�� 	 LabBS# [ 5d   q    68   �� *MPerBS+$ 	LostFocus=�6q     5e      -   5        	colBE�� 	 LabBE# [ 5e   q    68   �� *MPerBE+$ 	LostFocus=�6q     5f      -   5        	colCS�� 	 LabCS# [ 5f   q    68   �� *MPerCS+$ 	LostFocus=�6q     5g      -   5        	colCE�� 	 LabCE# [ 5g   q    68   �� *MPerCE+$ 	LostFocus=�6q     5h      -   5        	colDS�� 	 LabDS# [ 5h   q    68   �� *MPerDS+$ 	LostFocus=�6q     5i      -   5        	colDE�� 	 LabDE# [ 5i   q    68   �� *MPerDE+$ 	LostFocus=�6q     5j      -   5        
colInS��# [  5j   =    
 labInS# [ 6g     5k      -   5        
colInA��# [ 5k   =    
 labInA# [ 6g     5l      -   5        
colInB��# [ 5l   =    
 labInB# [ 6g     5m      -   5        
colInC��# [ 5m   =    
  labInC# [  6g     5n      -   5        
colInD��# [ 5n   =    
! labInD# [! 6g     5o      -   5        
colIJV��# [ 5o   =    
" labIJV# [" 6g     5p      -   5        
colBPV��# [ 5p   =    
# labBPV# [# 6g     5q      -   5        
colCPV��# [ 5q   =    
$ labCPV# [$ 6g     5r      -   5        
colOut��# [ 5r   =    
% labOut# [% 6g     5s      -   5        colAS01�� & labAS01# [& 5s   >    5t      �� 5u      .R�5v   "   �� $   HBBio.MethodEdit.MixtureGridUC   MListAS#��Z 6+     �� DisplayMemberPath$  MString��6`   ! �� SelectedValuePath$! MEnum��6|   " �� SelectedValue" 6�   �� $ 	MAction01�# �� Mode$# TwoWay=�$ 	LostFocus=�5w      5y        5z      -   5        colAS02�� ' labAS02# [' 5z   ?    5{      �� 5|      .R�5}   "   �� #��Z 6+   $  MString��6`   $! MEnum��6|   " 6�   �� $ 	MAction02�$# TwoWay=�$ 	LostFocus=�5~      5�        5�      -   5        colAS03�� ( labAS03# [( 5�   >    5�      �� 5�      .R�5�   "   �� #��Z 6+   $  MString��6`   $! MEnum��6|   " 6�   �� $ 	MAction03�$# TwoWay=�$ 	LostFocus=�5�      5�        5�      -    5        colAS04�� ) labAS04# [) 5�   ?    5�      �� 5�      .R�5�   "   �� #��Z 6+   $  MString��6`   $! MEnum��6|   " 6�   �� $ 	MAction04�$# TwoWay=�$ 	LostFocus=�5�      5�      4  .System.Windows.Controls.DataGridCheckBoxColumn  5�      -!   5        colMixer�� * labMixer# [* 5�   Z    6?   �� *MMixer+6Z     5�      -"   5        
colUVClear�� 5�   A   �� *
MUVClear+ 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   .+�5�   &   ��  	+ labUV#��[+ 60   �� 5�   &    , 
labUVReset#��[, 60   5�   #   5�      5�      5�      5�      5�      5�      