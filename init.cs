$PLYRARCH::numLines = 0;

if (!isFile($PLYRARCH::rawArchiveFile)) {
	%file = new FileObject();
	%successful = true;
	
	// Attempt to create file
	if (%file.openForWrite($PLYRARCH::rawArchiveFile))
	{
		echo("'" @ $PLYRARCH::archiveFile @ "' created successfully");
	}
	else
	{
		echo("'" @ $PLYRARCH::archiveFile @ "' could not be created");
		%successful = false;
	}
	
	%file.close();
	%file.delete();
	
	return %successful;
}
else
{
	echo("'" @ $PLYRARCH::archiveFile @ "' already created");
	
	return true;
}