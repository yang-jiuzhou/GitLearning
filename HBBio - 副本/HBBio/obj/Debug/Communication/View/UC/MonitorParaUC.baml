   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.CommunicationHBBio.Communication  5      '    !HBBio.Communication.MonitorParaUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -local!clr-namespace:HBBio.Communication  5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ labJudge��5      $��0��6&   $��0��63      Com_Monitor_Judge1#���   6C   �� 5   
   -   5         ��Name$ 	cboxJudge��5      $��0��6&   $��2��63   $��3��6C    �� DisplayMemberPath$ MString��6W    �� SelectedValuePath$ MEnum��6s    �� SelectedValue 6�   �� *MJudge+6�   �� 5   
   $��2��6   $��0��6"    �� Orientation$ 
Horizontal=�62   .+�5      �� ��6   �� *MName+�� 5      -   5        $ labMoreThan��5        Com_Monitor_MoreThan1#���  6-   5      $  Xceed.Wpf.Toolkit.DoubleUpDown  5    
   -   5          Name$ doubleMoreThan��5       $��2��64   $��2��6A   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Value	 6Q   �� *	MMoreThan+
   Minimum
 6m   �� *MMin+   Maximum 6�   �� *MMax+�� 5!   
   -   5        $ labMoreThanUnit��5!      $��2��6-   $��4��6:   ��6J   �� *MUnit+�� 5"   
   $��4��6   $��0��6"   $ 
Horizontal=�62   .+�5#      �� ��6   �� *MName+�� 5$      -   5        $ labLessThan��5$        Com_Monitor_LessThan1#���  6-   5%        5&   
   -   5        $ doubleLessThan��5&      $��4��64   $��2��6A   	 6Q   �� *	MLessThan+
 6m   �� *MMin+ 6�   �� *MMax+�� 5'   
   -   5        $ labLessThanUnit��5'      $��4��6-   $��4��6:   ��6J   �� *MUnit+�� 5(   
   -	   5        $ labStability��5(      $��6��6*     Com_Monitor_Stability1#���  67     5)   
   -
   5        $ doubleStabilityLength��5)      $��6��6;   $��2��6H   	 6X   �� *MStabilityLength+�� 5*   
   -   5        $ cboxStabilityUnit��5*      $��6��6.   $��4��6;   $ MName��6K   $ MMode��6e    6   �� *MStabilityUnit+5+      5,      