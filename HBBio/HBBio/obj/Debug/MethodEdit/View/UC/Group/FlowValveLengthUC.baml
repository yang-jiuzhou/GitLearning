   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5      )clr-namespace:HBBio.ShareHBBio.Share  5      9!clr-namespace:HBBio.CommunicationHBBio.Communication  5      (    "HBBio.MethodEdit.FlowValveLengthUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:HBBio.MethodEdit     5      +shareclr-namespace:HBBio.Share     5      2comm!clr-namespace:HBBio.Communication     5      -   5          � 	Resources  5         	ColumnInA%   HBBio.Communication.EnumInAInfo   NameList �� SelectedIndex �� Path �� UpdateSourceTrigger  	ColumnInB%   HBBio.Communication.EnumInBInfo   NameList  	ColumnInC%   HBBio.Communication.EnumInCInfo   NameList  	ColumnInD%   HBBio.Communication.EnumInDInfo   NameList  	ColumnOut%   HBBio.Communication.EnumOutInfo   NameList  	ColumnBPV%   HBBio.Communication.EnumBPVInfo	   NameList  
ColumnWash&    HBBio.Communication.EnumWashInfo
   NameList��%�  &	        &	 ]     &	 �     &	     &	 t    &	 �    &	 /    �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInA�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInB�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInC�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $
 MInD�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z 6    6J   �� $ MVOut�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z	 6    6J   �� $
 MBPV�$ 	LostFocus=�5      �� 5   
   .R�5      �� #��Z
 6    6K   �� $ MFillSystem�$ 	LostFocus=�5       5!      .��5"      ��  $  ME_EnumGroupType_FlowValveLength#���  6   .��5#   
   � $��10q�6    � ColumnDefinitions 5$      �� 5%      $
��Auto �6#   �� 5&      $��5 �6#   �� 5'      $��* �6#   .+�5)      �� $��0��6   .+�5*         HBBio.Share.IconButton  -   5          Name$ btnAdd��5*   #   	   HBBio.Share.IconButton 	  Click$ btnAdd_Click��6a    
 btnAdd � ToolTip# �  63   $��5q�6V      Image$+ %/Bio-LabChrom;component/Image/add.png��6v     5+      -   5        $ btnDel��5+   #   $ btnDel_Click��6a    
	 btnDel# � 	 63   $��5q�6V   $. (/Bio-LabChrom;component/Image/delete.png��6v     5,      -   5        $ btnUp��5,   #   $ btnUp_Click��6_    	
 btnUp# � 
 62   $��5q�6T   $5 //Bio-LabChrom;component/Image/upArrowCircle.png��6s     5-      -   5        $ btnDown��5-   #   $ btnDown_Click��6c     btnDown# �  64   $��5q�6X   $7 1/Bio-LabChrom;component/Image/downArrowCircle.png��6y     5.      -   5        $ btnCopy��5.   #   $ btnCopy_Click��6c     btnCopy# �  64   $��5q�6X   $, &/Bio-LabChrom;component/Image/copy.PNG��6y     5/      -   5        $ btnPaste��5/   #   $ btnPaste_Click��6e     btnPaste# �  65   $��5q�6Z   $- '/Bio-LabChrom;component/Image/paste.PNG��6|   50      &
   System.Windows.Controls.DataGrid
  51      -   5         
 Name$	 dgv��51      $��2��6$    
  FrozenColumnCount$ 1��64    
  CanUserReorderColumns .   
  CanUserSortColumns .   
  Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53        labNote,  &System.Windows.Controls.DataGridColumn   Header# [ 6C   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *MNote+6C   4  .System.Windows.Controls.DataGridTemplateColumn  54      -	   5        
colInA��   CellTemplate# [  54   =    
 labInA# [ 6g     55      -
   5        
colInB��# [ 55   =    
 labInB# [ 6g     56      -   5        
colInC��# [ 56   =    
 labInC# [ 6g     57      -   5        
colInD��# [ 57   =    
 labInD# [ 6g     58      -   5        	colBS�� 	 LabBS# [ 58   q    68   �� *MPerBS+$ 	LostFocus=�6q     59      -   5        	colBE�� 	 LabBE# [ 59   q    68   �� *MPerBE+$ 	LostFocus=�6q     5:      -   5        	colCS�� 	 LabCS# [ 5:   q    68   �� *MPerCS+$ 	LostFocus=�6q     5;      -   5        	colCE�� 	 LabCE# [ 5;   q    68   �� *MPerCE+$ 	LostFocus=�6q     5<      -   5        	colDS�� 	 LabDS# [ 5<   q    68   �� *MPerDS+$ 	LostFocus=�6q     5=      -   5        	colDE�� 	 LabDE# [ 5=   q    68   �� *MPerDE+$ 	LostFocus=�6q     5>      -   5        colWash��# [ 5>   >     labFillSystem# [ 6i     5?      -   5        colTVCV�� 5?   :   �� $ 	MBaseTVCV�$ 	LostFocus=�   HeaderTemplate 5@      �� 5A      .R�5B   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5C   &   �� ��60   �� $ DataContext.MBaseStr� �� RelativeSource ��  �� AncestorType" 
 �� 5D   &   $��(��60   �� 5E   &   ��60   �� $ DataContext.MBaseUnitStr� �� " 
 �� 5F   &   $��)��60   5G   #   5H      5J        5K      -   5        colFlowRate�� 5K   >   �� $ MFlowVolLen�$ 	LostFocus=� 5L      �� 5M      .R�5N   "   �� $ 
Horizontal=�6-   .+�5O   &   ��   labFlowRate#��[ 60   �� 5P   &   $��(��60   �� 5Q   &   ��60   �� $" DataContext.MFlowRateUnitStr� �� " 
 �� 5R   &   $��)��60   5S   #   5T      5V        5W      -   5        
colBPV��# [ 5W   =    
 labBPV# [ 6g     5X      -   5        
colOut��# [ 5X   =    
 labOut# [ 6g     5Y        ME_IncubationTime# [ 6g    6)   �� *MIncubation+$ 	LostFocus=�6g   5[      5\      5]      5^      