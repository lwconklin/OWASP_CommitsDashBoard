using System;
using System.Runtime.Serialization;



[DataContract]
public class Class1
{
	public Class1()
	{

    [DataMember(Name = "copyright")]
    public string Copyright {
        get; set;
    }
}
