   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   )    #HBBio.MethodEdit.DefineQuestionsWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.MethodEdit     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           ME_DefineQuestionsWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�69   $	��500��6_    � WindowStartupLocation$ CenterOwner=�6n    � 	Resources 5
      #   HBBio.Share.EnumBoolConverter  EnumBoolConverter��%   &	         5   
   6"   5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��15 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��25 �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $��5 �6    �� 5       $
��Auto �6    �� 5!      $��5 �6    �� 5"      $
��Auto �6    �� 5#      $��5 �6    �� 5$      $
��Auto �6    .+�5&      �� -   5         ��Name$ labQuestion��5&      $��0��6-   $��0��6:     ME_Question1#���  6J   �� 5'      -   5         ��Name$ txtQuestion��5'      $��0��6+   $��2��68   ��6H   ��  �� Path$ 	MQuestion�	 �� Mode$	 TwoWay=��� 5(      -   5        $ labType��5(      $��2��6)   $��0��66     ME_AnswerType1#���  6F   	� 5)      -   5        
 	�Name$
 rbtnTI��5)      $��4��6*   $��0��67    " ME_EnumAnswerType_TextualInput#���  6G    d� 	IsChecked 6�   �� $ MType� �� 	Converter# [ %   HBBio.MethodEdit.EnumAnswerType   TextualInput �� ConverterParameter# Z ]� 5*      $��4��6   $��2��6%   {�65   ��  �� ElementName$ rbtnTI��$ 	IsChecked�.+�5+      �� -   5        $ labDefaultAnswer��5+      $
��Left=�66     ME_DefaultAnswer1#���  6L   �� 5,      -   5        $ txtDefaultAnswer��5,      ��64   �� $ MDefaultAnswer�$	 TwoWay=�{�6e   �� $ rbtnTI��$ 	IsChecked�5-      	� 5.      -   5        $
 rbtnNA��5.      $��6��6*   $��0��67   $��3��6G     ME_EnumAnswerType_NoAnswer#���  6[    6�   �� $ MType�# [    NoAnswer# Z 	� 5/      -	   5        $
 rbtnNV��5/      $��8��6*   $��0��67    " ME_EnumAnswerType_NumericValue#���  6G    6�   �� $ MType�# [    NumericValue# Z �� 50      $��8��6   $��2��6&    �� Orientation$ 
Horizontal=�6n   {�66   �� $ rbtnNV��$ 	IsChecked�.+�51      �� ��6   �� $
 MMin�$	 TwoWay=��� 52      $
�� -> ��6   �� 53      ��6   �� $
 MMax�$	 TwoWay=�54      	� 55      -
   5        $
 rbtnMC��55      $��10��6*   $��0��68   $	��Top=�6H    $  ME_EnumAnswerType_MultipleChoice#���  6`    6�   �� $ MType�# [    MultipleChoice# Z � 56      $��10��6   $��2��6!   {�61   �� $ rbtnMC��$ 	IsChecked� 57      �� 58      $
��Auto �6'   �� 59      $��5 �6'   �� 5:      $��* �6'   �� 5;      $��5 �6'   �� 5<      $
��Auto �6'    5>      �� 5?      $
��Auto �6$   �� 5@      $��5 �6$   �� 5A      $
��Auto �6$   .+�5C      �� $��0��6   $��0��6)    	 
ME_Choice1#��� 	 69   �� 5D      -   5        $ 	txtChoice��5D      $��0��6-   $��2��6:      HBBio.Share.IconButton  5E      -   5          Name$ btnAddChoice��5E   #      HBBio.Share.IconButton   Click$ btnAddChoice_Click��6y   $��0��69   $��4��6F    

 btnAdd � ToolTip# � 
 6V      Image$+ %/Bio-LabChrom;component/Image/add.png��6�   �� 5F      -   5         ��Name$ 
listChoice��5F      $��2��6.   $��2��6;   $	��100��6K     5G      -   5        $ btnDelChoice��5G   #   $ btnDelChoice_Click��6�   $��2��69   $��4��6F    
 btnDel# �  6V   $��Center=�6y   $	��Top=�6�   $. (/Bio-LabChrom;component/Image/delete.png��6�   5H      5I      � 5J   
   $��2��6    5K      �� 5L      $��* �6#   �� 5M      $
��Auto �6#   �� 5N      $��5 �6#   �� 5O      $
��Auto �6#   .+�5Q      �� -   5         ��Name$ btnOK��5Q      $��1��6$    	 btnOK#���  64   6V   �� 5R      -   5        $ 	btnCancel��5R      $��3��6(     	btnCancel#���  68   6^   5S      5T      5U      