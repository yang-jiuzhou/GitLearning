   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N;clr-namespace:System.Windows;assembly=PresentationFrameworkSystem.Windows 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ControlRead �� BasedOn �� Property �� Value �� 
TargetType  BorderBaseRead  ExpandCollapseToggleStyle'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� TargetProperty	 �� 
TargetName
 R� KeyTime  System.Windows.Visibility   Hidden R� Value   Visible F�Name  
SolidBlyph  TreeViewItemFocusVisual �� StrokeDashArray J� Opacity �� Path �� RelativeSource �� AncestorType � ColumnDefinitions � RowDefinitions8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlPressedDarkColor �� Value  ControlMoveDarkColor d�Name �� 	ClickMode d� 	IsChecked ��Name �� Padding ��Name ��Name �� Triggers  P� Property! P� Value" �� 
TargetName# ]� 
Conditions$ �� Property% �� Value�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:HBBio  5      Ay;clr-namespace:System.Windows;assembly=PresentationFramework  5      %n  'R�       7   7  &	 9    &	 �    'Q� �    7  7  7  7  �� 5      "  R�6   8   .�5   
   ��  � ��$
 Auto=�6G   �� 5   
    � ��$
 Auto=�6E   �� 5   
    � �� 5	      �� 5
      " R�6"   .R�5      �� $��Border��6   8�� ._�5      �� |�.  ��.  $��4q�6R   .��5      �� 5      5      5      5      5      �� 5      "  d�6.   .�5   
   ��  � |� .  6&   �� 5   
    � �� 5      �� 5      " d�6"   .R�5      � $��15��6   $��13��6&   
������  5        5      CheckStates��. 5   "     Checked��. 5   &   �� .��5   *   T� $ (UIElement.Visibility)�6H   $	 	Collapsed��6{   ..�5    .   i� $
 0��6E   # Z 6Q   5!   +   T� 5"   *   $ (UIElement.Visibility)�6H   $	 Expanded��6{   ..�5#   .   i� $
 0��6E   # Z 6Q   5$   +   5%   '   5&   #     5'   "   	Unchecked��6.     5(   "   Indeterminate��6.   5)      .+�5+      F� $ 	Collapsed��6   #���  62   $
��Left=�6V   $��Center=�6q   $��1,1,1,1q�6�   "���� 	=     � z  	= � 	=  z 	6�   F� 5,      $ Expanded��6   #���  61   $
��Left=�6U   $��Center=�6p   $��1,1,1,1q�6�   "����     	= � z  	= � 	=  z 	$y�Hidden=�6�   5-      5.      50      51      �� 52      .�53   
   ��  � �� 54      �� 55      .R�56      �� ._�57      �� $��0q�6$   $��5��6/   
���   �$	 1 2R�6R   $ 0Q�6h   58      59      5;      5<      �� 5=      "  Q�6   .�5>   
   ��  � ��
 ���� 6'   �� 5?   
    � �� 67   �� $  HorizontalContentAlignment� �� " ���� 5@   
    � �� 65   �� $ VerticalContentAlignment� �� " ���� 5A   
    � ��$ 1,0,0,0q�6$   �� 5B   
    �@�FocusVisualStyle8   6-   �� 5C   
    � �� 5D      �� 5E      " Q�6"   .R�5F      �  5G      �� 5H      $��19��6/   $
��Auto �6=   �� 5I      $
��Auto �6/   �� 5J      $��* �6/    5L      �� 5M      $
��Auto �6,   �� 5N       5P        5Q      SelectionStates��. 5R   "     Selected��. 5S   &   �� .��5T   *   �� $	 Bd��6G   $0 *(Panel.Background).(SolidColorBrush.Color)�6b   .g�5U   .     $ 0��6B   8  6N   5V   +   5W   '   5X   #     5Y   "   
Unselected��6.     5Z   "   SelectedInactive��. 5[   &   �� .��5\   *   �� $	 Bd��6G   $0 *(Panel.Background).(SolidColorBrush.Color)�6b   .g�5]   .     $ 0��6B   8  6N   5^   +   5_   '   5`   #   5a        5b      ExpansionStates��. 5c   "     Expanded��. 5d   &   �� .��5e   *   T� $ (UIElement.Visibility)�6H   $	 	ItemsHost��6{   ..�5f   .   i� $
 0��6E   # Z 6Q   5g   +   5h   '   5i   #     5j   "   	Collapsed��6.   5k      .+�5m      d� $ Expander��6'   8�� $ Press=�6l    6~   �� *
IsExpanded+ �� *TemplatedParent+�� 5n      $ Bd��6!   $��1��6-   #��z��6=   #��z��6g   #��z��6�   # z��6�   ._�5o      �� $ PART_Header��6/   $��Header��6D   #��z��6[   5p      �� 5q      $ 	ItemsHost��6)   $��1��6<   $��1��6I   $��2��6Y   5r       5s      P� 5t        �@��HasItems! .  . �5u      �� $" Expander��6%    � y�$ Hidden=�6Q   5v      ]� 5w      # 5x      �� 5y   "   $ � ��% .  6A   �� 5z   "   $ � ��$
% Auto��6=   ./�5|      �� $" PART_Header��6%    � ��$ 75��6R   5}      ]� 5~      # 5      �� 5�   "   $ � ��% .  6A   �� 5�   "   $ � ��$
% Auto��6>   ./�5�      �� $" PART_Header��6%    � ��$ 19��6S   5�      5�      5�      5�      5�      