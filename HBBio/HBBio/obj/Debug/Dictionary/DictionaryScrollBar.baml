   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBarLineButton  �� 
TargetType �� Property �� Value �� 
TargetType ��Name  BorderBaseRead F�Name  
SolidBlyph �� RelativeSource'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState	   
Storyboard
 �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlMoveDarkColor �� Value  ControlPressedDarkColor  ControlDisabledDarkColor  ScrollBarPageButton  ScrollBarThumb �� CornerRadius  VerticalScrollBar � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton 	 HorizontalScrollBar � ColumnDefinitions 
 ControlBase �� BasedOn �� Triggers P� Property P� Value�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:HBBio  5      %�  &	        7  7  7  7  7  &	 �    &	      7  7  7  7  &	 z
    7   7  7  7  7   &		 $    7   7  7  7  7   '�� �    7 
 7 	 7  �� 5      "  ��6(   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
    � |� .  6&   �� 5	   
    � �� 5
      �� 5      " ��6"   .R�5      �� $ border��6   8��  ._�5      F� $ arrow��6   $��Center=�6.   $��Center=�6K   8�� ��6�   �� *	Content+ �� *TemplatedParent+ 5        5      CommonStates��. 5   "     
Normal��6.     5   "   	MouseOver��.	 5   &   �� .��5   *   �� $
 border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   5   '   5   #     5   "   Pressed��.	 5   &   �� .��5   *   �� $
 border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   5   '   5   #     5   "   Disabled��.	 5    &   �� .��5!   *   �� $
 border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5"   .     $ 0��6B   8  6N   5#   +   5$   '   5%   #   5&      5(      5)      5+      5,      �� 5/      "  ��6(   .�50   
   ��  �@J�SnapsToDevicePixels . 60   �� 51   
    �@�OverridesDefaultStyle . 62   �� 52   
    � �� .  6&   �� 53   
    � |� .  6&   �� 54   
    � �� 55      �� 56      " ��6"   .R�57      �� 
������ 6   58      5:      5;      �� 5>      "  n�5?      .�5@   
   ��  �@J�SnapsToDevicePixels . 60   �� 5A   
    �@�OverridesDefaultStyle . 62   �� 5B   
    � �� .  6&   �� 5C   
    � |� .  6&   �� 5D   
    � �� 5E      �� 5F      " n�6"   .R�5G      �� $ border��6   8��  $ 1��6U   $��2q�6f    5H        5I      CommonStates��. 5J   "     
Normal��6.     5K   "   	MouseOver��.	 5L   &   �� .��5M   *   �� $
 border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5N   .     $ 0��6B   8  6N   5O   +   5P   '   5Q   #     5R   "   Pressed��.	 5S   &   �� .��5T   *   �� $
 border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5U   .     $ 0��6B   8  6N   5V   +   5W   '   5X   #     5Y   "   Disabled��.	 5Z   &   �� .��5[   *   �� $
 border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5\   .     $ 0��6B   8  6N   5]   +   5^   '   5_   #   5`      5b      5c      5e      5f      �� 5i      " ��60   .R�5j   
   �  5k      �� 5l      $��25��6    �� 5m      $��* �6    �� 5n      $��25��6    .+�5p      �� $��3��6   $ 2��6&   ���#F0F0F067   �� 5q      $��0��6   8��  $��25��6U   $��ScrollBar.LineUpCommand��6a   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5r      $ 
PART_Track��6   $��1��6(   ��.  5s      �� 5t      8�� $��ScrollBar.PageUpCommand��6P    5v      n� 5w      8�� 6    5y      �� 5z      8�� $��ScrollBar.PageDownCommand��6P   5|      �� 5}      $��2��6   8�� $��25��6U   $��ScrollBar.LineDownCommand��6a   $��M 0 0 L 4 4 L 8 0 Z��6�   5~      5      �� 5�      " ��62   .R�5�   
   �  5�      �� 5�      $��25��6#   �� 5�      $��* �6#   �� 5�      $��25��6#   .+�5�      �� $��3��6   $ 2��6)   ���#F0F0F06:   �� 5�      $��0��6   8��  $��25��6X   $��ScrollBar.LineLeftCommand��6c   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5�      $ 
PART_Track��6   $��1��6(   ��.   5�      �� 5�      8�� $��ScrollBar.PageLeftCommand��6P    5�      n� 5�      8�� 6    5�      �� 5�      8�� $ ��ScrollBar.PageRightCommand��6P   5�      �� 5�      $��2��6   8�� $��25��6X   $ ��ScrollBar.LineRightCommand��6c   $��M 0 0 L 4 4 L 0 8 Z��6�   5�      5�      �� 5�      "  ��6   8    5�   
   P� 5�       �@��Orientation$ 
Horizontal=�6-   . �5�      ��  � ��$
 Auto��6*   �� 5�       � ��$ 25��6+   �� 5�       � ��8  6-   5�      P� 5�       �@��Orientation$ Vertical=�6-   . �5�      ��  � ��$ 25��6*   �� 5�       � ��$
 Auto��6+   �� 5�       � ��8  6-   5�      5�      5�      