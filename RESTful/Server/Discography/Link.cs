using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibraryServerRESTful.Discography
{
    [DataContract]
    public class Link<E>
    {
        [DataMember] //[DataMember(Name = "href")]
        public string href { get; set; }
        [DataMember]
        public E resource { get; set; }
    }
}
