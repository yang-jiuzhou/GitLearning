   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  $    HBBio.MethodEdit.ResultNameWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:HBBio.MethodEdit     +shareclr-namespace:HBBio.Share     -      ME_ResultWin  � Title#  �    � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterOwner=� � 	Resources #   HBBio.Share.EnumBoolConverter  EnumBoolConverter��%   &	         .��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto �.+�� -    �Name$
 grid��$��0��$��20q� � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $��* ��� $��5 ��� $
��Auto � �� $
��Auto ��� $��15 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��15 ��� $
��Auto �.+��� -    ��Name$ labType��$��0��$��0��  ME_ResultName1#���  	� -    	�Name$ 
rbtnNoName��$��2��$��2��  ME_EnumResultType_NoName#���  	 d� 	IsChecked	 �� 
 �� Path$
 MType� �� 	Converter# [ %   HBBio.MethodEdit.EnumResultType   NoName �� ConverterParameter# Z 	� -   $ rbtnName��$��4��$��2��  ME_EnumResultType_DlyName#���  	 �� $
 MType�# [    DlyName# Z �� -    ��Name$ txtName��$��4��$��4��$��3������ *
MDlyName+{���  �� ElementName$ rbtnName��$
 	IsChecked�	� -   $ rbtnMethodName��$��6��$��2��   ME_EnumResultType_MethodName#���  	 �� $
 MType�# [    
MethodName# Z 	� -   $ rbtnDate��$��8��$��2��  ME_EnumResultType_DateTime#���  	 �� $
 MType�# [    DateTime# Z �� -	    ��Name$ chboxAUI��$��10��$��6��  ME_AddUniqueIdentifier#���  	 �� *	MUnique+� $��1�� �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -
    ��Name$ btnOK��$��1�� 	 btnOK#���  �� -   $ 	btnCancel��$��3�� 	 	btnCancel#��� 	 