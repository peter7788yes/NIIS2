﻿using Newtonsoft.Json;
using System;

public class SystemElementarySchoolVM
{
    [JsonProperty(PropertyName = "I")]
    public int ElementarySchoolID { get; set; }

    [JsonProperty(PropertyName = "N")]
	public string SchoolName { get; set; }
}