   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   '    !HBBio.MethodEdit.PersonalPhaseWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.MethodEdit     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           ME_PersonalPhaseWin  � Title#  �   5	   	   $	��800��67   $
��1000��6D   $	��309��6Q   $	��500��6a    � WindowStartupLocation$ CenterScreen=�6p    � 	Resources 5
      )  #HBBio.Share.BoolVisibilityConverter  BoolVisibilityConverter��%   &	         5   
   6(   5      .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $
��Auto=�6$   $
��Auto=�6I   .��5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ chboxFlowRate��5      $	��Top=�66   !   HBBio.MethodEdit.FlowRateUC  5      -   5          Name$ 
flowRateUC��5   '   {�.  6;   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $ chboxValveSelection��5      $	��Top=�6<   '   !HBBio.MethodEdit.ValveSelectionUC  5      -   5          Name$ valveSelectionUC��5   -   {�.     VisibilityPer 5   -   �� 	 �� ElementName$!	 rbtnValveSelectionUCPerShow��
 �� Path$
 	IsChecked� �� 	Converter# [    VisibilityFillSystem 5   -   �� $(	 "rbtnValveSelectionUCFillSystemShow��$
 	IsChecked�# [ �� 5         ME_Attributes#���  6   .��5!      �� .+�5"      �� $��5q�6)   $ 
Horizontal=�64   .+�5#   "   �� $��5q�6,    
 LabPer#���  67   	� 5$   "   -   5         	�Name$! rbtnValveSelectionUCPerShow��5$   .   $��5q�6S     labShow#���  6^    d� 	IsChecked$
 TrueY�6�   	� 5%   "   $��5q�6.     labHide#���  69   5&      �� 5'      $��5q�6)   $ 
Horizontal=�64   .+�5(   "   �� $��5q�6,     ME_FillSystem#���  67   	� 5)   "   -   5        $( "rbtnValveSelectionUCFillSystemShow��5)   .   $��5q�6Z   #���  6e   $
 TrueY�6�   	� 5*   "   $��5q�6.   #���  69   5+      5,      5-      5.      �� 5/      $ 
Horizontal=�6   .+�50      �� -   5        $ 
chboxMixer��50      $	��Top=�63      HBBio.MethodEdit.MixerUC  51      -	   5          Name$ mixerUC��51   $   {�.  65   52      �� 53      $ 
Horizontal=�6   .+�54      �� -
   5        $ chboxBPV��54      $	��Top=�61      HBBio.MethodEdit.BPVUC  55      -   5          Name$ bpvUC��55   "   {�.  61   56      �� 57      $ 
Horizontal=�6   .+�58      �� -   5        $ chboxUVReset��58      $	��Top=�65       HBBio.MethodEdit.UVResetUC  59      -   5          Name$ 	uvResetUC��59   &   {�.  69   5:      �� 5;      $ 
Horizontal=�6   .+�5<      �� -   5        $  chboxSampleApplicationTech��5<      $	��Top=�6C   .   (HBBio.MethodEdit.SampleApplicationTechUC  5=      -   5          Name$ sampleApplicationTechUC��5=   4   {�.  6U   5>      �� 5?      $ 
Horizontal=�6   .+�5@      �� -   5        $ 	chboxTVCV��5@      $	��Top=�62      HBBio.MethodEdit.TVCVUC  5A      -   5          Name$ tvcvUC��5A   #   {�.  63   5B      �� 5C      $ 
Horizontal=�6   .+�5D      �� -   5        $ chboxFlowValveLength��5D      $	��Top=�6=   (	   "HBBio.MethodEdit.FlowValveLengthUC	  5E      -   5         	 Name$ flowValveLengthUC��5E   .   {�.  6I   5F      �� 5G      $ 
Horizontal=�6   .+�5H      �� -   5        $ chboxFlowRatePer��5H      $	��Top=�69   $
   HBBio.MethodEdit.FlowRatePerUC
  5I      -   5         
 Name$ flowRatePerUC��5I   *   {�.  6A   5J      �� 5K      $ 
Horizontal=�6   .+�5L      �� -   5        $ chboxPHCDUVUntil��5L      $	��Top=�69   $   HBBio.MethodEdit.PHCDUVUntilUC  5M      -   5          Name$ pHCdUVUnitUC��5M   *   {�.     VisibilityHeader$ Visible=�6�      
TextHeader 6R   �� $	 txtpHCdUVUnitUCHeader��$

 Text�6�   �� 5N      #���  6   .��5O      �� .+�5P      �� $��5q�6)   $ 
Horizontal=�64   .+�5Q   "   �� $��5q�6,     labName#���  67   �� 5R   "   -   5         ��Name$ txtpHCdUVUnitUCHeader��5R   *   $��5q�6I   5S      5T      5U      5V      �� 5W      $ 
Horizontal=�6   .+�5X      �� -   5        $ chboxCollValveCollector��5X      $	��Top=�6@   +   %HBBio.MethodEdit.CollValveCollectorUC  5Y      -   5          Name$ collUC��5Y   1   {�.  6A   5Z      �� 5[      $ 
Horizontal=�6   .+�5\      �� -   5        $ chboxCIP��5\      $	��Top=�61      HBBio.MethodEdit.CIPUC  5]      -   5          Name$ cipUC��5]   "   {�.  61   5^      5_      5`      �� 5a   
   $��2��6   .+�5b      ��   labName1#���  6   �� 5c      -   5        $ txtName��5c      $��40,0,0,0q�6'   5d      � 5e   
   $��4��6    � ColumnDefinitions 5f      �� 5g      $��* �6#   �� 5h      $
��Auto �6#   �� 5i      $��5 �6#   �� 5j      $
��Auto �6#   .+�5l      �� -   5         ��Name$ btnOK��5l      $��1��6$    		 btnOK#��� 	 64   6V   �� 5m      -   5        $ 	btnCancel��5m      $��3��6(    
 	btnCancel#��� 
 68   6^   5n      5o      5p      