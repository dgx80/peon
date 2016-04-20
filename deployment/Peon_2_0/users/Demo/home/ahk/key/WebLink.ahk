; Internet browser!!!!!!!!!!!!!!!!!!!!!!!!!!!
#Include lib.ahk
#Include ui\PopUpDlg.ahk
#SingleInstance ignore

return

init()
{
	InitDlg("WebLink","WebLink",0)
}

;/////////////////////////////////////////////////////////////////////////////////////////////////////
;/////////////////////////////////////////////////////////////////////////////////////////////////////
;Autohotkey


F12::

	returnInterface()
	
return

1::

	web("https://twitter.com/")

return

3::

	web("https://sites.google.com/site/lapprentiinformaticien/")

return

4::

	web("http://grooveshark.com")

return

f::

	web("www.facebook.com")

return

g::

	web("www.google.ca")

return

h::

	web("www.hotmail.com")

return

m::

	web("http://maps.google.ca/maps?hl=fr&tab=ml")

return

p::

	web("www.mypeon.net")

return

t::

	web("http://translate.google.fr/")

return

w::

	web("http://fr.wikipedia.org/wiki/Wikip%C3%A9dia:Accueil_principal")

return

y::

	web("http://www.youtube.com/?gl=FR&hl=fr")

return

f10::

	peonLauncher("weblink.delete")

return

f11::

	peonLauncher("weblink.add")

return
