using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KingMarket.Service.Exceptions
{
    [DataContract]
    public class GeneralException
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Description { get; set; }
    }
}