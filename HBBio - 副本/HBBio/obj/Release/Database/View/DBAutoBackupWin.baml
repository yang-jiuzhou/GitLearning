   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:HBBio.DatabaseHBBio.Database  $    HBBio.Database.DBAutoBackupWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:HBBio.Database     -      DB_DBAutoBackupWin  � Title#  �    � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��  DB_Parameter#���  .��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ labAutoBackup��$��0��$��0��  DB_AutoBackup1#���  �� -    ��Name$ chboxEnabled��$��0��$��2��  	labEnable#���  �� -   $ labFrequency��$��2��$��0��  DB_Frequency1#���  �� -    ��Name$ cboxFrequency��$��2��$��2��$��3���� -   $ labMaxCount��$��4��$��0��  DB_MaxCount1#���  �� -   $ cboxMaxCount��$��4��$��2��$��3���� -   $ labPathSelect��$��6��$��0��  DB_PathSelect1#���  �� -	   $ 
chboxLocal��$��6��$��2��  DB_Local#���  �� -
   $ chboxRemote��$��6��$��4��  	DB_Remote#���  �� -    ��Name$ 	gboxLocal��$��2��#���  .��� $��10q� �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� -   $ labPathLocal��$��0�� 	 DB_Path#��� 	 �� -   	 ��Name$	 txtPathLocal��$��2���� -   
 ��Name$
 btnPathLocal��$��4��$��.��$��35���� -   $ 
gboxRemote��$��4��#���  .��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� -   $ labIP��$��0��$��0�� 	
 DB_IP#��� 
 �� -   $	 txtIP��$��0��$��2���� -   $
 btnPathRemote��$��0��$��4��$��.��$��35���� -   $ labPathRemote��$��2��$��0��#��� 	 �� -   $	 txtPathRemote��$��2��$��2���� -   $ labUserName��$��4��$��0��  DB_UserName#���  �� -   $	 txtUserName��$��4��$��2���� -   $ labPwd��$��6��$��0�� 
 DB_Pwd#���  �� -   $	 txtPwd��$��6��$��2��� $��2�� �� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -   $
 btnOK��$��1�� 	 btnOK#���  �� -   $
 	btnCancel��$��3��  	btnCancel#���  