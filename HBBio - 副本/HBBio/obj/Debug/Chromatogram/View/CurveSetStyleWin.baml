   M S B A M L   `   `   `  ????O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   G CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram 5   	   )clr-namespace:HBBio.ShareHBBio.Share 5	   	   )   #HBBio.Chromatogram.CurveSetStyleWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   0sys&clr-namespace:System;assembly=mscorlib  5   	   ,local clr-namespace:HBBio.Chromatogram  5   	   %shareclr-namespace:HBBio.Share  5	   	   -   5           Ch_CurveSetStyleWin  ? Title#  ?   5   	   $	??500??67   $	??750??6D    ? WindowStartupLocation$ CenterScreen=?6P    ? 	Resources 5       	 False  True??%M   &	       &	      ??5   
   	false??6+   ??5   
   true??6)   5      .??5      ? $??10q?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $??* ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6     
labD_XAxis#???  6    .??5      ?? -   5         ??Name$ spanel??5       ?? Orientation$ 
Horizontal=?6)   .+?5      ?? $??5q?6     labD_AxisScale#???  6'   ?? 5      -   5         ??Name$ cboxXAxisScale??5      $??5q?63    ?? DisplayMemberPath$ MString??6>    ?? SelectedValuePath$ MEnum??6Z   	 ?? SelectedValue	 6t   ?? *MModel.MAxisScale+?? 5      $??5q?6    
 labMin#???  6'   $  Xceed.Wpf.Toolkit.DoubleUpDown  5      -   5        
  Name$
 
doubleXMin??5   $   $??5q?68   ?  ?Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6C   ?? *MMin+?? 5      $??5q?6    
 labMax#???  6'     5      -   5        $
 
doubleXMax??5   $   $??5q?68    6C   ?? *MMax+5       5!      ?? 5"   
   $??2??6     
labD_YAxis#???  6    .??5#      &   System.Windows.Controls.DataGrid  -   5        $	??dgv??5#      $??5q?6"      CanUserReorderColumns .     CanUserSortColumns .     Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%        Com_SignalName,  &System.Windows.Controls.DataGridColumn   Header# ?  6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ?? *MModel.MName+6J     5&       	 Com_Unit# ? 	 6J      
IsReadOnly .  6)   ?? *MModel.MUnit+6n   4  .System.Windows.Controls.DataGridTemplateColumn  5'       
 	Com_Color# ? 
 6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6R    .    CellTemplate 5(      ?? 5)      .R?5*   "   ??  ??Name$ newColor??6,   -   5        ??5*   >   ?? *MModel.MBrush+6c   5+      5-      4	  .System.Windows.Controls.DataGridCheckBoxColumn	  5.      -   5        colShow??$ * 5.   _    .  6>   ?? *MModel.MShow+ 5/      ? 50      -	   5         ?Name$ 	gridCheck??50   #   ??66   ??  ?? ElementName$ colShow?? ?? Path$ ActualWidth? ? ColumnDefinitions 51   "   ?? 52   &   $
??Auto ?67   ?? 53   &   $??* ?67   ?? 54   &   $
??Auto ?67   ?? 55   &   $??15 ?67   ?? 56   &   $
??Auto ?67   ?? 57   &   $??15 ?67   .+?59   "   ?? $??0??6,     Com_Show#???  6<   
  HBBio.Share.IconButton
  5:   "   $??2??63   #??[ 6`   $??18??6?     btnClear ? ToolTip# ?  6?   
  Image$2 ,/Bio-LabChrom;component/Image/selectNull.png??6  ??6C   ?? *
MCommand+??6?   ?? *	MinHeight+ ?? RelativeSource ?? *Self+??6?   ?? *	MinHeight+ ?? *Self+??6  ?? *	MinHeight+ ?? *Self+ 
  ImageHeight 6`  ?? *	MinHeight+ ?? *Self+ 
  
ImageWidth 6?  ?? *	MinHeight+ ?? *Self+6  
  5;   "   $??4??63   #??[ 6`   $??18??6?     btnSelectAll# ?  6?  $1 +/Bio-LabChrom;component/Image/selectAll.png??6  ??6C   ?? *
MCommand+??6?   ?? *	MinHeight+ ?? *Self+??6?   ?? *	MinHeight+ ?? *Self+??6  ?? *	MinHeight+ ?? *Self+ 6_  ?? *	MinHeight+ ?? *Self+ 6?  ?? *	MinHeight+ ?? *Self+6  5<      5>      4  .System.Windows.Controls.DataGridComboBoxColumn  5?      -
   5        dgvSignalAxisScale??# ?  5?   I      DisplayMemberPath$ MString??6s      SelectedValuePath$ MEnum??6?       SelectedValueBinding  6?   ?? *MModel.MAxisScale+  5@      # ?  6)    6K   ?? *MMin+  5A      # ?  6)    6K   ?? *MMax+5C      5D      ? 5E   
   $??4??6    5F      ?? 5G      $??* ?6#   ?? 5H      $
??Auto ?6#   ?? 5I      $??5 ?6#   ?? 5J      $
??Auto ?6#   .+?5L      ?? -   5        ! ??Name$! btnOK??5L      $??1??6$    	 btnOK#???  64   6V   ?? 5M      -   5        $! 	btnCancel??5M      $??3??6(     	btnCancel#???  68   6^   5N      5O      5P      