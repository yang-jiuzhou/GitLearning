   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   bool2VisibilityConverter&    System.Windows.Controls.DataGrid  �� 
TargetType  ControlRead �� BasedOn �� Property �� Value  	SolidLine �� 
TargetType ��Name  BorderBaseRead  SolidBackground'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� 
TargetName
 �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlDisabledDarkColor �� Value  BorderDisabledDarkColor ��Name � ColumnDefinitions � RowDefinitions  GradientBorder  GradientBackgroundRead    SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!    HeadersVisibilityConverter �� 	Converter �� RelativeSource �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� Orientation �� ViewportHeight �� ViewportSize �� ScrollableHeight  � Maximum*! �� #ComputedVerticalScrollBarVisibility" � Value# �� Mode$ �� ViewportWidth% �� ScrollableWidth,& �� %ComputedHorizontalScrollBarVisibility' J� SnapsToDevicePixels( �� Triggers) P� Property* P� Value)  #System.Windows.Controls.DataGridRow 	 ControlReadAlignment+ �� CornerRadius?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?	  9System.Windows.Controls.Primitives.DataGridCellsPresenter,   
ItemsPanelA
  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter-   DetailsVisibility$.   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation/   Vertical#0    RowDetailsScrollingConverter:  4System.Windows.Controls.Primitives.DataGridRowHeader
1   Row2   Transitions%  System.Windows.VisualTransition3   GeneratedDuration 
 ControlMoveDarkColor  BorderMoveDarkColor  ControlPressedDarkColor  BorderPressedDarkColor  ControlSelectedDarkColor  BorderSelectedDarkColor*  $System.Windows.Controls.DataGridCell4 �� Orientation=  7System.Windows.Controls.Primitives.DataGridColumnHeader�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:HBBio  5      %�  &	        '        7  7  7  7  7  7  7  7  7  '  �
    7 	 7  7  7 
 7  7  7  7  7  '  �    7 	 7 
 7  7  7  7  7  '  �    7 	 7  7   '  �    7 	 7  �� 5      6#   �� 5      "    6   8   .�5   
   ��  �@  HeadersVisibility$ Column=�6.   �� 5	   
    � ��$ Center=�65   �� 5
   
    � ��$ Center=�65   �� 5   
    �@  VerticalGridLinesBrush8  63   �� 5   
     �@  HorizontalGridLinesBrush8  65   �� 5   
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5   
    � �� . 6:   �� 5   
    �@��PanningMode$
 Both=�65   �� 5   
    �@��IsFlicksEnabled .  63   �� 5   
    �@  CanUserAddRows .  6+   �� 5   
    �@  AutoGenerateColumns .  60   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� $ border��6   8�� 8��  5        5      CommonStates��. 5   "     Disabled��. 5   &   �� .��5   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5   .     $ 0��6B   8  6N   5   +   �� 5   *   $	 border��6G   $P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5   .     $ 0��6B   8  6N   5    +   5!   '   5"   #     5#   "   
Normal��6.   5$      ._�5&      �� $ DG_ScrollViewer��6'   |�.  ��5'      �� 5(   "   " ��62   .R�5)   &   �  5*   *   �� 5+   .   $
��Auto �6?   �� 5,   .   $��* �6?   �� 5-   .   $
��Auto �6?    5/   *   �� 50   .   $
��Auto �6<   �� 51   .   $��* �6<   �� 52   .   $
��Auto �6<   .+�54   *   �� 8�� 8�� #��Z 6�   |�.  ��61   Z� y�6�   �� *HeadersVisibility+# Z # Z  �� "   ��6�  �� *CellsPanelHorizontalOffset+ �� "     55   *   $! PART_ColumnHeadersPresenter��6I   $��1��56   3   y�57   3   �� *HeadersVisibility+# Z # Z  �� "   �� 58   *   $! PART_ScrollContentPresenter��6A   $��2��59   C   $��1��5:   C   # z��5;   C   �� 5<   *   $ PART_VerticalScrollBar��64   $��2��5=   6   $��1��5>   6   $ Vertical=�5?   6   # z 5@   6   #  z 5A   6   #y�z! 5B   6   " 5C   6   �� *VerticalOffset+$# OneWay=� �� *TemplatedParent+� 5D   *   $��1��6/   $��2��6?    5E   .   �� 5F   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� 5G   2   $��* �6C   .+�5I   .   �� $ PART_HorizontalScrollBar��68   $��1��5J   8   $ 
Horizontal=�5K   8   # z$ 5L   8   #  z% 5M   8   #y�z& 5N   8   " 5O   8   �� *HorizontalOffset+$# OneWay=� �� *TemplatedParent+5P   +   5Q   '   5R   #   .��5T      �� #' z' 6-   5U      5V      5W      5Y      ( 5Z   
   P� 5[      ) �@��
IsGrouping* . . �5\      ��  � �� .  6B   5]      5_      �� 5b      "   6   8   .�5c   
   ��  � ��$ 30��6&   �� 5d   
    �@=�ErrorTemplate 65   Z� �� 5e   
    �@ ValidationErrorTemplate 5f      �� 5g      .R�5h      �� 
���  ��$��2,0,0,0q�61   $��!��6B   $��Center=�6K   5i      5k      �� 5l   
    � �� 5m      �� 5n      "  6"   .R�5o      �� $ border��6   8�� $+ 0��6U   8�� ._�5p        $��-2q�61    5q      �� 5r   "   $
��Auto �63   �� 5s   "   $��* �63    5u      �� 5v   "   $��* �60   �� 5w   "   $
��Auto �60   .+�5y      	  $��1��65   #��z, 6E   #' z' 6o   
  5z      $��1��67   $��1��6G   #y�z- 6T   . 6�   �� *AreRowDetailsFrozen+# Z/ # Z0  �� "     5{      $��2��60   $. Vertical=�6A   y�6�   �� *HeadersVisibility+# Z1 # Z  �� "   5|       5}        5~      CommonStates��2 5�   "     5�   &   $3 0:0:0.5D�67   . 5�   "     
Normal��6.     5�   "   	MouseOver��. 5�   &   �� .��5�   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   �� 5�   *   $	 border��6G   $P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   5�   '   5�   #     5�   "   MouseOver_Selected��. 5�   &   �� .��5�   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   �� 5�   *   $	 border��6G   $P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   5�   '   5�   #     5�   "   Normal_Selected��. 5�   &   �� .��5�   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   �� 5�   *   $	 border��6G   $P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   5�   '   5�   #   5�      5�      5�      5�      ( 5�   
   P� 5�      ) �@ 	IsNewItem* . . �5�      ��  � �� 6+   �� *NewItemMargin+ �� "   5�      5�      �� 5�      "   6   8   .�5�   
   ��  � �� 5�      �� 5�      "  6"   .R�5�      �� $ border��6   
������ 
������ $��1q�6`    5�        5�      CommonStates��2 5�   "     5�   &   $3 0:0:0.5D�67   . 5�   "     
Normal��6.     5�   "   	MouseOver��. 5�   &   �� .��5�   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   �� 5�   *   $	 border��6G   $2
 ,(Border.BorderBrush).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   5�   '   5�   #     5�   "   MouseOver_Selected��. 5�   &   �� .��5�   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   �� 5�   *   $	 border��6G   $2
 ,(Border.BorderBrush).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   5�   '   5�   #     5�   "   Pressed��6.     5�   "   Disabled��. 5�   &   �� .��5�   *   �� $	 border��6G   $0
 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   �� 5�   *   $	 border��6G   $2
 ,(Border.BorderBrush).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   8  6N   5�   +   5�   '   5�   #   5�      ._�5�      �� $��Center=�6+   #' z' 6F   5�      5�      5�      ( 5�   
   P� 5�      ) �@J�IsKeyboardFocusWithin* . . �5�      ��  � �� �	#FF97A60760   5�      5�      �� 5�      "   6   8   .�5�   
   ��  � ��$ 16(�6%   �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      � .+�5�      �� $ rowHeaderBorder��6!   8�� $+ 0��6b   $��1q�6s   ._�5�      �� $4 
Horizontal=�6)   .+�5�   "   �� #��z��63   $��Center=�6V   #' z' 6q   �� 5�   "   ' .  ��6F   �� *ValidationErrorTemplate+ �� "  y�6�   �� *(Validation.HasError)+8   �� "  5�      5�      5�      5�      5�      5�      �� 5�      "   6   8   .�5�   
   ��  � ��$ 16(�6%   �� 5�   
    � �� 5      �� 5     "  6"   .R�5     � .+�5     �� $ columnHeaderBorder��6!   8�� $+ 0��6e   $��1q�6v   ._�5     �� #��z��6/   #��z��6R   #' z' 6�   #��z��6�   5     5     5     5	     5
     5     