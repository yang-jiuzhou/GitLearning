   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.ProjectManagerHBBio.ProjectManager  5      )clr-namespace:HBBio.ShareHBBio.Share  5      (    "HBBio.ProjectManager.ProjectTreeUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:HBBio.ProjectManager     5      +shareclr-namespace:HBBio.Share     5      -   5        .??5	      ? $??5q?6     ? ColumnDefinitions  5
   
   ?? 5      $
??Auto ?6   ?? 5      $??5 ?6   ?? 5      $
??Auto ?6   ?? 5      $??5 ?6   ?? 5      $
??Auto ?6   ?? 5      $??* ?6   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $??* ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $??0??6!      	E_Filter1#???   61   ?? 5   
   -   5         ??Name$ 	txtFilter??5      $??0??6%   $??2??62   $??5??6B   6V   R? 5   
   -   5         R?Name$ treeView??5      $??2??6%   $??0??62   $??7??6B   ??5      ?? 5       ?? DataType#  HBBio.ProjectManager.TreeNode"  6+    ?? ItemsSource 6N   ??  ?? Path$ 
MChildList?.R?5      ?? $??0??6!    ?? Orientation$ 
Horizontal=?61   .+?5       ?? $
??Left=?6    $	??Top=?66   $??20??6N   ??5!      ?? 5"   "    ?? 
TargetType" ??6(   .?5#   &   ?? 	 ?? Property	 ? ??
 ?? Value
 6?   ?? *MIcon+ ?? Mode$ OneTime=? ?? Triggers 5$   &   ?? 5%   *    ?? Binding 66   ??  ?? RelativeSource ??  ?? AncestorType$ TreeViewItemL?$ 
IsExpanded? ?? Value True6?   .a?5&   .   ?? 	 ? ??
 6G   ?? *MIconExpanded+$ OneTime=?5'   +   5)   #   5+      ?? 5,      $
??Left=?6%   $ Vertical=?6;   .+?5-      ?? $??50??6?    ?? Padding$ 5q?6M   ??6(   ?? *MName+6M   ?? 5.      -   5        
????  ?$y?	Collapsed=?5.   O   ??6&   ?? *MName+6?   5/      50      51      ??53      ?? 54      " Q?6   56      ?? 57   
   -   5         ??Name$ listbox??57      $??2??6#   $??0??60   $??7??6@   $y?Hidden=?6T    ?? SelectionMode$ Extended=?6h   ??58      ?? 59      .R?5:      ?? $ 5q?67   ??6    ?? *MName+67   5;      5=         HBBio.Share.IconButton  5>   
   -   5          Name$ btnNew??5>         HBBio.Share.IconButton   Click$ btnNew_Click??6k   $??4??6+   $??0??68    
 btnAdd ? ToolTip# ?  6H      Image$2 ,/Bio-LabChrom;component/Image/folderPlus.png??6?     5?   
   -   5        $ 	btnRename??5?      $ btnRename_Click??6q   $??4??6.   $??2??6;     	btnRename# ?  6K   $. (/Bio-LabChrom;component/Image/rename.PNG??6?     5@   
   -   5        $ btnDel??5@      $ btnDel_Click??6?   $??4??6+   $??4??68    
 btnDel# ?  6H   $3 -/Bio-LabChrom;component/Image/folderMinus.png??6?   {?6k   ??  ?? ElementName$ 	btnRename??$ 	IsEnabled?6?     5A   
   -	   5        $ btnAbout??5A      $ btnAbout_Click??6s   $??4??6-   $??6??6:     btnAttribute# ?  6J   $- '/Bio-LabChrom;component/Image/about.png??6?   5B      5C      