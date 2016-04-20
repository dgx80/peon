#Include lib.ahk
#Include ui\PopUpDlg.ahk
#SingleInstance ignore

return



init()
{
	InitDlg("User","peon_user",1)	
}

;//////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Folder!!!!!!!!!!!!!!!!!!!!!!!!

;////////////////////Folder
A::

PeonLauncher("user.add")

return

C::

PeonLauncher("user.change")

return

D::

PeonLauncher("user.delete")

return


F12::

	returnInterface()
	
return