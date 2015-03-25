package PLYRARCH_package
{
	function secureClientCmd_ClientJoin(%name, %objid, %bl_id, %var1, %var2, %admin, %superadmin)
	{
		PLYRARCH_addPlayer(%bl_id, %name);
		parent::secureClientCmd_ClientJoin(%name, %objid, %bl_id, %var1, %var2, %admin, %superadmin);
	}
	
	function secureClientCmd_ClientDrop(%name, %objid)
	{
		parent::secureClientCmd_ClientDrop(%name, %objid);
	}
};