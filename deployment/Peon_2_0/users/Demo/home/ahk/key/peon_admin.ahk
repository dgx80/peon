#Include lib.ahk
#Include ui\PopUpDlg.ahk
#SingleInstance ignore


return



init()
{
	InitDlg("Admin","peon_admin",1)	
}

;//////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Folder!!!!!!!!!!!!!!!!!!!!!!!!

;////////////////////Folder
U::

	runPath("key\peon_user.ahk")

return

P::

	runPath("key\peon_profile.ahk")

return

S::

	PeonLauncher("settings.advanced")

return

1::

	PeonLauncher("script.update.all")

return

a::

	PeonLauncher("peon.about")

return


F12::

	returnInterface()
	
return