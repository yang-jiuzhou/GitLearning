   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HBBio.ShareHBBio.Share  5   	       HBBio.Share.MessageBoxWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:HBBio.Share     5   	   -   5           labTips  ? Title#  ?   5   	    ? SizeToContent$ WidthAndHeight=?6+   $	??250??6Q    ? Topmost .  ? WindowStartupLocation$ CenterScreen=?6o   .??5	      ? -   5         ?Name$
 grid??5	      $??15q?6    ? RowDefinitions 5
   
   ?? 5      $??* ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? -   5         ??Name$ group??5      $??0??6"     labInfo#???  6/   .??5      ?? -   5         ??Name$ txtInfo??5      $??15q?6)   $
??Wrap=?65   5      ?? 5   
   -   5         ??Name$ btnOK??5      $??2??6    	 btnOK#???  6-   $??Right=?6O   6k   ? 5   
   $??3??6   	 ? ColumnDefinitions	 5      ?? 5      $??* ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??10 ?6#   ?? 5      $
??Auto ?6#   .+?5      ?? -   5        $ btnYes??5   %   $??1??6    
 btnYes#???  65   6X   ?? 5      -   5        $ btnNo??5   %   $??3??6    	 btnNo#???  64   6V   5      5      5      