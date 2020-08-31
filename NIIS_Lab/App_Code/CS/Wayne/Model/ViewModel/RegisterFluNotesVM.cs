﻿using Newtonsoft.Json;
using System;

public class RegisterFluNotesVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public DateTime InoculationDate { get; set; }

    [JsonProperty(PropertyName = "FN")]
    public int FluNotes { get; set; }
}