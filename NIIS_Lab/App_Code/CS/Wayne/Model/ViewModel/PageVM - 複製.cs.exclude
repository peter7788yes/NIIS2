using System;

public class PageVM
{
    public int rdCount { get; set; }
    public int pgNow { get; set; }
    public int pgCount //{ get; set; }
    { 
        get {
            return (int)Math.Ceiling((double)rdCount / (double)pgSize);
        }
    }
    public int pgSize { get; set; }
    public object message { get; set; }
    public object message2 { get; set; }


    public PageVM()
    {
        pgNow = 1;
        pgSize = 10;
    }
}
