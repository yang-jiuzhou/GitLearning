   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   %    HBBio.Communication.CalOEMCDWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -local!clr-namespace:HBBio.Communication  5   	   -   5           Com_CalOEMCDWin  ? Title#  ?   5	   	    ? SizeToContent$ WidthAndHeight=?63    ? WindowStartupLocation$ CenterOwner=?6Y   .??5
      ? $??10q?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $
??Wrap=?6!     Com_CalOEMCDInfo#???  65   ? 5   
   $??2??6    ? ColumnDefinitions 5      ?? 5      $??* ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??5 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??25 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??5 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??* ?6#   .+?5!      ?? $??1??6     	Com_CalCD#???  6(   $  Xceed.Wpf.Toolkit.DoubleUpDown  5"      -   5          Name$ doubleCD??5"       $??3??62   ?  ?Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ F3??6C   ,  &Xceed.Wpf.Toolkit.Primitives.InputBase   
IsReadOnly . 6U   ?? 5#      $??5??6     Com_CalTemp#???  6(     5$      -   5        $ doubleTT??5$       $??7??62    . 6B   5%      ? 5&   
   $??4??6    5'      ?? 5(      $
??Auto ?6    ?? 5)      $??10 ?6    ?? 5*      $
??Auto ?6    ?? 5+      $??10 ?6    ?? 5,      $
??Auto ?6    ?? 5-      $??10 ?6    ?? 5.      $
??Auto ?6     50      ?? 51      $??* ?6#   ?? 52      $
??Auto ?6#   ?? 53      $??15 ?6#   ?? 54      $
??Auto ?6#   ?? 55      $??5 ?6#   ?? 56      $
??Auto ?6#   ?? 57      $??* ?6#   .+?59      ?? -   5         ??Name$ labSS1??59      $??0??6(   $??1??65     Com_StandardSolutions1#???  6E     5:      -   5        $ 	doubleSS1??5:       $??0??63   $??3??6@   $ F3??6P   ?  ?Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Value$	 0X?6b   {?6l   ?? 
 ?? ElementName$
 btnS1?? ?? Path$ 	IsEnabled??? 5;      -   5         ??Name$ btnS1??5;      $??0??6$   $??5??61     Com_Standard1#???  6A   {?.  6}   ?? 5<      -   5        $ labSS2??5<      $??2??6(   $??1??65     Com_StandardSolutions2#???  6E     5=      -   5        $ 	doubleSS2??5=       $??2??63   $??3??6@   $ F3??6P   $	 1.413X?6b   {?6p   ?? $
 btnS2??$ 	IsEnabled??? 5>      -	   5        $ btnS2??5>      $??2??6$   $??5??61     Com_Standard2#???  6A   {?.  6}   ?? 5?      -
   5        $ labSS3??5?      $??4??6(   $??1??65     Com_StandardSolutions3#???  6E     5@      -   5        $ 	doubleSS3??5@       $??4??63   $??3??6@   $ F3??6P   $	 12.880X?6b   {?6q   ?? $
 btnS3??$ 	IsEnabled??? 5A      -   5        $ btnS3??5A      $??4??6$   $??5??61    	 Com_Standard3#??? 	 6A   {?.  6}   ?? 5B      -   5        $ labSS4??5B      $??6??6(   $??1??65    
 Com_StandardSolutions4#??? 
 6E     5C      -   5        $ 	doubleSS4??5C       $??6??63   $??3??6@   $ F3??6P   $	 80.000X?6b   {?6q   ?? $
 btnS4??$ 	IsEnabled??? 5D      -   5        $ btnS4??5D      $??6??6$   $??5??61     Com_Standard4#???  6A   {?.  6}   5E      ? 5F   
   $??6??6    5G      ?? 5H      $??* ?6#   ?? 5I      $
??Auto ?6#   ?? 5J      $??15 ?6#   ?? 5K      $
??Auto ?6#   .+?5M      ?? -   5        $ btnCalStart??5M      $??1??6*     Com_CalStart#???  6:   6c   ?? 5N      -   5        $ btnOut??5N      $??3??6%     btnClose#???  65   6Z   5O      5P      5Q      