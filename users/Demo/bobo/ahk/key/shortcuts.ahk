#Include ui\PopUpDlg.ahk
#Include lib.ahk
#SingleInstance ignore

return



init()
{
	InitDlg("Shortcuts","Shortcuts",0)	
}

;//////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Folder!!!!!!!!!!!!!!!!!!!!!!!!

;////////////////////Folder

F12::

	returnInterface()
	
return

f10::

	peonLauncher("shortcutFolder.delete")

return

f11::

	peonLauncher("shortcutFolder.add")

return
