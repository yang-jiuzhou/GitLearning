   M S B A M L   `   `   `  ????G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   )    #HBBio.MethodEdit.MixtureGridItemWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   *localclr-namespace:HBBio.MethodEdit  5   	   ,comm!clr-namespace:HBBio.Communication  5   	   -   5           ME_MixtureGridItemWin  ? Title#  [  5
   	   $	??750??68   $	??750??6D    ? WindowStartupLocation$ CenterScreen=?6Q   .??5      ? $??10q?6    ? RowDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $
??Auto=?6$   .??5      ?  5      ?? 5      $
??Auto ?6$   ?? 5      $??10 ?6$   ?? 5      $
??Auto ?6$   ?? 5      $??5 ?6$   ?? 5      $
??Auto ?6$   ?? 5      $??10 ?6$   ?? 5      $
??Auto ?6$   .+?5      ?? $??0??6     ME_MGI_Base#??[ 6(   .??5      ?? $??5q?6#   $
??Auto=?6.   .??5      ? ??6$   ?? *Width+ ?? RelativeSource ??  ?? Mode$ FindAncestor=? ?? AncestorType" ??.+?5      ?? $??10q?6)    ?? Orientation$ 
Horizontal=?65   .+?5    "   ??   labNote1#??[ 6,   ?? 5!   "   ??6*   ?? *MNote+5"      ?? 5#      $??10q?6)   $ 
Horizontal=?65   .+?5$   "   ??   labFillSystem1#??[ 6,   ?? 5%   "   &    HBBio.Communication.EnumWashInfo   NameList#??Z 6+    ?? SelectedIndex 6_   ?? 	 ?? Path$	 MFillSystem?
 ?? Mode$
 TwoWay=? ?? UpdateSourceTrigger$ 	LostFocus=?5&      ?? 5'      $??10q?6)   $ 
Horizontal=?65   .+?5(   "   ??   ME_IncubationTime1#??[ 6,   $  Xceed.Wpf.Toolkit.DoubleUpDown  5)   "   ?  ?Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 64   ?? *MIncubation+5*      5+      5,      5-      ?? 5.      $??2??6     ME_MGI_Flow#??[ 6(   .??5/      ?? $??5q?6#   $
??Auto=?6.   .??50      ? ??6$   ?? *Width+ ?? $ FindAncestor=?" ??.+?51      ?? $??10q?6)   $ 
Horizontal=?65   .+?52   "   ?? -   5         ??Name$ labTVCV??52   ,     53   "    64   ?? *	MBaseTVCV+54      ?? 55      -   5         ??Name$ colSampleFlowRate??55   )   $??10q?6D   $ 
Horizontal=?6P   .+?56   "   ?? -   5        $ labSampleFlowRate??56   ,     57   "    64   ?? *MFlowVolLenSample+58      ?? 59      -   5        $ colSystemFlowRate??59   )   $??10q?6D   $ 
Horizontal=?6P   .+?5:   "   ?? -   5        $ labSystemFlowRate??5:   ,     5;   "    64   ?? *MFlowVolLenSystem+5<      ?? 5=      -   5        $
 colB??5=   )   $??10q?67   $ 
Horizontal=?6C   .+?5>   "   ??  
 LabBS1#??[ 6,     5?   "    64   ?? *MPerBS+?? 5@   "   $??20,0,0,0q?6,    
 LabBE1#??[ 6>     5A   "    64   ?? *MPerBE+5B      ?? 5C      -   5        $
 colC??5C   )   $??10q?67   $ 
Horizontal=?6C   .+?5D   "   ??  
 LabCS1#??[ 6,     5E   "    64   ?? *MPerCS+?? 5F   "   $??20,0,0,0q?6,    
	 LabCE1#??[	 6>     5G   "    64   ?? *MPerCE+5H      ?? 5I      -	   5        $
 colD??5I   )   $??10q?67   $ 
Horizontal=?6C   .+?5J   "   ??  

 LabDS1#??[
 6,     5K   "    64   ?? *MPerDS+?? 5L   "   $??20,0,0,0q?6,    
 LabDE1#??[ 6>     5M   "    64   ?? *MPerDE+5N      5O      5P      5Q      ?? 5R      $??4??6     ME_MGI_Valve#??[ 6(   .??5S      ?? $??5q?6#   $
??Auto=?6.   .??5T      ? ??6$   ?? *Width+ ?? $ FindAncestor=?" ??.+?5U      ?? -
   5        $ colInS??5U   )   $??10q?69   $ 
Horizontal=?6E   .+?5V   "   ??   labInS1#??[ 6,   ?? 5W   "   %   HBBio.Communication.EnumInSInfo   NameList#??Z 6+    6^   ?? $
	 MInS?$
 TwoWay=?$ 	LostFocus=?5X      ?? 5Y      -   5        $ colInA??5Y   )   $??10q?69   $ 
Horizontal=?6E   .+?5Z   "   ??   labInA1#??[ 6,   ?? 5[   "   %   HBBio.Communication.EnumInAInfo   NameList#??Z 6+    6^   ?? $
	 MInA?$
 TwoWay=?$ 	LostFocus=?5\      ?? 5]      -   5        $ colInB??5]   )   $??10q?69   $ 
Horizontal=?6E   .+?5^   "   ??   labInB1#??[ 6,   ?? 5_   "   %   HBBio.Communication.EnumInBInfo   NameList#??Z 6+    6^   ?? $
	 MInB?$
 TwoWay=?$ 	LostFocus=?5`      ?? 5a      -   5        $ colInC??5a   )   $??10q?69   $ 
Horizontal=?6E   .+?5b   "   ??   labInC1#??[ 6,   ?? 5c   "   %   HBBio.Communication.EnumInCInfo   NameList#??Z 6+    6^   ?? $
	 MInC?$
 TwoWay=?$ 	LostFocus=?5d      ?? 5e      -   5        $ colInD??5e   )   $??10q?69   $ 
Horizontal=?6E   .+?5f   "   ??   labInD1#??[ 6,   ?? 5g   "   %   HBBio.Communication.EnumInDInfo   NameList#??Z 6+    6^   ?? $
	 MInD?$
 TwoWay=?$ 	LostFocus=?5h      ?? 5i      -   5        $ colIJY??5i   )   $??10q?69   $ 
Horizontal=?6E   .+?5j   "   ??   labIJV1#??[ 6,   ?? 5k   "   %	   HBBio.Communication.EnumIJVInfo 	  NameList#??Z 6+    6^   ?? $
	 MIJV?$
 TwoWay=?$ 	LostFocus=?5l      ?? 5m      -   5        $ colBPV??5m   )   $??10q?69   $ 
Horizontal=?6E   .+?5n   "   ??   labBPV1#??[ 6,   ?? 5o   "   %
   HBBio.Communication.EnumBPVInfo 
  NameList#??Z 6+    6^   ?? $
	 MBPV?$
 TwoWay=?$ 	LostFocus=?5p      ?? 5q      -   5        $ colCPV??5q   )   $??10q?69   $ 
Horizontal=?6E   .+?5r   "   ??   labCPV1#??[ 6,   ?? 5s   "   %   HBBio.Communication.EnumCPVInfo   NameList#??Z 6+    6^   ?? $
	 MCPV?$
 TwoWay=?$ 	LostFocus=?5t      ?? 5u      -   5        $ colOut??5u   )   $??10q?69   $ 
Horizontal=?6E   .+?5v   "   ??   labOut1#??[ 6,   ?? 5w   "   %   HBBio.Communication.EnumOutInfo   NameList#??Z 6+    6^   ?? $	 MVOut?$
 TwoWay=?$ 	LostFocus=?5x      5y      5z      5{      ?? 5|      $??6??6     ME_MGI_Other#??[ 6(   .??5}      ?? $??5q?6#   $
??Auto=?6.   .??5~      ? -   5         ?Name$ otherWPanel??5~   $   ??69   ?? *Width+ ?? $ FindAncestor=?" ??.+?5      ?? -   5        $ colMixer??5   )   $??10q?6;   $ 
Horizontal=?6G   .+?5?   "   ??   	labMixer1#??[ 6,   ?? 5?   "    d? 	IsChecked 6+   ?? *MMixer+5?      ?? 5?      -   5        $ 
colUVClear??5?   )   $??10q?6=   $ 
Horizontal=?6I   .+?5?   "   ??  	 labUV#??[ 6,   ?? 5?   "     labUVReset1#??[ 6,   ?? 5?   "    6+   ?? *
MUVClear+5?      5?      5?      5?      5?      5?      ? 5?   
   $??2??6    ? ColumnDefinitions 5?      ?? 5?      $??* ?6#   ?? 5?      $
??Auto ?6#   ?? 5?      $??5 ?6#   ?? 5?      $
??Auto ?6#   .+?5?      ?? -   5         ??Name$ btnOK??5?      $??1??6$    	 btnOK#???  64   6V   ?? 5?      -   5        $ 	btnCancel??5?      $??3??6(     	btnCancel#???  68   6^   5?      5?      5?      