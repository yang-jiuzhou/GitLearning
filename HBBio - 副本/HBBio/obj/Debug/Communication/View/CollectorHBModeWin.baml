   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   ,    &HBBio.Communication.CollectorHBModeWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.Communication     5   	   -   5           Com_CollectorHBModeWin  ? Title#  ?   5       ? SizeToContent$ WidthAndHeight=?69    ? WindowStartupLocation$ CenterOwner=?6_   .??5	      ? $??10q?6    ? ColumnDefinitions 5
   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? -   5         ??Name$ labGetL??5      $??0??6%   $??0??62     Com_TrayLeftCount1#???  6B   ?? 5   
   -   5         ??Name$ txtGetL??5      $??0??6#   $??2??60    ?? 
IsReadOnly . ??6@   ?? *	MCountL+6Y   ?? 5   
   -   5        $ labGetR??5      $??2??6%   $??0??62     Com_TrayRightCount1#???  6B   ?? 5   
   -   5        $ txtGetR??5      $??2??6#   $??2??60    . ??6@   ?? *	MCountR+6Y   ?? 5    
   -   5        $ labSetL??5       $??4??6%   $??0??62     Com_TrayLeftMode1#???  6B   ?? 5!   
   -   5         ??Name$ cboxSetL??5!      $??4??6%   $??2??62   	 ?? 
IsReadOnly	 . 
 ?? SelectedIndex
 6B   ?? *MModeL+6c   ?? 5"   
   -   5        $ labSetR??5"      $??6??6%   $??0??62     Com_TrayRightMode1#???  6B   ?? 5#   
   -	   5        $ cboxSetR??5#      $??6??6%   $??2??62   	 . 
 6B   ?? *MModeR+6c   ?? 5$   
   -
   5         ??Name$ btnGet??5$      $??8??6!   $??0??6.   $??3??6>     Com_FindMode#???  6R   6{   ?? 5%   
   -   5        $ btnSet??5%      $??10??6!   $??0??6/   $??3??6?     btnApply#???  6S   6x   5&      5'      