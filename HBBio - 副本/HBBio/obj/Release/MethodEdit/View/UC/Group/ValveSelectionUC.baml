   M S B A M L   `   `   `  ���� G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  )clr-namespace:HBBio.ShareHBBio.Share  9!clr-namespace:HBBio.CommunicationHBBio.Communication  '    !HBBio.MethodEdit.ValveSelectionUC   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  *localclr-namespace:HBBio.MethodEdit  %shareclr-namespace:HBBio.Share  ,comm!clr-namespace:HBBio.Communication  .����  #  ME_EnumGroupType_ValveSelection#���   .��� -     �Name$
  grid��$��10q� � ColumnDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* �.+��� -    ��Name$ chboxUTSIAIMS��$��0��$��8��  ME_SameMSValve#���   d� 	IsChecked �� *MEnableSameMS+�� -    ��Name$ labInA��$��2��  labInA1#���  �� -    ��Name$ cboxInA��$��2��$��2��%   HBBio.Communication.EnumInAInfo   NameList#��Z  �� SelectedIndex �� *MInA+�� -   $ labInB��$��4��  labInB1#���  �� -   $ cboxInB��$��4��$��2��%   HBBio.Communication.EnumInBInfo	   NameList#��Z	  �� *MInB+$  Xceed.Wpf.Toolkit.DoubleUpDown  -   
  Name$
 
doublePerB��$��4��$��4���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Maximum$	 100X�   Value �� *MPerB+y���  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType'   !HBBio.MethodEdit.ValveSelectionUC"   �� Path$ VisibilityPer��� -   $ labPerB��$��4��$��6��$��%B��y���  �� $ FindAncestor=�"  $ VisibilityPer��� -	   $ labInC��$��6��  labInC1#���  �� -
   $ cboxInC��$��6��$��2��%   HBBio.Communication.EnumInCInfo   NameList#��Z  �� *MInC+  -   $
 
doublePerC��$��6��$��4��$	 100X� �� *MPerC+y���  �� $ FindAncestor=�"  $ VisibilityPer��� -   $ labPerC��$��6��$��6��$��%C��y���  �� $ FindAncestor=�"  $ VisibilityPer��� -   $ labInD��$��8��  labInD1#���  �� -   $ cboxInD��$��8��$��2��%   HBBio.Communication.EnumInDInfo   NameList#��Z  �� *MInD+  -   $
 
doublePerD��$��8��$��4��$	 100X� �� *MPerD+y���  �� $ FindAncestor=�"  $ VisibilityPer��� -   $ labPerD��$��8��$��6��$��%D��y���  �� $ FindAncestor=�"  $ VisibilityPer��� -   $ labBPV��$��10��  labBPV1#���  �� -   $ cboxBPV��$��10��$��2��%   HBBio.Communication.EnumBPVInfo   NameList#��Z  �� *MBPV+�� -   $ chboxFTSWTSB��$��12��$��8��  
ME_FTSWTSB#���   �� *MEnableWash+y���  �� $ FindAncestor=�"  $ VisibilityFillSystem�