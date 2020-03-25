using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace tut2.Models
{
    public class StudiesList
    {

        [XmlAttribute(attributeName: "name")]
        public string name{ get; set; }
        [XmlAttribute(attributeName: "numberOfStudents")]
        public int numberOfStudents { get; set; }

    }
}
