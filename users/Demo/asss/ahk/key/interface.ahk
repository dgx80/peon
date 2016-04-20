#Include lib.ahk
#Include ui\PopUpDlg.ahk
#SingleInstance ignore	
return

init()
{
	InitDlg("Interface","Interface",1)
}

;/////////////////////////////////////////////////////////////////////////////
;/////////////////////////////////////////////////////////////////////////////

S::
	onkey()
	Run %A_ScriptDir%\shortcuts.ahk
	terminate()
	
return

W::
	onkey()

	Run %A_ScriptDir%\WebLink.ahk
	terminate()

return

P::
	onkey()

	Run %A_ScriptDir%\peon_admin.ahk
	terminate()

return
C::
	onkey()

	Run %A_ScriptDir%\clipboard_copy.ahk
	terminate()

return
C::
	onkey()

	Run %A_ScriptDir%\clipboard_paste.ahk
	terminate()

return

F1::

PeonLauncher("profile.change")

return

F2::

PeonLauncher("user.change")

return

1::

PeonLauncher("top.1")

return

2::

PeonLauncher("top.2")

return
3::

PeonLauncher("top.3")

return
4::

PeonLauncher("top.4")

return
5::

PeonLauncher("top.5")

return
