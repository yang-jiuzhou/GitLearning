   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N;clr-namespace:System.Windows;assembly=PresentationFrameworkSystem.Windows R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ControlRead �� BasedOn �� Property �� Value �� 
TargetType  BorderBaseRead  ExpandCollapseToggleStyle'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� TargetProperty	 �� 
TargetName
 R� KeyTime  System.Windows.Visibility   Hidden R� Value   Visible F�Name  
SolidBlyph  TreeViewItemFocusVisual �� StrokeDashArray J� Opacity �� Path �� RelativeSource �� AncestorType � ColumnDefinitions � RowDefinitions8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlPressedDarkColor �� Value  ControlMoveDarkColor d�Name �� 	ClickMode d� 	IsChecked ��Name �� Padding ��Name ��Name �� Triggers  P� Property! P� Value" �� 
TargetName# ]� 
Conditions$ �� Property% �� Value�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     localclr-namespace:HBBio  Ay;clr-namespace:System.Windows;assembly=PresentationFramework  %p  'R�       7   7  &	 �     &	 �    'Q� %    7  7  7  7  �� "  R�8   .���  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� " R�.R��� $��Border��8�� ._��� |�.  ��.  $��4q�.���� �� "  d�.���  � |� .  ��  � �� �� " d�.R�� $��15��$��13��
������    CheckStates��.   Checked��. �� .��T� $ (UIElement.Visibility)�$	 	Collapsed��..�i� $
 0��# Z T� $ (UIElement.Visibility)�$	 Expanded��..�i� $
 0��# Z   	Unchecked��  Indeterminate��.+�F� $ 	Collapsed��#���  $
��Left=�$��Center=�$��1,1,1,1q�"���� 	=     � z  	= � 	=  z 	F� $ Expanded��#���  $
��Left=�$��Center=�$��1,1,1,1q�"����     	= � z  	= � 	=  z 	$y�Hidden=��� .���  � �� �� .R��� ._��� $��0q�$��5��
���   �$	 1 2R�$ 0Q��� "  Q�.���  � ��
 ���� ��  � �� �� $  HorizontalContentAlignment� �� " ����  � �� �� $ VerticalContentAlignment� �� " ����  � ��$ 1,0,0,0q���  �@�FocusVisualStyle8   ��  � �� �� " Q�.R��  �� $��19��$
��Auto ��� $
��Auto ��� $��* � �� $
��Auto ���    SelectionStates��.   Selected��. �� .���� $	 Bd��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8    
Unselected��  SelectedInactive��. �� .���� $	 Bd��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8    ExpansionStates��.   Expanded��. �� .��T� $ (UIElement.Visibility)�$	 	ItemsHost��..�i� $
 0��# Z   	Collapsed��.+�d� $ Expander��8�� $ Press=� �� *
IsExpanded+ �� *TemplatedParent+�� $ Bd��$��1��#��z��#��z��#��z��# z��._��� $ PART_Header��$��Header��#��z���� $ 	ItemsHost��$��1��$��1��$��2�� P�   �@��HasItems! .  . ��� $" Expander�� � y�$ Hidden=�]� # �� $ � ��% .  �� $ � ��$
% Auto��./��� $" PART_Header�� � ��$ 75��]� # �� $ � ��% .  �� $ � ��$
% Auto��./��� $" PART_Header�� � ��$ 19��