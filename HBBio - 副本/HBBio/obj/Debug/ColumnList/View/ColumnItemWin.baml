   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.ColumnListHBBio.ColumnList  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   $    HBBio.ColumnList.ColumnItemWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.ColumnList     5   	   +shareclr-namespace:HBBio.Share     5   	      C_ColumnItemWin  ? Title#  ?   5	   	   $	??500??63   $	??400??6@    ? WindowStartupLocation$ CenterOwner=?6L    ? 	Resources 5
        ColumnRunParameters!   HBBio.Share.TextBoxDoubleEx  Name ?? 
IsReadOnly   Value ?? Path ?? Mode ?? ValidatesOnDataErrors	 ?? NotifyOnValidationError
 ?? UpdateSourceTrigger ??Name ?? 
IsReadOnly ?? SelectedIndex  ColumnDetails ??Name??%?  &	       &	 p    ?? 5   
   .R?5      ? .+?5        $ gtValue??6(    . y?69   ?? *
MShowValue+ 6m   ?? $ MText?$ TwoWay=? . 	 . $
 PropertyChanged=??? 5      $ gtIndex??6    . y?6,   ?? *MShowCombobox+??6c   ?? *MCbox+ 6?   ?? $ MIndex?$ TwoWay=? . 	 . $
 PropertyChanged=?5      5      ?? 5   
   .R?5      ? .+?5        $ gtValue??6(    . y?69   ?? *
MShowValue+ 6m   ?? $ MText?$ TwoWay=? . 	 . $
 PropertyChanged=??? 5      $ gtText??6    . y?6*   ?? *	MShowText+??6]   ?? $ MText?$ TwoWay=? . 	 . $
 PropertyChanged=?5      5      5      .??5      ? $??10q?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??5 ?6   ?? 5      $??* ?6   ?? 5      $??5 ?6   ?? 5      $
??Auto ?6    ? RowDefinitions 5    
   ?? 5!      $
??Auto ?6   ?? 5"      $??5 ?6   ?? 5#      $
??Auto ?6   ?? 5$      $??5 ?6   ?? 5%      $
??Auto ?6   ?? 5&      $??5 ?6   ?? 5'      $??* ?6   ?? 5(      $??10 ?6   ?? 5)      $
??Auto ?6   .+?5+   
   ?? -   5         ??Name$ labName??5+      $??0??6%   $??0??65     labName1#???  6B   ?? 5,   
   -   5        $ txtName??5,      $??2??6#   $??0??63   $??3??6@   ??6T   ?? $ MName?$ TwoWay=??? 5-   
   $??0??6   $??2??6$     labNote1#???  61   ?? 5.   
   -   5        $ txtNote??5.      $??2??6#   $??2??63   $??3??6@    ?? TextWrapping$
 Wrap=?6}    ?? AcceptsReturn . " ?? VerticalScrollBarVisibility$
 Auto=?6?   $??80??6?   ??6T   ?? $ MNote?$ TwoWay=?6?   ?? 5/   
   $??0??6   $??4??6$     labUser1#???  61   ?? 50   
   -   5        $ txtUser??50      $??2??6#   $??4??63   $??3??6@    . ??6T   ?? $ MUser?$ TwoWay=?6}   ?? 51   
   $??0??6   $??6??6%   $??5??62   .@?52      ??   C_RunParameters#???  6   .??53      ? .+?54      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvRunParameters??54         HeadersVisibility$ Column=?69      CanUserReorderColumns .     CanUserSortColumns .     Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56        C_Parameters,  &System.Windows.Controls.DataGridColumn   Header# ?  61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6s      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6Y   ?? *MName+6~   4  .System.Windows.Controls.DataGridTemplateColumn  57        C_Value# ?  65      CellTemplate# [ 6X   $ * 6?     58       
	 C_Unit# ? 	 61   $ * 6n    .  6S   ?? *MUnit+6x   5:      5;      5<      ?? 5=       
 	C_Details#??? 
 6   .??5>      ? .+?5?        -   5        $ 
dgvDetails??5?         ColumnWidth$ * 63   $ Column=?6C    .   .   5@        5A      # ?  61    .  6Y   ?? *MName+6s     5B      # ?  65   # [ 6X     5C      # ? 	 61    .  6S   ?? *MUnit+6m   5E      5F      5G      5H      5I      5J      