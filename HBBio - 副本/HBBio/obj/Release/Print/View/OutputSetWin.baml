   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HBBio.PrintHBBio.Print      HBBio.Print.OutputSetWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  %localclr-namespace:HBBio.Print  -      Print_OutputSetWin  � Title#  �    � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterOwner=�.��]� $��5q�.+�]� $	��Top=�.+��� $
��Left=�.+���   	labHeader#���  .��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $��* � � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ labIcon��$��0��$��0��  labIcon1#���  �� -    ��Name$ 	imageIcon��$��0��$��2��$	��100��$	��100���� $��0��$��4��.+��� -    ��Name$ btnIcon��$��5q�  btnIconSelect#���  �� -   $ btnIconClear��$��5q�  btnIconClear#���  �� -   $ labTitle��$��2��$��0��  	labTitle1#���  �� -    ��Name$ txtTitle��$��2��$��2��$��4����   labMainText#���  .��� $��10q� �� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   $ 	labMargin��$��0��$��0��  
labMargin1#���  � $��0��$��2�� �� $
��Auto ��� $��10 ��� $��* ��� $��15 ��� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto �.+��� -	   $ labLeft��$��0��$��0��  labLeft1#���  %  Xceed.Wpf.Toolkit.IntegerUpDown  -
   	  Name$	 numLeft��$��0��$��2���� -   $ labRight��$��0��$��4�� 	 	labRight1#��� 	   -   $	 numRight��$��0��$��6���� -   $ labTop��$��2��$��0�� 
 labTop1#��� 
   -   $	 numTop��$��2��$��2���� -   $ 	labBottom��$��2��$��4��  
labBottom1#���    -   $	 	numBottom��$��2��$��6���� -   $ labMarkerStyle��$��2��$��0��  Print_MarkerStyle1#���  �� -   
 ��Name$
 cboxMarkerStyle��$��2��$��2����   	labFooter#���  .����  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ chboxSigner��$��5q�  A_SignerName#���  �� -   $ chboxReviewer��$��5q�  A_ReviewerName#���  �� -   $ chboxOutputTime��$��5q�  labOutputTime#���  ��   labOther#���  .��� $��10q� �� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -   $ labSize��$��0��$��0��  labFont1#���  �� -   $
 cboxSize��$��0��$��2���� -   $ 	labFamily��$��2��$��0��  
labFamily1#���  �� -   $
 
cboxFamily��$��2��$��2���� -   $ labFore��$��4��$��0��  labForeground1#���  �� -   $ btnFore��$��4��$��2��$��25����Z� �� -   $ labBack��$��6��$��0��  labBackground1#���  �� -   $ btnBack��$��6��$��2��$��25����Z� �� $ 
Horizontal=�$��Right=�$��Bottom=�.+��� -   $ btnOK��$��0,0,10,0q� 	 btnOK#���  �� -   $ 	btnCancel��  	btnCancel#���  