   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:HBBio.AdministrationHBBio.Administration  5   	   &     HBBio.Administration.UserLockWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:HBBio.Administration     5   	   -   5           A_UserLockWin  ? Title#  ?   5   	    ? SizeToContent$ WidthAndHeight=?61   $	??350??6W    ? WindowStartupLocation$ CenterScreen=?6f   .??5	      ? $??10q?6    ? RowDefinitions 5
   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ? $??0??6    ? ColumnDefinitions 5      ?? 5      $
??Auto ?6#   ?? 5      $??10 ?6#   ?? 5      $??* ?6#    5      ?? 5      $
??Auto ?6    ?? 5      $??10 ?6    ?? 5      $
??Auto ?6    .+?5      ?? -   5         ??Name$ labNameCurr??5      $??0??6-   $??0??6:     A_UserName1#???  6J   ?? 5      -   5         ??Name$ txtNameCurr??5      $??0??6+   $??2??68   ?? 5      $??2??6   $??0??6%     A_PwdLogin1#???  65   G? 5      -   5         G?Name$ 
pwdPwdCurr??5      $??2??6.   $??2??6;   5      ? 5   
   $??2??6    5       ?? 5!      $??* ?6#   ?? 5"      $
??Auto ?6#   ?? 5#      $??10 ?6#   ?? 5$      $
??Auto ?6#   .+?5&      ?? -   5         ??Name$ btnOK??5&      $??1??6$    	 btnOK#???  64   	 ?? 	IsDefault	 . 6g   ?? 5'      -   5        $ 	btnCancel??5'      $??3??6(     	btnCancel#???  68   6^   5(      5)      5*      