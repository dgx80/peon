#Include lib.ahk
#Include ui\PopUpDlg.ahk
#SingleInstance ignore


return



init()
{
	InitDlg("Profile","peon_profile",1)	
}

;//////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Folder!!!!!!!!!!!!!!!!!!!!!!!!

;////////////////////Folder
A::

PeonLauncher("profile.add")

return

D::

PeonLauncher("profile.delete")

return

C:: 

PeonLauncher("profile.change")

return


F12::

	returnInterface()
	
return