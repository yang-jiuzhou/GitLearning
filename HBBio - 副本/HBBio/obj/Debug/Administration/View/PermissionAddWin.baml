   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   +    %HBBio.Administration.PermissionAddWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	      A_PermissionAddWin  ? Title#  ?   5   	   $	??450??66   $	??650??6B    ? WindowStartupLocation$ CenterOwner=?6O   .??5	      ? $??10q?6    ? RowDefinitions 5
   
   ?? 5      $??* ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ? $??0??6    ? ColumnDefinitions 5      ?? 5      $
??Auto ?6#   ?? 5      $??10 ?6#   ?? 5      $??* ?6#    5      ?? 5      $
??Auto ?6    ?? 5      $??5 ?6    ?? 5      $
??Auto ?6    ?? 5      $??5 ?6    ?? 5      $??* ?6    .+?5      ?? -   5         ??Name$ labName??5      $??0??6)   $??0??69     A_PermissionName1#???  6F   ?? 5      -   5         ??Name$ txtName??5      $??2??6'   $??0??67   ?? 5      -   5        $ labNote??5      $??0??6)   $??2??69     labNote1#???  6F   ?? 5      -   5        $ txtNote??5      $??2??6'   $??2??67    ?? TextWrapping$
 Wrap=?6D    ?? AcceptsReturn . " ?? VerticalScrollBarVisibility$
 Auto=?6m   $??80??6?   R? 5       -   5        	 R?Name$
	 tree??5       $??2??6%   $??4??65   ??6B   ?? 
 ?? Path$
 MPermissionNodes???5!      ?? 5"       ?? DataType)  #HBBio.Administration.PermissionNode"  6/    ?? ItemsSource 6X   ?? $
 
MChildList?.R?5#      ?? ??6#   ?? *MName+{?6=   ?? *
MEnabled+ d? 	IsChecked 6\   ?? *MYesNo+5$      ??5&      ?? 5'       ?? 
TargetType" Q?6   .?5(      ??  ?? Property ?@Q?
IsExpanded ?? Value . 6D   5)      5+      5,      ? 5-   
   $??2??6    5.      ?? 5/      $??* ?6#   ?? 50      $
??Auto ?6#   ?? 51      $??10 ?6#   ?? 52      $
??Auto ?6#   .+?54      ?? -   5         ??Name$ btnOK??54      $??1??6$    	 btnOK#???  64   6V   ?? 55      -   5        $ 	btnCancel??55      $??3??6(     	btnCancel#???  68   6^   56      57      58      