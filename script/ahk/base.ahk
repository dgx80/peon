#SingleInstance ignore
#Persistent

SetWorkingDir %A_ScriptDir%

#Include ..\script\ahk\definitions.ahk

F12::

run %path_Interface%
	
return
!F12::

send F12

return

	