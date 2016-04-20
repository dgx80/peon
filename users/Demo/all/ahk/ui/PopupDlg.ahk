; Main Popup Generic!!!!!!!!!!!!!!!!!!!!!!!!!!!

#SingleInstance ignore

#Include definitions.ahk

init()
return

MyListView:
if A_GuiEvent = DoubleClick
{
    LV_GetText(RowText, A_EventInfo)  ; Get the text from the row's first field.
    ToolTip You double-clicked row number %A_EventInfo%. Text: "%RowText%"
}
return


Backspace::
	run %path_Interface%
	ExitApp
Return

Escape::
	ExitApp
return


InitDlg(title,name, bShowBack)
{

	Gui, font, s8 bold , Verdana
	Gui, Add, Text,cCF8F4A x380 y15, Profil: all
	Gui, Add, Text, cWhite x380 y0, User: Demo
	
	Gui, font, s14 bold , Verdana
	;Gui, Add, Picture, x480 y0 w15 h15, ui/icon_run.png 
	Gui, Add, Text, cD4FF00 x5 y0, Peon: %title%
	
	Gui, Color, 061F73
	Gui +AlwaysOnTop +Disabled -Border
	loadData(name,bShowBack)
	ShowDlg()
}
ShowDlg()
{
	Gui, Show, xCenter, yCenter
}

loadData(srcFile,bShowBack)
{		
	nCount = 0
	
	Loop, read, %A_WorkingDir%\data\%srcFile%.txt
		nCount ++
	
	if bIsRootDlg > 0
	{
		nCount+=2
	}
	else
	{
		nCount+=1
	}
	; Create the ListView with two columns, Name and Size:
	Gui, font, s14 bold , Verdana
	
	Gui, Add, ListView, r%nCount% w500 gMyListView, Key | Name
	
	
	Loop, read, %A_WorkingDir%\data\%srcFile%.txt
	{
		line := A_LoopReadLine  ; When loop finishes, this will hold the last line.

		StringSplit, line, line, `,
		LV_Add("", line1, line2 )

	}	
	if bIsRootDlg > 0
	{
		LV_Add("", "Backspace", "Back" )
	}
	else
	{
		Hotkey, Backspace,off
	}
	LV_Add("", "Escape", "Quit" )
	
;	LV_ModifyCol(1)  ; Auto-size each column to fit its contents.
;	LV_ModifyCol(2)  ; For sorting purposes, indicate that column 2 is an integer.

	return 
}
 
