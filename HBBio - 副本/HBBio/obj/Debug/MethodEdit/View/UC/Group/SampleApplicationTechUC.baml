   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5      )clr-namespace:HBBio.ShareHBBio.Share  5      9!clr-namespace:HBBio.CommunicationHBBio.Communication  5	      .    (HBBio.MethodEdit.SampleApplicationTechUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      *localclr-namespace:HBBio.MethodEdit  5      %shareclr-namespace:HBBio.Share  5      ,comm!clr-namespace:HBBio.Communication  5	        ? 	Resources  5      #   HBBio.Share.EnumBoolConverter   EnumBoolConverter??%   &	          5   
   6>   5      .??5      ??  * &ME_EnumGroupType_SampleApplicationTech#???  6   .??5   
   ? -   5         ?Name$
 grid??5      $??10q?6    ? ColumnDefinitions 5      ?? 5      $
??Auto ?6#   ?? 5      $??5 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??5 ?6#   ?? 5      $
??Auto ?6#   ?? 5      $??* ?6#    ? RowDefinitions 5      ?? 5      $
??Auto ?6    ?? 5      $??5 ?6    ?? 5      $
??Auto ?6    ?? 5      $??5 ?6    ?? 5      $
??Auto ?6    ?? 5      $??5 ?6    ?? 5      $
??Auto ?6    ?? 5       $??5 ?6    ?? 5!      $
??Auto ?6    ?? 5"      $??5 ?6    ?? 5#      $
??Auto ?6    ?? 5$      $??5 ?6    ?? 5%      $
??Auto ?6    ?? 5&      $??5 ?6    ?? 5'      $
??Auto ?6    ?? 5(      $??5 ?6    ?? 5)      $
??Auto ?6    ?? 5*      $??* ?6    .+?5,      	? $??0??6   $??6??6'      ME_EnumSAT_ManualLoopFilling#???  6;    d? 	IsChecked 6t   ??  ?? Path$ MEnumSAT? ?? 	Converter# [     HBBio.MethodEdit.EnumSAT   ManualLoopFilling ?? ConverterParameter# Z 	? 5-      $??2??6   $??6??6'    $  ME_EnumSAT_SamplePumpLoopFilling#???  6;    6x   ?? $ MEnumSAT?# [  	   SamplePumpLoopFilling# Z	 	? 5.      $??4??6   $??6??6'     ME_EnumSAT_ISDOC#???  6;    6h   ?? $ MEnumSAT?# [  
   ISDOC# Z
 ?? 5/      $??6??6   $??6??6%     ME_AutoSampleValve#???  69   $
??Wrap=?6e   y?6y   ?? *MVisibAutoSampleValve+?? 50      $??8??6   $??6??6%     ME_NTSPWBWWSPTSA#???  69   $
??Wrap=?6c   y?6w   ?? *MVisibMENTSPWBWWSPTSA+?? 51      $??10??6     labInS1#???  6&   y?6G   ?? *	MVisibInS+?? 52      $??10??6   $??2??6%   %   HBBio.Communication.EnumInSInfo   NameList#??Z 65    ?? SelectedIndex 6h   ?? $
 MInS? ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?y?6?   ?? *	MVisibInS+?? 53      $??12??6    ?? Orientation$ 
Horizontal=?6'   y?6@   ?? *MVisibSampleTVCV+.+?54      ??   	ME_Sample#???  6   ?? 55      ??6   ?? *
MBaseStr+?? 56      $??:??6   57      $  Xceed.Wpf.Toolkit.DoubleUpDown  58      $??12??6    $??2??6.   ?  ?Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6>   ?? *MSampleTVCV+y?6\   ?? *MVisibSampleTVCV+?? 59      $??12??6   $??4??6&   ??66   ?? *MBaseUnitStr+y?6T   ?? *MVisibSampleTVCV+?? 5:      $??14??6    	 ME_FillLoopWith1#??? 	 6&   y?6P   ?? *MVisibFillLoopWith+  5;      $??14??6    $??2??6.    6>   ?? *MFillLoopWith+y?6^   ?? *MVisibFillLoopWith+?? 5<      $??14??6   $??4??6&      HBBio.Share.DlyBase   
SC_VUNITML#??Z 66   y?6a   ?? *MVisibFillLoopWith+?? 5=      $??16??6    
 ME_EmptyLoopWith1#??? 
 6&   y?6Q   ?? *MVisibEmptyLoopWith+  5>      $??16??6    $??2??6.    6>   ?? *MEmptyLoopWith+y?6_   ?? *MVisibEmptyLoopWith+?? 5?      $??16??6   $??4??6&   #??Z 66   y?6a   ?? *MVisibEmptyLoopWith+5@      5A      5B      