   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  )clr-namespace:HBBio.ShareHBBio.Share  )    #HBBio.Communication.SystemConfigWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -local!clr-namespace:HBBio.Communication  %shareclr-namespace:HBBio.Share  -      Com_SystemConfigWin  � Title#  �    � SizeToContent$ Height=�$	��750�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $��* ��� $��10 ��� $
��Auto �.+�� -    �Name$ 	warpPanel��$��0��.+��� $��10q�  
Com_Column#���  .��� -    �Name$ 
gridColumn��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ labColumnVol1��$��0��$��0��  labColumnVol1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  $��0��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *
MColumnVol+�� -   $ labColumnDiameter1��$��2��$��0��  labColumnDiameter1#���    $��2��$��2�� �� *MColumnDiameter+�� -   $ labColumnHeight1��$��4��$��0��  labColumnHeight1#���    $��4��$��2�� �� *MColumnHeight+�� $��10q�  Com_Wash#���  .��� -   $ gridWash��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   $ labWashTime1��$��0��$��0��  Com_WashTime1#���    $��0��$��2��	   Minimum$		 0.1X� �� *	MWashTime+�� -	   $ labWashFlowPer1��$��2��$��0��  Com_WashFlowPer1#���    $��2��$��2��
   Maximum$	
 100X� �� *MWashFlowPer+�� -
    ��Name$ grouppHCdUV��$��10q�  Com_pHCdUVDelayVol#���  .��� -   $ 
gridpHCdUV��$��5q� �� $
��Auto ��� $��10 � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   $ 
labDefault��$��0��$��0�� 	 Com_DefaultDelayVol1#��� 	   -     Name$ doubleDefaultDelayVol��$��0��$��2�� �� *	MDelayVol+�� -   $ labDefaultUnit��$��0��$��4��   HBBio.Share.DlyBase   
SC_VUNITML#��Z �� -   $ groupCollector��$��10q� 
 Com_Coll#��� 
 .��� -   $ gridCollector��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   $ labGLTJ��$��0��$��0��  Com_CollBottleVol#���    $��0��$��2�� �� *MGLTJ+�� -    ��Name$ btnCollector��$��2��$��0��$��3��  Com_CollPallet#���  �� $��10q�  Com_AlarmWarning#���  .���� -   $ btnAlarmWarning�� 
 btnSet#���  $��Center=��� -   $ groupAS��$��10q� 
 Com_AS#���  .��� -   $ wrapAS���� -   $ groupCal��$��10q�  Com_Cal#���  .��� -   $ gridCal��$��5q��� $��10q�  Com_PID#���  .��� -   $ gridPID��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   $ labPIDP��$��0��$��0��$
��P : ��  $��0��$��2�� �� *MPIDP+�� -   $ labPIDI��$��2��$��0��$
��I : ��  $��2��$��2�� �� *MPIDI+�� -   $ labPIDD��$��4��$��0��$
��D : ��  $��4��$��2�� �� *MPIDD+�� -   $ 
groupOther��$��10q�  	Com_Other#���  .��� -   $ 	gridOther��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ chboxResetValve��$��0��  Com_ResetValve#���   d� 	IsChecked �� *MResetValve+�� -   $ chboxCloseUV��$��2��  Com_CloseUV#���   �� *
MCloseUV+�� -    $ chboxOpenMixer��$��4��  Com_OpenMixer#���   �� *
MOpenMixer+�� -!   $ 
chboxUVIJV��$��6��  	Com_UVIJV#���   �� *MUVIJV+� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -"   $ btnOK��$��1�� 	 btnOK#���  �� -#   $ 	btnCancel��$��3��  	btnCancel#���  