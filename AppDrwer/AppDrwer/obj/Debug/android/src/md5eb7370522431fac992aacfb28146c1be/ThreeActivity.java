package md5eb7370522431fac992aacfb28146c1be;


public class ThreeActivity
	extends md5eb7370522431fac992aacfb28146c1be.Bck
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("AppDrwer.ThreeActivity, AppDrwer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ThreeActivity.class, __md_methods);
	}


	public ThreeActivity ()
	{
		super ();
		if (getClass () == ThreeActivity.class)
			mono.android.TypeManager.Activate ("AppDrwer.ThreeActivity, AppDrwer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}