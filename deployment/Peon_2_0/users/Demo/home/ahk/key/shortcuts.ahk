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

c::

	openFolder("Cmd","C:\Windows\System32\cmd.exe")

return

s::

	openFolder("System32","C:/Windows/System32")

return

u::

	openFolder("Users","C:/Users")

return

f10::

	peonLauncher("shortcutFolder.delete")

return

f11::

	peonLauncher("shortcutFolder.add")

return
