﻿using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Bot_Application1.Serialization
{
    [DataContract]
    public class Utterance
    {
        [DataMember]
        public string query { get; set; }
        [DataMember]
        public List<Intent> intents { get; set; }
        [DataMember]
        public List<Entity> entities { get; set; }
    }
}