   M S B A M L   `   `   `  ???? G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.ColumnListHBBio.ColumnList  )clr-namespace:HBBio.ShareHBBio.Share  $    HBBio.ColumnList.ColumnItemWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.ColumnList     +shareclr-namespace:HBBio.Share        C_ColumnItemWin  ? Title#  ?   $	??500??$	??400?? ? WindowStartupLocation$ CenterOwner=? ? 	Resources   ColumnRunParameters!   HBBio.Share.TextBoxDoubleEx  Name ?? 
IsReadOnly   Value ?? Path ?? Mode ?? ValidatesOnDataErrors	 ?? NotifyOnValidationError
 ?? UpdateSourceTrigger ??Name ?? 
IsReadOnly ?? SelectedIndex  ColumnDetails ??Name??%)  &	       &	     ?? .R?? .+?  $ gtValue?? . y??? *
MShowValue+ ?? $ MText?$ TwoWay=? . 	 . $
 PropertyChanged=??? $ gtIndex?? . y??? *MShowCombobox+???? *MCbox+ ?? $ MIndex?$ TwoWay=? . 	 . $
 PropertyChanged=??? .R?? .+?  $ gtValue?? . y??? *
MShowValue+ ?? $ MText?$ TwoWay=? . 	 . $
 PropertyChanged=??? $ gtText?? . y??? *	MShowText+???? $ MText?$ TwoWay=? . 	 . $
 PropertyChanged=?.??? $??10q? ? ColumnDefinitions ?? $
??Auto ??? $??5 ??? $??* ??? $??5 ??? $
??Auto ? ? RowDefinitions ?? $
??Auto ??? $??5 ??? $
??Auto ??? $??5 ??? $
??Auto ??? $??5 ??? $??* ??? $??10 ??? $
??Auto ?.+??? -    ??Name$ labName??$??0??$??0??  labName1#???  ?? -   $ txtName??$??2??$??0??$??3?????? $ MName?$ TwoWay=??? $??0??$??2??  labNote1#???  ?? -   $ txtNote??$??2??$??2??$??3?? ?? TextWrapping$
 Wrap=? ?? AcceptsReturn . " ?? VerticalScrollBarVisibility$
 Auto=?$??80?????? $ MNote?$ TwoWay=??? $??0??$??4??  labUser1#???  ?? -   $ txtUser??$??2??$??4??$??3?? . ???? $ MUser?$ TwoWay=??? $??0??$??6??$??5??.@???   C_RunParameters#???  .??? .+?&   System.Windows.Controls.DataGrid  -     Name$ dgvRunParameters??   HeadersVisibility$ Column=?   CanUserReorderColumns .     CanUserSortColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn    C_Parameters,  &System.Windows.Controls.DataGridColumn   Header# ?     Width5  /System.Windows.Controls.DataGridLengthConverter$ 2*    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding ?? *MName+4  .System.Windows.Controls.DataGridTemplateColumn    C_Value# ?     CellTemplate# [ $ *    
	 C_Unit# ? 	 $ *  .  ?? *MUnit+??  
 	C_Details#??? 
 .??? .+?  -   $ 
dgvDetails??   ColumnWidth$ * $ Column=? .   .     # ?   .  ?? *MName+  # ?  # [   # ? 	  .  ?? *MUnit+