   M S B A M L   `   `   `  ???? G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  +    %HBBio.Administration.PermissionAddWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:HBBio.Administration        A_PermissionAddWin  ? Title#  ?   $	??450??$	??650?? ? WindowStartupLocation$ CenterOwner=?.??? $??10q? ? RowDefinitions ?? $??* ??? $??10 ??? $
??Auto ?.+?? $??0?? ? ColumnDefinitions ?? $
??Auto ??? $??10 ??? $??* ? ?? $
??Auto ??? $??5 ??? $
??Auto ??? $??5 ??? $??* ?.+??? -    ??Name$ labName??$??0??$??0??  A_PermissionName1#???  ?? -    ??Name$ txtName??$??2??$??0???? -   $ labNote??$??0??$??2??  labNote1#???  ?? -   $ txtNote??$??2??$??2?? ?? TextWrapping$
 Wrap=? ?? AcceptsReturn . " ?? VerticalScrollBarVisibility$
 Auto=?$??80??R? -   	 R?Name$
	 tree??$??2??$??4?????? 
 ?? Path$
 MPermissionNodes?????  ?? DataType)  #HBBio.Administration.PermissionNode"   ?? ItemsSource ?? $
 
MChildList?.R??? ???? *MName+{??? *
MEnabled+ d? 	IsChecked ?? *MYesNo+????  ?? 
TargetType" Q?.???  ?? Property ?@Q?
IsExpanded ?? Value . ? $??2?? ?? $??* ??? $
??Auto ??? $??10 ??? $
??Auto ?.+??? -    ??Name$ btnOK??$??1?? 	 btnOK#???  ?? -   $ 	btnCancel??$??3??  	btnCancel#???  