   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  )clr-namespace:HBBio.ShareHBBio.Share  ,    &HBBio.Administration.AdministrationWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration     +shareclr-namespace:HBBio.Share     -      A_AdministrationWin  � Title#  �   $	��463��$	��750��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q�.+��� -    ��Name$ 
tabControl��.@��� -    ��Name$ tabItemUser�� 
 A_User#���  .��]� $��5q�y���  �� ElementName$ tabItemUser�� �� Path$ 
Visibility�.+��� $	��Top=�$	��0,5q� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ 
btnAddUser��$	��5,0q� 
 btnAdd#���  �� -   $ btnEditUser��$	��5,0q�  btnEdit#���  �� -   $ 
btnDelUser��$	��5,0q� 
 btnDel#���  &   System.Windows.Controls.DataGrid  -     Name$ dgvUser��	   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 * 
   
IsReadOnly
 .    CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    
A_UserName,  &System.Windows.Controls.DataGridColumn   Header# �  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	MUserName+    A_PermissionName# �   �� *MPermissionName+    labNote# �   �� *MNote+    
A_Validity# �   �� *MDaysRemaining+   	 	labStatus# � 	  �� *	MStatus+�� -   $ tabItemPermission�� 
 A_Permission#��� 
 .��]� $��5q�y��� $ tabItemPermission��$ 
Visibility�.+��� $	��Top=�$	��0,5q�$ 
Horizontal=�.+��� -	   $ btnAddPermission��$	��5,0q�#���  �� -
   $ btnEditPermission��$	��5,0q�#���  �� -   $ btnDelPermission��$	��5,0q�#���    -   $ dgvPermission��$	 * 
 .  .   .     # �   �� *MName+�� -   $ tabItemTactics��  	A_Tactics#���  .��]� $��5q�y��� $ tabItemTactics��$ 
Visibility�.+��� $	��Top=�$	��0,5q�$ 
Horizontal=�.+��� -   $ btnEditTactics��$	��5,0q�#���    -   $ 
dgvTactics��$	 * 
 .  .   .       A_TacticsName# �   �� *MColumnName+   
 labSet# �   �� *MColumnValue+�� -   $ tabItemSignerReviewer��  A_SignerReviewer#���  .��]� $��5q�y��� $ tabItemSignerReviewer��$ 
Visibility�.+�� $	��Top=�$��5q� � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+��� $��0��$ 
Horizontal=�.+��� -   $ btnEditSignerReviewer��  	btnModify#���  �� $��1��$ 
Horizontal=�.+�   HBBio.Share.IconButton  -     Name$ btnSignatureClear��   HBBio.Share.IconButton   Click$ btnSignatureClear_Click��$
��15,0q�$��25��   ImageHeight$ 25Q�$��25��$��25��   
ImageWidth$ 25Q�$��25��  btnClear � ToolTip# �     Image$2 ,/Bio-LabChrom;component/Image/selectNull.png��  -   $ btnSignatureAll��$ btnSignatureAll_Click��$
��15,0q�$��25��$ 25Q�$��25��$��25��$ 25Q�$��25��  btnSelectAll# �  $1 +/Bio-LabChrom;component/Image/selectAll.png���� $��2��$ 
Horizontal=�.+�  -   $ btnReviewerClear��$ btnReviewerClear_Click��$
��15,0q�$��25��$ 25Q�$��25��$��25��$ 25Q�$��25��# �  $2 ,/Bio-LabChrom;component/Image/selectNull.png��  -   $ btnReviewerAll��$ btnReviewerAll_Click��$
��15,0q�$��25��$ 25Q�$��25��$��25��$ 25Q�$��25��# �  $1 +/Bio-LabChrom;component/Image/selectAll.png��  -   $ dgvSignerReviewer��$	 * 
 .  .   .       A_SignerReviewerName# �   �� *MName+4  .System.Windows.Controls.DataGridCheckBoxColumn    A_Signature# �   �� *	MSigner+    
A_Reviewer# �   �� *	MReviewer+