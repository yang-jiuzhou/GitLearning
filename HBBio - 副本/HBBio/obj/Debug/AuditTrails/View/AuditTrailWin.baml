   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  5   	   )clr-namespace:HBBio.ShareHBBio.Share  5   	   %    HBBio.AuditTrails.AuditTrailWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   +localclr-namespace:HBBio.AuditTrails  5   	   %shareclr-namespace:HBBio.Share  5   	   -   5           AT_AuditTrailWin  � Title#  �   5
   	   $	��463��64   $	��800��6A   $	��463��6M   $	��750��6]    � WindowStartupLocation$ CenterScreen=�6l   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5         HBBio.Share.IconButton  -   5          Name$ 	btnSearch��5         HBBio.Share.IconButton   Click$ btnSearch_Click��6h   $��0��62     	AT_Search � ToolTip# �  6B      Image$. (/Bio-LabChrom;component/Image/search.png��6�   $
��Left=�6�   $��35��6�     5      -   5        $ btnPDF��5      $ btnPDF_Click��6b   $��2��6/    
 AT_Out# �  6?   $+ %/Bio-LabChrom;component/Image/pdf.png��6w     5      -   5        $ btnExcel��5      $ btnExcel_Click��6d   $��4��61   # �  6A   $- '/Bio-LabChrom;component/Image/excel.png��6{   5      � 5    
   -   5         �Name$ 
gridSearch��5       $��2��6#    5!      �� 5"      $
��Auto �6#   �� 5#      $��10 �6#   �� 5$      $��* �6#   �� 5%      $��15 �6#   �� 5&      $
��Auto �6#   �� 5'      $��10 �6#   �� 5(      $��* �6#   �� 5)      $��15 �6#   �� 5*      $
��Auto �6#   �� 5+      $��10 �6#   �� 5,      $��* �6#    5.      �� 5/      $
��Auto �6    �� 50      $��10 �6    �� 51      $
��Auto �6    .+�53      �� -   5        	 ��Name$	 labBeginTime��53      $��0��6.   $��0��6;     AT_BeginTime#���  6K   &   Xceed.Wpf.Toolkit.DateTimePicker  54      -   5        
  Name$
 	timeBegin��54   "   $��0��65   $��2��6B   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6R   �� *MDateTimeStart+�� 55      -   5        $	 labType��55      $��0��6)   $��4��66     AT_Type1#���  6F   $��Right=�6h   $	��0,5q�6�   $��48��6�   �� 56      -	   5         ��Name$ cboxType��56      $��0��6)   $��6��66    �� DisplayMemberPath$ MName��6F    �� SelectedValuePath$ MName��6`    �� SelectedValue 6z   �� *MType+�� 57      -
   5        $	 
labEndTime��57      $��2��6,   $��0��69     
AT_EndTime#���  6I     58      -   5        $
 timeEnd��58   "   $��2��63   $��2��6@    6P   �� *MDateTimeStop+�� 59      -   5        $	 labUserName��59      $��2��6-   $��4��6:     AT_UserName1#���  6J   �� 5:      -   5        $ cboxUserName��5:      $��2��6-   $��6��6:   $ MName��6J   $ MName��6d    6~   �� *	MUserName+�� 5;      -   5        $	 	labFilter��5;      $��0��6+   $��8��68     
labFilter1#���  6H   �� 5<      -   5         ��Name$ 	txtFilter��5<      $��0��6)   $��10��66   ��6G   �� *	MFilter+5=      +   %HBBio.AuditTrails.AuditTrailsSearchUC  5>   
   -   5          Name$ auditTrailsSearchUC��5>   $   $��4��6A   5?      5@      