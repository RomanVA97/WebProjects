package md5eb7370522431fac992aacfb28146c1be;


public class ShowActivity
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
		mono.android.Runtime.register ("AppDrwer.ShowActivity, AppDrwer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ShowActivity.class, __md_methods);
	}


	public ShowActivity ()
	{
		super ();
		if (getClass () == ShowActivity.class)
			mono.android.TypeManager.Activate ("AppDrwer.ShowActivity, AppDrwer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
