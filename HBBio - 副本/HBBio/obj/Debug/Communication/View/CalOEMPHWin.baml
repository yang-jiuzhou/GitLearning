   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   %    HBBio.Communication.CalOEMPHWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -local!clr-namespace:HBBio.Communication  5   	   -   5           Com_CalOEMPHWin  ? Title#  ?   5	   	    ? SizeToContent$ WidthAndHeight=?63    ? WindowStartupLocation$ CenterOwner=?6Y   .??5
      ? $??10q?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $
??Wrap=?6!     Com_CalOEMPHInfo#???  65   ? 5   
   $??2??6    ? ColumnDefinitions 5      ?? 5      $??* ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??5 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??25 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??5 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??* ?6#   .+?5!      ?? $??1??6     	Com_CalPH#???  6(   $  Xceed.Wpf.Toolkit.DoubleUpDown  5"      -   5          Name$ doublePH??5"       $??3??62   ,  &Xceed.Wpf.Toolkit.Primitives.InputBase   
IsReadOnly . 6B   ?? 5#      $??5??6     Com_CalTemp#???  6(     5$      -   5        $ doubleTT??5$       $??7??62    . 6B   5%      ? 5&   
   $??4??6    5'      ?? 5(      $
??Auto ?6    ?? 5)      $??10 ?6    ?? 5*      $
??Auto ?6    ?? 5+      $??10 ?6    ?? 5,      $
??Auto ?6     5.      ?? 5/      $??* ?6#   ?? 50      $
??Auto ?6#   ?? 51      $??15 ?6#   ?? 52      $
??Auto ?6#   ?? 53      $??5 ?6#   ?? 54      $
??Auto ?6#   ?? 55      $??* ?6#   .+?57      ?? -   5         ??Name$ labSS1??57      $??0??6(   $??1??65     Com_StandardSolutions1#???  6E     58      -   5        $ 	doubleSS1??58       $??0??63   $??3??6@   ?  ?Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 4X?6P   	   Maximum$	 14X?6Z   {?6g   ?? 
 ?? ElementName$
 btnS1?? ?? Path$ 	IsEnabled??? 59      -   5         ??Name$ btnS1??59      $??0??6$   $??5??61     Com_Standard1#???  6A   {?.  6}   ?? 5:      -   5        $ labSS2??5:      $??2??6(   $??1??65     Com_StandardSolutions2#???  6E     5;      -   5        $ 	doubleSS2??5;       $??2??63   $??3??6@   $ 7X?6P   $	 14X?6Z   {?6g   ?? $
 btnS2??$ 	IsEnabled??? 5<      -	   5        $ btnS2??5<      $??2??6$   $??5??61     Com_Standard2#???  6A   {?.  6}   ?? 5=      -
   5        $ labSS3??5=      $??4??6(   $??1??65     Com_StandardSolutions3#???  6E     5>      -   5        $ 	doubleSS3??5>       $??4??63   $??3??6@   $ 10X?6P   $	 14X?6[   {?6h   ?? $
 btnS3??$ 	IsEnabled??? 5?      -   5        $ btnS3??5?      $??4??6$   $??5??61    	 Com_Standard3#??? 	 6A   {?.  6}   5@      ? 5A   
   $??6??6    5B      ?? 5C      $??* ?6#   ?? 5D      $
??Auto ?6#   ?? 5E      $??15 ?6#   ?? 5F      $
??Auto ?6#   .+?5H      ?? -   5        $ btnCalStart??5H      $??1??6*    
 Com_CalStart#??? 
 6:   6c   ?? 5I      -   5        $ btnOut??5I      $??3??6%     btnClose#???  65   6Z   5J      5K      5L      