   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  #    HBBio.Chromatogram.ContrastUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:HBBio.Chromatogram     -   .��]� .+�� -     �Name$  
stackPanel��$	��Top=� � Orientation$ 
Horizontal=��� -    ��Name$ canvas��
�������.+��� -    ��Name$ image���� -    ��Name$ btnMenu��$��>>����Z�  � ContextMenu �� -    ��Name$
 menu��.@�m� -    m�Name$ menuUndoZoom��   labD_UndoZoom#���   m� -   $ menuResetZoom��  labD_ResetZoom#���  �� m� -	   $ menuGrid��  	labD_Grid#���   m� IsCheckable . 	 m� 	IsChecked	 .  m�   
labD_Ruler#���  .@�m� -
   $ menuRulerOdd��  labD_RulerOddO#���   . m� -   $ menuRulerEven��  labD_RulerEven#���   . �� �� m� -   $ 	menuCurve��  D_Curve#���  �� m� -   $ 
menuSelect��  D_Signal#���  �� -   
 ��Name$
 rectZoom��$y�	Collapsed=�
���   ��� -    ��Name$ lineRulerOdd��
���   �$y�	Collapsed=�$��5��	 �� Y1$ 40���� -   $ lineRulerEven��
���   �$y�	Collapsed=�$��5��$ 40���� -    ��Name$ txtRulerInfo��$y�	Collapsed=�$	��150��$
��Auto��$
��Wrap=�