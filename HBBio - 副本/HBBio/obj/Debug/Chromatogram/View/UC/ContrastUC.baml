   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  5      #    HBBio.Chromatogram.ContrastUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:HBBio.Chromatogram     5      -   5        .��5
      ]� .+�5   
   � -   5          �Name$  
stackPanel��5      $	��Top=�6(    � Orientation$ 
Horizontal=�6=   �� 5   
   -   5         ��Name$ canvas��5      
�������.+�5      �� -   5         ��Name$ image��5      �� 5      -   5         ��Name$ btnMenu��5      $��>>��6&   ��63   Z�  � ContextMenu 5      �� 5      -   5         ��Name$
 menu��5   "   .@�5      m� -   5         m�Name$ menuUndoZoom��5   #      labD_UndoZoom#���   69   6b   m� 5      -   5        $ menuResetZoom��5   #     labD_ResetZoom#���  6:   6d   �� 5      m� 5      -	   5        $ menuGrid��5   #     	labD_Grid#���  65    m� IsCheckable . 	 m� 	IsChecked	 .  6   m� 5        
labD_Ruler#���  6#   .@�5      m� -
   5        $ menuRulerOdd��5   '     labD_RulerOddO#���  6=    . 6z   m� 5      -   5        $ menuRulerEven��5   '     labD_RulerEven#���  6>    . 6{   5      �� 5      �� 5      m� 5      -   5        $ 	menuCurve��5   #     D_Curve#���  66   6Y   �� 5      m� 5      -   5        $ 
menuSelect��5   #     D_Signal#���  67   5      5       �� 5!      -   5        
 ��Name$
 rectZoom��5!      $y�	Collapsed=�6*   
���   �6A   �� 5"      -   5         ��Name$ lineRulerOdd��5"      
���   �$y�	Collapsed=�68   $��5��6O   	 �� Y1$ 40��6c   �� 5#      -   5        $ lineRulerEven��5#      
���   �$y�	Collapsed=�69   $��5��6P   $ 40��6d   �� 5$      -   5         ��Name$ txtRulerInfo��5$      $y�	Collapsed=�6.   $	��150��6E   $
��Auto��6Q   $
��Wrap=�6_   5%      5&      5'      