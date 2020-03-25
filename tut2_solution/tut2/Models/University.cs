using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace tut2.Models
{
    public class University
    {
        [XmlAttribute(attributeName: "createdAt")]
        public string createdAt { set; get; }

        [XmlAttribute(attributeName: "author")]
        public string author { set; get; }

        public HashSet<Student> students { set; get; }
        public HashSet<StudiesList> activeStudies { set; get; }
    }
}
