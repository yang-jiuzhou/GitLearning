   M S B A M L   `   `   `  ???? G  CBio-LabChrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HBBioHBBio  ;"clr-namespace:HBBio.AdministrationHBBio.Administration  5clr-namespace:HBBio.AuditTrailsHBBio.AuditTrails  7 clr-namespace:HBBio.ChromatogramHBBio.Chromatogram  3clr-namespace:HBBio.CollectionHBBio.Collection  3clr-namespace:HBBio.ColumnListHBBio.ColumnList  9!clr-namespace:HBBio.CommunicationHBBio.Communication  /clr-namespace:HBBio.DatabaseHBBio.Database  3clr-namespace:HBBio.EvaluationHBBio.Evaluation  +clr-namespace:HBBio.ManualHBBio.Manual  3clr-namespace:HBBio.MethodEditHBBio.MethodEdit  -clr-namespace:HBBio.PassDogHBBio.PassDog  )clr-namespace:HBBio.PrintHBBio.Print  ;"clr-namespace:HBBio.ProjectManagerHBBio.ProjectManager  )clr-namespace:HBBio.ShareHBBio.Share  9!clr-namespace:HBBio.SystemControlHBBio.SystemControl  1clr-namespace:HBBio.TubeStandHBBio.TubeStand      HBBio.DicEvent\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   
titleStyle  ?? 
TargetType ?? Property ?? Value  minBtnStyle ?? 
TargetType ?? RadiusX ?? RadiusY	 ?? X1	 ?? Y1	 ?? X2		 ?? Y2
 ?? Triggers P? Property P? Value  maxBtnStyle  closeBtnStyle A?Name  	winCorner ?? 
BottomLeft ?? BottomRight ?? TopLeft ?? TopRight  winTitleCorner  winStatusCorner  BaseWindowControlTemplate ??Name ?? CornerRadius ? RowDefinitions ?? Orientation ??Name ? Title ?Name  BaseWindowStyle'  !System.Windows.Shell.WindowChrome   CaptionHeight   ResizeBorderThickness,   &HBBio.Administration.AdministrationWin ?? BasedOn+   %HBBio.Administration.PermissionAddWin,   &HBBio.Administration.PermissionEditWin&    HBBio.Administration.RegisterWin&    HBBio.Administration.ReviewerWin$   HBBio.Administration.SignerWin)   #HBBio.Administration.TacticsEditWin%	   HBBio.Administration.UserAddWin&
    HBBio.Administration.UserEditWin&    HBBio.Administration.UserLockWin%   HBBio.Administration.UserPwdWin(   "HBBio.Administration.UserSwitchWin%   HBBio.AuditTrails.AuditTrailWin!   HBBio.AuditTrails.OutputWin&    HBBio.Chromatogram.CurveColorWin)   #HBBio.Chromatogram.CurveSetStyleWin%   HBBio.Chromatogram.MarkerAddWin%   HBBio.Chromatogram.MarkerDelWin(   "HBBio.Collection.CollectionItemWin(   "HBBio.ColumnList.ColumnHandlingWin$   HBBio.ColumnList.ColumnItemWin    HBBio.ColumnList.OutputWin(   "HBBio.Communication.AddressPortWin)   #HBBio.Communication.AlarmWarningWin%   HBBio.Communication.CalOEMCDWin%   HBBio.Communication.CalOEMPHWin,   &HBBio.Communication.CollectorHBModeWin-   'HBBio.Communication.CollectorQBHModeWin'   !HBBio.Communication.CommunSetsWin-   'HBBio.Communication.ConfAlarmWarningWin'    !HBBio.Communication.EditCommunWin/!   )HBBio.Communication.EditProcessPictureWin&"    HBBio.Communication.NewCommunWin)#   #HBBio.Communication.SystemConfigWin'$   !HBBio.Database.DBBackupRestoreWin$%   HBBio.Database.DBAutoBackupWin"&   HBBio.Evaluation.ContrastWin$'   HBBio.Evaluation.EvaluationWin((   "HBBio.Evaluation.IntegrationSetWin))   #HBBio.Evaluation.OutputSelectSetWin *   HBBio.Evaluation.OutputWin+   HBBio.Manual.ManualWin),   #HBBio.MethodEdit.DefineQuestionsWin&-    HBBio.MethodEdit.MethodEditorWin%.   HBBio.MethodEdit.MethodQueueWin#/   HBBio.MethodEdit.NewMethodWin0   HBBio.MethodEdit.NotesWin 1   HBBio.MethodEdit.OutputWin'2   !HBBio.MethodEdit.PersonalPhaseWin)3   #HBBio.MethodEdit.RenamePhaseNameWin$4   HBBio.MethodEdit.ResultNameWin5   HBBio.PassDog.CheckSNWin6   HBBio.Print.OutputSetWin!7   HBBio.Print.PrintPreviewWin,8   &HBBio.ProjectManager.ProjectManagerWin/9   )HBBio.ProjectManager.RunLocationResultWin':   !HBBio.ProjectManager.RunResultWin1;   +HBBio.ProjectManager.TreeNodeInformationWin<   HBBio.Share.MessageBoxWin=   HBBio.Share.StringBoolWin">   HBBio.SystemControl.AboutWin&?    HBBio.SystemControl.CountdownWin*@   $HBBio.SystemControl.SystemControlWin*A   $HBBio.SystemControl.SystemMonitorWin"B   HBBio.TubeStand.TubeStandWin&C    HBBio.TubeStand.TubeStandItemWin   D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     localclr-namespace:HBBio  5administration"clr-namespace:HBBio.Administration  /auditTrailsclr-namespace:HBBio.AuditTrails  1chromatogram clr-namespace:HBBio.Chromatogram  -
collectionclr-namespace:HBBio.Collection  -
columnListclr-namespace:HBBio.ColumnList  3communication!clr-namespace:HBBio.Communication  )databaseclr-namespace:HBBio.Database  -
evaluationclr-namespace:HBBio.Evaluation  %manualclr-namespace:HBBio.Manual  -
methodEditclr-namespace:HBBio.MethodEdit  'passDogclr-namespace:HBBio.PassDog  #printclr-namespace:HBBio.Print  5projectManager"clr-namespace:HBBio.ProjectManager  #shareclr-namespace:HBBio.Share  3systemControl!clr-namespace:HBBio.SystemControl  +	tubeStandclr-namespace:HBBio.TubeStand  %?  &	        &	 q     &	     &	 ?    &	 F    &	 k    &	 ?    &	 ?    7  7  7   7  7  7  &	 ?    7  '  ?    7  '  ?    7  '  ?    7  '  ?    7  '  ?    7  '  ?    7  '  ?    7  '	  ?    7  '
  	    7  '  	    7  '  &	    7  '  5	    7  '  D	    7  '  S	    7  '  b	    7  '  q	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  ?	    7  '  
    7  '  
    7  '  %
    7  '  4
    7  '  C
    7  '   R
    7  '!  a
    7  '"  p
    7  '#  
    7  '$  ?
    7  '%  ?
    7  '&  ?
    7  ''  ?
    7  '(  ?
    7  ')  ?
    7  '*  ?
    7  '+  ?
    7  ',      7  '-      7  '.  $    7  '/  3    7  '0  B    7  '1  Q    7  '2  `    7  '3  o    7  '4  ~    7  '5  ?    7  '6  ?    7  '7  ?    7  '8  ?    7  '9  ?    7  ':  ?    7  ';  ?    7  '<  ?    7  '=      7  '>      7  '?  #    7  '@  2    7  'A  A    7  'B  P    7  'C  _    7  ?? "  ??.???  ?@?Cursor$
 Hand????  ? ??$
 Auto????  ? ??$ 35????  ? ??$	 Top=??? "  ??.???  ? ??$ 2,0,2,0q???  ?@?Cursor$
 Hand????  ? ?? ?? " ??.R?? .+??? $??26??$??26??
???????$ 4??$ 4??$??2???? $??26??$??26?????#4f535d$??2??$ 4??$ 4??
???????? .+??? $
??Left=?$	??Top=?$??5q?.+??? $??2??
???????$ 0??$ 8??$ 16??$	 8??
 P?  ?@J?IsMouseOver . . ???  ?@J?Opacity$	 .75Q??? "  ??.???  ? ??$ 2,0,2,0q???  ?@?Cursor$
 Hand????  ? ?? ?? " ??.R?? .+??? $??26??$??26??
???????$ 4??$ 4??$??2???? $??26??$??26?????#4f535d$??2??$ 4??$ 4??
???????? .+??? $
??Left=?$	??Top=?$??5q?.+??? $??16??$??16?????#4f535d$??2??
???????
 P?  ?@J?IsMouseOver . . ???  ?@J?Opacity$	 .75Q??? "  ??.???  ? ??$ 2,0,2,0q???  ?@?Cursor$
 Hand????  ? ?? ?? " ??.R?? .+?A? $ 	borderBtn??$??26??$??26??$??2?????#4f535d
???????? .+??? $
??Left=?$	??Top=?$??5q?.+??? $??2??
???????$ 0??$ 0??$ 16??$	 16???? $??2??
???????$ 0??$ 16??$ 16??$	 0??
 P?  ?@J?IsMouseOver . . ???  ?@J?Opacity$	 .75Q??? $ 5Q?$ 5Q?$ 5Q?$ 5Q??? $ 0Q?$ 0Q?$ 3Q?$ 3Q??? $ 5Q?$ 5Q?$ 0Q?$ 0Q??? " ?.R?? .+??? $ borderBg??-   $??8q????#EEF5FD???	#FF268DE4$??2q?8   ._??  ?? $
??auto ??? $??* ?.+??? $ borderTitle??-   $??0?????	#FF9DBBCD8  8?? #??z??#??z??._?? 
?????? .+?]? 
?????? .+??? $??3q?$
??Left=?$-??'/Bio-LabChrom;component/Image/hblog.ico???? $??Right=?$??Right=?$??Center=?$ 
Horizontal=?.+??? $ btnMin??-   8?? ?? $ btnMax??-   8?? ?? $ btnClose??-   8?? ?? #??z $	??5,0q?
???   ?$
??Left=?$
??Bold"?? $ gridContent??$??1??.+??? ?? "  ?.???  ? ??8   ??  ?@?WindowStyle$
 None=???  ?@?AllowsTransparency . ??  ?@ WindowChrome   $ 1Q?$ 15q??? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "  	 8   ?? "  
 8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "   8   ?? "    8   ?? "  ! 8   ?? "  " 8   ?? "  # 8   ?? "  $ 8   ?? "  % 8   ?? "  & 8   ?? "  ' 8   ?? "  ( 8   ?? "  ) 8   ?? "  * 8   ?? "  + 8   ?? "  , 8   ?? "  - 8   ?? "  . 8   ?? "  / 8   ?? "  0 8   ?? "  1 8   ?? "  2 8   ?? "  3 8   ?? "  4 8   ?? "  5 8   ?? "  6 8   ?? "  7 8   ?? "  8 8   ?? "  9 8   ?? "  : 8   ?? "  ; 8   ?? "  < 8   ?? "  = 8   ?? "  > 8   ?? "  ? 8   ?? "  @ 8   ?? "  A 8   ?? "  B 8   ?? "  C 8   