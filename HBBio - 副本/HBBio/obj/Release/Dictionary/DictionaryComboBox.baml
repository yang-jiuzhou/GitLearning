   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ControlReadAlignment �� BasedOn �� Property �� Value  ComboBoxToggleButton �� 
TargetType ��Name  BorderBaseRead �� CornerRadius J� SnapsToDevicePixels  GradientBackgroundRead F�Name  
SolidBlyph'   !System.Windows.VisualStateManager	    VisualStateGroups%  System.Windows.VisualStateGroup
   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlMoveDarkColor �� Value  BorderMoveDarkColor  ControlPressedLightColor  ControlPressedDarkColor 	 BorderPressedLightColor 
 BorderPressedDarkColor  ControlDisabledDarkColor  BorderDisabledLightColor d�Name d� 	IsChecked �� Mode �� RelativeSource ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate J� IsHitTestVisible �Name � AllowsTransparency � ActualWidth �� MaxDropDownHeight ��Name �� ClearTypeHint ��Name �� ElementName  K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation!   DirectionalNavigation" �� Triggers# P� Property$ P� Value% �� 
TargetName& P� 
SourceName�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     localclr-namespace:HBBio  %�  '��       7   &	 <     7  7  7  7  7  7  7  7 	 7 
 7 
 7  7  7  '��      7   7  �� "  ��8   .���  �@�OverridesDefaultStyle .  �� "  d�.���  � |� .  ��  �@��	ClickMode$ Press=���  � �� �� " d�.R��� $ border��8��  ._��� $ 0,5,5,0��$��25�� . $��Right=�8�� ._�F� $ arrow��8�� $��Center=�$��Center=�"����        � 	=  	= � z     		   CommonStates��.
   
Normal��  	MouseOver��. �� .���� $ border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  �� $ border��$P J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Pressed��. �� .���� $ border��$N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  �� $ border��$P J(Border.BorderBrush).(GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ border��$P J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  �� $ arrow��$* $(Shape.Fill).(SolidColorBrush.Color)�.g�  $ 0��8 	   Disabled��. �� .���� $ border��$N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8 
 �� $ border��$P J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  �� $ arrow��$* $(Shape.Fill).(SolidColorBrush.Color)�.g�  $ 0��8  �� "  ��8   .���  � ��$ 	15,5,35,5q���  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  �@��PanningMode$
 Both=���  �@��IsFlicksEnabled .  ��  � �� �� " ��.R��  . 	   CommonStates��.
   
Normal��  	MouseOver��  Disabled��.+�d� $ ToggleButton��$��2��#��z��#��z��8��  �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $ ContentSite��#��z #��z #��z��#��z�� .  #��z��# z #��z��� $ Popup�� . |�.  $��Bottom=�$��Slide=����� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.���� $ dropDownBorder��$��2q�
�������$	��150��#��z #��z ._��� $ dropDownScrollViewer��.��� $ Enabled=�.+��� $
��Left=�$	��Top=�$��0��$��0��.+��� $ 
opaqueRect������ *
Background+$ dropDownBorder������ *ActualHeight+$ dropDownBorder������ *ActualWidth+$ dropDownBorder����   . $! Continue=����	#FFFFFFFF" P� # �@��HasItems$ .  . ��� $% dropDownBorder�� � ��$ 95��P� # �@��
IsGrouping$ . . ���  � �� .  P� $& dropDownScrollViewer��# � ��$ .  . ��� $% 
opaqueRect�� �� *VerticalOffset+$ dropDownScrollViewer�� �@��Top