   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBarLineButton  �� 
TargetType �� Property �� Value �� 
TargetType ��Name  BorderBaseRead F�Name  
SolidBlyph �� RelativeSource'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState	   
Storyboard
 �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlMoveDarkColor �� Value  ControlPressedDarkColor  ControlDisabledDarkColor  ScrollBarPageButton  ScrollBarThumb �� CornerRadius  VerticalScrollBar � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton 	 HorizontalScrollBar � ColumnDefinitions 
 ControlBase �� BasedOn �� Triggers P� Property P� Value�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     localclr-namespace:HBBio  %  &	        7  7  7  7  7  &	 �    &	 �    7  7  7  7  &	 Z    7   7  7  7  7   &		 �    7   7  7  7  7   '�� �	    7 
 7 	 7  �� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ border��8��  ._�F� $ arrow��$��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+   CommonStates��.   
Normal��  	MouseOver��.	 �� .���� $
 border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Pressed��.	 �� .���� $
 border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Disabled��.	 �� .���� $
 border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  �� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " n�.R��� $ border��8��  $ 1��$��2q�   CommonStates��.   
Normal��  	MouseOver��.	 �� .���� $
 border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Pressed��.	 �� .���� $
 border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Disabled��.	 �� .���� $
 border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  �� " ��.R��  �� $��25���� $��* ��� $��25��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��25��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $ 
PART_Track��$��1����.  �� 8�� $��ScrollBar.PageUpCommand�� n� 8��  �� 8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��25��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z���� " ��.R��  �� $��25���� $��* ��� $��25��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��25��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $ 
PART_Track��$��1����.   �� 8�� $��ScrollBar.PageLeftCommand�� n� 8��  �� 8�� $ ��ScrollBar.PageRightCommand���� $��2��8�� $��25��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z���� "  ��8    P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 25����  � ��8  P�  �@��Orientation$ Vertical=�. ���  � ��$ 25����  � ��$
 Auto����  � ��8  