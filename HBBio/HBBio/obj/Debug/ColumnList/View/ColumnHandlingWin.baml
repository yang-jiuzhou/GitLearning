   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.ColumnListHBBio.ColumnList  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   (    "HBBio.ColumnList.ColumnHandlingWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.ColumnList     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           C_ColumnHandlingWin  � Title#  �   5	   	   $	��700��67   $	��750��6D   $	��309��6P   $	��500��6`    � WindowStartupLocation$ CenterScreen=�6o    � 	Resources 5
        ColumnRunParameters!   HBBio.Share.TextBoxDoubleEx  Name �� 
IsReadOnly   Value �� Path �� Mode �� ValidatesOnDataErrors	 �� NotifyOnValidationError
 �� UpdateSourceTrigger ��Name �� SelectedIndex  ColumnDetails ��Name��%�  &	       &	 ~    �� 5   
   .R�5      � .+�5        $ gtValue��6(   y�69   �� *
MShowValue+ 6[   �� *	MReadOnly+ 6|   �� $ MText�$ TwoWay=� . 	 . $
 PropertyChanged=��� 5      $ gtIndex��6   y�6,   �� *MShowCombobox+��6Q   �� *MCbox+ 6o   �� $ MIndex�$ TwoWay=� . 	 . $
 PropertyChanged=�5      5      �� 5   
   .R�5      � .+�5        $ gtValue��6(   y�69   �� *
MShowValue+ 6[   �� *	MReadOnly+ 6|   �� $ MText�$ TwoWay=� . 	 . $
 PropertyChanged=��� 5      $ gtText��6   y�6*   �� *	MShowText+��6K   �� $ MText�$ TwoWay=� . 	 . $
 PropertyChanged=�5      5      5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��5 �6   �� 5      $��6* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5       $��* �6    �� 5!      $��10 �6    �� 5"      $
��Auto �6    .+�5$      � $��0��6    5%      �� 5&      $
��Auto �6'   �� 5'      $��5 �6'   �� 5(      $��* �6'    5*      �� 5+      $
��Auto �6$   �� 5,      $��5 �6$   �� 5-      $��* �6$   .+�5/      �� $��0��6   $��0��6)     
labScreen1#���  69   �� 50      -   5        $ 	txtFilter��50      $��0��6-   $��2��6:   6J   �� 51      -   5         ��Name$ listColumnList��51      $��2��62   $��0��6?   $��3��6O    �� SelectionMode$ Extended=�6c   6|   52      � 53      $��2��6    54      �� 55      $��* �6'   �� 56      $
��Auto �6'   �� 57      $��5 �6'   �� 58      $
��Auto �6'   �� 59      $��5 �6'   �� 5:      $
��Auto �6'   �� 5;      $��5 �6'   �� 5<      $
��Auto �6'   �� 5=      $��5 �6'   �� 5>      $
��Auto �6'   �� 5?      $��5 �6'   �� 5@      $
��Auto �6'   .+�5B         HBBio.Share.IconButton  -   5          Name$ btnPrint��5B   #      HBBio.Share.IconButton   Click$ btnPrint_Click��6j   $��1��65     btnPrint � ToolTip# �  6E      Image$+ %/Bio-LabChrom;component/Image/pdf.png��6�     5C      -   5        $ 	btnExport��5C   #   $ btnExport_Click��6l   $��3��66     	btnExport# �  6F   $. (/Bio-LabChrom;component/Image/export.png��6�     5D      -   5        $ 	btnImport��5D   #   $ btnImport_Click��6l   $��5��66     	btnImport# �  6F   $. (/Bio-LabChrom;component/Image/import.png��6�     5E      -   5        $ btnCopy��5E   #   $ btnCopy_Click��6h   $��7��64     btnCopy# �  6D   $, &/Bio-LabChrom;component/Image/copy.png��6~     5F      -   5        $ btnAdd��5F   #   $ btnAdd_Click��6f   $��9��63    
 btnAdd# �  6C   $+ %/Bio-LabChrom;component/Image/add.png��6{     5G      -	   5        $ btnDel��5G   #   $ btnDel_Click��6g   $��11��63    
	 btnDel# � 	 6D   $. (/Bio-LabChrom;component/Image/delete.png��6|   5H      5I      � 5J   
   $��2��6    5K      �� 5L      $
��Auto �6#   �� 5M      $��5 �6#   �� 5N      $��* �6#   �� 5O      $��5 �6#   �� 5P      $
��Auto �6#    5R      �� 5S      $
��Auto �6    �� 5T      $��5 �6    �� 5U      $
��Auto �6    �� 5V      $��5 �6    �� 5W      $
��Auto �6    �� 5X      $��5 �6    �� 5Y      $��* �6    �� 5Z      $��10 �6    �� 5[      $
��Auto �6    .+�5]      �� -
   5         ��Name$ labName��5]      $��0��6)   $��0��69    
 labName1#��� 
 6F   �� 5^      -   5        $ txtName��5^      $��2��6'   $��0��67   $��3��6D   ��6X   �� $ MName�$ TwoWay=��� 5_      -   5        $ labNote��5_      $��0��6)   $��2��69     labNote1#���  6F   �� 5`      -   5        $ txtNote��5`      $��2��6'   $��2��67   $��3��6D    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6�   $��80��6�   ��6X   �� $ MNote�$ TwoWay=�6�   �� 5a      -   5        $ labUser��5a      $��0��6)   $��4��69     labUser1#���  6F   �� 5b      -   5        $ txtUser��5b      $��2��6'   $��4��67   $��3��6D    . ��6X   �� $ MUser�$ TwoWay=�6�   �� 5c      $��0��6   $��6��6)   $��5��66   .@�5d      ��   C_RunParameters#���  6   .��5e      � .+�5f      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvRunParameters��5f   #      HeadersVisibility$ Column=�6=      CanUserReorderColumns .     CanUserSortColumns .     Columns 5g      0  *System.Windows.Controls.DataGridTextColumn  5h   "     C_Parameters,  &System.Windows.Controls.DataGridColumn   Header# �  65       Width5  /System.Windows.Controls.DataGridLengthConverter$  2* 6w   !   
IsReadOnly! . 1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 6]   �� *MName+6�   4	  .System.Windows.Controls.DataGridTemplateColumn	  5i   "     C_Value# �  69   # 	  CellTemplate## [ 6\   $  * 6�     5j   "    
 C_Unit# �  65   $  * 6r   ! . " 6W   �� *MUnit+6|   5l      5m      5n      �� 5o        	C_Details#���  6   .��5p      � .+�5q        -   5        $ 
dgvDetails��5q   #   $   ColumnWidth$$ * 67   $ Column=�6G    .   .   5r        5s   "   # �  65   ! . " 6]   �� *MName+6w   	  5t   "   # �  69   ## [ 6\     5u   "   # �  65   ! . " 6W   �� *MUnit+6q   5w      5x      5y      5z        5{      -   5        $ btnSave��5{      $ btnSave_Click��6q   $��4��60   $��8��6@     btnSave# �  6M   $, &/Bio-LabChrom;component/Image/save.png��6�   $
��Left=�6�   $��35��6�   5|      5}      5~      