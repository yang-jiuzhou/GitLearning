   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  '    !HBBio.MethodEdit.PersonalPhaseWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.MethodEdit     +shareclr-namespace:HBBio.Share     -      ME_PersonalPhaseWin  � Title#  �   $	��800��$
��1000��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=� � 	Resources )  #HBBio.Share.BoolVisibilityConverter  BoolVisibilityConverter��%   &	         .��� $��5q� � RowDefinitions �� $��* ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$
��Auto=�$
��Auto=�.���� .+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ chboxFlowRate��$	��Top=�!   HBBio.MethodEdit.FlowRateUC  -     Name$ 
flowRateUC��{�.  �� $ 
Horizontal=�.+��� -   $ chboxValveSelection��$	��Top=�'   !HBBio.MethodEdit.ValveSelectionUC  -     Name$ valveSelectionUC��{�.     VisibilityPer �� 	 �� ElementName$!	 rbtnValveSelectionUCPerShow��
 �� Path$
 	IsChecked� �� 	Converter# [    VisibilityFillSystem �� $(	 "rbtnValveSelectionUCFillSystemShow��$
 	IsChecked�# [ ��   ME_Attributes#���  .���� .+��� $��5q�$ 
Horizontal=�.+��� $��5q� 
 LabPer#���  	� -    	�Name$! rbtnValveSelectionUCPerShow��$��5q�  labShow#���   d� 	IsChecked$
 TrueY�	� $��5q�  labHide#���  �� $��5q�$ 
Horizontal=�.+��� $��5q�  ME_FillSystem#���  	� -   $( "rbtnValveSelectionUCFillSystemShow��$��5q�#���  $
 TrueY�	� $��5q�#���  �� $ 
Horizontal=�.+��� -   $ 
chboxMixer��$	��Top=�   HBBio.MethodEdit.MixerUC  -	     Name$ mixerUC��{�.  �� $ 
Horizontal=�.+��� -
   $ chboxBPV��$	��Top=�   HBBio.MethodEdit.BPVUC  -     Name$ bpvUC��{�.  �� $ 
Horizontal=�.+��� -   $ chboxUVReset��$	��Top=�    HBBio.MethodEdit.UVResetUC  -     Name$ 	uvResetUC��{�.  �� $ 
Horizontal=�.+��� -   $  chboxSampleApplicationTech��$	��Top=�.   (HBBio.MethodEdit.SampleApplicationTechUC  -     Name$ sampleApplicationTechUC��{�.  �� $ 
Horizontal=�.+��� -   $ 	chboxTVCV��$	��Top=�   HBBio.MethodEdit.TVCVUC  -     Name$ tvcvUC��{�.  �� $ 
Horizontal=�.+��� -   $ chboxFlowValveLength��$	��Top=�(	   "HBBio.MethodEdit.FlowValveLengthUC	  -    	 Name$ flowValveLengthUC��{�.  �� $ 
Horizontal=�.+��� -   $ chboxFlowRatePer��$	��Top=�$
   HBBio.MethodEdit.FlowRatePerUC
  -    
 Name$ flowRatePerUC��{�.  �� $ 
Horizontal=�.+��� -   $ chboxPHCDUVUntil��$	��Top=�$   HBBio.MethodEdit.PHCDUVUntilUC  -     Name$ pHCdUVUnitUC��{�.     VisibilityHeader$ Visible=�   
TextHeader �� $	 txtpHCdUVUnitUCHeader��$

 Text��� #���  .���� .+��� $��5q�$ 
Horizontal=�.+��� $��5q�  labName#���  �� -    ��Name$ txtpHCdUVUnitUCHeader��$��5q��� $ 
Horizontal=�.+��� -   $ chboxCollValveCollector��$	��Top=�+   %HBBio.MethodEdit.CollValveCollectorUC  -     Name$ collUC��{�.  �� $ 
Horizontal=�.+��� -   $ chboxCIP��$	��Top=�   HBBio.MethodEdit.CIPUC  -     Name$ cipUC��{�.  �� $��2��.+���   labName1#���  �� -   $ txtName��$��40,0,0,0q�� $��4�� � ColumnDefinitions �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 		 btnOK#��� 	 �� -   $ 	btnCancel��$��3�� 
 	btnCancel#��� 
 