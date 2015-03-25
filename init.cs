$PLYRARCH::numLines = 0;

if (!isFile($PLYRARCH::rawArchiveFile)) {
	%file = new FileObject();
	%successful = true;
	
	// Attempt to create file
	if (%file.openForWrite($PLYRARCH::rawArchiveFile))
	{
		echo("'" @ $PLYRARCH::rawArchiveFile @ "' created successfully");
		%file.close();
	}
	else
	{
		echo("'" @ $PLYRARCH::rawArchiveFile @ "' could not be created");
		%successful = false;
	}
	%file.delete();
	
	return %successful;
}
else
{
	echo("'" @ $PLYRARCH::rawArchiveFile @ "' already created");
	
	return true;
}