   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  )clr-namespace:HBBio.ShareHBBio.Share  %    HBBio.AuditTrails.AuditTrailWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  +localclr-namespace:HBBio.AuditTrails  %shareclr-namespace:HBBio.Share  -      AT_AuditTrailWin  � Title#  �   $	��463��$	��800��$	��463��$	��750�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $��* �.+�� $��0�� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* �.+�   HBBio.Share.IconButton  -     Name$ 	btnSearch��   HBBio.Share.IconButton   Click$ btnSearch_Click��$��0��  	AT_Search � ToolTip# �     Image$. (/Bio-LabChrom;component/Image/search.png��$
��Left=�$��35��  -   $ btnPDF��$ btnPDF_Click��$��2�� 
 AT_Out# �  $+ %/Bio-LabChrom;component/Image/pdf.png��  -   $ btnExcel��$ btnExcel_Click��$��4��# �  $- '/Bio-LabChrom;component/Image/excel.png��� -    �Name$ 
gridSearch��$��2�� �� $
��Auto ��� $��10 ��� $��* ��� $��15 ��� $
��Auto ��� $��10 ��� $��* ��� $��15 ��� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $��10 ��� $
��Auto �.+��� -   	 ��Name$	 labBeginTime��$��0��$��0��  AT_BeginTime#���  &   Xceed.Wpf.Toolkit.DateTimePicker  -   
  Name$
 	timeBegin��$��0��$��2���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *MDateTimeStart+�� -   $	 labType��$��0��$��4��  AT_Type1#���  $��Right=�$	��0,5q�$��48���� -	    ��Name$ cboxType��$��0��$��6�� �� DisplayMemberPath$ MName�� �� SelectedValuePath$ MName�� �� SelectedValue �� *MType+�� -
   $	 
labEndTime��$��2��$��0��  
AT_EndTime#���    -   $
 timeEnd��$��2��$��2�� �� *MDateTimeStop+�� -   $	 labUserName��$��2��$��4��  AT_UserName1#���  �� -   $ cboxUserName��$��2��$��6��$ MName��$ MName�� �� *	MUserName+�� -   $	 	labFilter��$��0��$��8��  
labFilter1#���  �� -    ��Name$ 	txtFilter��$��0��$��10������ *	MFilter++   %HBBio.AuditTrails.AuditTrailsSearchUC  -     Name$ auditTrailsSearchUC��$��4��