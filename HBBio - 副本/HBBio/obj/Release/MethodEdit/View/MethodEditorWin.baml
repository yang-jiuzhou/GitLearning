   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  9!clr-namespace:HBBio.CommunicationHBBio.Communication  &     HBBio.MethodEdit.MethodEditorWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  *localclr-namespace:HBBio.MethodEdit  %shareclr-namespace:HBBio.Share  ,comm!clr-namespace:HBBio.Communication  -      ME_MethodEditorWin  � Title#  �   $	��800��$
��1000��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=� � 	Resources )  #HBBio.Share.EnumVisibilityConverter  EnumVisibilityConverter"   HBBio.Share.IntBoolConverter  IntBoolConverter$   HBBio.Share.BoolFalseConverter  BoolFalseConverter �� 
TargetType  labCopyPhase  btnPhase �� 
TargetType ��Name �� Padding �� CornerRadius �� 
StartPoint	 �� EndPoint
 � Offset � Color �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value��%y  &	       &	      &	 
     &	      &	 !           �� " ��.�-   �� " ��.R��� $ border��$	��100��$	��2.5q�$ 8q�
���   �$��2q�$ 25������ $ 0.2,0&�$	 0.8,1&�.��� $
 0Q�$ White��� $	
 0.5Q�$ #D6DBE9��._��� #��z��$��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ border�� � ��
 ��  �.���  � RowDefinitions �� $��7* ��� $��3* �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $��* �.+��� $��0��.+�   HBBio.Share.IconButton  -     Name$ btnCopy��   HBBio.Share.IconButton   Click$ btnCopy_Click��  btnCopy � ToolTip# �  $��5q�   Image$, &/Bio-LabChrom;component/Image/copy.PNG��  -   $ btnPaste��$ btnPaste_Click��  btnPaste# �  $��5q�{�.  $- '/Bio-LabChrom;component/Image/paste.PNG��  -   $ btnSave��$ btnSave_Click��  btnSave# �  $��5q�$, &/Bio-LabChrom;component/Image/save.png��  -   $ btnSend��$ btnSend_Click�� 	 btnSend# � 	 $��5q�$, &/Bio-LabChrom;component/Image/send.png��]� $��2��.+�� $��Bottom=� �� $��* ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��* �.+�  -   $ btnNew��$ btnNew_Click��$��1�� 
 ME_AddPersonalPhase# � 
 $+ %/Bio-LabChrom;component/Image/add.PNG��  -   $ btnDel��$ btnDel_Click��$��3��  ME_DelPersonalPhase# �  $. (/Bio-LabChrom;component/Image/delete.PNG���� $
��Auto=����� -	    ��Name$ 	gridPhase�� J� 	AllowDrop . 
������ .+��� #��[ #��[   ME_EnumPhaseType_ColumnCIP#���  �� #��[ #��[  & "ME_EnumPhaseType_SampleApplication#���  �� #��[ #��[   ME_EnumPhaseType_ColumnWash#���  �� #��[ #��[   ME_EnumPhaseType_Elution#���  �� #��[ #��[  " ME_EnumPhaseType_Equilibration#���  �� #��[ #��[  " ME_EnumPhaseType_Miscellaneous#���  �� #��[ #��[   ME_EnumPhaseType_SystemCIP#���  �� #��[ #��[   ME_EnumPhaseType_Mixture#���  � $��4��
������� �� $
��Auto ��� $��5 ��� $��* �.+��� -
    ��Name$ btnMethodSettings��$��0��#��[   ME_MethodSettings#���  �� $��2��$
��Auto=����� -   $ stackPanelPhaseList��
������� . �� -    ��Name$ 
tabControl��$��6��.@��� #���  .���� -    ��Name$ scrMS��$
��Auto=�.��� -    �Name$ wrapPanelMS������  �� ElementName$ scrMS�� �� Path$ Width�.+���   ME_BasicInformation#���  .��� $��5q� �� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��$��0��  A_UserName1#���  �� -     ��Name$  labUserName��$��0��$��2������ *	MUserName+�� $��2��$��0��  ME_CreateTime1#���  �� -   $  labCreateTime��$��2��$��2������ *MCreateTime+�� $��4��$��0��  ME_ModifyTime1#���  �� -   $  labModifyTime��$��4��$��2������ *MModifyTime+��   ME_LoopSettings#���  .���� $��5q�! �� Orientation$! 
Horizontal=�$	��Top=�.+���   ME_LoopSet1#���  %  Xceed.Wpf.Toolkit.IntegerUpDown  -   "  Name$" intLoop��$	��5,0q��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]#   Minimum$# 1X�$   Value$ �� *MLoop+��   ME_ColumnSelection#���  .��� $��5q� �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��  ME_ColumnName1#���  �� -   % ��Name$% cboxColumnName��$��2��$��0��& �� DisplayMemberPath$& MName��' �� SelectedValuePath$	' MID��( �� SelectedValue( �� *	MColumnId+�� -   ) ��Name$) 	btnColumn��$��0��$��4��$��35��$��35��$��.���� $��2��  ME_ColumnVolume1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -   *  Name$* doubleCV��$��2��$��2���	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]+ 	  Minimum$
+ 0.01X�, 	  Value, �� *
MColumnVol+{��� $ cboxColumnName��$ SelectedValue�- �� 	Converter#- [ �� -   $  labCV��$��4��$��2��
   HBBio.Share.DlyBase. 
  
SC_VUNITML#��Z. �� -   $  labCPV��$��0��$��4��  labCPV1#���  �� -   $% cboxCPV��$��2��$��4��/ �� SelectedIndex/ �� $
 MCPV�0 �� Mode$0 TwoWay=�1 �� UpdateSourceTrigger$1 PropertyChanged=�y��� $ labCPV��$ 
Visibility���   ME_UnitSelection#���  .��� $��5q� �� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��   ME_MethodBaseUnit1#���   �� -   $% cboxMBU��$��2��$��0��$& MName��$' MMode��( �� *	MBaseUnit+�� $��2�� ! ME_FlowRateUnit1#��� ! �� -   $% cboxFRU��$��2��$��2��$& MName��$' MMode��( �� *MFlowRateUnit+{��� $ doubleCV��$ 	IsEnabled�#- [ ��  " labFlowRate#��� " .��� $��5q� �� $��* ��� $��5 ��� $
��Auto � �� $
��Auto �.+�  -   $* doubleFlowRate��$
��Left=�2 	  Maximum2 �� *MMaxFlowRate+, �� *	MFlowRate+�� -   $  labFlowRateUnitStr��$��2������ *MFlowRateUnitStr+�� -   3 ��Name$3 groupValveSelection�� # ME_ValveSelection#��� # .��� $��5q� �� $
��Auto ��� $��5 ��� $
��Auto ��� $��* � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* �.+��� -   $  	labInletA��$��0��$��0�� $ labInA1#��� $ �� -   $% 
cboxInletA��$��2��$��0��/ �� $
 MInA�$0 TwoWay=�$1 PropertyChanged=�y��� $ 	labInletA��$ 
Visibility��� -    $  	labInletB��$��0��$��2�� % labInB1#��� % �� -!   $% 
cboxInletB��$��2��$��2��/ �� $
 MInB�$0 TwoWay=�$1 PropertyChanged=�y��� $ 	labInletB��$ 
Visibility��� -"   $  	labInletC��$��0��$��4�� & labInC1#��� & �� -#   $% 
cboxInletC��$��2��$��4��/ �� $
 MInC�$0 TwoWay=�$1 PropertyChanged=�y��� $ 	labInletC��$ 
Visibility��� -$   $  	labInletD��$��0��$��6�� ' labInD1#��� ' �� -%   $% 
cboxInletD��$��2��$��6��/ �� $
 MInD�$0 TwoWay=�$1 PropertyChanged=�y��� $ 	labInletD��$ 
Visibility��� -&   $  labBPV��$��0��$��8�� ( labBPV1#��� ( �� -'   $% cboxBPV��$��2��$��8��/ �� $
 MBPV�$0 TwoWay=�$1 PropertyChanged=�y��� $ labBPV��$ 
Visibility��� -(   $3 groupMonitorSettings�� ) ME_UVSettings#��� ) .��   HBBio.Communication.UVUC  -)   4  Name$
4 ucUV���� -*   $3 groupAirSensorAlarm�� * ME_AirSensorAlarm#��� * .���� -+   $ stackAirSensorAlarm��$! 
Horizontal=��� -,   $3 groupAlarmWarning�� + ME_AlarmWarning#��� + .���� --   $) btnAlarmWarning��$	��Top=�$��5q� , ME_AlarmWarningSetting#��� , ��  - labOther#��� - .���� .+��� -.   $) btnRNL��$��5q� . ME_ResultName#��� . �� -/   $) btnSP��$��5q� / ME_StartProtocol#��� / $y�	Collapsed=��� -0   $) btnMN��$��5q� 0 ME_MethodNotes#��� 0 ��  1 ME_DlyPhase#��� 1 .���� -1   $	 scr��$��0q�$
��Auto=�.��� -2   $
 warp������ $	 scr��$ Width�.+�!   HBBio.MethodEdit.FlowRateUC  -3   5  Name$5 
flowRateUC��y��� *
MVisible+'   !HBBio.MethodEdit.ValveSelectionUC  -4   6  Name$6 valveSelectionUC��y��� *
MVisible+   HBBio.MethodEdit.MixerUC  -5   7  Name$7 mixerUC��y��� *
MVisible+   HBBio.MethodEdit.BPVUC  -6   8  Name$8 bpvUC��y��� *
MVisible+    HBBio.MethodEdit.UVResetUC  -7   9  Name$9 	uvResetUC��y��� *
MVisible+.   (HBBio.MethodEdit.SampleApplicationTechUC  -8   :  Name$: sampleApplicationTechUC��y��� *
MVisible+   HBBio.MethodEdit.TVCVUC  -9   ;  Name$; tvcvUC��y��� *
MVisible+$   HBBio.MethodEdit.MixtureGridUC  -:   <  Name$< mixtureGridUC��y��� *
MVisible+(   "HBBio.MethodEdit.FlowValveLengthUC  -;   =  Name$= flowValveLengthUC��y��� *
MVisible+$   HBBio.MethodEdit.FlowRatePerUC  -<   >  Name$> flowRatePerUC��y��� *
MVisible+$   HBBio.MethodEdit.PHCDUVUntilUC  -=   ?  Name$? pHCdUVUnitUC��y��� *
MVisible++   %HBBio.MethodEdit.CollValveCollectorUC  ->   @  Name$@ collUC��$	��500��y��� *
MVisible+   HBBio.MethodEdit.CIPUC  -?   A  Name$A cipUC��y��� *
MVisible+�� #���  .��� -@   B �Name$B gridMiscellaneous��$��5q�$
��Auto=�$
��Auto=� �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -A   C ��Name$C chboxSetMark��$��0��$��2�� 2 
ME_SetMark#��� 2 $��0,0,0,1q�$��Bottom=�D d� 	IsCheckedD �� $ MEnableSetMark�$0 TwoWay=�$1 PropertyChanged=��� -B   E ��Name$E 
txtSetMark��$��2��$��2������ $ MSetMark�$0 TwoWay=�$1 PropertyChanged=�{��� $ chboxSetMark��$ 	IsChecked��� -C   $C chboxMethodDelay��$��0��$��4�� 3 ME_MethodDelay#��� 3 $��0,0,0,1q�$��Bottom=�D �� $ MEnableMethodDelay�$0 TwoWay=�$1 PropertyChanged=�  -D   $* txtMethodDelay��$��2��$��4��, �� *MMethodDelay+{��� $ chboxMethodDelay��$ 	IsChecked��� $��4��$��4������ *MBaseUnitStr+�� -E   $C chboxMessage��$��0��$��6�� 4 
ME_Message#��� 4 D �� $ MEnableMessage�$0 TwoWay=�$1 PropertyChanged=��� -F   $E 
txtMessage��$��2��$��6������ $ MMessage�$0 TwoWay=�$1 PropertyChanged=�{��� $ chboxMessage��$ 	IsChecked��� -G   $C chboxPauseAfterMessage��$��4��$��6�� 5 ME_PauseAfterMessage#��� 5 D �� $ MEnablePauseAfterMessage�$0 TwoWay=�$1 PropertyChanged=�{��� $ chboxMessage��$ 	IsChecked��� -H   $C chboxPauseTimer��$��0��$��8�� 6 ME_PauseTimer#��� 6 D �� $ MEnablePauseTimer�$0 TwoWay=�$1 PropertyChanged=�  -I   $* txtPauseTimer��$��2��$��8��, �� *MPauseTimer+{��� $ chboxPauseTimer��$ 	IsChecked��� $��4��$��8��F 
  SC_TUNIT#��ZF %   HBBio.MethodEdit.MethodCanvasUC  -J   G  Name$G methodCanvas��%   HBBio.MethodEdit.MethodCanvasUCH   	MouseDown$H methodCanvas_MouseDown��$��1��