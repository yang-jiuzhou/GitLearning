   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  9!clr-namespace:HBBio.CommunicationHBBio.Communication  (    "HBBio.MethodEdit.FlowValveLengthUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:HBBio.MethodEdit     +shareclr-namespace:HBBio.Share     2comm!clr-namespace:HBBio.Communication     -     � 	Resources     	ColumnInA%   HBBio.Communication.EnumInAInfo   NameList �� SelectedIndex �� Path �� UpdateSourceTrigger  	ColumnInB%   HBBio.Communication.EnumInBInfo   NameList  	ColumnInC%   HBBio.Communication.EnumInCInfo   NameList  	ColumnInD%   HBBio.Communication.EnumInDInfo   NameList  	ColumnOut%   HBBio.Communication.EnumOutInfo   NameList  	ColumnBPV%   HBBio.Communication.EnumBPVInfo	   NameList  
ColumnWash&    HBBio.Communication.EnumWashInfo
   NameList��%�  &	        &	 8     &	 p     &	 �     &	 �     &	     &	 Q    �� .R��� #��Z  �� $
 MInA�$ 	LostFocus=��� .R��� #��Z  �� $
 MInB�$ 	LostFocus=��� .R��� #��Z  �� $
 MInC�$ 	LostFocus=��� .R��� #��Z  �� $
 MInD�$ 	LostFocus=��� .R��� #��Z  �� $ MVOut�$ 	LostFocus=��� .R��� #��Z	  �� $
 MBPV�$ 	LostFocus=��� .R��� #��Z
  �� $ MFillSystem�$ 	LostFocus=�.����  $  ME_EnumGroupType_FlowValveLength#���  .��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��5 ��� $��* �.+��� $��0��.+�   HBBio.Share.IconButton  -     Name$ btnAdd��	   HBBio.Share.IconButton 	  Click$ btnAdd_Click�� 
 btnAdd � ToolTip# �  $��5q�   Image$+ %/Bio-LabChrom;component/Image/add.png��  -   $ btnDel��$ btnDel_Click�� 
	 btnDel# � 	 $��5q�$. (/Bio-LabChrom;component/Image/delete.png��  -   $ btnUp��$ btnUp_Click�� 	
 btnUp# � 
 $��5q�$5 //Bio-LabChrom;component/Image/upArrowCircle.png��  -   $ btnDown��$ btnDown_Click��  btnDown# �  $��5q�$7 1/Bio-LabChrom;component/Image/downArrowCircle.png��  -   $ btnCopy��$ btnCopy_Click��  btnCopy# �  $��5q�$, &/Bio-LabChrom;component/Image/copy.PNG��  -   $ btnPaste��$ btnPaste_Click��  btnPaste# �  $��5q�$- '/Bio-LabChrom;component/Image/paste.PNG��&
   System.Windows.Controls.DataGrid
  -    
 Name$	 dgv��$��2�� 
  FrozenColumnCount$ 1�� 
  CanUserReorderColumns .   
  CanUserSortColumns .   
  Columns 0  *System.Windows.Controls.DataGridTextColumn    labNote,  &System.Windows.Controls.DataGridColumn   Header# [ 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *MNote+4  .System.Windows.Controls.DataGridTemplateColumn  -	   
colInA��   CellTemplate# [   
 labInA# [   -
   
colInB��# [  
 labInB# [   -   
colInC��# [  
 labInC# [   -   
colInD��# [  
 labInD# [   -   	colBS�� 	 LabBS# [  �� *MPerBS+$ 	LostFocus=�  -   	colBE�� 	 LabBE# [  �� *MPerBE+$ 	LostFocus=�  -   	colCS�� 	 LabCS# [  �� *MPerCS+$ 	LostFocus=�  -   	colCE�� 	 LabCE# [  �� *MPerCE+$ 	LostFocus=�  -   	colDS�� 	 LabDS# [  �� *MPerDS+$ 	LostFocus=�  -   	colDE�� 	 LabDE# [  �� *MPerDE+$ 	LostFocus=�  -   colWash��# [   labFillSystem# [   -   colTVCV�� �� $ 	MBaseTVCV�$ 	LostFocus=�   HeaderTemplate �� .R���  �� Orientation$ 
Horizontal=�.+��� ���� $ DataContext.MBaseStr� �� RelativeSource ��  �� AncestorType" 
 �� $��(���� ���� $ DataContext.MBaseUnitStr� �� " 
 �� $��)��  -   colFlowRate�� �� $ MFlowVolLen�$ 	LostFocus=� �� .R��� $ 
Horizontal=�.+���   labFlowRate#��[ �� $��(���� ���� $" DataContext.MFlowRateUnitStr� �� " 
 �� $��)��  -   
colBPV��# [  
 labBPV# [   -   
colOut��# [  
 labOut# [     ME_IncubationTime# [  �� *MIncubation+$ 	LostFocus=�