   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  )    #HBBio.MethodEdit.DefineQuestionsWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.MethodEdit     +shareclr-namespace:HBBio.Share     -      ME_DefineQuestionsWin  � Title#  �    � SizeToContent$ WidthAndHeight=�$	��500�� � WindowStartupLocation$ CenterOwner=� � 	Resources #   HBBio.Share.EnumBoolConverter  EnumBoolConverter��%   &	         .��� $��10q� � RowDefinitions �� $
��Auto ��� $��15 ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��25 ��� $��* � �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ labQuestion��$��0��$��0��  ME_Question1#���  �� -    ��Name$ txtQuestion��$��0��$��2������  �� Path$ 	MQuestion�	 �� Mode$	 TwoWay=��� -   $ labType��$��2��$��0��  ME_AnswerType1#���  	� -   
 	�Name$
 rbtnTI��$��4��$��0�� " ME_EnumAnswerType_TextualInput#���   d� 	IsChecked �� $ MType� �� 	Converter# [ %   HBBio.MethodEdit.EnumAnswerType   TextualInput �� ConverterParameter# Z ]� $��4��$��2��{���  �� ElementName$ rbtnTI��$ 	IsChecked�.+��� -   $ labDefaultAnswer��$
��Left=�  ME_DefaultAnswer1#���  �� -   $ txtDefaultAnswer������ $ MDefaultAnswer�$	 TwoWay=�{��� $ rbtnTI��$ 	IsChecked�	� -   $
 rbtnNA��$��6��$��0��$��3��  ME_EnumAnswerType_NoAnswer#���   �� $ MType�# [    NoAnswer# Z 	� -	   $
 rbtnNV��$��8��$��0�� " ME_EnumAnswerType_NumericValue#���   �� $ MType�# [    NumericValue# Z �� $��8��$��2�� �� Orientation$ 
Horizontal=�{��� $ rbtnNV��$ 	IsChecked�.+��� ���� $
 MMin�$	 TwoWay=��� $
�� -> ���� ���� $
 MMax�$	 TwoWay=�	� -
   $
 rbtnMC��$��10��$��0��$	��Top=� $  ME_EnumAnswerType_MultipleChoice#���   �� $ MType�# [    MultipleChoice# Z � $��10��$��2��{��� $ rbtnMC��$ 	IsChecked� �� $
��Auto ��� $��5 ��� $��* ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��$��0�� 	 
ME_Choice1#��� 	 �� -   $ 	txtChoice��$��0��$��2��   HBBio.Share.IconButton  -     Name$ btnAddChoice��   HBBio.Share.IconButton   Click$ btnAddChoice_Click��$��0��$��4�� 

 btnAdd � ToolTip# � 
    Image$+ %/Bio-LabChrom;component/Image/add.png���� -    ��Name$ 
listChoice��$��2��$��2��$	��100��  -   $ btnDelChoice��$ btnDelChoice_Click��$��2��$��4�� 
 btnDel# �  $��Center=�$	��Top=�$. (/Bio-LabChrom;component/Image/delete.png��� $��2�� �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3��  	btnCancel#���  