   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  )clr-namespace:HBBio.ShareHBBio.Share  9!clr-namespace:HBBio.CommunicationHBBio.Communication  3clr-namespace:HBBio.CollectionHBBio.Collection      HBBio.Manual.ManualWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  &localclr-namespace:HBBio.Manual  %shareclr-namespace:HBBio.Share  ,comm!clr-namespace:HBBio.Communication  )collclr-namespace:HBBio.Collection  -      M_ManualWin  � Title#  �   $	��463��$
��1000��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=� � 	Resources #   HBBio.Share.EnumBoolConverter  EnumBoolConverter$   HBBio.Share.BoolFalseConverter  BoolFalseConverter��%   &	       &	          .��� $��10q� � RowDefinitions �� $��* ��� $��10 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $	��180 ��� $��5 ��� $��* ��� $��5 ��� $
��Auto ��� $��5 ��� $	��200 �.+�R� -    R�Name$
 tree��$��0��.@�Q� -    Q�Name$ treeItemPump�� 
 M_Pump#���  .@�Q� -   $ treeItemPumpSystem��  M_PumpSystem#���  Q� -   $ treeItemPumpSample��  M_PumpSample#���  Q� -   $ treeItemValve��  M_Valve#���  .@�Q� -   $ treeItemCollectionValve��  M_CollectionValve#���  Q� -   $! treeItemCollectionCollector��  M_CollectionCollector#���  Q� -	   $ treeItemMonitors�� 	 
M_Monitors#��� 	 Q� -
   $ treeItemAlarmWarning�� 
 M_AlarmWarning#��� 
 Q� -   $ treeItemOther��  M_Other#���  .@�Q� -   $ treeItemMarker��  M_Marker#���  Q� -   $ treeItemPause��  M_Pause#���  Q� -   $ treeItemStop�� 
 M_Stop#���  Q� -   $ 
treeItemUV��  M_UV#���  Q� -   $ 
treeItemRI��  M_RI#���  Q� -   $ treeItemMixer��  M_Mixer#���  �� -    ��Name$ 
tabControl��$��2��.@��� -    ��Name$ tabPumpSystem��#���  .��� -   	 �Name$	 gridPump��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��1 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��1 ��� $
��Auto �.+��� -   
 ��Name$
 	labLength��$��0��$��0��  
LabLength1#���  $  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ doubleLength��$��0��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$
 0.01X�   Value �� *	MLength+�� -   $
 labLengthUnit��$��0��$��4��  LabUnit1#���  �� -    ��Name$ cboxLengthUnit��$��0��$��6�� �� DisplayMemberPath$ MName�� �� SelectedValuePath$ MMode�� �� SelectedValue �� *MLengthUnit+��  �� Orientation$ 
Horizontal=�$��2��$��0��.+��� -   $
 labFlow��  labFlowRate#���  �� -   $
 
labFlowMax���� $	�� : ��  -   $ 
doubleFlow��$��2��$��2�� �� *MFlow+�� -   $
 labFlowUnit��$��2��$��4��  ME_FlowRateUnit1#���  �� -   $ cboxFlowUnit��$��2��$��6��$ MName��$ MMode�� �� *	MFlowUnit+�� -   $
 labBS��$��4��$��0�� 
 LabBS1#���    -   $ doubleBS��$��4��$��2��   Maximum$	 100X� �� *MBS+�� -    $
 labBE��$��4��$��4�� 
 LabBE1#���    -!   $ doubleBE��$��4��$��6��$	 100X� �� *MBE+�� -"   $
 labCS��$��6��$��0�� 
 LabCS1#���    -#   $ doubleCS��$��6��$��2��$	 100X� �� *MCS+�� -$   $
 labCE��$��6��$��4�� 
 LabCE1#���    -%   $ doubleCE��$��6��$��6��$	 100X� �� *MCE+�� -&   $
 labDS��$��8��$��0�� 
 LabDS1#���    -'   $ doubleDS��$��8��$��2��$	 100X� �� *MDS+�� -(   $
 labDE��$��8��$��4�� 
 LabDE1#���    -)   $ doubleDE��$��8��$��6��$	 100X� �� *MDE+�� -*    ��Name$ chboxPT��$��10��$��0��  LabControlPT#���   d� 	IsChecked �� *	MEnablePT+  -+   $ doublePT��$��10��$��2�� �� *
MControlPT+{���  �� ElementName$ chboxPT�� �� Path$ 	IsChecked��� -,   $ tabPumpSample��#���  .��� --   $	 gridPumpSample��$��5q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��1 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��1 ��� $
��Auto �.+��� -.   $
 labSampleLength��$��0��$��0��#���    -/   $ doubleSampleLength��$��0��$��2��$
 0.01X� �� *	MLength+�� -0   $
 labSampleLengthUnit��$��0��$��4��#���  �� -1   $ cboxSampleLengthUnit��$��0��$��6��$ MName��$ MMode�� �� *MLengthUnit+�� $ 
Horizontal=�$��2��$��0��.+��� -2   $
 labSampleFlow��#���  �� -3   $
 labSampleFlowMax���� $	�� : ��  -4   $ doubleSampleFlow��$��2��$��2�� �� *MFlow+�� -5   $
 labSampleFlowUnit��$��2��$��4��#���  �� -6   $ cboxSampleFlowUnit��$��2��$��6��$ MName��$ MMode�� �� *	MFlowUnit+�� -7   $ chboxPTS��$��4��$��0��#���   �� *	MEnablePT+  -8   $ 	doublePTS��$��4��$��2�� �� *
MControlPT+{��� $ chboxPTS��$ 	IsChecked��� -9   $ tabValve��#���  .���� $��10q�.+��� -:   $
 labPosition��  M_Position1#���  $
��0,10q��� -;   $ cboxPosition�� �� SelectedIndex �� *MIndex+�� -<   $ tabCollection��  M_Collection#���  .��#   HBBio.Collection.CollectionUC  -=     Name$ collectionUC���� ->   $ tabAS��  M_AS#���  .��(   "HBBio.Communication.ASManualParaUC  -?     Name$ asParaUC��$��10q��� -@   $ 	tabpHCdUV��#��� 	 .��� -A   $	 
gridpHCdUV��$��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -B   $
 labMonitorAction��$��0��   M_MonitorAction1#���   �� -C   $ cboxMonitorAction��$��2��$��10q�$ MString��$ MEnum�� �� *	MAction+�� -D   $
 labMonitorSetting��$��4�� ! M_MonitorSetting1#��� ! '   !HBBio.Communication.MonitorParaUC  -E     Name$ monitorParaUC��$��8��$��10q��� -F   $ tabAlarmWarning��#��� 
 .��� -G   $	 gridAlarmWarning��$��10q� �� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -H   $
 labHH��$��0��$��0�� 
" labHH1#��� " �� -I   $
 
labHHRange��$��0��$��2��$��5���� -J   $ cboxHH��$��2��$��2��$ MString��$ MEnum�� �� *
MCheckHH+ �� Mode$ TwoWay=�  -K   $ doubleHH��$��2��$��4�� �� *	MValMin+ �� *	MValMax+ �� *MValHH+�� -L   $
 	labHHUnit��$��2��$��6���� -M   $
 labLL��$��4��$��0�� 
# labLL1#��� # �� -N   $
 
labLLRange��$��4��$��2��$��5������ $ 
labHHRange��$
 Text��� -O   $ cboxLL��$��6��$��2��$ MString��$ MEnum�� �� *
MCheckLL+  -P   $ doubleLL��$��6��$��4�� �� *	MValMin+ �� *	MValMax+ �� *MValLL+�� -Q   $
 	labLLUnit��$��6��$��6������ $ 	labHHUnit��$
 Text��� -R   $

 labH��$��8��$��0�� 	$ labH1#��� $ �� -S   $
 	labHRange��$��8��$��2��$��5������ $ 
labHHRange��$
 Text��� -T   $ cboxH��$��10��$��2��$ MString��$ MEnum�� �� *	MCheckH+  -U   $ doubleH��$��10��$��4�� �� *	MValMin+ �� *	MValMax+ �� *MValH+�� -V   $
 labHUnit��$��10��$��6������ $ 	labHHUnit��$
 Text��� -W   $

 labL��$��12��$��0�� 	% labL1#��� % �� -X   $
 	labLRange��$��12��$��2��$��5������ $ 
labHHRange��$
 Text��� -Y   $ cboxL��$��14��$��2��$ MString��$ MEnum�� �� *	MCheckL+  -Z   $ doubleL��$��14��$��4�� �� *	MValMin+ �� *	MValMax+ �� *MValL+�� -[   $
 labLUnit��$��14��$��6������ $ 	labHHUnit��$
 Text��� -\   $ 	tabMarker��#���  .��� -]   $	 
gridMarker��$��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$��0�� & labName#��� & �� -^    ��Name$ 	txtMarker��$��0��$��2��$��6������ *MType+�� $��2��$��0�� ' D_MA_Position#��� ' 	� -_    	�Name$ rbtnNow��$��2��$��2�� ( D_MA_Now#��� (  �� *	MIsReal+	� -`   $ rbtnPast��$��4��$��2�� ) 	D_MA_Past#��� )  �� *	MIsReal+ �� 	Converter# [   -a   $ doubelMarkerTVCV��$��4��$��4�� �� *MVal+{��� $ rbtnPast��$ 	IsChecked��� -b   $
 labMarkerTVCV��$��4��$��6���� -c   $ tabPauseStop�� * M_PauseStop#��� * .��� -d   $	 gridPauseStop��$��10q� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -e   $
 labPauseStop��$��0��$��0��$��3��	� -f   $ rbtnT��$��2��$��0�� + labT#��� +  �� $ MLengthUnit�# [    HBBio.Share.EnumBase    T! �� ConverterParameter#! Z    -g   $ doubleT��$��2��$��2�� �� *	MLength+{��� $ rbtnT��$ 	IsChecked�	� -h   $ rbtnV��$��4��$��0�� , labV#��� ,  �� $ MLengthUnit�# [ "   V#! Z"   -i   $ doubleV��$��4��$��2�� �� *	MLength+{��� $ rbtnV��$ 	IsChecked�	� -j   $ rbtnCV��$��6��$��0�� 	- labCV#��� -  �� $ MLengthUnit�# [ 	#   CV#! Z#   -k   $ doubleCV��$��6��$��2�� �� *	MLength+{��� $ rbtnCV��$ 	IsChecked��� -l   $ tabUV��#���  .��	   HBBio.Communication.UVUC	  -m   $ 	 Name$
$ ucUV���� -n   $ tabRI��#���  .��
   HBBio.Communication.RIUC
  -o   % 
 Name$
% ucRI���� -p   $ tabMixer��#���  .��!   HBBio.Communication.MixerUC  -q   &  Name$& ucMixer��� $��4�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* �.+��� -r   ' ��Name$' btnAdd��$��1��$��>>���� -s   $' btnDel��$��3��$��<<���� -t   $
 labPause��$��5�� . M_CountdownPause1#��� . $y�Hidden=��� -u   $
 txtPause��$��7���� -v   $
 labStop��$��9�� / M_CountdownStop1#��� / $y�Hidden=��� -w   $
 txtStop��$��11���� -x   ( ��Name$( listOper��$��6��� $��2�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -y   $' btnOK��$��1�� 0 btnApply#��� 0 �� -z   $' 	btnCancel��$��3�� 1 btnClose#��� 1 