   M S B A M L   `   `   `  ����G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HBBio.SystemControlHBBio.SystemControl  5      )clr-namespace:HBBio.ShareHBBio.Share  5      %    HBBio.SystemControl.RunStatusUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HBBio.SystemControl     5      +shareclr-namespace:HBBio.Share     5        � 	Resources  5	       �� 
TargetType �� Property �� Value��%Q   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 5q�6'   5      5      .��5      �� $
��Auto=�6   .��5   
   � -   5         �Name$
 grid��5       � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   $��0��6%      ME_MethodQueueName#���   65   �� 5       -   5         ��Name$ labMethodQueueName��5       $��0��64   $��1��6A   �� 5!      $��1��6   $��0��6%     ME_MethodCount#���  65   �� 5"      -   5        $ labMethodCount��5"      $��1��60   $��1��6=   �� 5#      $��2��6   $��0��6%     ME_MethodName#���  65   �� 5$      -   5        $ labMethodName��5$      $��2��6/   $��1��6<   �� 5%      $��3��6   $��0��6%     ME_LoopCount#���  65   �� 5&      -   5        $ labLoopCount��5&      $��3��6.   $��1��6;   �� 5'      $��4��6   $��0��6%     ME_LoopIndex#���  65   �� 5(      -   5        $ labLoopIndex��5(      $��4��6.   $��1��6;   �� 5)      $��5��6   $��0��6%     ME_PhaseCount#���  65   �� 5*      -   5        $ labPhaseCount��5*      $��5��6/   $��1��6<   �� 5+      $��6��6   $��0��6%     ME_PhaseName#���  65   �� 5,      -   5        $ labPhaseName��5,      $��6��6.   $��1��6;   �� 5-      $��7��6   $��0��6%     ME_PhaseRunning#���  65   �� 5.      -	   5        $ labPhaseRunning��5.      $��7��61   $��1��6>   5/      50      51      