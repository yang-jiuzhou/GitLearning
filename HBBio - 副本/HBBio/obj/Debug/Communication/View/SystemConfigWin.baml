   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   )    #HBBio.Communication.SystemConfigWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -local!clr-namespace:HBBio.Communication  5   	   %shareclr-namespace:HBBio.Share  5   	   -   5           Com_SystemConfigWin  � Title#  �   5
   	    � SizeToContent$ Height=�67   $	��750��6U    � WindowStartupLocation$ CenterScreen=�6x   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � -   5         �Name$ 	warpPanel��5      $��0��6'   .+�5      �� $��10q�6     
Com_Column#���  6#   .��5      � -   5         �Name$ 
gridColumn��5      $��5q�6+    5      �� 5      $
��Auto �6(   �� 5      $��10 �6(   �� 5      $
��Auto �6(   �� 5      $��10 �6(   �� 5      $
��Auto �6(    � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5      $��5 �6+   �� 5      $
��Auto �6+   .+�5       �� -   5         ��Name$ labColumnVol1��5        $��0��67   $��0��6D     labColumnVol1#���  6T   $  Xceed.Wpf.Toolkit.DoubleUpDown  5!      $��0��6(   $��2��65   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6E   �� *
MColumnVol+�� 5"      -   5        $ labColumnDiameter1��5"       $��2��6<   $��0��6I     labColumnDiameter1#���  6Y     5#      $��2��6(   $��2��65    6E   �� *MColumnDiameter+�� 5$      -   5        $ labColumnHeight1��5$       $��4��6:   $��0��6G     labColumnHeight1#���  6W     5%      $��4��6(   $��2��65    6E   �� *MColumnHeight+5&      5'      �� 5(      $��10q�6     Com_Wash#���  6#   .��5)      � -   5        $ gridWash��5)      $��5q�6)    5*      �� 5+      $
��Auto �6(   �� 5,      $��10 �6(   �� 5-      $
��Auto �6(    5/      �� 50      $
��Auto �6+   �� 51      $��5 �6+   �� 52      $
��Auto �6+   .+�54      �� -   5        $ labWashTime1��54       $��0��66   $��0��6C     Com_WashTime1#���  6S     55      $��0��6(   $��2��65   	   Minimum$		 0.1X�6a    6E   �� *	MWashTime+6a   �� 56      -	   5        $ labWashFlowPer1��56       $��2��69   $��0��6F     Com_WashFlowPer1#���  6V     57      $��2��6(   $��2��65   
   Maximum$	
 100X�6d    6E   �� *MWashFlowPer+6d   58      59      �� 5:      -
   5         ��Name$ grouppHCdUV��5:      $��10q�6,     Com_pHCdUVDelayVol#���  68   .��5;      � -   5        $ 
gridpHCdUV��5;      $��5q�6+    5<      �� 5=      $
��Auto �6(   �� 5>      $��10 �6(    5@      �� 5A      $
��Auto �6+   �� 5B      $��5 �6+   �� 5C      $
��Auto �6+   �� 5D      $��5 �6+   �� 5E      $
��Auto �6+   .+�5G      �� -   5        $ 
labDefault��5G       $��0��64   $��0��6A    	 Com_DefaultDelayVol1#��� 	 6Q     5H      -   5          Name$ doubleDefaultDelayVol��5H   (   $��0��6G   $��2��6T    6d   �� *	MDelayVol+�� 5I      -   5        $ labDefaultUnit��5I       $��0��68   $��4��6E      HBBio.Share.DlyBase   
SC_VUNITML#��Z 6U   5J      5K      �� 5L      -   5        $ groupCollector��5L      $��10q�6/    
 Com_Coll#��� 
 6;   .��5M      � -   5        $ gridCollector��5M      $��5q�6.    5N      �� 5O      $
��Auto �6(   �� 5P      $��10 �6(   �� 5Q      $
��Auto �6(    5S      �� 5T      $
��Auto �6+   �� 5U      $��5 �6+   �� 5V      $
��Auto �6+   .+�5X      �� -   5        $ labGLTJ��5X       $��0��61   $��0��6>     Com_CollBottleVol#���  6N     5Y      $��0��6(   $��2��65    6E   �� *MGLTJ+�� 5Z      -   5         ��Name$ btnCollector��5Z      $��2��63   $��0��6@   $��3��6P     Com_CollPallet#���  6d   6�   5[      5\      �� 5]      $��10q�6     Com_AlarmWarning#���  6#   .��5^      �� -   5        $ btnAlarmWarning��5^       
 btnSet#���  62   $��Center=�6U   6p   5_      �� 5`      -   5        $ groupAS��5`      $��10q�6(    
 Com_AS#���  64   .��5a      � -   5        $ wrapAS��5a      5b      �� 5c      -   5        $ groupCal��5c      $��10q�6)     Com_Cal#���  65   .��5d      � -   5        $ gridCal��5d      $��5q�6(   5e      �� 5f      $��10q�6     Com_PID#���  6#   .��5g      � -   5        $ gridPID��5g      $��5q�6(    5h      �� 5i      $
��Auto �6(   �� 5j      $��10 �6(   �� 5k      $
��Auto �6(   �� 5l      $��10 �6(   �� 5m      $
��Auto �6(    5o      �� 5p      $
��Auto �6+   �� 5q      $��5 �6+   �� 5r      $
��Auto �6+   .+�5t      �� -   5        $ labPIDP��5t       $��0��61   $��0��6>   $
��P : ��6N     5u      $��0��6(   $��2��65    6E   �� *MPIDP+�� 5v      -   5        $ labPIDI��5v       $��2��61   $��0��6>   $
��I : ��6N     5w      $��2��6(   $��2��65    6E   �� *MPIDI+�� 5x      -   5        $ labPIDD��5x       $��4��61   $��0��6>   $
��D : ��6N     5y      $��4��6(   $��2��65    6E   �� *MPIDD+5z      5{      �� 5|      -   5        $ 
groupOther��5|      $��10q�6+     	Com_Other#���  67   .��5}      � -   5        $ 	gridOther��5}      $��5q�6*    5~      �� 5      $
��Auto �6(   �� 5�      $��10 �6(   �� 5�      $
��Auto �6(   �� 5�      $��10 �6(   �� 5�      $
��Auto �6(   �� 5�      $��10 �6(   �� 5�      $
��Auto �6(   .+�5�      �� -   5         ��Name$ chboxResetValve��5�      $��0��68     Com_ResetValve#���  6E    d� 	IsChecked 6p   �� *MResetValve+�� 5�      -   5        $ chboxCloseUV��5�      $��2��65     Com_CloseUV#���  6B    6j   �� *
MCloseUV+�� 5�      -    5        $ chboxOpenMixer��5�      $��4��67     Com_OpenMixer#���  6D    6n   �� *
MOpenMixer+�� 5�      -!   5        $ 
chboxUVIJV��5�      $��6��63     	Com_UVIJV#���  6@    6f   �� *MUVIJV+5�      5�      5�      � 5�   
   $��2��6    5�      �� 5�      $��* �6#   �� 5�      $
��Auto �6#   �� 5�      $��10 �6#   �� 5�      $
��Auto �6#   .+�5�      �� -"   5        $ btnOK��5�      $��1��6$    	 btnOK#���  64   6V   �� 5�      -#   5        $ 	btnCancel��5�      $��3��6(     	btnCancel#���  68   6^   5�      5�      5�      