   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  G CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram )clr-namespace:HBBio.ShareHBBio.Share )   #HBBio.Chromatogram.CurveSetStyleWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  0sys&clr-namespace:System;assembly=mscorlib  ,local clr-namespace:HBBio.Chromatogram  %shareclr-namespace:HBBio.Share  -      Ch_CurveSetStyleWin  � Title#  �   $	��500��$	��750�� � WindowStartupLocation$ CenterScreen=� � 	Resources  	 False  True��%1   &	       &	      ��	false����true��.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� $��0��  
labD_XAxis#���  .���� -    ��Name$ spanel�� �� Orientation$ 
Horizontal=�.+��� $��5q�  labD_AxisScale#���  �� -    ��Name$ cboxXAxisScale��$��5q� �� DisplayMemberPath$ MString�� �� SelectedValuePath$ MEnum��	 �� SelectedValue	 �� *MModel.MAxisScale+�� $��5q� 
 labMin#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -   
  Name$
 
doubleXMin��$��5q��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *MMin+�� $��5q� 
 labMax#���    -   $
 
doubleXMax��$��5q� �� *MMax+�� $��2��  
labD_YAxis#���  .��&   System.Windows.Controls.DataGrid  -   $	��dgv��$��5q�   CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    Com_SignalName,  &System.Windows.Controls.DataGridColumn   Header# �  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *MModel.MName+   	 Com_Unit# � 	    
IsReadOnly .  �� *MModel.MUnit+4  .System.Windows.Controls.DataGridTemplateColumn   
 	Com_Color# � 
    Width5  /System.Windows.Controls.DataGridLengthConverter$ *  .    CellTemplate �� .R���  ��Name$ newColor��-   ���� *MModel.MBrush+4	  .System.Windows.Controls.DataGridCheckBoxColumn	  -   colShow��$ *  .  �� *MModel.MShow+ � -	    �Name$ 	gridCheck������  �� ElementName$ colShow�� �� Path$ ActualWidth� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��15 �.+��� $��0��  Com_Show#���  
  HBBio.Share.IconButton
  $��2��#��[ $��18��  btnClear � ToolTip# �   
  Image$2 ,/Bio-LabChrom;component/Image/selectNull.png������ *
MCommand+���� *	MinHeight+ �� RelativeSource �� *Self+���� *	MinHeight+ �� *Self+���� *	MinHeight+ �� *Self+ 
  ImageHeight �� *	MinHeight+ �� *Self+ 
  
ImageWidth �� *	MinHeight+ �� *Self+
  $��4��#��[ $��18��  btnSelectAll# �  $1 +/Bio-LabChrom;component/Image/selectAll.png������ *
MCommand+���� *	MinHeight+ �� *Self+���� *	MinHeight+ �� *Self+���� *	MinHeight+ �� *Self+ �� *	MinHeight+ �� *Self+ �� *	MinHeight+ �� *Self+4  .System.Windows.Controls.DataGridComboBoxColumn  -
   dgvSignalAxisScale��# �     DisplayMemberPath$ MString��   SelectedValuePath$ MEnum��    SelectedValueBinding  �� *MModel.MAxisScale+  # �   �� *MMin+  # �   �� *MMax+� $��4�� �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   ! ��Name$! btnOK��$��1�� 	 btnOK#���  �� -   $! 	btnCancel��$��3��  	btnCancel#���  