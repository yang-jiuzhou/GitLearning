   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   )    #HBBio.MethodEdit.RenamePhaseNameWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.MethodEdit     5   	   -   5           ME_RenamePhaseNameWin  ? Title#  ?   5   	    ? SizeToContent$ WidthAndHeight=?69    ? WindowStartupLocation$ CenterOwner=?6_   .??5	      ? $??10q?6    ? ColumnDefinitions 5
   
   ?? 5      $??* ?6   ?? 5      $??5 ?6   ?? 5      $
??Auto ?6   ?? 5      $??5 ?6   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??15 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $??0??6!     ME_PhaseOld1#???  61   ?? 5   
   -   5         ??Name$ txtOld??5      $??0??6"   $??2??6/   $??3??6?    ?? 
IsReadOnly . 6S   ?? 5   
   $??2??6   $??0??6!     ME_PhaseNew1#???  61   ?? 5   
   -   5        $ txtNew??5      $??2??6"   $??2??6/   $??3??6?   ?? 5   
   -   5         ??Name$ btnOK??5      $??4??6    $??2??6-    	 btnOK#???  6=   6_   ?? 5   
   -   5        $ 	btnCancel??5      $??4??6$   $??4??61     	btnCancel#???  6A   6g   5      5      