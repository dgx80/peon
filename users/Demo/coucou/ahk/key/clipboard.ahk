#Include lib.ahk
#Include ui\PopUpDlg.ahk
#SingleInstance ignore


return



init()
{
	InitDlg("Clipboard","clipboard",1)
}


F12::

	returnInterface()
	
return
Q::

	peonLauncher("clipboard.quickadd")

return

f10::

	peonLauncher("clipboard.delete")

return

f11::

	peonLauncher("clipboard.add")

return
