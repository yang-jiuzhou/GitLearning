   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.EvaluationHBBio.Evaluation  )    #HBBio.Evaluation.OutputSelectSetWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.Evaluation     -      E_OutputSelectSetWin  � Title#  �    � SizeToContent$ Height=�$	��400�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� �� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* �.+��� -    ��Name$ labNote��$��0��$��0��  E_OutputNote1#���  $	��Top=��� -    ��Name$ txtNote��$��0��$��2�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . "	 �� VerticalScrollBarVisibility$
	 Auto=�$	��100��$	��100��� $��2��.+��� -   
 ��Name$
 	chboxUser��$��10q�$	��150��  E_OutputUser#���  �� -   $
 chboxChromatogramName��$��10q�$	��150��  E_OutputChromatogramName#���  �� -   $
 chboxChromatogram��$��10q�$	��150��  E_OutputChromatogram#���  �� -   $
 chboxIntegration��$��10q�$	��150��  E_OutputIntegration#���  �� -   $
 chboxMethod��$��10q�$	��150��  E_OutputMethod#���  �� -	   $
 chboxLog��$��10q�$	��150��  E_OutputLog#���  � $��4�� �� $��* ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -
    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3�� 	 	btnCancel#��� 	 