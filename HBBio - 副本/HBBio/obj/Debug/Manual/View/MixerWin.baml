   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HBBio.ManualHBBio.Manual  5   	   9!clr-namespace:HBBio.CommunicationHBBio.Communication  5   	       HBBio.Manual.MixerWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:HBBio.Manual     5   	   2comm!clr-namespace:HBBio.Communication     5   	   -   5           
M_MixerWin  � Title#  �   5	   	    � SizeToContent$ WidthAndHeight=�6.   ���#EEF5FD J� Opacity$	 0.8Q�6i    � AllowsTransparency .  � WindowStyle$
 None=�6�   .��5
      �� ���	#FF268DE4$��2q�6%    �� CornerRadius$ 5��69   ._�5   
   � $��2q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��10 �6#   �� 5      $
��Auto �6#   .+�5      � $��0��6   $��0��6    $��3��60   ���	#FF9DBBCD 5      �� 5      $
��Auto �6'   �� 5      $��5 �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5       �� $��0��6   $��25��6(   $��25��63   $-��'/Bio-LabChrom;component/Image/hblog.ico��6?   �� 5!      $��2��6   #���   6,   $
��Bold"�6P   �� 5"      -   5         ��Name$ btnClose��5"      $��4��6+     closeBtnStyle#��[ 6;   6b   5#      !   HBBio.Communication.MixerUC  5$      -   5        	  Name$	 ucMixer��5$      $��2��6,   $��0��69   $��3��6I   $
��10,0q�6]   �� 5%      -   5        $ btnApply��5%      $��4��6'   $��0��64   $��3��6D   $
��10,0q�6Y     btnApply#���  6g   6�   5&      5'      5(      