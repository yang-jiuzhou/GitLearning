   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ControlRead �� BasedOn �� Property �� Value �� 
TargetType  BorderBaseRead K� IsItemsHost'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� 
TargetName
 �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlDisabledDarkColor �� Value  BorderDisabledDarkColor �� Triggers P� Property P� Value  ControlReadAlignment ��Name �� CornerRadius  ControlPressedDarkColor  ControlMoveDarkColor�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:HBBio  5      %�  '��       7   7  7  7  '�� �    7  7  7  7  �� 5      "  ��6   8   .�5   
   ��  � ��$
 Auto=�6G   �� 5   
    � ��$
 Auto=�6E   �� 5   
    � �� . 6:   �� 5   
    � �� 5	      �� 5
      " ��6"   .R�5      �� $��border��6   8�� ._�5      �� $��0q�6'   |�.  .��5      �� $��2q�6)    . 64   5       5        5      CommonStates��. 5   "     Disabled��. 5   &   �� .��5   *   �� $	 border��6G   $N
 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   �� 5   *   $	 border��6G   $P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   5   '   5   #   5      5       5      P� 5       �@��
IsGrouping . . �5       ��  � �� .  6N   5!      5#      5%      5&      �� 5(      "  ��6   8   .�5)   
   ��  � �� 5*      �� 5+      " ��6"   .R�5,      �� $ border��6   8�� $ 0��6U   $��1q�6f    5-        5?      SelectionStates��. 5@   "     Selected��. 5A   &   �� .��5B   *   �� $	 border��6G   $N
 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5C   .     $ 0��6B   8  6N   5D   +   5E   '   5F   #     5G   "   
Unselected��6.     5H   "   SelectedInactive��. 5I   &   �� .��5J   *   �� $	 border��6G   $N
 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5K   .     $ 0��6B   8  6N   5L   +   5M   '   5N   #   5O      ._�5Q      �� 5R      5S      5U      5V      5W      