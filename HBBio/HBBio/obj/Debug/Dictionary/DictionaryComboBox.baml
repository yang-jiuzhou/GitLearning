   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ControlReadAlignment �� BasedOn �� Property �� Value  ComboBoxToggleButton �� 
TargetType ��Name  BorderBaseRead �� CornerRadius J� SnapsToDevicePixels  GradientBackgroundRead F�Name  
SolidBlyph'   !System.Windows.VisualStateManager	    VisualStateGroups%  System.Windows.VisualStateGroup
   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlMoveDarkColor �� Value  BorderMoveDarkColor  ControlPressedLightColor  ControlPressedDarkColor 	 BorderPressedLightColor 
 BorderPressedDarkColor  ControlDisabledDarkColor  BorderDisabledLightColor d�Name d� 	IsChecked �� Mode �� RelativeSource ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate J� IsHitTestVisible �Name � AllowsTransparency � ActualWidth �� MaxDropDownHeight ��Name �� ClearTypeHint ��Name �� ElementName  K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation!   DirectionalNavigation" �� Triggers# P� Property$ P� Value% �� 
TargetName& P� 
SourceName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:HBBio  5      %�  '��       7   &	 a     7  7  7  7  7  7  7  7 	 7 
 7 
 7  7  7  '�� j	    7   7  �� 5      "  ��6   8   .�5   
   ��  �@�OverridesDefaultStyle .  62   5      �� 5      "  d�6)   .�5	   
   ��  � |� .  6&   �� 5
   
    �@��	ClickMode$ Press=�6&   �� 5   
    � �� 5      �� 5      " d�6"   .R�5      �� $ border��6   8��  ._�5      �� $ 0,5,5,0��6!   $��25��68    . $��Right=�6^   8�� ._�5      F� $ arrow��6#   8�� $��Center=�6U   $��Center=�6r   "����        � 	=  	= � z     	6�   5      	 5        5      CommonStates��.
 5   "     
Normal��6.     5   "   	MouseOver��. 5   &   �� .��5   *   �� $ border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   �� 5   *   $ border��6G   $P J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   5   '   5   #     5   "   Pressed��. 5    &   �� .��5!   *   �� $ border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6f   .g�5"   .     $ 0��6B   8  6N   5#   +   �� 5$   *   $ border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5%   .     $ 0��6B   8  6N   5&   +   �� 5'   *   $ border��6G   $P J(Border.BorderBrush).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6f   .g�5(   .     $ 0��6B   8  6N   5)   +   �� 5*   *   $ border��6G   $P J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5+   .     $ 0��6B   8  6N   5,   +   �� 5-   *   $ arrow��6G   $* $(Shape.Fill).(SolidColorBrush.Color)�6e   .g�5.   .     $ 0��6B   8 	 6N   5/   +   50   '   51   #     52   "   Disabled��. 53   &   �� .��54   *   �� $ border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�55   .     $ 0��6B   8 
 6N   56   +   �� 57   *   $ border��6G   $P J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�58   .     $ 0��6B   8  6N   59   +   �� 5:   *   $ arrow��6G   $* $(Shape.Fill).(SolidColorBrush.Color)�6e   .g�5;   .     $ 0��6B   8  6N   5<   +   5=   '   5>   #   5?      5A      5B      5D      5E      �� 5G      "  ��6   8   .�5H   
   ��  � ��$ 	15,5,35,5q�6$   �� 5I   
    � ��$
 Auto=�6G   �� 5J   
    � ��$
 Auto=�6E   �� 5K   
    � �� . 6:   �� 5L   
    �@��PanningMode$
 Both=�65   �� 5M   
    �@��IsFlicksEnabled .  63   �� 5O   
    � �� 5P      �� 5Q      " ��6"   .R�5R      �  . 	 5S        5T      CommonStates��.
 5U   "     
Normal��6.     5V   "   	MouseOver��6.     5W   "   Disabled��6.   5X      .+�5Z      d� $ ToggleButton��6'   $��2��5[   '   #��z��5\   '   #��z��5]   '   8��  5^   '   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5_   '   �� 5`      $ ContentSite��6+   #��z 5a   +   #��z 5b   +   #��z��5c   +   #��z��5d   +    .  #��z��5f   +   # z 5g   +   #��z��5h   +   � 5i      $ Popup��6     . |�.  $��Bottom=�5m       $��Slide=�5n       ��5l       �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.��5o      �� $ dropDownBorder��6%   $��2q�6=   
�������$	��150��6d   #��z 6s   #��z 6�   ._�5p   "   �� $ dropDownScrollViewer��6/   .��5q   &   � $ Enabled=�6+   .+�5r   *   �� $
��Left=�61   $	��Top=�6L   $��0��6d   $��0��6o   .+�5s   .   �� $ 
opaqueRect��68   ��6L   �� *
Background+$ dropDownBorder����6�   �� *ActualHeight+$ dropDownBorder����6�   �� *ActualWidth+$ dropDownBorder��5t   +   �� 5u   *     . $! Continue=�6H   ���	#FFFFFFFF6|   5v   '   5w   #   5x      5y      5z      " 5{      P� 5|      # �@��HasItems$ .  . �5}      �� $% dropDownBorder��6%    � ��$ 95��6V   5~      P� 5      # �@��
IsGrouping$ . . �5�      ��  � �� .  6N   5�      P� 5�      $& dropDownScrollViewer��6K   # � ��$ .  . �5�      �� $% 
opaqueRect��6;    6S   �� *VerticalOffset+$ dropDownScrollViewer�� �@��Top5�      5�      5�      5�      5�      