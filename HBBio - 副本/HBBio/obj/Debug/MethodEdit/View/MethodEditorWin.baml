   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   9!clr-namespace:HBBio.CommunicationHBBio.Communication  5	   	   &     HBBio.MethodEdit.MethodEditorWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   *localclr-namespace:HBBio.MethodEdit  5   	   %shareclr-namespace:HBBio.Share  5   	   ,comm!clr-namespace:HBBio.Communication  5	   	   -   5           ME_MethodEditorWin  � Title#  �   5   	   $	��800��66   $
��1000��6C   $	��309��6P   $	��500��6`    � WindowStartupLocation$ CenterScreen=�6o    � 	Resources 5      )  #HBBio.Share.EnumVisibilityConverter  EnumVisibilityConverter"   HBBio.Share.IntBoolConverter  IntBoolConverter$   HBBio.Share.BoolFalseConverter  BoolFalseConverter �� 
TargetType  labCopyPhase  btnPhase �� 
TargetType ��Name �� Padding �� CornerRadius �� 
StartPoint	 �� EndPoint
 � Offset � Color �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value��%�  &	       &	      &	 &     &	 9     &	 t       5   
   6(     5   
   6!     5   
   6#   �� 5   
   " ��6   .�5      -   5        5      �� 5   
   " ��6+   .R�5      �� $ border��6   $	��100��6%   $	��2.5q�64   $ 8q�6A   
���   �$��2q�6a   $ 25��6u   ��5      �� 5      $ 0.2,0&�6*   $	 0.8,1&�6=   .��5      � $
 0Q�6'   $ White��62   6B   � 5      $	
 0.5Q�6'   $ #D6DBE9��64   6F   5      ._�5      �� #��z��6#   $��Center=�6G   $��Center=�6d   5       5      P� 5        �@J�IsMouseOver . . �5!      �� $ border��6    � ��
 ��  �6H   5"      5$      5%      .��5&      �  � RowDefinitions 5'   
   �� 5(      $��7* �6   �� 5)      $��3* �6   .+�5+   
   � $��0��6    � ColumnDefinitions 5,      �� 5-      $
��Auto �6#   �� 5.      $��5 �6#   �� 5/      $
��Auto �6#   �� 50      $��5 �6#   �� 51      $
��Auto �6#   �� 52      $��5 �6#   �� 53      $��* �6#   .+�55      �� $��0��6   .+�56         HBBio.Share.IconButton  -   5          Name$ btnCopy��56   #      HBBio.Share.IconButton   Click$ btnCopy_Click��6c     btnCopy � ToolTip# �  64   $��5q�6X      Image$, &/Bio-LabChrom;component/Image/copy.PNG��6y     57      -   5        $ btnPaste��57   #   $ btnPaste_Click��6w     btnPaste# �  65   $��5q�6Z   {�.  $- '/Bio-LabChrom;component/Image/paste.PNG��6�     58      -   5        $ btnSave��58   #   $ btnSave_Click��6c     btnSave# �  64   $��5q�6X   $, &/Bio-LabChrom;component/Image/save.png��6y     59      -   5        $ btnSend��59   #   $ btnSend_Click��6c    	 btnSend# � 	 64   $��5q�6X   $, &/Bio-LabChrom;component/Image/send.png��6y   5:      ]� 5;      $��2��6   .+�5<      � $��Bottom=�6    5=      �� 5>      $��* �6+   �� 5?      $
��Auto �6+   �� 5@      $��15 �6+   �� 5A      $
��Auto �6+   �� 5B      $��* �6+   .+�5D        -   5        $ btnNew��5D   '   $ btnNew_Click��6w   $��1��67    
 ME_AddPersonalPhase# � 
 6G   $+ %/Bio-LabChrom;component/Image/add.PNG��6�     5E      -   5        $ btnDel��5E   '   $ btnDel_Click��6w   $��3��67     ME_DelPersonalPhase# �  6G   $. (/Bio-LabChrom;component/Image/delete.PNG��6�   5F      �� 5G      $
��Auto=�6   ��5H      �� 5I      -	   5         ��Name$ 	gridPhase��5I   %    J� 	AllowDrop . 
������ .+�5J      �� #��[ 6$   #��[ 6J     ME_EnumPhaseType_ColumnCIP#���  6o   �� 5K      #��[ 6$   #��[ 6J    & "ME_EnumPhaseType_SampleApplication#���  6o   �� 5L      #��[ 6$   #��[ 6J     ME_EnumPhaseType_ColumnWash#���  6o   �� 5M      #��[ 6$   #��[ 6J     ME_EnumPhaseType_Elution#���  6o   �� 5N      #��[ 6$   #��[ 6J    " ME_EnumPhaseType_Equilibration#���  6o   �� 5O      #��[ 6$   #��[ 6J    " ME_EnumPhaseType_Miscellaneous#���  6o   �� 5P      #��[ 6$   #��[ 6J     ME_EnumPhaseType_SystemCIP#���  6o   �� 5Q      #��[ 6$   #��[ 6J     ME_EnumPhaseType_Mixture#���  6o   5R      5T      5U      � 5V      $��4��6   
������� 5W      �� 5X      $
��Auto �6$   �� 5Y      $��5 �6$   �� 5Z      $��* �6$   .+�5\      �� -
   5         ��Name$ btnMethodSettings��5\      $��0��63   #��[ 6@     ME_MethodSettings#���  6e   6�   �� 5]      $��2��6   $
��Auto=�6,   ��5^      �� 5_      -   5        $ stackPanelPhaseList��5_   %   
������� . 6�   5a      5b      �� 5c      -   5         ��Name$ 
tabControl��5c      $��6��6-   .@�5d      �� #���  6   .��5e      �� -   5         ��Name$ scrMS��5e   #   $
��Auto=�62   .��5f      � -   5         �Name$ wrapPanelMS��5f   $   ��69   ��  �� ElementName$ scrMS�� �� Path$ Width�.+�5g      ��   ME_BasicInformation#���  6'   .��5h   "   � $��5q�6'    5i   &   �� 5j   *   $
��Auto �6;   �� 5k   *   $��5 �6;   �� 5l   *   $
��Auto �6;    5n   &   �� 5o   *   $
��Auto �68   �� 5p   *   $��5 �68   �� 5q   *   $
��Auto �68   �� 5r   *   $��5 �68   �� 5s   *   $
��Auto �68   .+�5u   &   �� $��0��60   $��0��6=     A_UserName1#���  6M   �� 5v   &   -   5          ��Name$  labUserName��5v   0   $��0��6E   $��2��6R   ��6b   �� *	MUserName+�� 5w   &   $��2��60   $��0��6=     ME_CreateTime1#���  6M   �� 5x   &   -   5        $  labCreateTime��5x   0   $��2��6G   $��2��6T   ��6d   �� *MCreateTime+�� 5y   &   $��4��60   $��0��6=     ME_ModifyTime1#���  6M   �� 5z   &   -   5        $  labModifyTime��5z   0   $��4��6G   $��2��6T   ��6d   �� *MModifyTime+5{   #   5|      �� 5}        ME_LoopSettings#���  6'   .��5~   "   �� $��5q�6-   ! �� Orientation$! 
Horizontal=�68   $	��Top=�6Q   .+�5   &   ��   ME_LoopSet1#���  60   %  Xceed.Wpf.Toolkit.IntegerUpDown  5�   &   -   5        "  Name$" intLoop��5�   9   $	��5,0q�6J   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]#   Minimum$# 1X�6o   $   Value$ 6W   �� *MLoop+6o   5�   #   5�      �� 5�        ME_ColumnSelection#���  6'   .��5�   "   � $��5q�6'    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68    5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   $��5 �6;   �� 5�   *   $
��Auto �6;   �� 5�   *   $��5 �6;   �� 5�   *   $
��Auto �6;   .+�5�   &   �� $��0��60     ME_ColumnName1#���  6=   �� 5�   &   -   5        % ��Name$% cboxColumnName��5�   /   $��2��6G   $��0��6W   & �� DisplayMemberPath$& MName��6d   ' �� SelectedValuePath$	' MID��6~   ( �� SelectedValue( 6�   �� *	MColumnId+�� 5�   &   -   5        ) ��Name$) 	btnColumn��5�   -   $��0��6@   $��4��6M   $��35��6]   $��35��6k   $��.��6v   6�   �� 5�   &   $��2��60     ME_ColumnVolume1#���  6=   $  Xceed.Wpf.Toolkit.DoubleUpDown  5�   &   -   5        *  Name$* doubleCV��5�   8   $��2��6J   $��2��6Z   �	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]+ 	  Minimum$
+ 0.01X�6g   , 	  Value, 6v   �� *
MColumnVol+{�6�   �� $ cboxColumnName��$ SelectedValue�- �� 	Converter#- [ �� 5�   &   -   5        $  labCV��5�   0   $��4��6?   $��2��6O   
   HBBio.Share.DlyBase. 
  
SC_VUNITML#��Z. 6\   �� 5�   &   -   5        $  labCPV��5�   0   $��0��6@   $��4��6P     labCPV1#���  6]   �� 5�   &   -   5        $% cboxCPV��5�   /   $��2��6@   $��4��6P   / �� SelectedIndex/ 6]   �� $
 MCPV�0 �� Mode$0 TwoWay=�1 �� UpdateSourceTrigger$1 PropertyChanged=�y�6�   �� $ labCPV��$ 
Visibility�5�   #   5�      �� 5�        ME_UnitSelection#���  6'   .��5�   "   � $��5q�6'    5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   $��5 �6;   �� 5�   *   $
��Auto �6;    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68   .+�5�   &   �� $��0��60      ME_MethodBaseUnit1#���   6=   �� 5�   &   -   5        $% cboxMBU��5�   /   $��2��6@   $��0��6P   $& MName��6]   $' MMode��6w   ( 6�   �� *	MBaseUnit+�� 5�   &   $��2��60    ! ME_FlowRateUnit1#��� ! 6=   �� 5�   &   -   5        $% cboxFRU��5�   /   $��2��6@   $��2��6P   $& MName��6]   $' MMode��6w   ( 6�   �� *MFlowRateUnit+{�6�   �� $ doubleCV��$ 	IsEnabled�#- [ 5�   #   5�      �� 5�       " labFlowRate#��� " 6'   .��5�   "   � $��5q�6'    5�   &   �� 5�   *   $��* �6;   �� 5�   *   $��5 �6;   �� 5�   *   $
��Auto �6;    5�   &   �� 5�   *   $
��Auto �68   .+�5�   &     -   5        $* doubleFlowRate��5�   8   $
��Left=�6�   2 	  Maximum2 6P   �� *MMaxFlowRate+, 6q   �� *	MFlowRate+6�   �� 5�   &   -   5        $  labFlowRateUnitStr��5�   0   $��2��6L   ��6\   �� *MFlowRateUnitStr+5�   #   5�      �� 5�      -   5        3 ��Name$3 groupValveSelection��5�   '    # ME_ValveSelection#��� # 6D   .��5�   "   � $��5q�6'    5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   $��5 �6;   �� 5�   *   $
��Auto �6;   �� 5�   *   $��* �6;    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68   �� 5�   *   $��5 �68   �� 5�   *   $
��Auto �68   �� 5�   *   $��* �68   .+�5�   &   �� -   5        $  	labInletA��5�   0   $��0��6C   $��0��6S    $ labInA1#��� $ 6`   �� 5�   &   -   5        $% 
cboxInletA��5�   /   $��2��6C   $��0��6S   / 6`   �� $
 MInA�$0 TwoWay=�$1 PropertyChanged=�y�6�   �� $ 	labInletA��$ 
Visibility��� 5�   &   -    5        $  	labInletB��5�   0   $��0��6C   $��2��6S    % labInB1#��� % 6`   �� 5�   &   -!   5        $% 
cboxInletB��5�   /   $��2��6C   $��2��6S   / 6`   �� $
 MInB�$0 TwoWay=�$1 PropertyChanged=�y�6�   �� $ 	labInletB��$ 
Visibility��� 5�   &   -"   5        $  	labInletC��5�   0   $��0��6C   $��4��6S    & labInC1#��� & 6`   �� 5�   &   -#   5        $% 
cboxInletC��5�   /   $��2��6C   $��4��6S   / 6`   �� $
 MInC�$0 TwoWay=�$1 PropertyChanged=�y�6�   �� $ 	labInletC��$ 
Visibility��� 5�   &   -$   5        $  	labInletD��5�   0   $��0��6C   $��6��6S    ' labInD1#��� ' 6`   �� 5�   &   -%   5        $% 
cboxInletD��5�   /   $��2��6C   $��6��6S   / 6`   �� $
 MInD�$0 TwoWay=�$1 PropertyChanged=�y�6�   �� $ 	labInletD��$ 
Visibility��� 5�   &   -&   5        $  labBPV��5�   0   $��0��6@   $��8��6P    ( labBPV1#��� ( 6]   �� 5�   &   -'   5        $% cboxBPV��5�   /   $��2��6@   $��8��6P   / 6]   �� $
 MBPV�$0 TwoWay=�$1 PropertyChanged=�y�6�   �� $ labBPV��$ 
Visibility�5�   #   5�      �� 5�      -(   5        $3 groupMonitorSettings��5�   '    ) ME_UVSettings#��� ) 6E   .��5�   "      HBBio.Communication.UVUC  -)   5        4  Name$
4 ucUV��5�   ,   5�      �� 5�      -*   5        $3 groupAirSensorAlarm��5�   '    * ME_AirSensorAlarm#��� * 6D   .��5�   "   �� -+   5        $ stackAirSensorAlarm��5�   -   $! 
Horizontal=�6J   5�      �� 5�      -,   5        $3 groupAlarmWarning��5�   '    + ME_AlarmWarning#��� + 6B   .��5�   "   �� --   5        $) btnAlarmWarning��5�   )   $	��Top=�6B   $��5q�6Z    , ME_AlarmWarningSetting#��� , 6e   6�   5�      �� 5�       - labOther#��� - 6'   .��5�   "   �� .+�5�   &   �� -.   5        $) btnRNL��5�   -   $��5q�6=    . ME_ResultName#��� . 6H   6r   �� 5�   &   -/   5        $) btnSP��5�   -   $��5q�6<    / ME_StartProtocol#��� / 6G   $y�	Collapsed=�6t   6�   �� 5�   &   -0   5        $) btnMN��5�   -   $��5q�6<    0 ME_MethodNotes#��� 0 6G   6r   5�   #   5�      5�      5�      5�      �� 5�       1 ME_DlyPhase#��� 1 6   .��5�      �� -1   5        $	 scr��5�   #   $��0q�60   $
��Auto=�6;   .��5�      � -2   5        $
 warp��5�   $   ��62   �� $	 scr��$ Width�.+�5�      !   HBBio.MethodEdit.FlowRateUC  -3   5        5  Name$5 
flowRateUC��5�   /   y�6C   �� *
MVisible+'   !HBBio.MethodEdit.ValveSelectionUC  5�      -4   5        6  Name$6 valveSelectionUC��5�   5   y�6O   �� *
MVisible+   HBBio.MethodEdit.MixerUC  5�      -5   5        7  Name$7 mixerUC��5�   ,   y�6=   �� *
MVisible+   HBBio.MethodEdit.BPVUC  5�      -6   5        8  Name$8 bpvUC��5�   *   y�69   �� *
MVisible+    HBBio.MethodEdit.UVResetUC  5�      -7   5        9  Name$9 	uvResetUC��5�   .   y�6A   �� *
MVisible+.   (HBBio.MethodEdit.SampleApplicationTechUC  5�      -8   5        :  Name$: sampleApplicationTechUC��5�   <   y�6]   �� *
MVisible+   HBBio.MethodEdit.TVCVUC  5�      -9   5        ;  Name$; tvcvUC��5�   +   y�6;   �� *
MVisible+$   HBBio.MethodEdit.MixtureGridUC  5�      -:   5        <  Name$< mixtureGridUC��5�   2   y�6I   �� *
MVisible+(   "HBBio.MethodEdit.FlowValveLengthUC  5�      -;   5        =  Name$= flowValveLengthUC��5�   6   y�6Q   �� *
MVisible+$   HBBio.MethodEdit.FlowRatePerUC  5�      -<   5        >  Name$> flowRatePerUC��5�   2   y�6I   �� *
MVisible+$   HBBio.MethodEdit.PHCDUVUntilUC  5�      -=   5        ?  Name$? pHCdUVUnitUC��5�   2   y�6H   �� *
MVisible++   %HBBio.MethodEdit.CollValveCollectorUC  5�      ->   5        @  Name$@ collUC��5�   9   $	��500��6I   y�6X   �� *
MVisible+   HBBio.MethodEdit.CIPUC  5�      -?   5        A  Name$A cipUC��5�   *   y�69   �� *
MVisible+5      5     5     �� 5     #���  6   .��5     � -@   5        B �Name$B gridMiscellaneous��5     $��5q�66   $
��Auto=�6A   $
��Auto=�6s    5     �� 5     $
��Auto �6/   �� 5     $��5 �6/   �� 5     $
��Auto �6/   �� 5	     $��5 �6/   �� 5
     $
��Auto �6/    5     �� 5     $
��Auto �6,   �� 5     $��5 �6,   �� 5     $
��Auto �6,   �� 5     $��5 �6,   �� 5     $
��Auto �6,   �� 5     $��5 �6,   �� 5     $
��Auto �6,   �� 5     $��5 �6,   �� 5     $
��Auto �6,   .+�5     �� -A   5        C ��Name$C chboxSetMark��5  #   $��0��69   $��2��6I    2 
ME_SetMark#��� 2 6V   $��0,0,0,1q�6�   $��Bottom=�6�   D d� 	IsCheckedD 6}   �� $ MEnableSetMark�$0 TwoWay=�$1 PropertyChanged=�6�   �� 5     -B   5        E ��Name$E 
txtSetMark��5  "   $��2��66   $��2��6F   ��6S   �� $ MSetMark�$0 TwoWay=�$1 PropertyChanged=�{�6�   �� $ chboxSetMark��$ 	IsChecked��� 5     -C   5        $C chboxMethodDelay��5  #   $��0��6=   $��4��6M    3 ME_MethodDelay#��� 3 6Z   $��0,0,0,1q�6�   $��Bottom=�6�   D 6�   �� $ MEnableMethodDelay�$0 TwoWay=�$1 PropertyChanged=�6�     5     -D   5        $* txtMethodDelay��5  ,   $��2��6D   $��4��6T   , 6a   �� *MMethodDelay+{�6�   �� $ chboxMethodDelay��$ 	IsChecked��� 5     $��4��6$   $��4��64   ��6A   �� *MBaseUnitStr+�� 5     -E   5        $C chboxMessage��5  #   $��0��69   $��6��6I    4 
ME_Message#��� 4 6V   D 6}   �� $ MEnableMessage�$0 TwoWay=�$1 PropertyChanged=��� 5     -F   5        $E 
txtMessage��5  "   $��2��66   $��6��6F   ��6S   �� $ MMessage�$0 TwoWay=�$1 PropertyChanged=�{�6�   �� $ chboxMessage��$ 	IsChecked��� 5     -G   5        $C chboxPauseAfterMessage��5  #   $��4��6C   $��6��6S    5 ME_PauseAfterMessage#��� 5 6`   D 6�   �� $ MEnablePauseAfterMessage�$0 TwoWay=�$1 PropertyChanged=�{�6�   �� $ chboxMessage��$ 	IsChecked��� 5     -H   5        $C chboxPauseTimer��5  #   $��0��6<   $��8��6L    6 ME_PauseTimer#��� 6 6Y   D 6�   �� $ MEnablePauseTimer�$0 TwoWay=�$1 PropertyChanged=�  5      -I   5        $* txtPauseTimer��5   ,   $��2��6C   $��8��6S   , 6`   �� *MPauseTimer+{�6~   �� $ chboxPauseTimer��$ 	IsChecked��� 5!     $��4��6$   $��8��64   F 
  SC_TUNIT#��ZF 6A   5"     5#     5$     5%     %   HBBio.MethodEdit.MethodCanvasUC  5&  
   -J   5        G  Name$G methodCanvas��5&     %   HBBio.MethodEdit.MethodCanvasUCH   	MouseDown$H methodCanvas_MouseDown��6B   $��1��65   6B   5'     5(     