   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   ,    &HBBio.Administration.AdministrationWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           A_AdministrationWin  � Title#  �   5	   	   $	��463��67   $	��750��6D   $	��309��6P   $	��500��6`    � WindowStartupLocation$ CenterScreen=�6o   .��5
      � $��10q�6   .+�5   
   �� -   5         ��Name$ 
tabControl��5      .@�5      �� -   5         ��Name$ tabItemUser��5       
 A_User#���  6+   .��5      ]� $��5q�6   y�6'   ��  �� ElementName$ tabItemUser�� �� Path$ 
Visibility�.+�5      �� $	��Top=�6!   $	��0,5q�66    �� Orientation$ 
Horizontal=�6C   .+�5      �� -   5         ��Name$ 
btnAddUser��5   !   $	��5,0q�65    
 btnAdd#���  6B   6e   �� 5      -   5        $ btnEditUser��5   !   $	��5,0q�66     btnEdit#���  6C   6g   �� 5      -   5        $ 
btnDelUser��5   !   $	��5,0q�65    
 btnDel#���  6B   6e   5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgvUser��5      	   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 * 60   
   
IsReadOnly
 .    CanUserReorderColumns .     CanUserSortColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5        
A_UserName,  &System.Windows.Controls.DataGridColumn   Header# �  61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6W   �� *	MUserName+  5        A_PermissionName# �  61    6]   �� *MPermissionName+  5        labNote# �  61    6T   �� *MNote+  5        
A_Validity# �  61    6W   �� *MDaysRemaining+  5       	 	labStatus# � 	 61    6V   �� *	MStatus+5      5      5      �� 5      -   5        $ tabItemPermission��5       
 A_Permission#��� 
 61   .��5      ]� $��5q�6   y�6'   �� $ tabItemPermission��$ 
Visibility�.+�5       �� $	��Top=�6!   $	��0,5q�66   $ 
Horizontal=�6C   .+�5!      �� -	   5        $ btnAddPermission��5!   !   $	��5,0q�6;   #���  6H   6k   �� 5"      -
   5        $ btnEditPermission��5"   !   $	��5,0q�6<   #���  6I   6m   �� 5#      -   5        $ btnDelPermission��5#   !   $	��5,0q�6;   #���  6H   6k   5$        5%      -   5        $ dgvPermission��5%      $	 * 66   
 .  .   .   5&        5'      # �  61    6]   �� *MName+5)      5*      5+      �� 5,      -   5        $ tabItemTactics��5,        	A_Tactics#���  6.   .��5-      ]� $��5q�6   y�6'   �� $ tabItemTactics��$ 
Visibility�.+�5.      �� $	��Top=�6!   $	��0,5q�66   $ 
Horizontal=�6C   .+�5/      �� -   5        $ btnEditTactics��5/   !   $	��5,0q�69   #���  6F   6j   50        51      -   5        $ 
dgvTactics��51      $	 * 63   
 .  .   .   52        53        A_TacticsName# �  61    6Z   �� *MColumnName+  54       
 labSet# �  61    6S   �� *MColumnValue+56      57      58      �� 59      -   5        $ tabItemSignerReviewer��59        A_SignerReviewer#���  65   .��5:      ]� $��5q�6   y�6'   �� $ tabItemSignerReviewer��$ 
Visibility�.+�5;      � $	��Top=�6   $��5q�60    � ColumnDefinitions 5<      �� 5=      $��* �6/   �� 5>      $��* �6/   �� 5?      $��* �6/   .+�5A      �� $��0��6%   $ 
Horizontal=�65   .+�5B      �� -   5        $ btnEditSignerReviewer��5B   %     	btnModify#���  6E   6k   5C      �� 5D      $��1��6%   $ 
Horizontal=�65   .+�5E         HBBio.Share.IconButton  -   5          Name$ btnSignatureClear��5E   /      HBBio.Share.IconButton   Click$ btnSignatureClear_Click��6�   $
��15,0q�6J   $��25��6X      ImageHeight$ 25Q�6g   $��25��6x   $��25��6�      
ImageWidth$ 25Q�6�   $��25��6�     btnClear � ToolTip# �  6�      Image$2 ,/Bio-LabChrom;component/Image/selectNull.png��6�     5F      -   5        $ btnSignatureAll��5F   /   $ btnSignatureAll_Click��6�   $
��15,0q�6H   $��25��6V   $ 25Q�6e   $��25��6v   $��25��6�   $ 25Q�6�   $��25��6�     btnSelectAll# �  6�   $1 +/Bio-LabChrom;component/Image/selectAll.png��6�   5G      �� 5H      $��2��6%   $ 
Horizontal=�65   .+�5I        -   5        $ btnReviewerClear��5I   /   $ btnReviewerClear_Click��6�   $
��15,0q�6I   $��25��6W   $ 25Q�6f   $��25��6w   $��25��6�   $ 25Q�6�   $��25��6�   # �  6�   $2 ,/Bio-LabChrom;component/Image/selectNull.png��6�     5J      -   5        $ btnReviewerAll��5J   /   $ btnReviewerAll_Click��6�   $
��15,0q�6G   $��25��6U   $ 25Q�6d   $��25��6u   $��25��6�   $ 25Q�6�   $��25��6�   # �  6�   $1 +/Bio-LabChrom;component/Image/selectAll.png��6�   5K      5L        5M      -   5        $ dgvSignerReviewer��5M      $	 * 6:   
 .  .   .   5N        5O        A_SignerReviewerName# �  61    6a   �� *MName+4  .System.Windows.Controls.DataGridCheckBoxColumn  5P        A_Signature# �  65    6\   �� *	MSigner+  5Q        
A_Reviewer# �  65    6[   �� *	MReviewer+5S      5T      5U      5V      5W      5X      