$PLYRARCH::directory = "Add-Ons/Script_PlayerArchiver";
exec($PLYRARCH::directory @ "/config.cs");

if (!exec($PLYRARCH::directory @ "/init.cs"))
{
	return false;
}

exec($PLYRARCH::directory @ "/packages.cs");
ActivatePackage(PLYRARCH_package);

function PLYRARCH_addPlayer(%bl_id, %name)
{
	%file = new FileObject();
	
	if (!%file.openForAppend($PLYRARCH::rawArchiveFile))
	{
		echo($PLYRARCH::archiveFile SPC "could not be opened for writing");
		%file.delete();
		return false;
	}
	
	%file.writeLine(%bl_id SPC %name);
	
	%file.close();
	%file.delete();
}