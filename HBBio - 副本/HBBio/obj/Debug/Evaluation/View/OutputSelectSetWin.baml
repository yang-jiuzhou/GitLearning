   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.EvaluationHBBio.Evaluation  5   	   )    #HBBio.Evaluation.OutputSelectSetWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.Evaluation     5   	   -   5           E_OutputSelectSetWin  � Title#  �   5   	    � SizeToContent$ Height=�68   $	��400��6V    � WindowStartupLocation$ CenterOwner=�6b   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    5      �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ labNote��5      $��0��6)   $��0��66     E_OutputNote1#���  6F   $	��Top=�6m   �� 5      -   5         ��Name$ txtNote��5      $��0��6'   $��2��64    �� TextWrapping$
 Wrap=�6D    �� AcceptsReturn . "	 �� VerticalScrollBarVisibility$
	 Auto=�6m   $	��100��6�   $	��100��6�   5      � 5   
   $��2��6   .+�5      �� -   5        
 ��Name$
 	chboxUser��5      $��10q�6*   $	��150��66     E_OutputUser#���  6E   �� 5      -   5        $
 chboxChromatogramName��5      $��10q�66   $	��150��6B     E_OutputChromatogramName#���  6Q   �� 5       -   5        $
 chboxChromatogram��5       $��10q�62   $	��150��6>     E_OutputChromatogram#���  6M   �� 5!      -   5        $
 chboxIntegration��5!      $��10q�61   $	��150��6=     E_OutputIntegration#���  6L   �� 5"      -   5        $
 chboxMethod��5"      $��10q�6,   $	��150��68     E_OutputMethod#���  6G   �� 5#      -	   5        $
 chboxLog��5#      $��10q�6)   $	��150��65     E_OutputLog#���  6D   5$      � 5%   
   $��4��6    5&      �� 5'      $��* �6#   �� 5(      $
��Auto �6#   �� 5)      $��10 �6#   �� 5*      $
��Auto �6#   .+�5,      �� -
   5         ��Name$ btnOK��5,      $��1��6$    	 btnOK#���  64   6V   �� 5-      -   5        $ 	btnCancel��5-      $��3��6(    	 	btnCancel#��� 	 68   6^   5.      5/      50      