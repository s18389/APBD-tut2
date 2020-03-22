using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using tut2.Models;

namespace tut2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathToFile = @"Data/data.csv";

            //Reading from file
            var fileInfo = new FileInfo(pathToFile);
            using (var streamReader = new StreamReader(fileInfo.OpenRead()))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    Console.WriteLine(line);
                }
            }

            var listOfStudents = new HashSet<Student>(new CustomComparer());
            var student = new Student
            {
                Email = "someEmail@Email.com",
                IndexNumber = "s1234",
                FirstName = "Jackob",
                LastName = "Michalski"
            };

            var student2 = new Student
            {
                Email = "someEmail@Email.com",
                IndexNumber = "s1234",
                FirstName = "Jackob",
                LastName = "Michalski"
            };

            listOfStudents.Add(student);
            listOfStudents.Add(student2);

            FileStream writer = new FileStream(@"result.xml", FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("university"));

            xmlSerializer.Serialize(writer, listOfStudents);
        }
    }
}
