   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   $    HBBio.MethodEdit.ResultNameWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HBBio.MethodEdit     5   	   +shareclr-namespace:HBBio.Share     5   	   -   5           ME_ResultWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�60    � WindowStartupLocation$ CenterOwner=�6V    � 	Resources 5
      #   HBBio.Share.EnumBoolConverter  EnumBoolConverter��%   &	         5   
   6>   5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � -   5         �Name$
 grid��5      $��0��6   $��20q�6*    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $
��Auto �6#   �� 5      $��5 �6#   �� 5      $��* �6#   �� 5      $��5 �6#   �� 5      $
��Auto �6#    5      �� 5      $
��Auto �6    �� 5      $��15 �6    �� 5      $
��Auto �6    �� 5       $��15 �6    �� 5!      $
��Auto �6    �� 5"      $��15 �6    �� 5#      $
��Auto �6    �� 5$      $��15 �6    �� 5%      $
��Auto �6    �� 5&      $��15 �6    �� 5'      $
��Auto �6    .+�5)      �� -   5         ��Name$ labType��5)      $��0��6)   $��0��66     ME_ResultName1#���  6F   	� 5*      -   5         	�Name$ 
rbtnNoName��5*      $��2��6.   $��2��6;     ME_EnumResultType_NoName#���  6K   	 d� 	IsChecked	 6�   �� 
 �� Path$
 MType� �� 	Converter# [ %   HBBio.MethodEdit.EnumResultType   NoName �� ConverterParameter# Z 	� 5+      -   5        $ rbtnName��5+      $��4��6,   $��2��69     ME_EnumResultType_DlyName#���  6I   	 6   �� $
 MType�# [    DlyName# Z �� 5,      -   5         ��Name$ txtName��5,      $��4��6'   $��4��64   $��3��6D   ��6X   �� *
MDlyName+{�6r   ��  �� ElementName$ rbtnName��$
 	IsChecked�	� 5-      -   5        $ rbtnMethodName��5-      $��6��62   $��2��6?      ME_EnumResultType_MethodName#���  6O   	 6�   �� $
 MType�# [    
MethodName# Z 	� 5.      -   5        $ rbtnDate��5.      $��8��6,   $��2��69     ME_EnumResultType_DateTime#���  6I   	 6�   �� $
 MType�# [    DateTime# Z �� 5/      -	   5         ��Name$ chboxAUI��5/      $��10��6)   $��6��67     ME_AddUniqueIdentifier#���  6G   	 6z   �� *	MUnique+50      � 51   
   $��1��6    52      �� 53      $��* �6#   �� 54      $
��Auto �6#   �� 55      $��5 �6#   �� 56      $
��Auto �6#   .+�58      �� -
   5         ��Name$ btnOK��58      $��1��6$    	 btnOK#���  64   6V   �� 59      -   5        $ 	btnCancel��59      $��3��6(    	 	btnCancel#��� 	 68   6^   5:      5;      5<      