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

function PLYRARCH_sortData()
{
	%file = new FileObject();
	%maxBLID = 0;
	
	if (isFile($PLYRARCH::archiveFile))
	{
		// Index sorted player data
		if(!%file.openForRead($PLYRARCH::archiveFile))
		{
			echo("'" @ $PLYRARCH::archiveFile @ "' could not be opened for reading");
			%file.delete();
			return false;
		}
		
		echo("Indexing sorted player data from" SPC $PLYRARCH::archiveFile @ "...");
		
		while(!%file.isEOF())
		{
			%line = %file.readLine();
			%div_right = strpos(%line, " ");
			%bl_id = getSubStr(%line, 0, %div_right);
			
			%playerData[%bl_id] = new ScriptObject();
			
			for(%n = 0; true; %n++)
			{
				%div_left = strpos(%line, "\"", %div_right + 1);
				if (%div_left == -1) {
					break;
				}
				%div_left++;
				%div_right = strpos(%line, "\"", %div_left);
				%playerData[%bl_id].names[%n] = getSubStr(%line, %div_left, %div_right - %div_left);
			}
			
			%playerData[%bl_id].occured = getSubStr(%line, %div_right + 2, strlen(%line));
			
			if (%bl_id > %maxBLID) {
				%maxBLID = %bl_id;
			}
		}
		
		%file.close();
	}
	
	// Index raw player data
	if(!%file.openForRead($PLYRARCH::rawArchiveFile))
	{
		echo("'" @ $PLYRARCH::rawArchiveFile @ "' could not be opened for reading");
		%file.delete();
		return false;
	}
	
	echo("Indexing raw player data from" SPC $PLYRARCH::rawArchiveFile @ "...");
	
	while(!%file.isEOF())
	{
		%line = %file.readLine();
		%lineLen = strlen(%line);
		%div = strpos(%line, " ");
		%bl_id = getSubStr(%line, 0, %div);
		%name = getSubStr(%line, %div + 1, %lineLen);
		
		// If data for this player already exists
		if (%playerData[%bl_id])
		{
			%isNewName = true;
			for (%i = 0; %playerData[%bl_id].names[%i] !$= ""; %i++)
			{
				if (%name $= %playerData[%bl_id].names[%i])
				{
					%isNewName = false;
					break;
				}
			}
			
			if (%isNewName)
			{
				%playerData[%bl_id].names[%i] = %name;
			}
			%playerData[%bl_id].occured += 1;
		}
		// If data for this player doesn't exist
		else
		{
			%playerData[%bl_id] = new ScriptObject() {
				names[0] = %name;
				occured = 1;
			};
			
			if (%bl_id > %maxBLID)
			{
				%maxBLID = %bl_id;
			}
		}
	}
	
	%file.close();
	
	// Delete raw data file
	// Opening it for writing will overwrite it
	echo("Clearing raw player data...");
	if(!%file.openForWrite($PLYRARCH::rawArchiveFile))
	{
		echo("'" @ $PLYRARCH::archiveFile @ "' could not be opened for writing");
		%file.delete();
		return false;
	}
	%file.close();
	
	// Open file to write data
	if(!%file.openForWrite($PLYRARCH::archiveFile))
	{
		echo("'" @ $PLYRARCH::archiveFile @ "' could not be opened for writing");
		%file.delete();
		return false;
	}
	
	echo("Writing all player data to" SPC $PLYRARCH::archiveFile @ "...");
	
	for (%bl_id = 0; %bl_id <= %maxBLID; %bl_id++)
	{
		// Only write lines for players whom there is data for
		if (!%playerData[%bl_id])
		{
			continue;
		}
		
		%str = %bl_id;
		for (%n = 0; %playerData[%bl_id].names[%n] !$= ""; %n++)
		{
			%str = %str SPC "\"" @ %playerData[%bl_id].names[%n] @ "\"";
		}
		%str = %str SPC %playerData[%bl_id].occured;
		
		%file.writeLine(%str);
		
		// Player's data is written and sorted; object data is no longer needed
		%playerData[%bl_id].delete();
	}
	
	echo("Player archive sorted:" SPC $PLYRARCH::archiveFile);
	
	%file.close();
	%file.delete();
}