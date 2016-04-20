
Mapping()
{
;	Loop % LV_GetCount()
;	{
;		LV_GetText( tx,A_Index)
;		Hotkey, %tx%,on
;	}
}

onkey()
{

}
terminate()
{
	Mapping()
	ExitApp
}

returnInterface()
{
	Run %A_ScriptDir%\interface.ahk
	ExitApp
}
openFolder(name, path)
{
	onkey()
	run %path%
;	WinWaitActive , %name%,,5
	terminate()
}
runPath(path)
{
;	Gui, Add, Picture, x480 y0 w15 h15, ui/icon_run.png 
	onkey()
	run %path%
	terminate()
}

PeonDataSheet(spread, work, header)
{
	onkey()
	Run ../Peon/bin/PeonLauncher.exe %spread% %work% %header%
	terminate()
}
PeonLauncher(cmd)
{	
	onkey()
	Run ..\..\bin\PeonLauncher.exe %cmd%
	terminate()
}
web(link)
{
	onkey()
	run %link%
	terminate()
}