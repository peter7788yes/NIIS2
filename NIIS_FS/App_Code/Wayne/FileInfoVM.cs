using Newtonsoft.Json;
using System;

public class FileInfoVM
{
    public string StorageFileName { get; set; }

    public string DisplayFileName { get; set; }

    public DateTime CreatedDate { get; set; }

    public int FileCateID { get; set; }
	
	public string FolderName { get; set; }
}