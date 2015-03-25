package PLYRARCH_package
{
	function secureClientCmd_ClientJoin(%name, %objid, %bl_id, %var1, %var2, %admin, %superadmin)
	{
		echo("secureClientCmd_ClientJoin() has been called...");
		echo("%name:" SPC %name);
		echo("%objid:" SPC %objid);
		echo("%bl_id:" SPC %bl_id);
		echo("%var1:" SPC %var1);
		echo("%var2:" SPC %var2);
		echo("%admin:" SPC %admin);
		echo("%superadmin:" SPC %superadmin);
		PLYRARCH_addPlayer(%bl_id, %name);
		parent::secureClientCmd_ClientJoin(%name, %objid, %bl_id, %var1, %var2, %admin, %superadmin);
	}
	
	function secureClientCmd_ClientDrop(%name, %objid)
	{
		echo("secureClientCmd_ClientDrop");
		echo("%name:" SPC %name);
		echo("%objid:" SPC %objid);
		parent::secureClientCmd_ClientDrop(%name, %objid);
	}
};