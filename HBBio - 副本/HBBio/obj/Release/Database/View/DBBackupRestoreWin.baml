   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:HBBio.DatabaseHBBio.Database  )clr-namespace:HBBio.ShareHBBio.Share  '    !HBBio.Database.DBBackupRestoreWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:HBBio.Database     +shareclr-namespace:HBBio.Share     -      DB_DBBackupRestoreWin  � Title#  �    � SizeToContent$ Height=�$	��700��$	��309��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto �.+�� $��0�� �� $
��Auto ��� $��10 ��� $
��Auto � � ColumnDefinitions �� $��* ��� $��10 ��� $��* �.+�	� -    	�Name$ 
rbtnBackup��$��0��$��0��  	DB_Backup#���  	� -   $ rbtnRestore��$��0��$��2��  
DB_Restore#���  �� $��2��$��0��{���  �� ElementName$ 
rbtnBackup�� �� Path$ 	IsChecked�.��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ checkBackupLocal��$��0��  DB_Local#���  {��� $ 
rbtnBackup��$ 	IsChecked��� $��2������ $ gridRestoreLocal��$ ActualHeight�{��� $ checkBackupLocal��$ 	IsChecked�.��� $��10q� �� $
��Auto ��� $��* � �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� -   	 ��Name$	 labPathLocal��$��0��  DB_Path#���  �� -   
 ��Name$
 txtPathLocal��$��2���� -    ��Name$ btnPathLocal��$��4��$��.��$��35���� -   $ checkBackupRemote��$��4��  	DB_Remote#���  {��� $ 
rbtnBackup��$ 	IsChecked��� $��6������ $ gridRestoreRemote��$ ActualHeight�{��� $ checkBackupRemote��$ 	IsChecked�.��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� -	   $	 labIP��$��0��$��0�� 	 DB_IP#���  �� -
   $
 txtIP��$��0��$��2���� -   $ btnPathRemote��$��0��$��4��$��.��$��35���� -   $	 labPathRemote��$��2��$��0��#���  �� -   $
 txtPathRemote��$��2��$��2���� -   $	 labUserName��$��4��$��0��  DB_UserName#���  �� -   $
 txtUserName��$��4��$��2���� -   $	 labPwd��$��6��$��0�� 
 DB_Pwd#���  �� -   $
 txtPwd��$��6��$��2���� $��2��$��2��{��� $ rbtnRestore��$ 	IsChecked�.��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+�	� -   $ rbtnRestoreLocal��$��0��#���  {��� $ rbtnRestore��$ 	IsChecked��� -    ��Name$ gridRestoreLocal��$��2��{��� $ rbtnRestoreLocal��$ 	IsChecked�.��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� -   $	 labRestoreLocalPath��$��0��$��0��#���  �� -   $
 txtRestoreLocalPath��$��0��$��2���� -   $ btnRestoreLocal��$��0��$��4��$��.��$��35���� $��2��$��0�� 	 DB_FileName#��� 	 �� -   $
 txtRestoreLocalFile��$��2��$��2��	� -   $ rbtnRestoreRemote��$��4��#���  {��� $ rbtnRestore��$ 	IsChecked��� -   $ gridRestoreRemote��$��6��{��� $ rbtnRestoreRemote��$ 	IsChecked�.��� $��10q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $��* ��� $��10 ��� $
��Auto �.+��� -   $	 labRestoreRemoteIP��$��0��$��0��#���  �� -   $
 txtRestoreRemoteIP��$��0��$��2���� -   $ btnRestoreRemote��$��0��$��4��$��.��$��35���� -   $	 labRestoreRemotePath��$��2��$��0��#���  �� -   $
 txtRestoreRemotePath��$��2��$��2���� $��4��$��0��#��� 	 �� -   $
 txtRestoreRemoteFile��$��4��$��2���� -    $	 labRemoteUserName��$��6��$��0��#���  �� -!   $
 txtRestoreRemoteUserName��$��6��$��2���� -"   $	 labRestoreRemotePwd��$��8��$��0��#���  �� -#   $
 txtRestoreRemotePwd��$��8��$��2��   HBBio.Share.IconButton  -$     Name$ btnCopy��   HBBio.Share.IconButton   Click$ btnCopy_Click��$��8��$��4��   Image$4 ./Bio-LabChrom;component/Image/transmission.png��$��23��   ImageHeight$ 23Q�$��23��   
ImageWidth$ 23Q�   HBBio.Share.LoadingWaitUC  -%     Name$ loadingWaitUC��$��0��$��3��$��0��$��3��$y�	Collapsed=�� $��2�� �� $
��Auto ��� $��* ��� $
��Auto ��� $��5 ��� $
��Auto �.+��� -&   $ btnAuto��$��0�� 
 btnAuto#��� 
 �� -'   $ btnOK��$��2�� 	 btnOK#���  $��32��$	��Top=��� -(   $ 	btnCancel��$��4��  	btnCancel#���  