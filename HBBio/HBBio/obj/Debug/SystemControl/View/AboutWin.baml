   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.SystemControlHBBio.SystemControl  5   	   "    HBBio.SystemControl.AboutWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HBBio.SystemControl     5   	      SC_AboutWin  � Title#  �   5   	    � SizeToContent$ Height=�6/   $	��300��6M    � WindowStartupLocation$ CenterScreen=�6Y   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6     SC_ProductionInformation#���  6    .��5      � $��10q�6    5      �� 5      $
��Auto �6$   �� 5      $��10 �6$   �� 5      $
��Auto �6$   �� 5      $��10 �6$   �� 5      $
��Auto �6$   .+�5      �� $��0��6     SC_Production#���  6)   �� 5      $��2��6     
SC_Version#���  6)   �� 5      $��4��6     
SC_Website#���  6)   5      5      �� 5   
   -   5         ��Name$ btnOK��5      $��2��6    	 btnOK#���  6-   $��Right=�6O   6k   5      5      