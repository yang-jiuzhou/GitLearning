   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   bool2VisibilityConverter&    System.Windows.Controls.DataGrid  �� 
TargetType  ControlRead �� BasedOn �� Property �� Value  	SolidLine �� 
TargetType ��Name  BorderBaseRead  SolidBackground'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� 
TargetName
 �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime  ControlDisabledDarkColor �� Value  BorderDisabledDarkColor ��Name � ColumnDefinitions � RowDefinitions  GradientBorder  GradientBackgroundRead    SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!    HeadersVisibilityConverter �� 	Converter �� RelativeSource �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� Orientation �� ViewportHeight �� ViewportSize �� ScrollableHeight  � Maximum*! �� #ComputedVerticalScrollBarVisibility" � Value# �� Mode$ �� ViewportWidth% �� ScrollableWidth,& �� %ComputedHorizontalScrollBarVisibility' J� SnapsToDevicePixels( �� Triggers) P� Property* P� Value)  #System.Windows.Controls.DataGridRow 	 ControlReadAlignment+ �� CornerRadius?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?	  9System.Windows.Controls.Primitives.DataGridCellsPresenter,   
ItemsPanelA
  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter-   DetailsVisibility$.   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation/   Vertical#0    RowDetailsScrollingConverter:  4System.Windows.Controls.Primitives.DataGridRowHeader
1   Row2   Transitions%  System.Windows.VisualTransition3   GeneratedDuration 
 ControlMoveDarkColor  BorderMoveDarkColor  ControlPressedDarkColor  BorderPressedDarkColor  ControlSelectedDarkColor  BorderSelectedDarkColor*  $System.Windows.Controls.DataGridCell4 �� Orientation=  7System.Windows.Controls.Primitives.DataGridColumnHeader�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     localclr-namespace:HBBio  %�  &	        '        7  7  7  7  7  7  7  7  7  '  �    7 	 7  7  7 
 7  7  7  7  7  '  a    7 	 7 
 7  7  7  7  7  '  �    7 	 7  7   '  (    7 	 7  �� �� "    8   .���  �@  HeadersVisibility$ Column=���  � ��$ Center=���  � ��$ Center=���  �@  VerticalGridLinesBrush8  ��   �@  HorizontalGridLinesBrush8  ��   �@  RowDetailsVisibilityMode$ VisibleWhenSelected=���  � �� . ��  �@��PanningMode$
 Both=���  �@��IsFlicksEnabled .  ��  �@  CanUserAddRows .  ��  �@  AutoGenerateColumns .  ��  � �� �� "   .R��� $ border��8�� 8��    CommonStates��.   Disabled��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    
Normal��._��� $ DG_ScrollViewer��|�.  ���� " ��.R��  �� $
��Auto ��� $��* ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto �.+��� 8�� 8�� #��Z |�.  ��Z� y��� *HeadersVisibility+# Z # Z  �� "   ���� *CellsPanelHorizontalOffset+ �� "     $! PART_ColumnHeadersPresenter��$��1��y��� *HeadersVisibility+# Z # Z  �� "   �� $! PART_ScrollContentPresenter��$��2��$��1��# z���� $ PART_VerticalScrollBar��$��2��$��1��$ Vertical=�# z #  z #y�z! " �� *VerticalOffset+$# OneWay=� �� *TemplatedParent+� $��1��$��2�� �� ���� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� $��* �.+��� $ PART_HorizontalScrollBar��$��1��$ 
Horizontal=�# z$ #  z% #y�z& " �� *HorizontalOffset+$# OneWay=� �� *TemplatedParent+.���� #' z' ( P� ) �@��
IsGrouping* . . ���  � �� .  �� "   8   .���  � ��$ 30����  �@=�ErrorTemplate Z� ��  �@ ValidationErrorTemplate �� .R��� 
���  ��$��2,0,0,0q�$��!��$��Center=���  � �� �� "  .R��� $ border��8�� $+ 0��8�� ._�  $��-2q� �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�	  $��1��#��z, #' z' 
  $��1��$��1��#y�z- . �� *AreRowDetailsFrozen+# Z/ # Z0  �� "     $��2��$. Vertical=�y��� *HeadersVisibility+# Z1 # Z  �� "      CommonStates��2   $3 0:0:0.5D�.   
Normal��  	MouseOver��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    MouseOver_Selected��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Normal_Selected��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$P
 J(Border.BorderBrush).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  ( P� ) �@ 	IsNewItem* . . ���  � �� �� *NewItemMargin+ �� "   �� "   8   .���  � �� �� "  .R��� $ border��
������ 
������ $��1q�   CommonStates��2   $3 0:0:0.5D�.   
Normal��  	MouseOver��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$2
 ,(Border.BorderBrush).(SolidColorBrush.Color)�.g�  $ 0��8    MouseOver_Selected��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$2
 ,(Border.BorderBrush).(SolidColorBrush.Color)�.g�  $ 0��8    Pressed��  Disabled��. �� .���� $	 border��$0
 *(Panel.Background).(SolidColorBrush.Color)�.g�  $ 0��8  �� $	 border��$2
 ,(Border.BorderBrush).(SolidColorBrush.Color)�.g�  $ 0��8  ._��� $��Center=�#' z' ( P� ) �@J�IsKeyboardFocusWithin* . . ���  � �� �	#FF97A607�� "   8   .���  � ��$ 16(���  � �� �� "  .R�� .+��� $ rowHeaderBorder��8�� $+ 0��$��1q�._��� $4 
Horizontal=�.+��� #��z��$��Center=�#' z' �� ' .  ���� *ValidationErrorTemplate+ �� "  y��� *(Validation.HasError)+8   �� "  �� "   8   .���  � ��$ 16(���  � �� �� "  .R�� .+��� $ columnHeaderBorder��8�� $+ 0��$��1q�._��� #��z��#��z��#' z' #��z��